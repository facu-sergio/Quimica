using Microsoft.Extensions.Logging;
using Quimica.Core.Bussiness;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;


namespace Quimica.Service.Business
{
    public class DireccionService : IDireccionServices
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<DireccionService> _logger;

        public DireccionService(IUnitOfWork unitOfWork, ILogger<DireccionService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> AddDireccionAsync(Address direccion)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var clienteRepository = _unitOfWork.GetRepository<Cliente>();
                var direccionRepository = _unitOfWork.GetRepository<Address>();

                direccion.id_cliente = await clienteRepository.AddAsync(direccion.cliente);
                int direccionId = await direccionRepository.AddAsync(direccion);
                _unitOfWork.Commit();

                return direccionId;
            }
            catch (Exception ex)
            {
                _unitOfWork?.Rollback();
                _logger.LogError(ex, $"DireccionService/AddDireccionAsync({direccion})");
                throw;
            }

        }

        public Task DeleteDireccionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Address>> GetAllDireccionesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Address> GetDireccionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Address>> GetDireccionesByClienteIdAsync(int idCliente)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDireccionAsync(Address direccion)
        {
            throw new NotImplementedException();
        }
    }
}
