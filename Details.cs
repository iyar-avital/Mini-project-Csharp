using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BE
{
    public class Details : INotifyPropertyChanged
    {
        private AreaINCountry area;
        private string subArea;
        private TypeOfUnit type;
        private int adults;
        private int children;
        private Option pool;
        private Option jacuzzi;
        private Option garden;
        private Option childrensAttractions;
        private bool isWifi;
        private bool isAirConditioner;
        public AreaINCountry Area
        {
            get { return area; }
            set
            {
                area = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("area"));
            }
        }
        public string SubArea
        {
            get { return subArea; }
            set
            {
                subArea = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("subArea"));
            }
        }
        public TypeOfUnit Type
        {
            get { return type; }
            set
            {
                type = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("type"));
            }
        }
        public int Adults
        {
            get { return adults; }
            set
            {
                adults = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("adults"));
            }
        }
        public int Children
        {
            get { return children; }
            set
            {
                children = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("children"));
            }
        }
        public Option Pool
        {
            get { return pool; }
            set
            {
                pool = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("pool"));
            }
        }
        public Option Jacuzzi
        {
            get { return jacuzzi; }
            set
            {
                jacuzzi = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("jacuzzi"));
            }
        }
        public Option Garden
        {
            get { return garden; }
            set
            {
                garden = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("garden"));
            }
        }
        public Option ChildrensAttractions
        {
            get { return childrensAttractions; }
            set
            {
                childrensAttractions = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("childrensAttractions"));
            }
        }
        public bool IsWifi
        {
            get { return isWifi; }
            set
            {
                isWifi = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("isWifi"));
            }
        }
        public bool IsAirConditioner
        {
            get { return isAirConditioner; }
            set
            {
                isAirConditioner = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("isAirConditioner"));
            }
        }
        public override string ToString()
        {
            return this.OurSmartToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
