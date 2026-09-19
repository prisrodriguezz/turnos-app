import "../../../estilos/publico/reservarTurno/resumenTurno.css";

function ResumenTurno({ reserva }) {
  return (
    <aside className="reserva-resumen">
      <h3>Tu reserva</h3>

      {!reserva.servicio ? (
        <div className="resumen-vacio">
          <span>♡</span>

          <p>
            A medida que avances, acá vas a ver el resumen de tu turno.
          </p>
        </div>
      ) : (
        <div className="resumen-datos">
          <div>
            <span>Servicio</span>
            <strong>{reserva.servicio.nombre}</strong>
          </div>

          {reserva.profesional && (
            <div>
              <span>Profesional</span>

              <strong>
                {reserva.profesional.nombre}{" "}
                {reserva.profesional.apellido}
              </strong>
            </div>
          )}

          {reserva.fecha && (
            <div>
              <span>Fecha</span>
              <strong>{reserva.fecha}</strong>
            </div>
          )}

          {reserva.horario && (
            <div>
              <span>Horario</span>
              <strong>{reserva.horario} hs</strong>
            </div>
          )}

          <div className="resumen-separador"></div>

          <div className="resumen-item">
            <span>Duración</span>
            <strong>{reserva.servicio.duracionMinutos} min</strong>
          </div>

          <div className="resumen-item">
            <span>Precio</span>

            <strong>
              ${reserva.servicio.costo.toLocaleString("es-AR")}
            </strong>
          </div>
        </div>
      )}
    </aside>
  );
}

export default ResumenTurno;