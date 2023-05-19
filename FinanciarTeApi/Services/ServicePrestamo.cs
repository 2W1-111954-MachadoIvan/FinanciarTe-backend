using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinanciarTeApi.Services
{
    public class ServicePrestamo : IServicePrestamo
    {
        private readonly FinanciarTeContext _context;

        public ServicePrestamo(FinanciarTeContext context)
        {
            _context = context;
        }

        public Task<ResultadoBase> DeletePrestamo(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DTOListadoPrestamos>> GetPrestamosByCliente(int id)
        {         
            var query = _context.Prestamos
                        .Where(p => p.IdCliente == id)
                        .GroupJoin(
                            _context.Cuotas,
                            p => p.IdPrestamo,
                            cc => cc.IdPrestamo,
                            (p, cc) => new { Prestamo = p, Cuotas = cc.DefaultIfEmpty() }
                        )
                        .Select(g => new DTOListadoPrestamos
                        {
                            idPrestamo = g.Prestamo.IdPrestamo,
                            idCliente = g.Prestamo.IdCliente,
                            montoOtorgado = g.Prestamo.MontoOtorgado,
                            cuotasPagas = g.Cuotas.Sum(c=>c.MontoAbonado) > 0 ? g.Cuotas.Where(c=>c.FechaPago != null).Count() + " de " + g.Prestamo.Cuotas : "0 de " + g.Prestamo.Cuotas,
                            saldoPendiente = g.Cuotas.Any() ? g.Prestamo.MontoOtorgado - g.Cuotas.Sum(c => c.MontoAbonado) : 0,
                            estado = _context.Prestamos.Any(p => p.IdPrestamoRefinanciado == g.Prestamo.IdPrestamo) ? "Refinanciado" :
                                     g.Cuotas.Any() ? (g.Prestamo.MontoOtorgado - g.Cuotas.Sum(c => c.MontoAbonado)) > 0 ? "Pendiente" : "Cancelado" : "Cancelado"
                        });

            return await query.ToListAsync();
        }

        public async Task<DTOPrestamo> GetPrestamoByID(int id)
        {
            var prestamo = await _context.Prestamos.Where(c=>c.IdPrestamo == id).FirstOrDefaultAsync();

            var refinanciado = await _context.Prestamos.Where(c => c.IdPrestamoRefinanciado == id).FirstOrDefaultAsync();

            List<DTOCuota> cuotas = await _context.Cuotas
                                      .Where(c => c.IdPrestamo == id)
                                      .Select(c => new DTOCuota
                                      {
                                          idCuota = c.IdCuota,
                                          idPrestamo = c.IdPrestamo,
                                          nroCuota = c.NumeroCuota,
                                          montoAbonado = c.MontoAbonado,
                                          MontoCuota = c.MontoCuota,
                                          fechaPago = c.FechaPago,
                                          FechaVencimiento = c.FechaVencimiento,
                                          cuotaVencida = c.CuotaVencida == true ? "Vencida" :
                                                         c.FechaPago > c.FechaVencimiento ? "Vencida" :
                                                         c.FechaPago == null && DateTime.Now > c.FechaVencimiento ? "Vencida" :
                                                         c.FechaPago == null && DateTime.Now < c.FechaVencimiento ? "En plazo" : "En plazo",
                                          //puntos = c.IdPuntosNavigation.CantidadPuntos
                                      }).ToListAsync();

            DTOPrestamo comando = new DTOPrestamo();

            if(prestamo != null)
            {
                comando.idPrestamo = prestamo.IdPrestamo;
                comando.idCliente = prestamo.IdCliente;
                comando.montoOtorgado = prestamo.MontoOtorgado;
                if (refinanciado != null)
                {
                    comando.estado = "Refinanciado";
                }
                if (cuotas.Sum(c=>c.montoAbonado) >= prestamo.MontoOtorgado)
                {
                    comando.estado = "Cancelado";
                }
                if(cuotas.Sum(c=>c.montoAbonado) >= 0)
                {
                    comando.estado = "Pendiente";
                }
                comando.saldoPendiente = prestamo.MontoOtorgado - cuotas.Sum(c=>c.montoAbonado);
                comando.cuotas = cuotas;
            }

            return comando;
        }

        public async Task<ResultadoBase> RegistrarPrestamo(ComandoPrestamo comando)
        {
            try
            {
                var transaccion = new Transaccione();

                transaccion.FechaTransaccion = comando.Fecha;
                transaccion.IdEntidadFinanciera = comando.idEntidadFinanciera;

                await _context.Transacciones.AddAsync(transaccion);
                await _context.SaveChangesAsync();

                var idTransaccion = _context.Transacciones.Where(c => c.IdTransaccion.Equals(transaccion.IdTransaccion)).Select(c => c.IdTransaccion).FirstOrDefault();

                DetalleTransaccione detalles = new DetalleTransaccione();

                detalles.IdTransaccion = idTransaccion;
                detalles.Detalle = "Nvo. Prestamo - Cliente:" + comando.idCliente;
                detalles.IdCategoria = 3;
                detalles.Monto = 0 - comando.montoOtorgado;


                await _context.DetalleTransacciones.AddAsync(detalles);
                await _context.SaveChangesAsync();
                
                var p = new Prestamo()
                {
                    IdCliente = comando.idCliente,
                    MontoOtorgado = comando.montoOtorgado,
                    Cuotas = comando.Cuotas,
                    DiaVencimientoCuota = comando.DiaVencimientoCuota,
                    IdScoring = comando.idScoring,
                    IndiceInteres = comando.IndiceInteres,
                    RefinanciaDeuda = comando.RefinanciaDeuda,
                    IdPrestamoRefinanciado = comando.IdPrestamoRefinanciado,
                    IdTransaccion = idTransaccion
                };

                await _context.Prestamos.AddAsync(p);
                await _context.SaveChangesAsync();

                var idPrestamo = _context.Prestamos.Where(c => c.IdPrestamo.Equals(p.IdPrestamo)).Select(c => c.IdPrestamo).FirstOrDefault();

                for (var i = 1; i <= comando.Cuotas; i++)
                {
                    Cuota cuota = new Cuota();
                    cuota.NumeroCuota = i;
                    cuota.MontoCuota = (comando.montoOtorgado*(1+comando.IndiceInteres)) / comando.Cuotas;
                    cuota.IdPrestamo = idPrestamo;
                    cuota.IdCliente = comando.idCliente;

                    DateTime fechaVencimiento = comando.Fecha.AddMonths(i);
                    fechaVencimiento = new DateTime(fechaVencimiento.Year, fechaVencimiento.Month, (int)comando.DiaVencimientoCuota);
                    cuota.FechaVencimiento = fechaVencimiento;

                    await _context.Cuotas.AddAsync(cuota);
                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResultadoBase { CodigoEstado = 500, Message = ex.Message, Ok = false });
            }

            return await Task.FromResult(new ResultadoBase { CodigoEstado = 200, Message = "Prestamo ingresado ok"/*Constantes.DefaultMessages.DefaultSuccesMessage.ToString()*/, Ok = true });
        }

        public async Task<ResultadoBase> ModificarPrestamo(ComandoPrestamo comando)
        {
            try
            {
                var transaccion = _context.Transacciones.Where(c => c.IdTransaccion == comando.idTransaccion).FirstOrDefault();

                transaccion.FechaTransaccion = comando.Fecha;
                transaccion.IdEntidadFinanciera = comando.idEntidadFinanciera;

                _context.Transacciones.Update(transaccion);
                await _context.SaveChangesAsync();

                var dt = _context.DetalleTransacciones.Where(c => c.IdTransaccion == comando.idTransaccion).FirstOrDefault();

                dt.Monto = comando.montoOtorgado;

                _context.DetalleTransacciones.Update(dt);
                await _context.SaveChangesAsync();

                var p = _context.Prestamos.Where(c => c.IdPrestamo == comando.idPrestamo).FirstOrDefault();

                p.MontoOtorgado = comando.montoOtorgado;
                p.Cuotas = comando.Cuotas;
                p.DiaVencimientoCuota = comando.DiaVencimientoCuota;
                p.IdScoring = comando.idScoring;
                p.IndiceInteres = comando.IndiceInteres;
                p.RefinanciaDeuda = comando.RefinanciaDeuda;
                p.IdPrestamoRefinanciado = comando.IdPrestamoRefinanciado;

                _context.Prestamos.Update(p);
                await _context.SaveChangesAsync();

                var cuotas = _context.Cuotas.Where(c => c.IdPrestamo == comando.idPrestamo).ToList();

                foreach (var cuota in cuotas)
                {
                    cuota.MontoCuota = (comando.montoOtorgado * (1 + comando.IndiceInteres)) / comando.Cuotas;

                    DateTime fechaVencimiento = (DateTime)cuota.FechaVencimiento;
                    fechaVencimiento = new DateTime(fechaVencimiento.Year, fechaVencimiento.Month, (int)comando.DiaVencimientoCuota);
                    cuota.FechaVencimiento = fechaVencimiento;

                    _context.Cuotas.Update(cuota);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResultadoBase { CodigoEstado = 500, Message = ex.Message, Ok = false });
            }

            return await Task.FromResult(new ResultadoBase { CodigoEstado = 200, Message = "Prestamo modificado ok"/*Constantes.DefaultMessages.DefaultSuccesMessage.ToString()*/, Ok = true });
        }
    }
}
