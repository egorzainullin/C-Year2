using System;

namespace CompNet
{
    /// <summary>
    /// Creates computers for more comfortable testing
    /// </summary>
    /// <exception cref="NotImplementedException">This operating system does not support unfortunately</exception>
    public class CompFactory
    {
        public IMachine CreateComp(OS os, bool isInfected)
        {
            switch(os)
            {
                case OS.Linux :
                    return new Computer("Linux", 0.4, isInfected);
                    break;
                case OS.Mac:
                    return new Computer("Mac", 0.2, isInfected);
                    break;
                case OS.Windows:
                    return new Computer("Windows", 0.8, isInfected);
                    break;
                default:
                    throw new NotImplementedException("can't recognize this operating system");
                    break;
            }
        }
    }
}
