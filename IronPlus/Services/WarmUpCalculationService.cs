using System;
using System.Collections.Generic;
using IronPlus.Helpers;
using IronPlus.Models;

namespace IronPlus.Services
{
    public class WarmUpCalculationService : IWarmUpCalculationService
    {


        public List<WarmUpWeight> CalculateWarmUps(double topSet, bool isKilograms, bool compCollar, int barbellWeight)
        {
            if (topSet <= 0)
            {
                throw new ArgumentException("Value cannot be negative or zero.");
            }

            double weightOfBarbell = barbellWeight;
            List<double> weightJumps;
            WarmUpWeight weight;
            WarmUpWeight previousWeight;

            if (isKilograms)
            {
                weightOfBarbell += compCollar ? 5 : 0;
                weightJumps = new List<double> { 50, 40, 30, 20, 10, 5, 2.5, 1.25 };
                weight = new WarmUpWeight() { Weight = weightOfBarbell, WeightAdded = 0, PercentOfTopSet = weightOfBarbell / topSet };
            }
            else
            {
                weightOfBarbell += compCollar ? 10 : 0;
                weightJumps = new List<double> { 90, 70, 50, 40, 30, 20, 10, 5 }; ;
                weight = new WarmUpWeight() { Weight = weightOfBarbell, WeightAdded = 0, PercentOfTopSet = weightOfBarbell / topSet };
            }


            var removeFromList = new List<double>();

            var lww = GeneralHelpers.RoundValueToNearest(topSet * .92, isKilograms ? 2.5 : 5);

            var lastWarmUp = new WarmUpWeight { Weight = lww, PercentOfTopSet = lww / topSet };

            List<WarmUpWeight> weights = new List<WarmUpWeight>();

            weights.Add(weight);

            while (weight.Weight < lastWarmUp.Weight)
            {
                foreach (double jump in weightJumps)
                {
                    if (ShouldWeightBeAdded(weight, jump))
                    {
                        previousWeight = weight;
                        weight = new WarmUpWeight() { Weight = weight.Weight + jump, WeightAdded = jump, PercentOfTopSet = (weight.Weight + jump) / topSet };
                        weights.Add(weight);
                        break;
                    }
                    else
                    {
                        removeFromList.Add(jump);
                    }

                }

                foreach (double jump in removeFromList)
                {
                    weightJumps.Remove(jump);
                }

                removeFromList.Clear();

                if (weightJumps.Count == 0)
                    break;
            }

            lastWarmUp.WeightAdded = lastWarmUp.Weight - weight.Weight;

            weights.Add(lastWarmUp);
            weights.Add(new WarmUpWeight { Weight = topSet, WeightAdded = topSet - lastWarmUp.Weight, PercentOfTopSet = 1 });

            return weights;


            bool ShouldWeightBeAdded(WarmUpWeight weightOnBar, double weightAdded)
            {
                var totalWeight = weightOnBar.Weight + weightAdded;
                var percentOfTopSet = totalWeight / topSet;
                var percentJump = percentOfTopSet - weight.PercentOfTopSet;

                if (totalWeight >= lastWarmUp.Weight)
                    return false;

                if (percentOfTopSet > .86)
                    return false;

                if (percentJump >= .34)
                    return false;

                if (percentJump <= 1 - lastWarmUp.PercentOfTopSet)
                    return false;

                return true;
            }
        }
    }
}
