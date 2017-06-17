namespace HealthTracker.Core
{
    /// <summary>
    /// Body Fat Percentage Formula
    /// </summary>
    public class BodyFatEstimate
    {
        private readonly BodyMassIndex bmi = new BodyMassIndex();

        public double EstimateFat(double weight, int height, int age, Sex sex)
            => (1.20 * bmi.CalcBMI(weight, height)) + (0.23 * age) - (10.8 * (sex == Sex.Male ? 1 : 0)) - 5.4;
    }
}