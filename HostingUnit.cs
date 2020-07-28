using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BE
{
    public class HostingUnit : INotifyPropertyChanged
    {
        private int hostingUnitKey;
        private Host owner;
        private string hostingUnitName;
        private Details allhostingUnitdetails;
        private int parkingPrice;
        private bool[,] diary;
        List<Uri> pictures;
        public int HostingUnitKey
        {
            get { return hostingUnitKey; }
            set
            {
                hostingUnitKey = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("hostingUnitKey"));
            }
        }
        public Host Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("owner"));
            }
        }
        public string HostingUnitName
        {
            get { return hostingUnitName; }
            set
            {
                hostingUnitName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("hostingUnitName"));
            }
        }
        public Details AllhostingUnitdetails
        {
            get { return allhostingUnitdetails; }
            set
            {
                allhostingUnitdetails = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("allhostingUnitdetails"));
            }
        }
        public int ParkingPrice
        {
            get { return parkingPrice; }
            set
            {
                parkingPrice = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("parkingPrice"));
            }
        }

        [XmlIgnore]
        public List<Uri> Pictures
        {
            get { return pictures; }
            set
            {
                pictures = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("pictures"));
            }
        }

        [XmlIgnore]
        public bool[,] Diary
        {
            get { return diary; }
            set
            {
                diary = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("diary"));
            }
        }

        [XmlArray("Diary")]
        public bool[] myDiary
        {
            get { return diary.Flatten(); }
            set { Diary = value.Expand(12); }
        }

        public bool this[DateTime index]
        {
            get { return diary[index.Month - 1, index.Day - 1]; }
            set { diary[index.Month - 1, index.Day - 1] = value; }
        }
        public override string ToString()
        {
            string str = "";
            str += "HostingUnitKey: " + HostingUnitKey + "\n" +
                   "Owner: " + "\n" + Owner.ToString() +
                   "HostingUnitName: " + HostingUnitName + "\n" +
                   AllhostingUnitdetails.ToString() +
                   "ParkingPrice: " + ParkingPrice + "\n" +
                   "Diary: " + "\n";
            DateTime startDate = new DateTime(2020, 01, 01);
            DateTime endDate = new DateTime();
            DateTime Date = new DateTime(2020, 01, 02);
            while (Date.Year != 2021)
            {
                if (this[Date] && !this[Date.AddDays(-1)])
                {
                    startDate = Date;
                    Date = Date.AddDays(1);
                }
                else if (this[Date] && Date.AddDays(1).Year != Date.Year)
                {
                    endDate = Date;
                    str += startDate.ToString("dd/MM") + "-" + endDate.ToString("dd/MM") + "\n";
                    Date = Date.AddDays(1);
                }
                else if (this[Date] && !this[Date.AddDays(1)])
                {
                    endDate = Date.AddDays(1);
                    str += startDate.ToString("dd/MM") + "-" + endDate.ToString("dd/MM") + "\n";
                    Date = Date.AddDays(1);
                }
                else
                {
                    Date = Date.AddDays(1);
                }
            }
            return str;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public static class Tools
    {
        public static T[] Flatten<T>(this T[,] arr)
        {
            int rows = arr.GetLength(0);  //12
            int columns = arr.GetLength(1); //31
            T[] arrFlattened = new T[rows * columns];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    var test = arr[j, i];
                    arrFlattened[i * rows + j] = arr[j, i];
                }
            }
            return arrFlattened;
        }
        public static T[,] Expand<T>(this T[] arr, int rows)
        {
            int length = arr.GetLength(0);  //12*31
            int columns = length / rows; //31
            T[,] arrExpanded = new T[rows, columns];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    arrExpanded[j, i] = arr[i * rows + j];
                }
            }
            return arrExpanded;
        }
    }

}


