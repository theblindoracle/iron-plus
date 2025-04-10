using System.Collections.Generic;
using System.Threading.Tasks;
using IronPlus.Models;

namespace IronPlus.Services
{
    public interface IWarmUpCalculationService
    {
        List<WarmUpWeight> CalculateWarmUps(double topSet, bool isKilograms, bool compCollar, int barbellWeight);
    }
}