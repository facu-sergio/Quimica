using Quimica.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Bussiness
{
    public interface IDireccionServices
    {
        Task<int> AddDireccionAsync(Address direccion);
        Task<Address> GetDireccionByIdAsync(int id);
        Task<IEnumerable<Address>> GetAllDireccionesAsync();
        Task<IEnumerable<Address>> GetDireccionesByClienteIdAsync(int idCliente);
        Task UpdateDireccionAsync(Address direccion);
        Task DeleteDireccionAsync(int id);
    }
}
