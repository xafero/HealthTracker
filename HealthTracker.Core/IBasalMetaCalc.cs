namespace HealthTracker.Core
{
    public interface IBasalMetaCalc
    {
        double CalcBMR(Sex sex, double weight, int height, int age);
    }
}