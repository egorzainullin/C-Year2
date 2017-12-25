using System;

namespace CompNet
{
    /// <summary>
    /// Creates computers for more comfortable testing
    /// </summary>
    /// <exception cref="NotImplementedException">This operating system is not support unfortunately</exception>
    public class CompFactory
    {
        /// <summary>
        /// Creates a computer with given parameters 
        /// </summary>
        /// <param name="os">Type of OS</param>
        /// <param name="isInfected">Is this computer has been infected</param>
        /// <returns>Computer with this parameters</returns>
        public IMachine CreateComp(OS os, bool isInfected)
        {
            switch (os)
            {
                case OS.Linux:
                    return new Computer("Linux", 0.4, isInfected);
                case OS.Mac:
                    return new Computer("Mac", 0.2, isInfected);
                case OS.Windows:
                    return new Computer("Windows", 0.8, isInfected);
                default:
                    throw new NotImplementedException("can't recognize this operating system");
            }
        }
    }
}
