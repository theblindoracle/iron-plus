using IronPlus.Interfaces;

namespace IronPlus.Services
{
    public class RpeCalculationService : IRpeCalculationService
    {
        double GetPercentOfOneRepMax(int reps, double rpe)
        {
            // Cap the RPE at 10.
            if (rpe > 10)
            {
                rpe = 10.0;
            }

            // No prediction if failure occurred, or if RPE is unreasonably low.
            if (reps < 1 || rpe < 4)
            {
                return 0.0;
            }

            // Handle the obvious case early to avoid bound errors.
            if (reps == 1 && rpe == 10.0)
            {
                return 1.0;
            }

            // defined such that 1@10 = 0, 1@9 = 1, 1@8 = 2, etc.
            var repsInReserve = (10.0 - rpe) + (reps - 1);

            var intersection = 2.92;

            // The highest values follow a quadratic.
            // Parameters were resolved via GNUPlot and match extremely closely.
            if (repsInReserve <= intersection)
            {
                var a = 0.347619;
                var B = -4.60714;
                var c = 99.9667;
                return (a * repsInReserve * repsInReserve + B * repsInReserve + c) / 100.0;
            }

            // Otherwise it's just a line, since Tuchscherer just guessed.
            var m = -2.64249;
            var b = 97.0955;
            return (m * repsInReserve + b) / 100.0;
        }

        public double CalculateWantedSetWeight(int reps, double rpe, double e1rm)
        {
            return GetPercentOfOneRepMax(reps, rpe) * e1rm;
        }


        public double CalculateOneRepMax(double weight, int reps, double rpe)
        {
            return weight / GetPercentOfOneRepMax(reps, rpe);
        }

    }
}
