using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;

namespace BE
{
    public class BankBranch : INotifyPropertyChanged, IComparable<BankBranch>
    {
        private int bankNumber;
        private string bankName;
        private int branchNumber;
        private string branchAddress;
        private string branchCity;
        public int BankNumber
        {
            get { return bankNumber; }
            set
            {
                bankNumber = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("bankNumber"));
            }
        }
        public string BankName
        {
            get { return bankName; }
            set
            {
                bankName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("bankName"));
            }
        }
        public int BranchNumber
        {
            get { return branchNumber; }
            set
            {
                branchNumber = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("branchNumber"));
            }
        }
        public string BranchAddress
        {
            get { return branchAddress; }
            set
            {
                branchAddress = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("branchAddress"));
            }
        }
        public string BranchCity
        {
            get { return branchCity; }
            set
            {
                branchCity = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("branchCity"));
            }
        }
        public override string ToString()
        {
            return "\n" + this.OurSmartToString();
        }

        public int CompareTo(BankBranch other)
        {
            if (BranchNumber.CompareTo(other.BranchNumber) == 0 && bankNumber.CompareTo(other.bankNumber) == 0)
                return 0;
            else
                return 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

