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

    public virtual DbSet<ContactosAlternativo> ContactosAlternativos { get; set; }

    public virtual DbSet<Cuota> Cuotas { get; set; }

    public virtual DbSet<DetalleTransaccione> DetalleTransacciones { get; set; }

    public virtual DbSet<EntidadesFinanciera> EntidadesFinancieras { get; set; }

    public virtual DbSet<HistoricosIndice> HistoricosIndices { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    public virtual DbSet<Puntaje> Puntajes { get; set; }

    public virtual DbSet<PuntosPorCliente> PuntosPorClientes { get; set; }

    public virtual DbSet<Scoring> Scorings { get; set; }

    public virtual DbSet<TiposEntidadFinanciera> TiposEntidadFinancieras { get; set; }

    public virtual DbSet<TiposTransaccion> TiposTransaccions { get; set; }

    public virtual DbSet<TiposUsuario> TiposUsuarios { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<ViewBalance> ViewBalances { get; set; }

    public virtual DbSet<ViewCliente> ViewClientes { get; set; }

    public virtual DbSet<ViewCuota> ViewCuotas { get; set; }

    public virtual DbSet<ViewCuotasMesEnCurso> ViewCuotasMesEnCursos { get; set; }

    public virtual DbSet<ViewHistoricoDolaIndice> ViewHistoricoDolaIndices { get; set; }

    public virtual DbSet<ViewHistoricoPunto> ViewHistoricoPuntos { get; set; }

    public virtual DbSet<ViewPrestamo> ViewPrestamos { get; set; }

    public virtual DbSet<ViewRecaudacionMensual> ViewRecaudacionMensuals { get; set; }

    public virtual DbSet<ViewResumenPrestamo> ViewResumenPrestamos { get; set; }

    public virtual DbSet<ViewTransaccionesConDet> ViewTransaccionesConDets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; Database = FinanciarTe; User ID = ivanmachadoob; Password = 1V4n11--4th0s--; TrustServerCertificate = true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.ToTable("CATEGORIAS");

            entity.Property(e => e.IdCategoria).HasColumnName("id_Categoria");
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.IdTipoTransaccion).HasColumnName("id_Tipo_Transaccion");

            entity.HasOne(d => d.IdTipoTransaccionNavigation).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.IdTipoTransaccion)
                .HasConstraintName("FK_CATEGORIAS_TIPOS_TRANSACCION");
        });

        modelBuilder.Entity<Ciudade>(entity =>
        {
            entity.HasKey(e => e.IdCiudad);

            entity.ToTable("CIUDADES");

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

            entity.ToTable("CLIENTES");

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

        modelBuilder.Entity<ContactosAlternativo>(entity =>
        {
            entity.HasKey(e => e.IdContactoAlternativo);

            entity.ToTable("CONTACTOS_ALTERNATIVOS");

            entity.Property(e => e.IdContactoAlternativo).HasColumnName("id_Contacto_alternativo");
            entity.Property(e => e.Apellidos).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.Nombres).HasMaxLength(500);
        });

        modelBuilder.Entity<Cuota>(entity =>
        {
            entity.HasKey(e => e.IdCuota);

            entity.ToTable("CUOTAS");

            entity.Property(e => e.IdCuota).HasColumnName("id_Cuota");
            entity.Property(e => e.CuotaVencida).HasColumnName("Cuota_vencida");
            entity.Property(e => e.FechaPago)
                .HasColumnType("date")
                .HasColumnName("Fecha_pago");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("date")
                .HasColumnName("Fecha_vencimiento");
            entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
            entity.Property(e => e.IdDetalleTransaccion).HasColumnName("id_Detalle_Transaccion");
            entity.Property(e => e.IdPrestamo).HasColumnName("id_Prestamo");
            entity.Property(e => e.IdTransaccion).HasColumnName("id_Transaccion");
            entity.Property(e => e.MontoAbonado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Monto_abonado");
            entity.Property(e => e.MontoCuota)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Monto_cuota");
            entity.Property(e => e.NumeroCuota).HasColumnName("Numero_Cuota");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cuota)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_CUOTAS_CLIENTES");

            entity.HasOne(d => d.IdDetalleTransaccionNavigation).WithMany(p => p.Cuota)
                .HasForeignKey(d => d.IdDetalleTransaccion)
                .HasConstraintName("FK_CUOTAS_DETALLE_TRANSACCIONES");

            entity.HasOne(d => d.IdPrestamoNavigation).WithMany(p => p.Cuota)
                .HasForeignKey(d => d.IdPrestamo)
                .HasConstraintName("FK_CUOTAS_PRESTAMOS");

            entity.HasOne(d => d.IdTransaccionNavigation).WithMany(p => p.Cuota)
                .HasForeignKey(d => d.IdTransaccion)
                .HasConstraintName("FK_CUOTAS_TRANSACCIONES");
        });

        modelBuilder.Entity<DetalleTransaccione>(entity =>
        {
            entity.HasKey(e => e.IdDetalleTransacciones);

            entity.ToTable("DETALLE_TRANSACCIONES");

            entity.Property(e => e.IdDetalleTransacciones).HasColumnName("id_Detalle_transacciones");
            entity.Property(e => e.Detalle).HasMaxLength(500);
            entity.Property(e => e.IdCategoria).HasColumnName("id_Categoria");
            entity.Property(e => e.IdTransaccion).HasColumnName("id_Transaccion");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MotivoAnulacion)
                .HasMaxLength(500)
                .HasColumnName("Motivo_anulacion");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.DetalleTransacciones)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_DETALLE_TRANSACCIONES_CATEGORIAS");

            entity.HasOne(d => d.IdTransaccionNavigation).WithMany(p => p.DetalleTransacciones)
                .HasForeignKey(d => d.IdTransaccion)
                .HasConstraintName("FK_DETALLE_TRANSACCIONES_TRANSACCIONES");
        });

        modelBuilder.Entity<EntidadesFinanciera>(entity =>
        {
            entity.HasKey(e => e.IdEntidadFinanciera);

            entity.ToTable("ENTIDADES_FINANCIERAS");

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

        modelBuilder.Entity<HistoricosIndice>(entity =>
        {
            entity.ToTable("HISTORICOS_INDICE");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.ValorDolar)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Valor_dolar");
            entity.Property(e => e.ValorDolarBlue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Valor_dolar_blue");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.IdPrestamo);

            entity.ToTable("PRESTAMOS");

            entity.Property(e => e.IdPrestamo).HasColumnName("id_Prestamo");
            entity.Property(e => e.DiaVencimientoCuota).HasColumnName("Dia_vencimiento_cuota");
            entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
            entity.Property(e => e.IdPrestamoRefinanciado).HasColumnName("id_Prestamo_refinanciado");
            entity.Property(e => e.IdScoring).HasColumnName("id_Scoring");
            entity.Property(e => e.IdTransaccion).HasColumnName("id_Transaccion");
            entity.Property(e => e.IndiceInteres)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Indice_interes");
            entity.Property(e => e.MontoADevolver)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Monto_a_devolver");
            entity.Property(e => e.MontoOtorgado).HasColumnName("Monto_otorgado");
            entity.Property(e => e.MotivoAnulacion)
                .HasMaxLength(500)
                .HasColumnName("Motivo_anulacion");
            entity.Property(e => e.RefinanciaDeuda).HasColumnName("Refinancia_Deuda");
            entity.Property(e => e.ValorCuota)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Valor_cuota");

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
            entity.HasKey(e => e.IdProvincia);

            entity.ToTable("PROVINCIAS");

            entity.Property(e => e.IdProvincia).HasColumnName("id_Provincia");
            entity.Property(e => e.Provincia1)
                .HasMaxLength(500)
                .HasColumnName("Provincia");
        });

        modelBuilder.Entity<Puntaje>(entity =>
        {
            entity.HasKey(e => e.IdPuntos);

            entity.ToTable("PUNTAJES");

            entity.Property(e => e.IdPuntos).HasColumnName("id_puntos");
            entity.Property(e => e.CantidadPuntos).HasColumnName("Cantidad_puntos");
            entity.Property(e => e.Descripción).HasMaxLength(500);
        });

        modelBuilder.Entity<PuntosPorCliente>(entity =>
        {
            entity.HasKey(e => e.IdPuntosAsignados);

            entity.ToTable("PUNTOS_POR_CLIENTES");

            entity.Property(e => e.IdPuntosAsignados).HasColumnName("id_puntos_asignados");
            entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
            entity.Property(e => e.IdDetalleTransaccion).HasColumnName("id_Detalle_Transaccion");
            entity.Property(e => e.IdPuntaje).HasColumnName("id_puntaje");
            entity.Property(e => e.IdTransaccion).HasColumnName("id_Transaccion");
            entity.Property(e => e.MotivoAnulacion)
                .HasMaxLength(500)
                .HasColumnName("Motivo_anulacion");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.PuntosPorClientes)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_PUNTOS_POR_CLIENTES_CLIENTES");

            entity.HasOne(d => d.IdDetalleTransaccionNavigation).WithMany(p => p.PuntosPorClientes)
                .HasForeignKey(d => d.IdDetalleTransaccion)
                .HasConstraintName("FK_PUNTOS_POR_CLIENTES_DETALLE_TRANSACCIONES");

            entity.HasOne(d => d.IdPuntajeNavigation).WithMany(p => p.PuntosPorClientes)
                .HasForeignKey(d => d.IdPuntaje)
                .HasConstraintName("FK_PUNTOS_POR_CLIENTES_PUNTAJES");

            entity.HasOne(d => d.IdTransaccionNavigation).WithMany(p => p.PuntosPorClientes)
                .HasForeignKey(d => d.IdTransaccion)
                .HasConstraintName("FK_PUNTOS_POR_CLIENTES_TRANSACCIONES");
        });

        modelBuilder.Entity<Scoring>(entity =>
        {
            entity.HasKey(e => e.IdScoring);

            entity.ToTable("SCORINGS");

            entity.Property(e => e.IdScoring).HasColumnName("id_Scoring");
            entity.Property(e => e.Beneficio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TiposEntidadFinanciera>(entity =>
        {
            entity.HasKey(e => e.IdTipoEntidad);

            entity.ToTable("TIPOS_ENTIDAD_FINANCIERA");

            entity.Property(e => e.IdTipoEntidad).HasColumnName("id_Tipo_Entidad");
            entity.Property(e => e.Descripción).HasMaxLength(500);
        });

        modelBuilder.Entity<TiposTransaccion>(entity =>
        {
            entity.HasKey(e => e.IdTipoTransaccion);

            entity.ToTable("TIPOS_TRANSACCION");

            entity.Property(e => e.IdTipoTransaccion).HasColumnName("id_Tipo_Transaccion");
            entity.Property(e => e.Descripción).HasMaxLength(500);
        });

        modelBuilder.Entity<TiposUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTipoUsuario);

            entity.ToTable("TIPOS_USUARIOS");

            entity.Property(e => e.IdTipoUsuario).HasColumnName("id_Tipo_Usuario");
            entity.Property(e => e.Descripción).HasMaxLength(500);
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion);

            entity.ToTable("TRANSACCIONES");

            entity.Property(e => e.IdTransaccion).HasColumnName("id_Transaccion");
            entity.Property(e => e.FechaTransaccion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_transaccion");
            entity.Property(e => e.IdEntidadFinanciera).HasColumnName("id_Entidad_Financiera");
            entity.Property(e => e.MotivoAnulacion)
                .HasMaxLength(500)
                .HasColumnName("Motivo_anulacion");

            entity.HasOne(d => d.IdEntidadFinancieraNavigation).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.IdEntidadFinanciera)
                .HasConstraintName("FK_TRANSACCIONES_ENTIDADES_FINANCIERAS");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuarios);

            entity.ToTable("USUARIOS");

            entity.Property(e => e.IdUsuarios).HasColumnName("id_Usuarios");
            entity.Property(e => e.Apellidos).HasMaxLength(500);
            entity.Property(e => e.Calle).HasMaxLength(500);
            entity.Property(e => e.Hashpass)
                .HasMaxLength(70)
                .HasColumnName("HASHPass");
            entity.Property(e => e.IdTipoUsuario).HasColumnName("id_Tipo_Usuario");
            entity.Property(e => e.Nombres).HasMaxLength(500);
            entity.Property(e => e.User).HasMaxLength(500);

            entity.HasOne(d => d.IdTipoUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTipoUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIOS_TIPOS_USUARIOS");
        });

        modelBuilder.Entity<ViewBalance>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_Balance");

            entity.Property(e => e.Descripción).HasMaxLength(500);
            entity.Property(e => e.IdEntidadFinanciera).HasColumnName("ID Entidad Financiera");
            entity.Property(e => e.MontoActual)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Monto actual");
            entity.Property(e => e.MontoInicial)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Monto_inicial");
        });

        modelBuilder.Entity<ViewCliente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_Cliente");

            entity.Property(e => e.Activo)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Apellidos).HasMaxLength(500);
            entity.Property(e => e.BeneficioScoring)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Beneficio Scoring");
            entity.Property(e => e.CantidadDePrestamos).HasColumnName("Cantidad de Prestamos");
            entity.Property(e => e.Ciudad).HasMaxLength(500);
            entity.Property(e => e.CodigoPostal).HasColumnName("Codigo_postal");
            entity.Property(e => e.ContactoAlternativo)
                .HasMaxLength(1002)
                .HasColumnName("Contacto Alternativo");
            entity.Property(e => e.Dirección).HasMaxLength(531);
            entity.Property(e => e.Dni).HasColumnName("DNI");
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.EmailContactoAlternativo)
                .HasMaxLength(500)
                .HasColumnName("Email Contacto Alternativo");
            entity.Property(e => e.FechaDeNacimiento)
                .HasColumnType("date")
                .HasColumnName("Fecha de nacimiento");
            entity.Property(e => e.Nombres).HasMaxLength(500);
            entity.Property(e => e.Provincia).HasMaxLength(500);
            entity.Property(e => e.PuntosActuales).HasColumnName("Puntos actuales");
            entity.Property(e => e.PuntosIniciales).HasColumnName("Puntos_iniciales");
            entity.Property(e => e.TelefonoContactoAlternativo).HasColumnName("Telefono Contacto Alternativo");
        });

        modelBuilder.Entity<ViewCuota>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_Cuotas");

            entity.Property(e => e.Cliente).HasMaxLength(1002);
            entity.Property(e => e.CuotaN).HasColumnName("Cuota N°");
            entity.Property(e => e.CuotaVencida)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("Cuota Vencida");
            entity.Property(e => e.Dni).HasColumnName("DNI");
            entity.Property(e => e.DíasVencidos).HasColumnName("Días Vencidos");
            entity.Property(e => e.FechaDePago)
                .HasMaxLength(30)
                .HasColumnName("Fecha de Pago");
            entity.Property(e => e.FechaDeVencimiento)
                .HasMaxLength(30)
                .HasColumnName("Fecha de Vencimiento");
            entity.Property(e => e.IdCuota).HasColumnName("ID Cuota");
            entity.Property(e => e.IdDetalleTransacción).HasColumnName("ID Detalle Transacción");
            entity.Property(e => e.IdPrestamo).HasColumnName("ID Prestamo");
            entity.Property(e => e.IdTransacción).HasColumnName("ID Transacción");
            entity.Property(e => e.MontoAbonado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Monto abonado");
            entity.Property(e => e.MontoDeCuota)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Monto de cuota");
            entity.Property(e => e.PuntosOtorgados).HasColumnName("Puntos otorgados");
        });

        modelBuilder.Entity<ViewCuotasMesEnCurso>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_CuotasMesEnCurso");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewHistoricoDolaIndice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_HistoricoDola_Indice");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Indice).HasColumnType("decimal(10, 4)");
            entity.Property(e => e.ValorDolar)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Valor_dolar");
            entity.Property(e => e.ValorDolarBlue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Valor_dolar_blue");
        });

        modelBuilder.Entity<ViewHistoricoPunto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_HistoricoPuntos");

            entity.Property(e => e.Cliente).HasMaxLength(1002);
            entity.Property(e => e.Descripción).HasMaxLength(500);
            entity.Property(e => e.Detalle).HasMaxLength(500);
            entity.Property(e => e.Dni).HasColumnName("DNI");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewPrestamo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_Prestamos");

            entity.Property(e => e.BeneficioScoring)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Beneficio Scoring");
            entity.Property(e => e.Cliente).HasMaxLength(1002);
            entity.Property(e => e.CuotasPagas).HasColumnName("Cuotas pagas");
            entity.Property(e => e.DniCliente).HasColumnName("DNI Cliente");
            entity.Property(e => e.Estado)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.IdPrestamo).HasColumnName("ID Prestamo");
            entity.Property(e => e.IndiceFinanciarTe)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Indice FinanciarTe");
            entity.Property(e => e.MontoADevolver)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Monto a devolver");
            entity.Property(e => e.MontoAbonado)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Monto abonado");
            entity.Property(e => e.MontoOtorgado).HasColumnName("Monto Otorgado");
            entity.Property(e => e.SaldoPendiente)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Saldo Pendiente");
            entity.Property(e => e.ValorDeLaCuota)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Valor de la Cuota");
            entity.Property(e => e.VencimientoPrimeraCuota)
                .HasColumnType("date")
                .HasColumnName("Vencimiento Primera Cuota");
            entity.Property(e => e.VencimientoUltimaCuota)
                .HasColumnType("date")
                .HasColumnName("Vencimiento Ultima Cuota");
        });

        modelBuilder.Entity<ViewRecaudacionMensual>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_RecaudacionMensual");

            entity.Property(e => e.RecaudaciónEsperada)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Recaudación esperada");
            entity.Property(e => e.RecaudaciónMensual)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Recaudación mensual");
        });

        modelBuilder.Entity<ViewResumenPrestamo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_ResumenPrestamos");

            entity.Property(e => e.CantidadDePrestamos).HasColumnName("Cantidad de Prestamos");
            entity.Property(e => e.CuotasVencidas).HasColumnName("Cuotas vencidas");
            entity.Property(e => e.Nombre).HasMaxLength(1002);
            entity.Property(e => e.PorcentajeCumplCuotas).HasColumnName("Porcentaje Cumpl Cuotas");
            entity.Property(e => e.PrestamosCancelados).HasColumnName("Prestamos Cancelados");
            entity.Property(e => e.PrestamosPendientes).HasColumnName("Prestamos Pendientes");
            entity.Property(e => e.PrestamosRefinanciados).HasColumnName("Prestamos Refinanciados");
            entity.Property(e => e.TotalDeCuotas).HasColumnName("Total de Cuotas");
        });

        modelBuilder.Entity<ViewTransaccionesConDet>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_TransaccionesConDet");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Descripción).HasMaxLength(500);
            entity.Property(e => e.Detalle).HasMaxLength(500);
            entity.Property(e => e.FechaTransaccion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_transaccion");
            entity.Property(e => e.IdDetalleTransacciones).HasColumnName("id_Detalle_Transacciones");
            entity.Property(e => e.IdTransaccion).HasColumnName("id_Transaccion");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
