using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public static class DataSource
    {
        public static List<Host>
        Hosts = new List<Host>()
        {
            new Host()
            {
                        HostId = "323075499",
                        PrivateName = "iyar",
                        FamilyName = "avital",
                        PhoneNumber = "055-6893301",
                        MailAdress = "iyaravital@gmail.com",
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = 4,
                            BankName = "Special_Bank",
                            BranchNumber = 416,
                            BranchAddress = "Shomrey hahomot 23",
                            BranchCity = "Modiin",
                        },
                        BankAccountNumber = 1111,
                        CollectionClearance = true,
                        IsInPlace = true,
                        Languages = "Hebrew"
            },
            new Host()
            {
                        HostId = "029529575",
                        PrivateName = "michal",
                        FamilyName = "avital",
                        PhoneNumber = "052-7565551",
                        MailAdress = "michalavital11@gmail.com",
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = 1,
                            BankName = "Bavels_Life",
                            BranchNumber = 124,
                            BranchAddress = "Hertzel & Gertel",
                            BranchCity = "Sinay",
                        },
                        BankAccountNumber = 1123,
                        CollectionClearance = false,
                        IsInPlace = false,
                        Languages = "Spanish"
            }
        };

        public static List<HostingUnit>
        HostingUnits = new List<HostingUnit>()
        {
             new HostingUnit()
             {
                  HostingUnitKey = 10000002,
                  Owner = Hosts[0],
                  HostingUnitName = "vanila",
                  AllhostingUnitdetails = new Details()
                  {
                    Area = AreaINCountry.North,
                    SubArea = "Safsufa",
                    Type = TypeOfUnit.Hotel_room,
                    Adults = 1,
                    Children = 1,
                    Pool = Option.Necessary,
                    Jacuzzi = Option.Necessary,
                    Garden = Option.Necessary,
                    ChildrensAttractions = Option.Necessary,
                    IsWifi = true,
                    IsAirConditioner = true
                  },
                  ParkingPrice = 75,
                  Diary = new bool[12,31]
             },
             new HostingUnit()
             {
                  HostingUnitKey = 10000004,
                  Owner = Hosts[1],
                  HostingUnitName = "Holy_Room",
                  AllhostingUnitdetails = new Details()
                  {
                       Area = AreaINCountry.North,
                       SubArea = "Safsufa",
                       Type = TypeOfUnit.Hotel_room,
                       Adults = 1,
                       Children = 1,
                       Pool = Option.Necessary,
                       Jacuzzi = Option.Necessary,
                       Garden = Option.Necessary,
                       ChildrensAttractions = Option.Necessary,
                       IsAirConditioner = false,
                       IsWifi = true
                  },
                  ParkingPrice = 0,
                  Diary = new bool[12,31]
             },
             new HostingUnit()
             {
                  HostingUnitKey = 10000032,
                  Owner = Hosts[0],
                  HostingUnitName = "popol",
                  AllhostingUnitdetails = new Details()
                  {
                    Area = AreaINCountry.Center,
                    SubArea = "Safsufa",
                    Type = TypeOfUnit.Zimer,
                    Adults = 13,
                    Children = 1,
                    Pool = Option.Necessary,
                    Jacuzzi = Option.Necessary,
                    Garden = Option.Necessary,
                    ChildrensAttractions = Option.Necessary,
                    IsWifi = true,
                    IsAirConditioner = true
                  },
                  ParkingPrice = 75,
                  Diary = new bool[12,31]
             },
             new HostingUnit()
             {
                  HostingUnitKey = 10000019,
                  Owner = Hosts[0],
                  HostingUnitName = "Looooool",
                  AllhostingUnitdetails = new Details()
                  {
                    Area = AreaINCountry.Center,
                    SubArea = "Safsufa",
                    Type = TypeOfUnit.Encampment,
                    Adults = 1,
                    Children = 4,
                    Pool = Option.Necessary,
                    Jacuzzi = Option.Necessary,
                    Garden = Option.Necessary,
                    ChildrensAttractions = Option.Necessary,
                    IsWifi = true,
                    IsAirConditioner = true
                  },
                  ParkingPrice = 75,
                  Diary = new bool[12,31]
             },
             new HostingUnit()
             {
                  HostingUnitKey = 10000008,
                  Owner = Hosts[0],
                  HostingUnitName = "Babushka",
                  AllhostingUnitdetails = new Details()
                  {
                      Area = AreaINCountry.North,
                      SubArea = "Safsufa",
                      Type = TypeOfUnit.Hotel_room,
                      Adults = 1,
                      Children = 0,
                      Pool = Option.possible,
                      Jacuzzi = Option.uninterested,
                      Garden = Option.Necessary,
                      ChildrensAttractions = Option.uninterested,
                      IsAirConditioner = true,
                      IsWifi = true
                  },
                  ParkingPrice = 5,
                  Diary = new bool[12,31]
             }
        };
        public static List<GuestRequest>
        GuestRequests = new List<GuestRequest>()
        {
            new GuestRequest()
            {
                   GuestRequestKey = 10000007,
                   GuestId = "323075499",
                   Private_Name = "itamar",
                   Family_Name = "Ben Gvir",
                   MailAddress = "st173107@gmail.com",
                   Status = StatusCostumer.Closed_through_the_site,
                   RegistrationDate = DateTime.Now,
                   EntryDate = new DateTime(2020,3,2),
                   ReleaseDate = new DateTime(2020,3,8),
                   Allguestrequestdetails = new Details()
                   {
                        Area = AreaINCountry.All,
                        SubArea = "don't care",
                        Type = TypeOfUnit.Hotel_room,
                        Adults = 2,
                        Children = 0,
                        Pool = Option.possible,
                        Jacuzzi = Option.uninterested,
                        Garden = Option.Necessary,
                        ChildrensAttractions = Option.uninterested,
                        IsWifi = true,
                        IsAirConditioner = true
                   }
            },
            new GuestRequest()
            {
                   GuestRequestKey = 10000004,
                   GuestId = "215012311",
                   Private_Name = "rivka",
                   Family_Name = "zizovi",
                   MailAddress = "zizovirivka@gmail.com",
                   Status = StatusCostumer.Active,
                   RegistrationDate = DateTime.Now,
                   EntryDate = new DateTime(2020,3,2),
                   ReleaseDate = new DateTime(2020,3,8),
                   Allguestrequestdetails = new Details()
                   {
                         Area = AreaINCountry.North,
                         SubArea = "Safsufa",
                         Type = TypeOfUnit.Hotel_room,
                         Adults = 1,
                         Children = 1,
                         Pool = Option.Necessary,
                         Jacuzzi = Option.Necessary,
                         Garden = Option.Necessary,
                         ChildrensAttractions = Option.Necessary,
                          IsWifi = true,
                        IsAirConditioner = false
                   }
            },
            new GuestRequest()
            {
                   GuestRequestKey = 10000012,
                   GuestId = "207781733",
                   Private_Name = "Ayelet",
                   Family_Name = "Shaked",
                   MailAddress = "iyaravital@gmail.com",
                   Status = StatusCostumer.Active,
                   RegistrationDate = DateTime.Now,
                   EntryDate = new DateTime(2020,3,1),
                   ReleaseDate = new DateTime(2020,3,26),
                   Allguestrequestdetails = new Details()
                   {
                         Area = AreaINCountry.North,
                         SubArea = "Safsufa",
                         Type = TypeOfUnit.Hotel_room,
                         Adults = 1,
                         Children = 0,
                         Pool = Option.possible,
                         Jacuzzi = Option.Necessary,
                         Garden = Option.uninterested,
                         ChildrensAttractions = Option.Necessary,
                         IsWifi = true,
                         IsAirConditioner = false
                   }
            }
        };
        public static List<Order>
        Orders = new List<Order>()
        {
             new Order()
             {
                 HostingUnitKey = 10000002,
                 GuestRequestKey = 10000007,
                 OrderKey = 10000078,
                 Status = StatusOrder.Mail_sent,
                 CreateDate = new DateTime (2020,2,12),
                 BookingFee = 12
             },
             new Order()
             {
                 HostingUnitKey = 10000002,
                 GuestRequestKey = 10000012,
                 OrderKey = 10000013,
                 Status = StatusOrder.Irrelevant,
                 CreateDate = new DateTime (2020,2,12),
                 BookingFee = 23
             }
        };
    }
}

