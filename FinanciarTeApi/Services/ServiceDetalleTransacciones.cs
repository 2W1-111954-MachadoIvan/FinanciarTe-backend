using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

namespace FinanciarTeApi.Services
{
    public class ServiceDetalleTransacciones : IServiceDetalleTransacciones
    {
        private readonly FinanciarTeContext _context;

        public ServiceDetalleTransacciones(FinanciarTeContext context)
        {
            _context = context;
        }

        public async Task<List<Transaccione>> GetListadoDetalleTransacciones()
        {
            throw new NotImplementedException();
        }

        public async Task<ResultadoBase> GetDetallesTransacciones(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultadoBase> RegistrarDetalleTransaccion()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResultadoBase { CodigoEstado = 500, Message = ex.Message, Ok = false });
            }

            return await Task.FromResult(new ResultadoBase { CodigoEstado = 200, Message = "Detalle de transacción ingresada correctamente"/*Constantes.DefaultMessages.DefaultSuccesMessage.ToString()*/, Ok = true });
        }
    }
}
