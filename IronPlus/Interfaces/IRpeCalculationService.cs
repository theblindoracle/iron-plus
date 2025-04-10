namespace IronPlus.Interfaces
{
    public interface IRpeCalculationService
    {
        double CalculateWantedSetWeight(int reps, double rpe, double e1rm);
        double CalculateOneRepMax(double weight, int reps, double rpe);
    }
}
