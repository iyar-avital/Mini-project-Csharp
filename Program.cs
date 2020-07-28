using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;
using DS;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IBL bl = FactoryBl.GetBl();
            try
            {
                int num;
                do
                {
                    Console.WriteLine("\nDELI-PECAN PROGRAM");
                    Console.WriteLine("\nChoose one of the following");
                    Console.WriteLine("1: add a new item");
                    Console.WriteLine("2: delete an item");
                    Console.WriteLine("3: update an item");
                    Console.WriteLine("4: print all the item");
                    Console.WriteLine("0: exit");
                    num = int.Parse(Console.ReadLine());
                    string id, mail, phone;
                    int ch;
                    DateTime date;
                    bool b;
                    GuestRequest g = new GuestRequest();
                    g.Allguestrequestdetails = new Details();
                    HostingUnit h = new HostingUnit();
                    h.Owner = new Host();
                    h.AllhostingUnitdetails = new Details();
                    h.Diary = new bool[12, 31];
                    Order o = new Order();
                    switch (num)
                    {
                        case 1:
                            {
                                Console.WriteLine("1: add guest request");
                                Console.WriteLine("2: add hosting unit");
                                Console.WriteLine("3: add order");
                                int choice = int.Parse(Console.ReadLine());
                                switch (choice)
                                {
                                    case 1:
                                        {
                                            Console.WriteLine("please enter all guest reqest details:");
                                            Console.WriteLine("guest Id:");
                                            id = Console.ReadLine();
                                            if (id.Length != 9)
                                                throw new HostingException("id number is incorrect", 0, "UI");
                                            g.GuestId = id;

                                            Console.WriteLine("private name:");
                                            g.Private_Name = Console.ReadLine();

                                            Console.WriteLine("family name:");
                                            g.Family_Name = Console.ReadLine();

                                            Console.WriteLine("mail address:");
                                            mail = Console.ReadLine();
                                            if (mail.Contains("@") != true || mail.Substring(mail.IndexOf("@") + 1).Contains(".") != true)
                                                throw new HostingException("Email address is incorrect", 0, "UI");
                                            g.MailAddress = mail;

                                            Console.WriteLine("entry date:");
                                            b = DateTime.TryParse(Console.ReadLine(), out date);
                                            if (b == false)
                                                throw new HostingException("your entry date is incorrect", 0, "UI");
                                            g.EntryDate = date;

                                            Console.WriteLine("release date:");
                                            b = DateTime.TryParse(Console.ReadLine(), out date);
                                            if (b == false)
                                                throw new HostingException("your release date is incorrect", 0, "UI");
                                            g.ReleaseDate = date;

                                            Console.WriteLine("area: all- 0 / North- 1 / South- 2 / Center- 3");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 3)
                                                throw new HostingException("your area is incorrect", 0, "UI");
                                            g.Allguestrequestdetails.Area = (AreaINCountry)ch;

                                            Console.WriteLine("type: Hotel room- 0 / Zimer- 1 / Encampment- 2 / Apartment- 3 / Sleeping_bag_on_the_sea- 4");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 4)
                                                throw new HostingException("your type is incorrect", 0, "UI");
                                            g.Allguestrequestdetails.Type = (TypeOfUnit)ch;

                                            Console.WriteLine("num of adults:");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0)
                                                throw new HostingException("number of people c'ant be a negative", 0, "UI");
                                            g.Allguestrequestdetails.Adults = ch;

                                            Console.WriteLine("num of children:");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0)
                                                throw new HostingException("number of people c'ant be a negative", 0, "UI");
                                            g.Allguestrequestdetails.Children = ch;

                                            Console.WriteLine("pool: Necessary- 0 / possible-  1 / uninterested- 2");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 2)
                                                throw new HostingException("your pool option is incorrect", 0, "UI");
                                            g.Allguestrequestdetails.Pool = (Option)ch;

                                            Console.WriteLine("jaccuzi: Necessary- 0 / possible-  1 / uninterested- 2");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 2)
                                                throw new HostingException("your jaccuzi option is incorrect", 0, "UI");
                                            g.Allguestrequestdetails.Jacuzzi = (Option)ch;

                                            Console.WriteLine("garden: Necessary- 0 / possible-  1 / uninterested- 2");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 2)
                                                throw new HostingException("your garden option is incorrect", 0, "UI");
                                            g.Allguestrequestdetails.Garden = (Option)ch;

                                            Console.WriteLine("childrens Attractions: Necessary- 0 / possible-  1 / uninterested- 2");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 2)
                                                throw new HostingException("your childrensAttractions option is incorrect", 0, "UI");
                                            g.Allguestrequestdetails.ChildrensAttractions = (Option)ch;

                                            Console.WriteLine("is WiFi");
                                            g.Allguestrequestdetails.IsWifi = bool.Parse(Console.ReadLine());

                                            Console.WriteLine("is AirConditioner");
                                            g.Allguestrequestdetails.IsAirConditioner = bool.Parse(Console.ReadLine());
                                            bl.addGuestRequest(g);
                                        }
                                        break;
                                    case 2:
                                        {
                                            Console.WriteLine("host id:");
                                            id = Console.ReadLine();
                                            if (id.Length != 9)
                                                throw new HostingException("id number is incorrect", 0, "UI");
                                            h.Owner.HostId = id;

                                            Console.WriteLine("private name:");
                                            h.Owner.PrivateName = Console.ReadLine();

                                            Console.WriteLine("family name:");
                                            h.Owner.FamilyName = Console.ReadLine();

                                            Console.WriteLine("phone Number:");
                                            phone = Console.ReadLine();
                                            if (phone.Length != 10 && phone.Length != 11)
                                                throw new HostingException("phone number is incorrect", 0, "UI");
                                            h.Owner.PhoneNumber = phone;

                                            Console.WriteLine("mail address:");
                                            mail = Console.ReadLine();
                                            if (mail.Contains("@") != true || mail.Substring(mail.IndexOf("@") + 1).Contains(".") != true)
                                                throw new HostingException("Email address is incorrect", 0, "UI");
                                            h.Owner.MailAdress = mail;

                                            Console.WriteLine("collection clearance: ");
                                            h.Owner.CollectionClearance = bool.Parse(Console.ReadLine());

                                            Console.WriteLine("languages: ");
                                            h.Owner.Languages = Console.ReadLine();

                                            Console.WriteLine("is In Place: ");
                                            h.Owner.IsInPlace = bool.Parse(Console.ReadLine());

                                            Console.WriteLine("hosting Unit Name: ");
                                            h.HostingUnitName = Console.ReadLine();

                                            Console.WriteLine("area: all- 0 / North- 1 / South- 2 / Center- 3");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 3)
                                                throw new HostingException("your area is incorrect", 0, "UI");
                                            h.AllhostingUnitdetails.Area = (AreaINCountry)ch;

                                            Console.WriteLine("enter your sub area:");
                                            h.AllhostingUnitdetails.SubArea = Console.ReadLine();

                                            Console.WriteLine("type: Hotel room- 0 / Zimer- 1 / Encampment- 2 / Apartment- 3 / Sleeping_bag_on_the_sea- 4");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 4)
                                                throw new HostingException("your type is incorrect", 0, "UI");
                                            h.AllhostingUnitdetails.Type = (TypeOfUnit)ch;

                                            Console.WriteLine("num of adults:");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0)
                                                throw new HostingException("number of people c'ant be a negative", 0, "UI");
                                            h.AllhostingUnitdetails.Adults = ch;

                                            Console.WriteLine("num of children:");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0)
                                                throw new HostingException("number of people c'ant be a negative", 0, "UI");
                                            h.AllhostingUnitdetails.Children = ch;

                                            Console.WriteLine("pool: there is- 0 / there is'nt-  1");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 2)
                                                throw new HostingException("your pool option is incorrect", 0, "UI");
                                            h.AllhostingUnitdetails.Pool = (Option)ch;

                                            Console.WriteLine("jaccuzi: there is- 0 / there is'nt-  1");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 2)
                                                throw new HostingException("your jaccuzi option is incorrect", 0, "UI");
                                            h.AllhostingUnitdetails.Jacuzzi = (Option)ch;

                                            Console.WriteLine("garden: there is- 0 / there is'nt-  1");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 2)
                                                throw new HostingException("your garden option is incorrect", 0, "UI");
                                            h.AllhostingUnitdetails.Garden = (Option)ch;

                                            Console.WriteLine("childrens Attractions: there is- 0 / there is'nt-  1");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 2)
                                                throw new HostingException("your childrensAttractions option is incorrect", 0, "UI");
                                            h.AllhostingUnitdetails.ChildrensAttractions = (Option)ch;

                                            Console.WriteLine("is WiFi");
                                            h.AllhostingUnitdetails.IsWifi = bool.Parse(Console.ReadLine());

                                            Console.WriteLine("is AirConditioner");
                                            h.AllhostingUnitdetails.IsAirConditioner = bool.Parse(Console.ReadLine());

                                            Console.WriteLine("parking Price: ");
                                            h.ParkingPrice = int.Parse(Console.ReadLine());

                                            bl.addHostingUnit(h);
                                        }
                                        break;
                                    case 3:
                                        {
                                            Console.WriteLine("guest request key:");
                                            o.GuestRequestKey = int.Parse(Console.ReadLine());

                                            Console.WriteLine("hosting unit key:");
                                            o.HostingUnitKey = int.Parse(Console.ReadLine());
                                            bl.addOrder(o);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case 2:
                            {
                                Console.WriteLine("you can delete just hosting unit:)");
                                Console.WriteLine("enter the hosting unit key of who you want delete");
                                ch = int.Parse(Console.ReadLine());
                                h.HostingUnitKey = ch;
                                bl.deleteHostingUnit(h);
                            }
                            break;
                        case 3:
                            {
                                Console.WriteLine("1: update guest request");
                                Console.WriteLine("2: update hosting unit");
                                Console.WriteLine("3: update order");
                                int choice = int.Parse(Console.ReadLine());
                                switch (choice)
                                {
                                    case 1:
                                        {
                                            Console.WriteLine("enter the guest request key of who you want change its status");
                                            ch = int.Parse(Console.ReadLine());
                                            g.GuestRequestKey = ch;
                                            Console.WriteLine("enter the new status");
                                            StatusCostumer statu = (StatusCostumer)int.Parse(Console.ReadLine());
                                            bl.updateGuestRequest(g, statu);
                                        }
                                        break;
                                    case 2:
                                        {
                                            h.Owner = new Host();
                                            h.AllhostingUnitdetails = new Details();
                                            h.Diary = new bool[12, 31];

                                            Console.WriteLine("hosting unit key you want update:");
                                            h.HostingUnitKey = int.Parse(Console.ReadLine());

                                            Console.WriteLine("host id:");
                                            id = Console.ReadLine();
                                            if (id.Length != 9)
                                                throw new HostingException("id number is incorrect", 0, "UI");
                                            h.Owner.HostId = id;

                                            Console.WriteLine("private name:");
                                            h.Owner.PrivateName = Console.ReadLine();

                                            Console.WriteLine("family name");
                                            h.Owner.FamilyName = Console.ReadLine();

                                            Console.WriteLine("phone Number:");
                                            phone = Console.ReadLine();
                                            if (phone.Length != 10 && phone.Length != 11)
                                                throw new HostingException("phone number is incorrect", 0, "UI");
                                            h.Owner.PhoneNumber = phone;

                                            Console.WriteLine("mail address:");
                                            mail = Console.ReadLine();
                                            if (mail.Contains("@") != true || mail.Substring(mail.IndexOf("@") + 1).Contains(".") != true)
                                                throw new HostingException("Email address is incorrect", 0, "UI");
                                            h.Owner.MailAdress = mail;

                                            Console.WriteLine("collection clearance: ");
                                            h.Owner.CollectionClearance = bool.Parse(Console.ReadLine());

                                            Console.WriteLine("languages: ");
                                            h.Owner.Languages = Console.ReadLine();

                                            Console.WriteLine("is In Place: ");
                                            h.Owner.IsInPlace = bool.Parse(Console.ReadLine());

                                            Console.WriteLine("hosting Unit Name: ");
                                            h.HostingUnitName = Console.ReadLine();

                                            Console.WriteLine("area: all- 0 / North- 1 / South- 2 / Center- 3");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 3)
                                                throw new HostingException("your area is incorrect", 0, "UI");
                                            h.AllhostingUnitdetails.Area = (AreaINCountry)ch;

                                            Console.WriteLine("enter your sub area:");
                                            h.AllhostingUnitdetails.SubArea = Console.ReadLine();

                                            Console.WriteLine("type: Hotel room- 0 / Zimer- 1 / Encampment- 2 / Apartment- 3 / Sleeping_bag_on_the_sea- 4");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 4)
                                                throw new HostingException("your type is incorrect", 0, "UI");
                                            h.AllhostingUnitdetails.Type = (TypeOfUnit)ch;

                                            Console.WriteLine("num of adults:");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0)
                                                throw new HostingException("number of people c'ant be a negative", 0, "UI");
                                            h.AllhostingUnitdetails.Adults = ch;

                                            Console.WriteLine("num of children:");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0)
                                                throw new HostingException("number of people c'ant be a negative", 0, "UI");
                                            h.AllhostingUnitdetails.Children = ch;

                                            Console.WriteLine("pool: there is- 0 / there is'nt-  1");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 2)
                                                throw new HostingException("your pool option is incorrect", 0, "UI");
                                            h.AllhostingUnitdetails.Pool = (Option)ch;

                                            Console.WriteLine("jaccuzi: there is- 0 / there is'nt-  1");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 2)
                                                throw new HostingException("your jaccuzi option is incorrect", 0, "UI");
                                            h.AllhostingUnitdetails.Jacuzzi = (Option)ch;

                                            Console.WriteLine("garden: there is- 0 / there is'nt-  1");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 2)
                                                throw new HostingException("your garden option is incorrect", 0, "UI");
                                            h.AllhostingUnitdetails.Garden = (Option)ch;

                                            Console.WriteLine("childrensAttractions: there is- 0 / there is'nt-  1");
                                            ch = int.Parse(Console.ReadLine());
                                            if (ch < 0 || ch > 2)
                                                throw new HostingException("your childrensAttractions option is incorrect", 0, "UI");
                                            h.AllhostingUnitdetails.ChildrensAttractions = (Option)ch;

                                            Console.WriteLine("is WiFi");
                                            h.AllhostingUnitdetails.IsWifi = bool.Parse(Console.ReadLine());

                                            Console.WriteLine("is AirConditioner");
                                            h.AllhostingUnitdetails.IsAirConditioner = bool.Parse(Console.ReadLine());

                                            Console.WriteLine("parking Price: ");
                                            h.ParkingPrice = int.Parse(Console.ReadLine());

                                            bl.updateHostingUnit(h);
                                        }
                                        break;
                                    case 3:
                                        {
                                            Console.WriteLine("enter the order key of who you want change its status");
                                            ch = int.Parse(Console.ReadLine());
                                            List<Order> oo = bl.getOrders(item => item.OrderKey == ch);
                                            Console.WriteLine("enter the new status");
                                            StatusOrder statu = (StatusOrder)int.Parse(Console.ReadLine());
                                            bl.updateOrder(oo[0], statu);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case 4:
                            {
                                Console.WriteLine("1: prints all guest request");
                                Console.WriteLine("2: prints all hosting unit");
                                Console.WriteLine("3: prints all order");
                                int choice = int.Parse(Console.ReadLine());
                                switch (choice)
                                {
                                    case 1:
                                        {
                                            List<GuestRequest> gs = bl.getGuestRequests();
                                            foreach (var item in gs)
                                            {
                                                Console.WriteLine(item + "\n");
                                            }
                                        }
                                        break;
                                    case 2:
                                        {
                                            List<HostingUnit> ho = bl.getHostingUnits();
                                            foreach (var item in ho)
                                            {
                                                Console.WriteLine(item + "\n");
                                            }
                                        }
                                        break;
                                    case 3:
                                        {
                                            List<Order> or = bl.getOrders();
                                            foreach (var item in or)
                                            {
                                                Console.WriteLine(item + "\n");
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                } while (num != 0);
            }
            catch(HostingException h)
            {
                Console.WriteLine("{0} {1} {2}", h.Message, h.nameObject, h.key);
            }
            Console.ReadKey();
        }
    }
}
