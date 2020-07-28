using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace BE
{
    public class Order : INotifyPropertyChanged
    {
        private int hostingUnitKey;
        private int guestRequestKey;
        private int orderKey;
        private StatusOrder status;
        private DateTime createDate;
        private int bookingFee;
        public int HostingUnitKey
        {
            get { return hostingUnitKey; }
            set
            {
                hostingUnitKey = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("hostingUnitKey"));
            }
        }
        public int GuestRequestKey
        {
            get { return guestRequestKey; }
            set
            {
                guestRequestKey = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("guestRequestKey"));
            }
        }
        public int OrderKey
        {
            get { return orderKey; }
            set
            {
                orderKey = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("orderKey"));
            }
        }
        public StatusOrder Status
        {
            get { return status; }
            set
            {
                status = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("status"));
            }
        }
        public DateTime CreateDate
        {
            get { return createDate; }
            set
            {
                createDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("createDate"));
            }
        }
        public int BookingFee
        {
            get { return bookingFee; }
            set
            {
                bookingFee = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("bookingFee"));
            }
        }
        public override string ToString()
        {
            return this.OurSmartToString();
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}

