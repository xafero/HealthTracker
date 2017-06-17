using System;

namespace HealthTracker.Core
{
    /// <summary>
    /// The body mass index (BMI) or Quetelet index
    /// </summary>
    public class BodyMassIndex
    {
        public double CalcBMI(double weight, int height)
            => weight / Math.Pow(height / 100.0, 2);

        public WeightCategory? RateBMI(double bmi)
        {
            if (bmi < 18.5d) return WeightCategory.Underweight;
            else if (bmi <= 24.9d) return WeightCategory.Normal;
            else if (bmi <= 29.9d) return WeightCategory.Overweight;
            else if (bmi > 29.9d) return WeightCategory.Obese;
            return null;
        }
    }
}