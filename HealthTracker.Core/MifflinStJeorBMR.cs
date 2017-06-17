namespace HealthTracker.Core
{
    /// <summary>
    /// Harris–Benedict equations revised by Mifflin and St Jeor in 1990
    /// </summary>
    public class MifflinStJeorBMR : IBasalMetaCalc
    {
        public double CalcBMR(Sex sex, double weight, int height, int age)
        {
            if (sex == Sex.Male)
                return (10 * weight) + (6.25 * height) - (5 * age) + 5;
            else
                return (10 * weight) + (6.25 * height) - (5 * age) - 161;
        }
    }
}