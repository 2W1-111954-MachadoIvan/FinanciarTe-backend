using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinanciarTeApi.Services
{
    public class ServiceCuotas : IServiceCuotas
    {
        private readonly FinanciarTeContext _context;
        public ServiceCuotas(FinanciarTeContext context)
        {
            _context = context;
        }
        public Task<ResultadoBase> DeleteCuota(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Cuota> GetCuotaByID(int id)
        {
            var comando = await _context.Cuotas
                                      .Where(c => c.IdCuota == id)
                                      .FirstOrDefaultAsync();

            Cuota cuota = new Cuota();

            if(comando != null)
            {
                cuota.IdCuota = comando.IdCuota;
                cuota.IdCliente = comando.IdCliente;
                cuota.IdPrestamo = comando.IdPrestamo;
                cuota.NumeroCuota = comando.NumeroCuota;
                cuota.MontoCuota = comando.MontoCuota;
                cuota.MontoAbonado = comando.MontoAbonado;
                cuota.FechaPago = comando.FechaPago;
                cuota.CuotaVencida = comando.CuotaVencida;
                cuota.IdTransaccion = comando.IdTransaccion;
                cuota.IdDetalleTransaccion = comando.IdDetalleTransaccion;
            }

            return cuota;
        }

        public async Task<List<ViewCuotasCliente>> GetCuotasPendientesByCliente(int id)
        {
            var maxFecha = await _context.ViewHistoricoDolaIndices.MaxAsync(c => c.Fecha);

            var indice = await _context.ViewHistoricoDolaIndices.Where(c => c.Fecha == maxFecha).Select(c => c.Indice).FirstOrDefaultAsync();

            var cuotas = await _context.ViewCuotas
                                      .Where(c => c.Dni == id && c.MontoAbonado == 0)
                                      .Select(c=> new ViewCuotasCliente
                                      {
                                            IdCuota = c.IdCuota,
                                            Dni = c.Dni,
                                            IdPrestamo = c.IdPrestamo,
                                            CuotaN = c.CuotaN,
                                            FechaDeVencimiento = c.FechaDeVencimiento,
                                            MontoDeCuota = c.MontoDeCuota,
                                            MontoAbonado = c.MontoDeCuota + (c.MontoDeCuota*((indice/20)*c.DíasVencidos)),
                                            FechaDePago = c.FechaDePago,
                                            CuotaVencida = c.CuotaVencida,
                                            DíasVencidos = c.DíasVencidos,
                                            IdTransacción = c.IdTransacción,
                                            IdDetalleTransacción = c.IdDetalleTransacción
                                      })
                                      .ToListAsync();

            return cuotas;
        }

        public async Task<List<ViewCuotasCliente>> GetViewCuotasByCliente(int id)
        {
            List<ViewCuotasCliente> cuotas = await _context.ViewCuotas
                                      .Where(c => c.Dni == id)
                                      .Select(c => new ViewCuotasCliente
                                      {
                                          Dni = c.Dni,
                                          Cliente = c.Cliente,
                                          IdPrestamo = c.IdPrestamo,
                                          IdCuota = c.IdCuota,
                                          CuotaN = c.CuotaN,
                                          FechaDeVencimiento = c.FechaDeVencimiento,
                                          MontoDeCuota = c.MontoDeCuota,
                                          FechaDePago = c.FechaDePago,
                                          MontoAbonado = c.MontoAbonado,
                                          CuotaVencida = c.CuotaVencida,
                                          IdTransacción = c.IdTransacción,
                                          IdDetalleTransacción = c.IdDetalleTransacción,
                                          PuntosOtorgados = c.PuntosOtorgados,
                                      }).ToListAsync();
            return cuotas;
        }

        public async Task<ResultadoBase> RegistrarPagoCuotas(ComandoCuota comando)
        {
            try
            {
                var transaccion = new Transaccione();

                transaccion.FechaTransaccion = comando.fechaPago;
                transaccion.IdEntidadFinanciera = comando.idEntidadFinanciera;

                await _context.Transacciones.AddAsync(transaccion);
                await _context.SaveChangesAsync();

                var idTransaccion = _context.Transacciones.Where(c => c.IdTransaccion.Equals(transaccion.IdTransaccion)).Select(c => c.IdTransaccion).FirstOrDefault();

                foreach (ComandoDetalleCuotas dc in comando.detalleCuotas)
                {

                    DetalleTransaccione detalles = new DetalleTransaccione();

                    detalles.IdTransaccion = idTransaccion;
                    detalles.Detalle = "Cuota: " + dc.NumeroCuota + " - Prestamo: " + dc.IdPrestamo;
                    detalles.IdCategoria = 1;
                    detalles.Monto = dc.MontoAbonado;

                    await _context.DetalleTransacciones.AddAsync(detalles);
                    await _context.SaveChangesAsync();

                    var idDetalleTransaccion = _context.DetalleTransacciones.Where(c=>c.IdDetalleTransacciones.Equals(detalles.IdDetalleTransacciones)).Select(c=>c.IdDetalleTransacciones).FirstOrDefault();

                    var cuota = _context.Cuotas.Include(c=>c.IdPrestamoNavigation).Where(c=>c.IdCuota == dc.IdCuota).FirstOrDefault();

                    cuota.CuotaVencida = comando.fechaPago > cuota.FechaVencimiento ? true : false;
                    cuota.FechaPago = comando.fechaPago;
                    cuota.MontoAbonado = dc.MontoAbonado;
                    cuota.IdTransaccion = idTransaccion;
                    cuota.IdDetalleTransaccion = idDetalleTransaccion;

                    _context.Cuotas.Update(cuota);
                    await _context.SaveChangesAsync();

                    var puntajeCuota = new PuntosPorCliente();

                    puntajeCuota.IdCliente = cuota.IdCliente;
                    puntajeCuota.IdTransaccion = idTransaccion;
                    puntajeCuota.IdDetalleTransaccion = idDetalleTransaccion;
                    puntajeCuota.IdPuntaje = cuota.CuotaVencida == true ? 2 : 1;

                    await _context.PuntosPorClientes.AddAsync(puntajeCuota);
                    await _context.SaveChangesAsync();

                    if(cuota.NumeroCuota == cuota.IdPrestamoNavigation.Cuotas && 
                                            !_context.Cuotas.Any(c =>c.IdPrestamo == cuota.IdPrestamo && c.CuotaVencida == true))
                    {
                        var puntajePrestamo = new PuntosPorCliente();

                        puntajePrestamo.IdCliente = cuota.IdCliente;
                        puntajePrestamo.IdTransaccion = idTransaccion;
                        puntajePrestamo.IdDetalleTransaccion = idDetalleTransaccion;
                        puntajePrestamo.IdPuntaje = 3;

                        await _context.PuntosPorClientes.AddAsync(puntajePrestamo);
                        await _context.SaveChangesAsync();
                    }

                    if (cuota.NumeroCuota == cuota.IdPrestamoNavigation.Cuotas &&
                                            _context.Cuotas.Any(c => c.CuotaVencida == true))
                    {
                        var puntajePrestamo = new PuntosPorCliente();

                        puntajePrestamo.IdCliente = cuota.IdCliente;
                        puntajePrestamo.IdTransaccion = idTransaccion;
                        puntajePrestamo.IdDetalleTransaccion = idDetalleTransaccion;
                        puntajePrestamo.IdPuntaje = 4;

                        await _context.PuntosPorClientes.AddAsync(puntajePrestamo);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResultadoBase { CodigoEstado = 500, Message = ex.Message, Ok = false });
            }

            return await Task.FromResult(new ResultadoBase { CodigoEstado = 200, Message = "Cuotas ingresadas ok"/*Constantes.DefaultMessages.DefaultSuccesMessage.ToString()*/, Ok = true });
        }

        public async Task<ResultadoBase> ModificarPagoCuotas(ComandoCuota comando)
        {
            try
            {
                var transaccion = await _context.Transacciones.Where(c => c.IdTransaccion == comando.idTransaccion).FirstOrDefaultAsync();

                transaccion.FechaTransaccion = comando.fechaPago;
                transaccion.IdEntidadFinanciera = comando.idEntidadFinanciera;

                _context.Transacciones.Update(transaccion);
                await _context.SaveChangesAsync();

                foreach (ComandoDetalleCuotas dc in comando.detalleCuotas)
                {
                    var dt = await _context.DetalleTransacciones.Where(c => c.IdDetalleTransacciones == dc.IdDetalleTransaccion).FirstOrDefaultAsync();
                    dt.IdTransaccion = dc.IdTransaccion;
                    dt.Detalle = "Cuota: " + dc.NumeroCuota + " - Prestamo: " + dc.IdPrestamo;
                    dt.IdCategoria = 1;
                    dt.Monto = dc.MontoAbonado;

                    _context.DetalleTransacciones.Update(dt);
                    await _context.SaveChangesAsync();

                    var cuota = await _context.Cuotas.Where(c=>c.IdCuota == dc.IdCuota).FirstOrDefaultAsync();
                    cuota.FechaPago = comando.fechaPago;
                    cuota.MontoAbonado = dc.MontoAbonado;
                    cuota.CuotaVencida = comando.fechaPago > cuota.FechaVencimiento ? true : false;


                    _context.Cuotas.Update(cuota);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResultadoBase { CodigoEstado = 500, Message = ex.Message, Ok = false });
            }

            return await Task.FromResult(new ResultadoBase { CodigoEstado = 200, Message = "Cuota modificada correctamente"/*Constantes.DefaultMessages.DefaultSuccesMessage.ToString()*/, Ok = true });
        }
    }
}
