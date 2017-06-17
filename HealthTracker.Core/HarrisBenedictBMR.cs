using System;

namespace HealthTracker.Core
{
    /// <summary>
    /// Harris–Benedict equations published in 1918 and 1919
    /// </summary>
    public class HarrisBenedictBMR : IBasalMetaCalc
    {
        public double CalcBMR(Sex sex, double weight, int height, int age)
        {
            if (sex == Sex.Male)
                return Math.Round(66.5 + (weight * 13.75) + (height * 5.003) - (age * 6.755));
            else
                return Math.Round(655.1 + (weight * 9.563) + (height * 1.850) - (age * 4.676));
        }
    }
}