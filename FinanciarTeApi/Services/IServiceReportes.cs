using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanciarTeApi.Services
{
    public interface IServiceReportes
    {
        Task<List<DTODolarIndice>> GetValoresHistoricosDolar();

        Task<List<DTODolarIndice>> GetMaxMinDolarIndice();

        Task<List<DTOCliente>> GetClientesConMasPrestamos();

        Task<List<DTORecaudacion>> GetRecaudacionMensual();

        Task<List<DTOBalance>> GetBalance();

        Task<List<DTOCuotasMesEnCurso>> GetCuotasMesEnCurso();
    }
}
