using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Toolbox
{
    public class ListOperations
    {
        public enum AveragingMethod
        {
            CalculateAverageByMean,
            CalculateAverageByMedian
        }

        public double CalculateAverage(List<double> numbers, AveragingMethod method)
        {
            switch (method)
            {
                case AveragingMethod.CalculateAverageByMean:
                    return numbers.Sum() / numbers.Count();

                case AveragingMethod.CalculateAverageByMedian:
                    var sortedNumbers = numbers.OrderBy(x => x).ToList();

                    // Case where there is an odd number of values in list
                    if (sortedNumbers.Count % 2 == 1)
                    {
                        return sortedNumbers[(sortedNumbers.Count() - 1) / 2];
                    }

                    // Case where there is an even number of values in list
                    return (sortedNumbers[sortedNumbers.Count / 2] +
                        sortedNumbers[sortedNumbers.Count / 2 - 1]) / 2;

                default:
                    throw new ArgumentException("Calculate avgerage argument invalid");
            }

        }
    }
}
