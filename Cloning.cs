using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public static class Cloning
    {
        public static Host Clone(this Host original)
        {
            Host host = new Host();
            host.HostId = original.HostId;
            host.PrivateName = original.PrivateName;
            host.FamilyName = original.FamilyName;
            host.PhoneNumber = original.PhoneNumber;
            host.MailAdress = original.MailAdress;
            host.BankBranchDetails = original.BankBranchDetails.Clone();
            host.BankAccountNumber = original.BankAccountNumber;
            host.CollectionClearance = original.CollectionClearance;
            host.Languages = original.Languages;
            host.IsInPlace = original.IsInPlace;
            return host;
        }

        public static BankBranch Clone(this BankBranch original)
        {
            BankBranch bankBranch = new BankBranch();
            bankBranch.BankNumber = original.BankNumber;
            bankBranch.BankName = original.BankName;
            bankBranch.BranchNumber = original.BranchNumber;
            bankBranch.BranchAddress = original.BranchAddress;
            bankBranch.BranchCity = original.BranchCity;
            return bankBranch;
        }

        public static Order Clone(this Order original)
        {
            Order order = new Order();
            order.HostingUnitKey = original.HostingUnitKey;
            order.GuestRequestKey = original.GuestRequestKey;
            order.OrderKey = original.OrderKey;
            order.Status = original.Status;
            order.CreateDate = original.CreateDate;
            order.BookingFee = original.BookingFee;
            return order;
        }

        public static GuestRequest Clone(this GuestRequest original)
        {
            GuestRequest guestRequest = new GuestRequest();
            guestRequest.GuestRequestKey = original.GuestRequestKey;
            guestRequest.GuestId = original.GuestId;
            guestRequest.Private_Name = original.Private_Name;
            guestRequest.Family_Name = original.Family_Name;
            guestRequest.MailAddress = original.MailAddress;
            guestRequest.Status = original.Status;
            guestRequest.RegistrationDate = original.RegistrationDate;
            guestRequest.EntryDate = original.EntryDate;
            guestRequest.ReleaseDate = original.ReleaseDate;
            guestRequest.Allguestrequestdetails = original.Allguestrequestdetails.Clone();
            return guestRequest;
        }

        public static HostingUnit Clone(this HostingUnit original)
        {
            HostingUnit hostingUnit = new HostingUnit();
            hostingUnit.HostingUnitKey = original.HostingUnitKey;
            hostingUnit.Owner = original.Owner;
            hostingUnit.HostingUnitName = original.HostingUnitName;
            hostingUnit.AllhostingUnitdetails = original.AllhostingUnitdetails.Clone();
            hostingUnit.ParkingPrice = original.ParkingPrice;
            hostingUnit.Pictures = original.Pictures;
            hostingUnit.Diary = new bool[12, 31];
            for (int i = 0; i < hostingUnit.Diary.GetLength(0); i++)
                for (int j = 0; j < hostingUnit.Diary.GetLength(1); j++)
                    hostingUnit.Diary[i, j] = original.Diary[i, j];
            return hostingUnit;
        }

        public static Details Clone(this Details original)
        {
            Details details = new Details();
            details.Area = original.Area;
            details.SubArea = original.SubArea;
            details.Type = original.Type;
            details.Adults = original.Adults;
            details.Children = original.Children;
            details.Pool = original.Pool;
            details.Jacuzzi = original.Jacuzzi;
            details.Garden = original.Garden;
            details.ChildrensAttractions = original.ChildrensAttractions;
            details.IsWifi = original.IsWifi;
            details.IsAirConditioner = original.IsAirConditioner;
            return details;
        }
    }
}
