using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompNet
{
    public class FakeRandom : Random
    { 
        public override double NextDouble()
        {
            return base.NextDouble();
        }
    }
}
