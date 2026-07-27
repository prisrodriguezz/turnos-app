using Microsoft.EntityFrameworkCore;
using backend.modelos;

namespace backend.Datos;

public class AppDbContext : DbContext
{
    // Constructor
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    // Representa el conjunto de Categorias, etc. Por eso va en plural
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<DisponibilidadProfesional> DisponibilidadesProfesional { get; set; }
    public DbSet<HorarioNegocio> HorariosNegocio { get; set; }
    public DbSet<Negocio> Negocios { get; set; }
    public DbSet<Profesional> Profesionales { get; set; }
    public DbSet<ProfesionalServicio> ProfesionalesServicios { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<Turno> Turnos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cant de numeros que se quieren guardar 
        modelBuilder.Entity<Servicio>()
            .Property(s => s.Costo)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Turno>()
            .Property(t => t.Total)
            .HasPrecision(10, 2);

        
        // Usuario - Negocio (1 a 1)
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Negocio)
            .WithOne(n => n.Usuario)
            .HasForeignKey<Usuario>(u => u.IdNegocio);
        
        // ProfesionalServicio (N a N)
        modelBuilder.Entity<ProfesionalServicio>()
        .HasKey(ps => new { ps.IdProfesional, ps.IdServicio });

        modelBuilder.Entity<ProfesionalServicio>()
            .HasOne(ps => ps.Profesional)
            .WithMany(p => p.ProfesionalServicios)
            .HasForeignKey(ps => ps.IdProfesional);

        modelBuilder.Entity<ProfesionalServicio>()
            .HasOne(ps => ps.Servicio)
            .WithMany(s => s.ProfesionalServicios)
            .HasForeignKey(ps => ps.IdServicio);

        // Profesional - Disponibilidad (1 a N)
        modelBuilder.Entity<DisponibilidadProfesional>()
            .HasOne(d => d.Profesional)
            .WithMany(p => p.Disponibilidades)
            .HasForeignKey(d => d.IdProfesional);

        // Configuracion de FK
        modelBuilder.Entity<Servicio>()
            .HasOne(s => s.Categoria)
            .WithMany(c => c.Servicios)
            .HasForeignKey(s => s.IdCategoria)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Servicio>()
            .HasOne(s => s.Negocio)
            .WithMany(n => n.Servicios)
            .HasForeignKey(s => s.IdNegocio)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Cliente)
            .WithMany(c => c.Turnos)
            .HasForeignKey(t => t.IdCliente);

        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Servicio)
            .WithMany(s => s.Turnos)
            .HasForeignKey(t => t.IdServicio);

        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Profesional)
            .WithMany(p => p.Turnos)
            .HasForeignKey(t => t.IdProfesional);

        modelBuilder.Entity<HorarioNegocio>()
            .HasOne(h => h.Negocio)
            .WithMany(n => n.Horarios)
            .HasForeignKey(h => h.IdNegocio)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}