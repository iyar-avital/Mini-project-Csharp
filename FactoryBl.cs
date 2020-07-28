using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FactoryBl
    {
        private static IBL bl;
        private FactoryBl() { }
        public static IBL GetBl()
        {
            if (bl == null)
                bl = new BlClass();
            return bl;
        }
    }
}
