using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinanciarTeApi.Services
{
    public class ServiceCobroCuotas : IServiceCobroCuotas
    {
        private readonly FinanciarTeContext _context;
        public ServiceCobroCuotas(FinanciarTeContext context)
        {
            _context = context;
        }
        public Task<ResultadoBase> DeleteCuota(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DTOCobroCuota> GetCuotaByID(int id)
        {
            var comando = await _context.CobrosCuotas
                                      .Where(c => c.IdCobroCuota == id)
                                      .FirstOrDefaultAsync();

            DTOCobroCuota cuota = new DTOCobroCuota();

            if(comando != null)
            {
                cuota.idCobroCuota = comando.IdCobroCuota;
                cuota.idCliente = comando.IdCliente;
                cuota.idPrestamo = comando.IdPrestamo;
                cuota.nroCuota = comando.NumeroCuota;
                cuota.montoAbonado = comando.MontoAbonado;
                cuota.fechaPago = comando.FechaPago;
                cuota.cuotaVencida = comando.CuotaVencida == true ? "Vencida" : "En plazo";
                cuota.puntos = comando.IdPuntosNavigation?.CantidadPuntos;
                cuota.idTransaccion = comando.IdTransaccion;
            }

            return cuota;
        }

        public async Task<List<DTOCobroCuota>> GetCuotasByCliente(int id)
        {
            List<DTOCobroCuota> cuotas = await _context.CobrosCuotas
                                      .Where(c => c.IdCliente == id)
                                      .Select(c => new DTOCobroCuota
                                      {
                                          idCobroCuota = c.IdCobroCuota,
                                          idCliente = c.IdCliente,
                                          idPrestamo = c.IdPrestamo,
                                          nroCuota = c.NumeroCuota,
                                          montoAbonado = c.MontoAbonado,
                                          fechaPago = c.FechaPago,
                                          cuotaVencida = c.CuotaVencida == true ? "Vencida" : "En plazo",
                                          puntos = c.IdPuntosNavigation.CantidadPuntos,
                                          idTransaccion = c.IdTransaccion
                                      }).ToListAsync();
            return cuotas;
        }

        public async Task<ResultadoBase> RegistrarCuotas(ComandoCobroCuota comando)
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

                    CobrosCuota cobrosCuota = new CobrosCuota();
                    cobrosCuota.NumeroCuota = dc.numeroCuota;
                    cobrosCuota.MontoAbonado = dc.montoAbonado;
                    cobrosCuota.IdPrestamo = dc.idPrestamo;
                    cobrosCuota.CuotaVencida = dc.cuotaVencida;
                    cobrosCuota.IdCliente = dc.idCliente;
                    cobrosCuota.IdTransaccion = idTransaccion;
                    cobrosCuota.IdPuntos = dc.idPuntos;
                    cobrosCuota.FechaPago = comando.fechaPago;
                    cobrosCuota.IdDetalleTransaccion = detalles.IdDetalleTransacciones;

                    await _context.CobrosCuotas.AddAsync(cobrosCuota);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResultadoBase { CodigoEstado = 500, Message = ex.Message, Ok = false });
            }

            return await Task.FromResult(new ResultadoBase { CodigoEstado = 200, Message = "Cuotas ingresadas ok"/*Constantes.DefaultMessages.DefaultSuccesMessage.ToString()*/, Ok = true });
        }

        public async Task<ResultadoBase> ModificarCuota(ComandoCobroCuota comando)
        {
            try
            {
                foreach (ComandoDetalleCuotas dc in comando.detalleCuotas)
                {
                    var dt = await _context.DetalleTransacciones.Where(c => c.IdDetalleTransacciones == dc.idDetalleTransaccion).FirstOrDefaultAsync();
                    dt.IdTransaccion = dc.idTransaccion;
                    dt.Detalle = "Cuota: " + dc.numeroCuota + " - Prestamo: " + dc.idPrestamo;
                    dt.IdCategoria = 1;
                    dt.Monto = dc.montoAbonado;

                    _context.DetalleTransacciones.Update(dt);
                    await _context.SaveChangesAsync();

                    var cobrosCuota = await _context.CobrosCuotas.Where(c=>c.IdCobroCuota == dc.idCobroCuota).FirstOrDefaultAsync();
                    cobrosCuota.NumeroCuota = dc.numeroCuota;
                    cobrosCuota.MontoAbonado = dc.montoAbonado;
                    cobrosCuota.IdPrestamo = dc.idPrestamo;
                    cobrosCuota.CuotaVencida = dc.cuotaVencida;
                    cobrosCuota.IdCliente = dc.idCliente;
                    cobrosCuota.IdTransaccion = dc.idTransaccion;
                    cobrosCuota.IdPuntos = dc.idPuntos;
                    cobrosCuota.FechaPago = comando.fechaPago;
                    cobrosCuota.IdDetalleTransaccion = dc.idDetalleTransaccion;

                    _context.CobrosCuotas.Update(cobrosCuota);
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
