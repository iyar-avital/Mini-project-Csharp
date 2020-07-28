using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace BE
{
    public class GuestRequest : INotifyPropertyChanged
    {
        private int guestRequestKey;
        private string guestId;
        private string private_name;
        private string family_Name;
        private string mailAddress;
        private StatusCostumer status;
        private DateTime registrationDate;
        private DateTime entryDate;
        private DateTime releaseDate;
        private Details allguestrequestdetails;
        public int GuestRequestKey
        {
            get { return guestRequestKey; }
            set
            {
                guestRequestKey = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("guestRequestKey"));
            }
        }
        public string GuestId
        {
            get { return guestId; }
            set
            {
                guestId = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("guestId"));
            }
        }
        public string Private_Name
        {
            get { return private_name; }
            set
            {
                private_name = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("private_name"));
            }
        }
        public string Family_Name
        {
            get { return family_Name; }
            set
            {
                family_Name = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("family_Name"));
            }
        }
        public string MailAddress
        {
            get { return mailAddress; }
            set
            {
                mailAddress = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("mailAddress"));
            }
        }
        public StatusCostumer Status
        {
            get { return status; }
            set
            {
                status = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("status"));
            }
        }
        public DateTime RegistrationDate
        {
            get { return registrationDate; }
            set
            {
                registrationDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("registrationDate"));
            }
        }
        public DateTime EntryDate
        {
            get { return entryDate; }
            set
            {
                entryDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("entryDate"));
            }
        }
        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set
            {
                releaseDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("releaseDate"));
            }
        }
        public Details Allguestrequestdetails
        {
            get { return allguestrequestdetails; }
            set
            {
                allguestrequestdetails = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("allguestrequestdetails"));
            }
        }
        public override string ToString()
        {
            return this.OurSmartToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}

