using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace BE
{
    public class Host : INotifyPropertyChanged
    {
        private string hostId;
        private string privateName;
        private string familyName;
        private string phoneNumber;
        private string mailAddress;
        private BankBranch bankBranchDetails;
        private int bankAccountNumber;
        private bool collectionClearance;
        private string languages;
        private bool isInPlace;
        public string HostId
        {
            get { return hostId; }
            set
            {
                hostId = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("hostId"));
            }
        }
        public string PrivateName
        {
            get { return privateName; }
            set
            {
                privateName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("privateName"));
            }
        }
        public string FamilyName
        {
            get { return familyName; }
            set
            {
                familyName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("familyName"));
            }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("phoneNumber"));
            }
        }
        public string MailAdress
        {
            get { return mailAddress; }
            set
            {
                mailAddress = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("mailAddress"));
            }
        }
        public BankBranch BankBranchDetails
        {
            get { return bankBranchDetails; } set { bankBranchDetails = value; }
        }
        public int BankAccountNumber
        {
            get { return bankAccountNumber; }
            set
            {
                bankAccountNumber = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("bankAccountNumber"));
                }
            }
        }
        public bool CollectionClearance
        {
            get { return collectionClearance; }
            set
            {
                collectionClearance = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("collectionClearance"));
            }
        }
        public string Languages
        {
            get { return languages; }
            set
            {
                languages = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("languages"));
            }
        }
        public bool IsInPlace
        {
            get { return isInPlace; }
            set
            {
                isInPlace = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("isInPlace"));
            }
        }
        public override string ToString()
        {
            return this.OurSmartToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
