using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FinanciarTeApi.Services
{
    public class ServiceTransacciones : IServiceTransacciones
    {
        private readonly FinanciarTeContext _context;

        public ServiceTransacciones(FinanciarTeContext context)
        {
            _context = context;
        }

        public async Task<List<DTOTransacciones>> GetListadoTransacciones()
        {
            var query = (from tr in _context.Transacciones.AsNoTracking()
                         join dt in _context.DetalleTransacciones.AsNoTracking() on tr.IdTransaccion equals dt.IdTransaccion
                         join ef in _context.EntidadesFinancieras.AsNoTracking() on tr.IdEntidadFinanciera equals ef.IdEntidadFinanciera
                         join tef in _context.TiposEntidadFinancieras.AsNoTracking() on ef.IdTipoEntidad equals tef.IdTipoEntidad
                         group new { tr, dt, ef, tef } by new { tr.IdTransaccion, tr.FechaTransaccion, ef.Descripción } into grp
                         orderby grp.Key.FechaTransaccion descending
                         select new DTOTransacciones
                         {
                             idTransaccion = grp.Key.IdTransaccion,
                             FechaTransaccion = grp.Key.FechaTransaccion,
                             EntidadFinanciera = grp.Key.Descripción,
                             MontoTotal = grp.Sum(x => x.dt.Monto)
                         });

            return await query.ToListAsync();
        }

        public async Task<DTOTransacciones_DetTr> GetTransaccionById(int id)
        {
            var transaccion = await _context.Transacciones.AsNoTracking()
                                .Include(c => c.IdEntidadFinancieraNavigation)
                                .Include(c => c.IdEntidadFinancieraNavigation.IdTipoEntidadNavigation)
                                .Where(c => c.IdTransaccion == id)
                                .FirstOrDefaultAsync();

            List<DetalleTransaccione> dt = await _context.DetalleTransacciones.AsNoTracking()
                                                            .Include(c => c.IdCategoriaNavigation)
                                                            .Include(c => c.IdCategoriaNavigation.IdTipoTransaccionNavigation)
                                                            .Where(c => c.IdTransaccionNavigation.IdTransaccion == id)
                                                            .ToListAsync();

            DTOTransacciones_DetTr comando = new DTOTransacciones_DetTr();

            if (transaccion != null)
            {
                comando.IdTransaccion = transaccion.IdTransaccion;
                comando.FechaTransaccion = transaccion.FechaTransaccion;
                comando.idEntidadFinanciera = transaccion.IdEntidadFinanciera;
                comando.EntidadFinanciera = transaccion.IdEntidadFinancieraNavigation.Descripción;

                if (dt.Count() > 0)
                {
                    foreach (DetalleTransaccione de in dt)
                    {
                        DTO_Detalle_Transaccion detTr = new DTO_Detalle_Transaccion();
                        detTr.idDetalleTransaccion = de.IdDetalleTransacciones;
                        detTr.IdTransaccion = de.IdTransaccion;
                        detTr.idCategoria = de.IdCategoria;
                        detTr.Categoria = de.IdCategoriaNavigation.Descripcion;
                        detTr.Detalle = de.Detalle;
                        detTr.TipoTransaccion = de.IdCategoriaNavigation.IdTipoTransaccionNavigation.Descripción;
                        detTr.Monto = de.Monto;

                        comando.detalleTransacciones.Add(detTr);
                    }
                }
            }

            return comando;
        }

        public async Task<ResultadoBase> RegistrarTransaccion(ComandoTransaccion comando)
        {
            try
            {
                var transaccion = new Transaccione();

                transaccion.FechaTransaccion = comando.fechaTransaccion;
                transaccion.IdEntidadFinanciera = comando.idEntidadFinanciera;

                await _context.Transacciones.AddAsync(transaccion);
                await _context.SaveChangesAsync();

                var idTransaccion = _context.Transacciones.Where(c => c.IdTransaccion.Equals(transaccion.IdTransaccion)).Select(c => c.IdTransaccion).FirstOrDefault();

                foreach (ComandoDetalleTransaccion dt in comando.detallesTransacciones)
                {
                    DetalleTransaccione detalles = new DetalleTransaccione();

                    detalles.IdTransaccion = idTransaccion;
                    detalles.Detalle = dt.detalle;
                    detalles.IdCategoria = dt.idCategoria;
                    if(dt.idCategoria < 3)
                    {
                        detalles.Monto = dt.monto;                        
                    }
                    else
                    {
                        detalles.Monto = 0 - dt.monto;
                    }
                    

                    await _context.DetalleTransacciones.AddAsync(detalles);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResultadoBase { CodigoEstado = 500, Message = ex.Message, Ok = false });
            }

            return await Task.FromResult(new ResultadoBase { CodigoEstado = 200, Message = "Transacción ingresada correctamente"/*Constantes.DefaultMessages.DefaultSuccesMessage.ToString()*/, Ok = true });
        }

        public async Task<ResultadoBase> ModificarTransaccion(ComandoTransaccion comando)
        {
            

            try
            {
                var transaccion = _context.Transacciones.Where(c => c.IdTransaccion == comando.idTransaccion).FirstOrDefault();

                transaccion.FechaTransaccion = comando.fechaTransaccion;
                transaccion.IdEntidadFinanciera = comando.idEntidadFinanciera;

                _context.Transacciones.Update(transaccion);
                await _context.SaveChangesAsync();                

                foreach (ComandoDetalleTransaccion dt in comando.detallesTransacciones)
                {
                    var detalles = _context.DetalleTransacciones.Where(c => c.IdDetalleTransacciones.Equals(dt.idDetalleTransaccion)).FirstOrDefault();
                    detalles.Detalle = dt.detalle;
                    detalles.Monto = dt.monto;
                    detalles.IdCategoria = dt.idCategoria;

                    _context.DetalleTransacciones.Update(detalles);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResultadoBase { CodigoEstado = 500, Message = ex.Message, Ok = false });
            }

            return await Task.FromResult(new ResultadoBase { CodigoEstado = 200, Message = "Transacción modificada correctamente"/*Constantes.DefaultMessages.DefaultSuccesMessage.ToString()*/, Ok = true });
        }
    }
}
