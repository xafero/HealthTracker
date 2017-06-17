using System;

namespace HealthTracker.Core
{
    /// <summary>
    /// Lean Body Mass Formula for Adults
    /// </summary>
    public class LeanBodyMass
    {
        /// <summary>
        /// The Boer Formula 1984
        /// </summary>
        public double CalcLBM(Sex sex, double weight, int height)
        {
            if (sex == Sex.Male)
                return 0.407 * weight + 0.267 * height - 19.2;
            else
                return 0.252 * weight + 0.473 * height - 48.3;
        }


        /// <summary>
        /// The James Formula 2009
        /// </summary>
        public double CalcLBM2(Sex sex, double weight, int height)
        {
            if (sex == Sex.Male)
                return 1.1 * weight - 128 * Math.Pow(weight / height, 2);
            else
                return 1.07 * weight - 148 * Math.Pow(weight / height, 2);
        }


        /// <summary>
        /// The Hume Formula 1966
        /// </summary>
        public double CalcLBM3(Sex sex, double weight, int height)
        {
            if (sex == Sex.Male)
                return 0.32810 * weight + 0.33929 * height - 29.5336;
            else
                return 0.29569 * weight + 0.41813 * height - 43.2933;
        }
    }
}