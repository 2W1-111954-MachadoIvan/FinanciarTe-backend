using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;
using Microsoft.EntityFrameworkCore;

namespace FinanciarTeApi.Services
{
    public class ServiceCliente : IServiceCliente
    {
        private readonly FinanciarTeContext _context;

        public ServiceCliente(FinanciarTeContext context)
        {
            _context = context;
        }

        public async Task<ResultadoBase> PostCliente(ComandoCliente comando)
        {
            try
            {
                var cliente = new Cliente
                {
                    Nombres = comando.Nombres,
                    Apellidos = comando.Apellidos,
                    NroDni = comando.NroDni,
                    Telefono = comando.Telefono,
                    FechaDeNacimiento = comando.FechaDeNacimiento,
                    Email = comando.Email,
                    IdContactoAlternativoNavigation = new ContactosAlternativo
                    {
                        Nombres = comando.nombresAlt,
                        Apellidos = comando.apellidosAlt,
                        Telefono = comando.telAlt,
                        Email = comando.emailAlt
                    },
                    IdCiudad = comando.IdCiudad,
                    Direccion = comando.Direccion,
                    Numero = comando.Numero,
                    CodigoPostal = comando.CodigoPostal,
                    PuntosIniciales = comando.PuntosIniciales,
                    Activo = true
                };
                await _context.AddAsync(cliente);
                await _context.SaveChangesAsync(/*_securityService.GetUserName() ?? Constantes.DefaultSecurityValues.DefaultUserName*/); //TODO: replace this with the logged in user.
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResultadoBase { CodigoEstado = 500, Message = ex.Message, Ok = false });
            }

            return await Task.FromResult(new ResultadoBase { CodigoEstado = 200, Message = "Cliente ingresado ok"/*Constantes.DefaultMessages.DefaultSuccesMessage.ToString()*/, Ok = true });
        }

        public async Task<List<DTOCliente>> GetClientes()
        {
            var query = _context.ViewClientes
                        .AsNoTracking()
                        .Select(g => new DTOCliente
                        {
                            Dni = g.Dni,
                            Nombres = g.Nombres,
                            Apellidos = g.Apellidos,
                            FechaDeNacimiento = g.FechaDeNacimiento,
                            Telefono = g.Telefono,
                            Email = g.Email,
                            Direccion = g.Dirección,
                            CodigoPostal = g.CodigoPostal,
                            Ciudad = g.Ciudad,
                            Provincia = g.Provincia,
                            PuntosIniciales = g.PuntosIniciales,
                            PuntosActuales = g.PuntosActuales,
                            CantidadDePrestamos = g.CantidadDePrestamos,
                            Scoring = g.Scoring,
                            BeneficioScoring = g.BeneficioScoring,
                            Activo = g.Activo,
                            ContactoAlternativo = g.ContactoAlternativo,
                            TelContAlt = g.TelefonoContactoAlternativo,
                            EmailContAlt = g.EmailContactoAlternativo,
                        });

            return await query.ToListAsync();
        }

        public async Task<DTOCliente> GetViewClienteByID(int id)
        {
            var cliente = await _context.ViewClientes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Dni == id);
            DTOCliente comando = new DTOCliente();

            if (cliente != null)
            {
                comando.Dni = cliente.Dni;
                comando.Nombres = cliente.Nombres;
                comando.Apellidos = cliente.Apellidos;
                comando.FechaDeNacimiento = cliente.FechaDeNacimiento;
                comando.Telefono = cliente.Telefono;
                comando.Email = cliente.Email;
                comando.Direccion = cliente.Dirección;
                comando.CodigoPostal = cliente.CodigoPostal;
                comando.Ciudad = cliente.Ciudad;
                comando.Provincia = cliente.Provincia;
                comando.PuntosIniciales = cliente.PuntosIniciales;
                comando.PuntosActuales = cliente.PuntosActuales;
                comando.CantidadDePrestamos = cliente.CantidadDePrestamos;
                comando.Scoring = cliente.Scoring;
                comando.BeneficioScoring = cliente.BeneficioScoring;
                comando.ContactoAlternativo = cliente.ContactoAlternativo;
                comando.Activo = cliente.Activo;
                comando.TelContAlt = cliente.TelefonoContactoAlternativo;
                comando.EmailContAlt = cliente.EmailContactoAlternativo;
            }
            return comando;

        }

        public async Task<ComandoCliente> GetClienteByID(int id)
        {
            var cliente = await _context.Clientes
                .AsNoTracking()
                .Include(x => x.IdCiudadNavigation)
                .Include(x => x.IdContactoAlternativoNavigation)
                .Include(x => x.IdCiudadNavigation.IdProvinciaNavigation)
                .FirstOrDefaultAsync(x => x.NroDni == id);
            ComandoCliente comando = new ComandoCliente();

            if (cliente != null)
            {
                comando.NroDni = cliente.NroDni;
                comando.Nombres = cliente.Nombres;
                comando.Apellidos = cliente.Apellidos;
                comando.FechaDeNacimiento = cliente.FechaDeNacimiento;
                comando.Telefono = cliente.Telefono;
                comando.Email = cliente.Email;
                comando.Direccion = cliente.Direccion;
                comando.Numero = cliente.Numero;
                comando.CodigoPostal = cliente.CodigoPostal;
                comando.Activo = cliente.Activo;
                comando.IdCiudad = cliente.IdCiudadNavigation.IdCiudad;
                comando.IdProvincia = cliente.IdCiudadNavigation.IdProvinciaNavigation.IdProvincia;
                comando.idContactoAlternativo = cliente.IdContactoAlternativoNavigation.IdContactoAlternativo;
                comando.telAlt = cliente.IdContactoAlternativoNavigation.Telefono;
                comando.emailAlt = cliente.IdContactoAlternativoNavigation.Email;
                comando.nombresAlt = cliente.IdContactoAlternativoNavigation.Nombres;
                comando.apellidosAlt = cliente.IdContactoAlternativoNavigation.Apellidos;
                comando.PuntosIniciales = cliente.PuntosIniciales;
            }
            return comando;

        }

        public async Task<ResultadoBase> PutCliente(ComandoCliente comando)
        {
            try
            {
                var cliente = await _context.Clientes.Include(c=> c.IdContactoAlternativoNavigation).Where(c=> c.NroDni.Equals(comando.NroDni)).FirstOrDefaultAsync();
                cliente.Nombres = comando.Nombres;
                cliente.Apellidos = comando.Apellidos;
                cliente.NroDni = comando.NroDni;
                cliente.Telefono = comando.Telefono;
                cliente.FechaDeNacimiento = comando.FechaDeNacimiento;
                cliente.Email = comando.Email;
                cliente.IdContactoAlternativoNavigation.Nombres = comando.nombresAlt;
                cliente.IdContactoAlternativoNavigation.Apellidos = comando.apellidosAlt;
                cliente.IdContactoAlternativoNavigation.Telefono = comando.telAlt;
                cliente.IdContactoAlternativoNavigation.Email = comando.emailAlt;
                cliente.IdCiudad = comando.IdCiudad;
                cliente.Direccion = comando.Direccion;
                cliente.Numero = comando.Numero;
                cliente.CodigoPostal = comando.CodigoPostal;
                cliente.PuntosIniciales = comando.PuntosIniciales;
                cliente.Activo = comando.Activo;
                _context.Update(cliente);
                await _context.SaveChangesAsync(/*_securityService.GetUserName() ?? Constantes.DefaultSecurityValues.DefaultUserName*/); //TODO: replace this with the logged in user.
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResultadoBase { CodigoEstado = 500, Message = ex.Message, Ok = false });
            }

            return await Task.FromResult(new ResultadoBase { CodigoEstado = 200, Message = "Cliente modificado ok"/*Constantes.DefaultMessages.DefaultSuccesMessage.ToString()*/, Ok = true });
        }

        public async Task<ResultadoBase> DeleteCliente(int id)
        {
            ResultadoBase resultado = new ResultadoBase();
            var cliente = await _context.Clientes.Where(c => c.NroDni.Equals(id)).FirstOrDefaultAsync();
            if (cliente != null)
            {
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El cliente se desactivo exitosamente.";
                cliente.Activo = false;
                _context.Update(cliente);
                await _context.SaveChangesAsync(/*_securityService.GetUserName() ?? Constantes.DefaultSecurityValues.DefaultUserName*/);
            }
            else
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al desactivar el cliente";
                return resultado;
            }

            return resultado;
        }

        public async Task<List<ComboBoxItemDto>> GetClientesForComboBox()
        {
            return await _context.Clientes.AsNoTracking().Select<Cliente, ComboBoxItemDto>(x => x).ToListAsync();
        }

    }
}
