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

        public async Task<DTOCuota> GetCuotaByID(int id)
        {
            var comando = await _context.Cuotas
                                      .Where(c => c.IdCuota == id)
                                      .FirstOrDefaultAsync();

            DTOCuota cuota = new DTOCuota();

            if(comando != null)
            {
                cuota.idCuota = comando.IdCuota;
                cuota.idCliente = comando.IdCliente;
                cuota.idPrestamo = comando.IdPrestamo;
                cuota.nroCuota = comando.NumeroCuota;
                cuota.MontoCuota = comando.MontoCuota;
                cuota.montoAbonado = comando.MontoAbonado;
                cuota.fechaPago = comando.FechaPago;
                cuota.cuotaVencida = comando.CuotaVencida == true ? "Vencida" : "En plazo";
                cuota.puntos = _context.Puntos.Where(c=>c.IdPuntos == comando.IdPuntos).Select(c=>c.CantidadPuntos).FirstOrDefault();
                cuota.idTransaccion = comando.IdTransaccion;
            }

            return cuota;
        }

        public async Task<List<DTOCuota>> GetCuotasByCliente(int id)
        {
            List<DTOCuota> cuotas = await _context.Cuotas
                                      .Where(c => c.IdCliente == id)
                                      .Select(c => new DTOCuota
                                      {
                                          idCuota = c.IdCuota,
                                          idCliente = c.IdCliente,
                                          idPrestamo = c.IdPrestamo,
                                          nroCuota = c.NumeroCuota,
                                          MontoCuota = c.MontoCuota,
                                          FechaVencimiento = c.FechaVencimiento,
                                          montoAbonado = c.MontoAbonado,
                                          fechaPago = c.FechaPago,
                                          cuotaVencida = c.FechaPago > c.FechaVencimiento ? "Vencida" : 
                                                         c.FechaPago == null && DateTime.Now > c.FechaVencimiento? "Vencida" :
                                                         c.FechaPago == null && DateTime.Now < c.FechaVencimiento ? "En plazo" : "En plazo",
                                          puntos = _context.Puntos.Where(x => x.IdPuntos == c.IdPuntos).Select(x => x.CantidadPuntos).FirstOrDefault(),
                                          idTransaccion = c.IdTransaccion
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
                    detalles.Detalle = "Cuota: " + dc.numeroCuota + " - Prestamo: " + dc.idPrestamo;
                    detalles.IdCategoria = 1;
                    detalles.Monto = dc.montoAbonado;

                    await _context.DetalleTransacciones.AddAsync(detalles);
                    await _context.SaveChangesAsync();

                    var idDetalleTransaccion = _context.DetalleTransacciones.Where(c=>c.IdDetalleTransacciones.Equals(detalles.IdDetalleTransacciones)).Select(c=>c.IdDetalleTransacciones).FirstOrDefault();

                    var cuota = _context.Cuotas.Where(c=>c.NumeroCuota == dc.numeroCuota && c.IdPrestamo == dc.idPrestamo).FirstOrDefault();

                    cuota.CuotaVencida = comando.fechaPago > cuota.FechaVencimiento ? true : false;
                    cuota.FechaPago = comando.fechaPago;
                    cuota.MontoAbonado = dc.montoAbonado;
                    cuota.IdTransaccion = idTransaccion;
                    cuota.IdDetalleTransaccion = idDetalleTransaccion;
                    cuota.IdPuntos = cuota.CuotaVencida == true ? 1 : 2;

                    _context.Cuotas.Update(cuota);
                    await _context.SaveChangesAsync();
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
                    var dt = await _context.DetalleTransacciones.Where(c => c.IdDetalleTransacciones == dc.idDetalleTransaccion).FirstOrDefaultAsync();
                    dt.IdTransaccion = dc.idTransaccion;
                    dt.Detalle = "Cuota: " + dc.numeroCuota + " - Prestamo: " + dc.idPrestamo;
                    dt.IdCategoria = 1;
                    dt.Monto = dc.montoAbonado;

                    _context.DetalleTransacciones.Update(dt);
                    await _context.SaveChangesAsync();

                    var cuota = await _context.Cuotas.Where(c=>c.IdCuota == dc.idCuota).FirstOrDefaultAsync();
                    cuota.FechaPago = comando.fechaPago;
                    cuota.MontoAbonado = dc.montoAbonado;
                    cuota.CuotaVencida = comando.fechaPago > cuota.FechaVencimiento ? true : false;
                    cuota.IdPuntos = cuota.CuotaVencida == true ? 2 : 1;


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
