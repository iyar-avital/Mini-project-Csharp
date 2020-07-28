using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        private static IDAL dl;
        private FactoryDal() { }
        public static IDAL GetDal()
        {
            if (dl == null)
                dl = new Dal_XML_imp();
            return dl;
        }
    }
}
