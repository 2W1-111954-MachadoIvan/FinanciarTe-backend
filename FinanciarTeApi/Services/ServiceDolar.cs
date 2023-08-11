using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;

namespace FinanciarTeApi.Services
{
    public class ServiceDolar : IServiceDolar
    {
        private readonly HttpClient _httpClient;
        private readonly FinanciarTeContext _context;

        public ServiceDolar(FinanciarTeContext context)
        {
            _httpClient = new HttpClient();
            _context = context;

        }
        
        public async Task<List<DTODolarIndice>> GetValoresHistoricosDolar()
        {
            var query = _context.ViewHistoricoDolaIndices
                        .AsNoTracking()
                        .Select(g => new DTODolarIndice
                        {
                            Fecha = g.Fecha,
                            ValorDolar = g.ValorDolar,
                            ValorDolarBlue = g.ValorDolarBlue,
                            Indice = g.Indice,
                        });

            return await query.ToListAsync();
        }

        public async Task<DTODolarIndice> GetUltimoValorDolar()
        {
            var maxFecha = await _context.ViewHistoricoDolaIndices.MaxAsync(c => c.Fecha);

            var query = _context.ViewHistoricoDolaIndices
                        .AsNoTracking()
                        .Where(c=>c.Fecha == maxFecha)
                        .Select(g => new DTODolarIndice
                        {
                            Fecha = g.Fecha,
                            ValorDolar = g.ValorDolar,
                            ValorDolarBlue = g.ValorDolarBlue,
                            Indice = g.Indice,
                        })
                        .FirstOrDefaultAsync();

            return await query;
        }

        public async Task<ResultadoBase> GetValorDolarHoy()
        {
            string apiUrl = "https://api.bluelytics.com.ar/v2/latest";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        JObject jsonObject = JObject.Parse(responseContent);

                        decimal valueOficial = (decimal)jsonObject["oficial"]["value_sell"];
                        decimal valueBlue = (decimal)jsonObject["blue"]["value_sell"];
                        DateTime lastUpdate = (DateTime)jsonObject["last_update"];

                        // Crear y guardar el nuevo registro en la base de datos
                        var newExchangeRate = new HistoricosIndice
                        {
                            Fecha = lastUpdate.Date,
                            ValorDolar = valueOficial,
                            ValorDolarBlue = valueBlue,
                        };

                        var valorDolarHoy = await _context.HistoricosIndices.Where(i => i.Fecha.Date == newExchangeRate.Fecha).FirstOrDefaultAsync();

                        if (valorDolarHoy == null)
                        {
                            _context.HistoricosIndices.Add(newExchangeRate);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            valorDolarHoy.ValorDolar = newExchangeRate.ValorDolar;
                            valorDolarHoy.ValorDolarBlue = newExchangeRate.ValorDolarBlue;
                            _context.HistoricosIndices.Update(valorDolarHoy);
                            await _context.SaveChangesAsync();
                        }                        

                        return await Task.FromResult(new ResultadoBase { CodigoEstado = 200, Message = "Valores ingresados ok"/*Constantes.DefaultMessages.DefaultSuccesMessage.ToString()*/, Ok = true });
                    }
                    else
                    {
                        // Manejar el caso de error en la respuesta de la API
                        // Puedes lanzar una excepción o realizar cualquier otra acción apropiada
                        throw new Exception($"Error al llamar a la API. Código de respuesta: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de la llamada a la API
                // Puedes lanzar una excepción, registrar el error, enviar una notificación, etc.
                return await Task.FromResult(new ResultadoBase { CodigoEstado = 500, Message = ex.Message, Ok = false });
            }

        }

    }
}
