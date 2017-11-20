using System;
namespace CompNet
{
    /// <summary>
    /// This class emulates real random for better testing
    /// </summary>
    public class FakeRandom : Random
    {
        private double[] values = { 0.5 };

        /// <summary>
        /// Set fake random values into next double method
        /// </summary>
        /// <exception cref="ArgumentException">It should be not null array</exception>
        public void SetArrayOfFakeRandomValues(double[] array)
        {
            if (array.Length == 0)
            {
                throw new ArgumentException("this array does not have elements");
            }
            values = array;
        }
        
        /// <summary>
        /// Count for element of values to output
        /// </summary>
        private int counter;

        /// <summary>
        /// Overrides NextDouble and gives fake random values
        /// </summary>
        /// <returns></returns>
        public override double NextDouble() => (values[counter++ % values.Length]);
    }
}
