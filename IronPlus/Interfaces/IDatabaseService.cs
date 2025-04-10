using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IronPlus.Models;

namespace IronPlus.Interfaces
{
    public interface IDatabaseService
    {
        Task<List<Barbell>> GetBarbellsAsync();
        Task<int> SaveBarbellAsync(Barbell barbell);
        Task<Barbell> GetBarbellAsync(int id);
        Task<int> DeleteBarbellAsync(Barbell barbell);
    }
}
