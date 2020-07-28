using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLWPF
{

    enum HostOrGuest
    {
        host, guest
    }

    class ValueUserControl : EventArgs
    {
        public HostOrGuest host_guest;
        public ValueUserControl(HostOrGuest hg)
        {
            host_guest = hg;
        }
    }
}
