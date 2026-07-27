using backend.Datos;
using backend.DTOs;
using backend.modelos;
using Microsoft.EntityFrameworkCore;

namespace backend.Servicios;

public class GeneradorHorariosService
{
    private readonly AppDbContext _context;     // Conexion en Entity Framework

    public GeneradorHorariosService(AppDbContext context)
    {
        _context = context;
    }

    // Obtener horarios disponibles
    public async Task<List<HorarioDisponibleDTO>> ObtenerHorariosDisponibles
        (DateTime fecha, int idServicio, int? idProfesional)
    {
        // Crea una lista de horarios, inicialmente esta vacia
        var horariosDisponibles = new List<HorarioDisponibleDTO>();

        // Buscar servicio
        var servicio = await _context.Servicios.Include(s => s.Negocio).FirstOrDefaultAsync(s => s.Id == idServicio);

        // Si el servicio no existe devuelve []
        if(servicio == null)
        {
            return horariosDisponibles;
        }

        // Verificamos si el negocio tiene profesionales; Si es TRUE, genera los horarios verificando disponibilidad
        if(servicio.Negocio.UsaProfesionales)
        {
            return await ObtenerHorariosConProfesionales(fecha, servicio, idProfesional);
        }

        // Buscar horario del negocio
        var diaSemana = fecha.DayOfWeek;    // Consulta que dia es

        var horariosNegocio = await _context.HorariosNegocio
            .Where(h =>
                h.IdNegocio == servicio.IdNegocio &&
                h.DiaSemana == diaSemana).ToListAsync();
        

        foreach(var horario in horariosNegocio)
        {
            var inicio = fecha.Date + horario.HoraInicio.ToTimeSpan();

            var fin = fecha.Date + horario.HoraFin.ToTimeSpan();

            // Genera los intervalos de tiempo;  ej. '08:00 - 08:30'
            while(inicio.AddMinutes(servicio.DuracionMinutos) <= fin)
            {
                var horaFin = inicio.AddMinutes(servicio.DuracionMinutos);

                // Verificar si existe turno ocupado
                bool ocupado = await _context.Turnos.AnyAsync(t =>
                    t.FechaHoraInicio == inicio &&
                    t.IdServicio == idServicio &&
                    t.Estado != EstadoTurno.Cancelado
                );

                // Si esta libre agrega a la lista de horario
                if(!ocupado)
                {
                    horariosDisponibles.Add(new HorarioDisponibleDTO
                    {
                        FechaHoraInicio = inicio,
                        FechaHoraFin = horaFin,
                        IdProfesional = idProfesional
                    });
                }

                inicio = inicio.AddMinutes(servicio.DuracionMinutos);
            }
        }

        return horariosDisponibles;
    }

    // Obtener Horarios con Profesionales
    private async Task<List<HorarioDisponibleDTO>> ObtenerHorariosConProfesionales(DateTime fecha, Servicio servicio, int? idProfesional)
    {
        var horarios = new List<HorarioDisponibleDTO>();

        var profesionales = await _context.ProfesionalesServicios
            .Where(ps => ps.IdServicio == servicio.Id)
            .Select(ps => ps.Profesional).ToListAsync();


        // Si viene un profesional seleccionado filtramos solamente ese
        if(idProfesional.HasValue)
        {
            profesionales = profesionales.Where(p => p.Id == idProfesional.Value).ToList();
        }

        foreach(var profesional in profesionales)
        {
            var disponibilidad = await _context.DisponibilidadesProfesional
                .Where(d =>
                    d.IdProfesional == profesional.Id &&
                    d.DiaSemana == fecha.DayOfWeek).ToListAsync();

            foreach(var horario in disponibilidad)
            {
                var inicio = fecha.Date + horario.HoraInicio.ToTimeSpan();

                var fin = fecha.Date + horario.HoraFin.ToTimeSpan();

                while(inicio.AddMinutes(servicio.DuracionMinutos) <= fin)
                {
                    var horaFin = inicio.AddMinutes(servicio.DuracionMinutos);

                    bool ocupado = await _context.Turnos.AnyAsync(t =>
                        t.IdProfesional == profesional.Id &&
                        t.FechaHoraInicio == inicio &&
                        t.Estado != EstadoTurno.Cancelado
                    );

                    if(!ocupado)
                    {
                        horarios.Add(new HorarioDisponibleDTO
                        {
                            FechaHoraInicio = inicio,
                            FechaHoraFin = horaFin,
                            IdProfesional = profesional.Id
                        });
                    }

                    inicio = inicio.AddMinutes(servicio.DuracionMinutos);
                }
            }
        }


        return horarios;
    }

}