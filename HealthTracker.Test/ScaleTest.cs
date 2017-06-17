using HealthTracker.Core;
using NUnit.Framework;
using System.Linq;

namespace HealthTracker.Test
{
    [TestFixture]
    public class ScaleTest
    {
        private static readonly string[] testInputs =
        {
            "02A109FF031AFFFF80070A2178AA",
            "02A109FF031A025186070A21D3AA",
            "02A109FF031AFFFF80070A2178AA",
            "02A109FF031EFFFF80070A217CAA",
            "02A109FF031EFFFF82070A217EAA",
            "02A109FF006AFFFF80070A21C5AA",
            "02A109FF0057FFFF80070A21B2AA",
            "02A109FF003EFFFF80070A2199AA",
            "02A109FF0048FFFF80070A21A3AA",
            "02A109FF0045FFFF80070A21A0AA",
            "02A109FF0093FFFF80070A21EEAA",
            "02A109FF0105FFFF80070A2161AA",
            "02A109FF017DFFFF80070A21D9AA",
            "02A109FF033FFFFF80070A219DAA",
            "02A109FF0386FFFF80070A21E4AA",
            "02A109FF0392FFFF80070A21F0AA",
            "02A109FF0389FFFF80070A21E7AA",
            "02A109FF0393FFFF80070A21F1AA",
            "02A109FF038EFFFF80070A21ECAA",
            "02A109FF038EFFFF82070A21EEAA",
            "02A109FF0000FFFF80070A215BAA",
            "02A109FF005EFFFF80070A21B9AA",
            "02A109FF0069FFFF80070A21C4AA",
            "02A109FF0062FFFF80070A21BDAA",
            "02A109FF0068FFFF80070A21C3AA",
            "02A109FF0053FFFF80070A21AEAA",
            "02A109FF0035FFFF80070A2190AA",
            "02A109FF001EFFFF80070A2179AA",
            "02A109FF004CFFFF80070A21A7AA",
            "02A109FF0057FFFF80070A21B2AA",
            "02A109FF0090FFFF80070A21EBAA",
            "02A109FF017BFFFF80070A21D7AA",
            "02A109FF030DFFFF80070A216BAA",
            "02A109FF037CFFFF80070A21DAAA",
            "02A109FF038E028086070A2176AA"
        };

        private static readonly string[] testOutputs =
        {
            "79,4|T|kg",
            "79,4|F|kg ; 593|F|ohm",
            "79,4|T|kg",
            "79,8|T|kg",
            "79,8|U|kg",
            "10,6|T|kg",
            "8,7|T|kg",
            "6,2|T|kg",
            "7,2|T|kg",
            "6,9|T|kg",
            "14,7|T|kg",
            "26,1|T|kg",
            "38,1|T|kg",
            "83,1|T|kg",
            "90,2|T|kg",
            "91,4|T|kg",
            "90,5|T|kg",
            "91,5|T|kg",
            "91|T|kg",
            "91|U|kg",
            "0|T|kg",
            "9,4|T|kg",
            "10,5|T|kg",
            "9,8|T|kg",
            "10,4|T|kg",
            "8,3|T|kg",
            "5,3|T|kg",
            "3|T|kg",
            "7,6|T|kg",
            "8,7|T|kg",
            "14,4|T|kg",
            "37,9|T|kg",
            "78,1|T|kg",
            "89,2|T|kg",
            "91|F|kg ; 640|F|ohm"
        };

        private readonly ScaleReader reader = new ScaleReader();

        [Test]
        public void ShouldReturnNiceWeightsAndSuch()
        {
            Assert.IsTrue(testInputs.Length == testOutputs.Length, testOutputs.Length + " ?!");
            for (var i = 0; i < testInputs.Length; i++)
            {
                var testInput = testInputs[i];
                var testOutput = testOutputs[i];
                Assert.AreEqual(testOutput, GetResult(testInput));
            }
        }

        private string GetResult(string hex) => string.Join(" ; ", reader.Read(hex.ToBytes())
            .Select(d => $"{d.Data}|{d.Status.ToString().First()}|{d.Unit}"));
    }
}