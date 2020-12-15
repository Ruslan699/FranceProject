using System.Collections.Generic;
using System.Threading.Tasks;
using DataModel.Models;

namespace DataModel.Repositories
{
    public interface IRegionsRepository
    {
        Task<ICollection<Region>> GetActive();
    }
}
