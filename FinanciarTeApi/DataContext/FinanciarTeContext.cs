using System;
using System.Collections.Generic;
using FinanciarTeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanciarTeApi.DataContext;

public partial class FinanciarTeContext : DbContext
{
    public FinanciarTeContext()
    {
    }

    public FinanciarTeContext(DbContextOptions<FinanciarTeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Ciudade> Ciudades { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<CobrosCuota> CobrosCuotas { get; set; }

    public virtual DbSet<ContactosAlternativo> ContactosAlternativos { get; set; }

    public virtual DbSet<DetalleTransaccione> DetalleTransacciones { get; set; }

    public virtual DbSet<EntidadesFinanciera> EntidadesFinancieras { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    public virtual DbSet<Punto> Puntos { get; set; }

    public virtual DbSet<Scoring> Scorings { get; set; }

    public virtual DbSet<TiposEntidadFinanciera> TiposEntidadFinancieras { get; set; }

    public virtual DbSet<TiposTransaccion> TiposTransaccions { get; set; }

    public virtual DbSet<TiposUsuario> TiposUsuarios { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=FinanciarTe;User Id=ivanmachado;Password=1V4n11--4th0s--;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("db_accessadmin");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A7B263BA499E6E60");

            entity.ToTable("CATEGORIAS", "dbo");

            entity.Property(e => e.IdCategoria).HasColumnName("id_Categoria");
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.IdTipoTransaccion).HasColumnName("id_Tipo_Transaccion");

            entity.HasOne(d => d.IdTipoTransaccionNavigation).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.IdTipoTransaccion)
                .HasConstraintName("FK_CATEGORIAS_TIPOS_TRANSACCION");
        });

        modelBuilder.Entity<Ciudade>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("PK__CIUDADES__0640366CB521AE20");

            entity.ToTable("CIUDADES", "dbo");

            entity.Property(e => e.IdCiudad).HasColumnName("id_Ciudad");
            entity.Property(e => e.Ciudad).HasMaxLength(500);
            entity.Property(e => e.IdProvincia).HasColumnName("id_Provincia");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Ciudades)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("FK_CIUDADES_PROVINCIAS");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.NroDni);

            entity.ToTable("CLIENTES", "dbo");

            entity.Property(e => e.NroDni)
                .ValueGeneratedNever()
                .HasColumnName("Nro_dni");
            entity.Property(e => e.Apellidos).HasMaxLength(500);
            entity.Property(e => e.CodigoPostal).HasColumnName("Codigo_postal");
            entity.Property(e => e.Direccion).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.FechaDeNacimiento)
                .HasColumnType("date")
                .HasColumnName("Fecha de nacimiento");
            entity.Property(e => e.IdCiudad).HasColumnName("id_Ciudad");
            entity.Property(e => e.IdContactoAlternativo).HasColumnName("id_Contacto_alternativo");
            entity.Property(e => e.Nombres).HasMaxLength(500);
            entity.Property(e => e.PuntosIniciales).HasColumnName("Puntos_iniciales");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("FK_CLIENTES_CIUDADES");

            entity.HasOne(d => d.IdContactoAlternativoNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdContactoAlternativo)
                .HasConstraintName("FK_CLIENTES_CONTACTOS_ALTERNATIVOS");
        });

        modelBuilder.Entity<CobrosCuota>(entity =>
        {
            entity.HasKey(e => e.IdCobroCuota).HasName("PK__COBROS_C__FDCFEFD33D66BD46");

            entity.ToTable("COBROS_CUOTAS", "dbo");

            entity.Property(e => e.IdCobroCuota).HasColumnName("id_Cobro_Cuota");
            entity.Property(e => e.CuotaVencida).HasColumnName("Cuota_vencida");
            entity.Property(e => e.FechaPago)
                .HasColumnType("date")
                .HasColumnName("Fecha_pago");
            entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
            entity.Property(e => e.IdPrestamo).HasColumnName("id_Prestamo");
            entity.Property(e => e.IdPuntos).HasColumnName("id_puntos");
            entity.Property(e => e.IdTransaccion).HasColumnName("id_Transaccion");
            entity.Property(e => e.MontoAbonado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Monto_abonado");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.CobrosCuota)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_COBROS_CUOTAS_CLIENTES");

            entity.HasOne(d => d.IdPuntosNavigation).WithMany(p => p.CobrosCuota)
                .HasForeignKey(d => d.IdPuntos)
                .HasConstraintName("FK_COBROS_CUOTAS_PUNTOS");

            entity.HasOne(d => d.IdTransaccionNavigation).WithMany(p => p.CobrosCuota)
                .HasForeignKey(d => d.IdTransaccion)
                .HasConstraintName("FK_COBROS_CUOTAS_TRANSACCIONES");
        });

        modelBuilder.Entity<ContactosAlternativo>(entity =>
        {
            entity.HasKey(e => e.IdContactoAlternativo).HasName("PK__CONTACTO__EB367B4029A55C29");

            entity.ToTable("CONTACTOS_ALTERNATIVOS", "dbo");

            entity.Property(e => e.IdContactoAlternativo).HasColumnName("id_Contacto_alternativo");
            entity.Property(e => e.Apellidos).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.Nombres).HasMaxLength(500);
        });

        modelBuilder.Entity<DetalleTransaccione>(entity =>
        {
            entity.HasKey(e => e.IdDetalleTransacciones).HasName("PK__DETALLE___E03E150F787EC883");

            entity.ToTable("DETALLE_TRANSACCIONES", "dbo");

            entity.Property(e => e.IdDetalleTransacciones).HasColumnName("id_Detalle_transacciones");
            entity.Property(e => e.Detalle).HasMaxLength(500);
            entity.Property(e => e.IdCategoria).HasColumnName("id_Categoria");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.DetalleTransacciones)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_DETALLE_TRANSACCIONES_CATEGORIAS");
        });

        modelBuilder.Entity<EntidadesFinanciera>(entity =>
        {
            entity.HasKey(e => e.IdEntidadFinanciera).HasName("PK__ENTIDADE__608E3ACEA40CD6EA");

            entity.ToTable("ENTIDADES_FINANCIERAS", "dbo");

            entity.Property(e => e.IdEntidadFinanciera).HasColumnName("id_Entidad_Financiera");
            entity.Property(e => e.Alias).HasMaxLength(500);
            entity.Property(e => e.Cbu)
                .HasMaxLength(500)
                .HasColumnName("CBU");
            entity.Property(e => e.Descripción).HasMaxLength(500);
            entity.Property(e => e.IdTipoEntidad).HasColumnName("id_Tipo_Entidad");
            entity.Property(e => e.MontoInicial)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Monto_inicial");
            entity.Property(e => e.NroCuenta)
                .HasMaxLength(500)
                .HasColumnName("Nro_Cuenta");

            entity.HasOne(d => d.IdTipoEntidadNavigation).WithMany(p => p.EntidadesFinancieras)
                .HasForeignKey(d => d.IdTipoEntidad)
                .HasConstraintName("FK_ENTIDADES_FINANCIERAS_TIPOS_ENTIDAD_FINANCIERA");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.IdPrestamo).HasName("PK__PRESTAMO__B41FF2E5F2ABE02F");

            entity.ToTable("PRESTAMOS", "dbo");

            entity.Property(e => e.IdPrestamo).HasColumnName("id_Prestamo");
            entity.Property(e => e.DiaVencimientoCuota).HasColumnName("Dia_vencimiento_cuota");
            entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
            entity.Property(e => e.IdPrestamoRefinanciado).HasColumnName("id_Prestamo_refinanciado");
            entity.Property(e => e.IdScoring).HasColumnName("id_Scoring");
            entity.Property(e => e.IdTransaccion).HasColumnName("id_Transaccion");
            entity.Property(e => e.IndiceInteres)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Indice_interes");
            entity.Property(e => e.MontoOtorgado).HasColumnName("Monto_otorgado");
            entity.Property(e => e.RefinanciaDeuda).HasColumnName("Refinancia_Deuda");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_PRESTAMOS_CLIENTES");

            entity.HasOne(d => d.IdScoringNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdScoring)
                .HasConstraintName("FK_PRESTAMOS_SCORINGS");

            entity.HasOne(d => d.IdTransaccionNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdTransaccion)
                .HasConstraintName("FK_PRESTAMOS_TRANSACCIONES");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("PK__PROVINCI__C83EC194A3D3C572");

            entity.ToTable("PROVINCIAS", "dbo");

            entity.Property(e => e.IdProvincia).HasColumnName("id_Provincia");
            entity.Property(e => e.Provincia1)
                .HasMaxLength(500)
                .HasColumnName("Provincia");
        });

        modelBuilder.Entity<Punto>(entity =>
        {
            entity.HasKey(e => e.IdPuntos).HasName("PK__PUNTOS__3486CBF383DDE525");

            entity.ToTable("PUNTOS", "dbo");

            entity.Property(e => e.IdPuntos).HasColumnName("id_puntos");
            entity.Property(e => e.CantidadPuntos).HasColumnName("Cantidad_puntos");
            entity.Property(e => e.Descripción).HasMaxLength(500);
        });

        modelBuilder.Entity<Scoring>(entity =>
        {
            entity.HasKey(e => e.IdScoring).HasName("PK__SCORINGS__E6A09A86B1CA5D8E");

            entity.ToTable("SCORINGS", "dbo");

            entity.Property(e => e.IdScoring).HasColumnName("id_Scoring");
            entity.Property(e => e.Beneficio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TiposEntidadFinanciera>(entity =>
        {
            entity.HasKey(e => e.IdTipoEntidad).HasName("PK__TIPOS_EN__30510B3A54DFE684");

            entity.ToTable("TIPOS_ENTIDAD_FINANCIERA", "dbo");

            entity.Property(e => e.IdTipoEntidad).HasColumnName("id_Tipo_Entidad");
            entity.Property(e => e.Descripción).HasMaxLength(500);
        });

        modelBuilder.Entity<TiposTransaccion>(entity =>
        {
            entity.HasKey(e => e.IdTipoTransaccion).HasName("PK__TIPOS_TR__A208B3604C8FCCCF");

            entity.ToTable("TIPOS_TRANSACCION", "dbo");

            entity.Property(e => e.IdTipoTransaccion).HasColumnName("id_Tipo_Transaccion");
            entity.Property(e => e.Descripción).HasMaxLength(500);
        });

        modelBuilder.Entity<TiposUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTipoUsuario).HasName("PK__TIPOS_US__669B0AF00EE0CD42");

            entity.ToTable("TIPOS_USUARIOS", "dbo");

            entity.Property(e => e.IdTipoUsuario).HasColumnName("id_Tipo_Usuario");
            entity.Property(e => e.Descripción).HasMaxLength(500);
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PK__TRANSACC__896AC4C838222314");

            entity.ToTable("TRANSACCIONES", "dbo");

            entity.Property(e => e.IdTransaccion).HasColumnName("id_Transaccion");
            entity.Property(e => e.FechaTransaccion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_transaccion");
            entity.Property(e => e.IdDetalleTransacciones).HasColumnName("id_Detalle_Transacciones");
            entity.Property(e => e.IdEntidadFinanciera).HasColumnName("id_Entidad_Financiera");

            entity.HasOne(d => d.IdDetalleTransaccionesNavigation).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.IdDetalleTransacciones)
                .HasConstraintName("FK_TRANSACCIONES_DETALLE_TRANSACCIONES");

            entity.HasOne(d => d.IdEntidadFinancieraNavigation).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.IdEntidadFinanciera)
                .HasConstraintName("FK_TRANSACCIONES_ENTIDADES_FINANCIERAS");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuarios).HasName("PK__USUARIOS__F80D2BD70DE397A3");

            entity.ToTable("USUARIOS", "dbo");

            entity.Property(e => e.IdUsuarios).HasColumnName("id_Usuarios");
            entity.Property(e => e.Apellidos).HasMaxLength(500);
            entity.Property(e => e.IdTipoUsuario).HasColumnName("id_Tipo_Usuario");
            entity.Property(e => e.Nombres).HasMaxLength(500);
            entity.Property(e => e.Usuario1)
                .HasMaxLength(500)
                .HasColumnName("Usuario");

            entity.HasOne(d => d.IdTipoUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTipoUsuario)
                .HasConstraintName("FK_USUARIOS_TIPOS_USUARIOS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
