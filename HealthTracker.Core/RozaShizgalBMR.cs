using System;

namespace HealthTracker.Core
{
    /// <summary>
    /// Harris–Benedict equations revised by Roza and Shizgal in 1984
    /// </summary>
    public class RozaShizgalBMR : IBasalMetaCalc
    {
        public double CalcBMR(Sex sex, double weight, int height, int age)
        {
            if (sex == Sex.Male)
                return Math.Round(88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age));
            else
                return Math.Round(447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age));
        }
    }
}