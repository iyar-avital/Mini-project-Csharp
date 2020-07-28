using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Net.Mime;
using System.Threading;

namespace BL
{
    class BlClass : IBL
    {
        IDAL dl = FactoryDal.GetDal();
        private void MoreThanMonth()
        {
            if ((DateTime.Now - dl.getLastDate()).Days == 0)
                return;
            dl.setLastDate(DateTime.Now);
            List<Order> orders = dl.getOrders(item=> item.Status == StatusOrder.Mail_sent);
            foreach (var item in orders)
            {
                if ((DateTime.Now - item.CreateDate).Days >= 30)
                    dl.updateOrder(item, StatusOrder.Closes_out_of_customer_disrespect);
            }
            Thread.Sleep(86400000);
        }

        public void MonthThread()
        {
            Thread t = new Thread(MoreThanMonth);
            t.Start();
        }

        #region GuestRequest

        public void addGuestRequest(GuestRequest guest)
        {
            if (guest.ReleaseDate <= guest.EntryDate)
                throw new HostingException("you cant come for only one day or less than, check your dates", guest.GuestRequestKey, "BL");
            if (guest.EntryDate < DateTime.Now)
                throw new HostingException("Error on date", guest.GuestRequestKey, "BL");
            //if (CheckDigit(guest.GuestId) == false)
            //    throw new HostingException("id number is incorrect", int.Parse(guest.GuestId), "BL");
            try
            {
                dl.addGuestRequest(guest.Clone());
            }
            catch (HostingException h)
            {
                throw h;
            }
        }

        public void updateGuestRequest(GuestRequest guest, StatusCostumer statusCostumer)
        {
            try
            {
                dl.updateGuestRequest(guest.Clone(), statusCostumer);
            }
            catch (HostingException h)
            {
                throw h;
            }
        }

        public int numOfOrdersWhichSent(GuestRequest guest)
        {
            var v = dl.getOrders(item => item.GuestRequestKey == guest.GuestRequestKey);
            return v.Count();
        }

        public List<GuestRequest> getGuestRequests(Func<GuestRequest, bool> predicat = null)
        {
            return dl.getGuestRequests(predicat);
        }
        #endregion

        #region HostingUnit

        //auxiliary function to addHostingunit

        public void addHost(Host host)
        {
            List<Host> h;
            try
            {
                h = dl.getHosts(item => item.HostId == host.HostId);
                if (h.Count() == 0)
                    dl.addHost(host.Clone());
                else
                throw new HostingException("the hoster already exist", int.Parse(host.HostId), "BL");
            }
            catch (HostingException hh)
            {
                throw hh;
            }
        }

        public bool isDownloadFinished()
        {
            return dl.isDownloadFinished();
        }

        public void addHostingUnit(HostingUnit hostingUnit)
        {
            try
            {
                dl.addHostingUnit(hostingUnit.Clone());
            }
            catch (HostingException h)
            {
                throw h;
            }
        }

        public void deleteHostingUnit(HostingUnit hostingUnit)
        {
            List<Order> orders = dl.getOrders(item => item.HostingUnitKey == hostingUnit.HostingUnitKey);
            foreach (var item in orders)
            {
                GuestRequest guest = dl.getOneguestRequest(item.GuestRequestKey);
                if (guest.Status == StatusCostumer.Active)
                    throw new HostingException("A hosting unit cannot be deleted as long as there is an invitation associated with it in open mode", hostingUnit.HostingUnitKey, "BL");
            }
            dl.deleteHostingUnit(hostingUnit.Clone());
        }

        public void updateHostingUnit(HostingUnit hostingUnit)
        {
            try
            {
                dl.updateHostingUnit(hostingUnit.Clone());
            }
            catch (HostingException h)
            {
                throw h;
            }
        }

        public List<HostingUnit> getAvailableUnits(DateTime date, int numOfDays)
        {
            List<HostingUnit> ls = new List<HostingUnit>();
            foreach (var item in dl.getHostingUnits())
            {
                for (DateTime d = date; d < date.AddDays(numOfDays); d = d.AddDays(numOfDays))
                {
                    if (item[d] == true)
                        break;
                }
                ls.Add(item);
            }
            return ls;
        }

        public int numOfDaysBetween(DateTime firstdate, DateTime? secondate = null)
        {
            if (secondate == null)
                secondate = DateTime.Now;
            if (firstdate > secondate)
                throw new HostingException("the first date must be have before the second date", firstdate.Day, "BL");
            TimeSpan time = (DateTime)secondate - firstdate;
            return time.Days;
        }

        public int numOfOrdersWhichsentToUnit(HostingUnit hosting)
        {
            var v = from item in dl.getOrders()
                    where item.HostingUnitKey == hosting.HostingUnitKey
                    select item;
            return v.Count();
        }

        public int numOfOrdersWhichClosed(HostingUnit hosting)
        {
            try
            {
                var v = from item in dl.getOrders(item => item.HostingUnitKey == hosting.HostingUnitKey)
                        select item;
                var t = from item in v
                        where dl.getOneguestRequest(item.GuestRequestKey).Status == StatusCostumer.Closed_through_the_site
                        select item;
                return t.Count();
            }
            catch (HostingException exeption)
            {
                throw exeption;
            }
        }

        public List<HostingUnit> MostPopularHostingUnit()
        {
            try
            {
                List<Order> orders = dl.getOrders();
                var v = from item in orders
                        group item by item.HostingUnitKey into g
                        select g.ToList();
                //item in v- a list of order, all the order belong to the same hosting unit
                var v1 = from item in v
                         select new { HostingUnit = dl.getOnehostingUnit(item[0].HostingUnitKey), numberOforders = item.Count() };
                //item in v1- anonimous type with hosting unit and number of its order
                var v2 = from item in v1
                         orderby item.numberOforders descending
                         select item.HostingUnit;
                //item in v2- hosting unit
                return v2.ToList();
            }
            catch (HostingException h)
            {
                throw h;
            }
        }

        public Host getOnehost(string id)
        {
            try
            {
                return dl.getOnehost(id);
            }
            catch (HostingException h)
            {
                throw h;
            }
        }

        public List<Host> getHosts(Func<Host, bool> predicat = null)
        {
            return dl.getHosts(predicat);
        }

        public List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> predicat = null)
        {
            return dl.getHostingUnits(predicat);
        }
        #endregion

        #region Order

        public void addOrder(Order order)
        {
            try
            {
                GuestRequest guest = dl.getOneguestRequest(order.GuestRequestKey);
                HostingUnit hosting = dl.getOnehostingUnit(order.HostingUnitKey);
                for (DateTime date = guest.EntryDate; date < guest.ReleaseDate; date = date.AddDays(1))
                {
                    if (hosting[date] == true)
                        throw new HostingException("Sorry but these dates are busy, Can't create invitation", order.OrderKey, "BL");
                }
                dl.addOrder(order.Clone());
            }
            catch (HostingException h)
            {
                throw h;
            }
        }

        public void updateOrder(Order order, StatusOrder statusOrder)
        {
            try
            {
                //Tests before a deal can be closed
                if (statusOrder == StatusOrder.Mail_sent)
                {
                    updateOrder_privateCase_sentMail(order.Clone());
                    return;
                }
                HostingUnit UnitInTheOrder = dl.getOnehostingUnit(order.HostingUnitKey); //the hostingUnit in the order
                GuestRequest GuestInTheOrder = dl.getOneguestRequest(order.GuestRequestKey); //the guest in order
                if (order.Status == StatusOrder.Closes_with_customer_response || order.Status == StatusOrder.Irrelevant)
                    throw new HostingException("Invitation status cannot be changed after it is closed", order.OrderKey, "BL");
                if (statusOrder == StatusOrder.Closes_with_customer_response)
                {
                    //checks again if the dates are busy because maybe another customer has already closed these dates
                    for (DateTime date = GuestInTheOrder.EntryDate; date < GuestInTheOrder.ReleaseDate; date = date.AddDays(1))
                    {
                        if (UnitInTheOrder[date] == true)
                            throw new HostingException("Sorry but these dates are busy, a deal cannot be closed", order.OrderKey, "BL");
                    }
                }
                dl.updateOrder(order.Clone(), statusOrder);

                //Changes after we closed a deal
                switch (statusOrder)
                {
                    case StatusOrder.Mail_sent: break;
                    case StatusOrder.Closes_out_of_customer_disrespect: break;
                    case StatusOrder.Closes_with_customer_response:
                        {
                            int numberOfDays = (GuestInTheOrder.ReleaseDate - GuestInTheOrder.EntryDate).Days;
                            dl.updateBookingFee(order.Clone(), numberOfDays * Configuration.Commission);
                            dl.saveTheDays(order.Clone());
                            updateGuestRequest(GuestInTheOrder, StatusCostumer.Closed_through_the_site);
                            List<Order> allTheCostumerOrders = dl.getOrders(item => item.GuestRequestKey == order.GuestRequestKey);
                            foreach (var item in allTheCostumerOrders)
                            {
                                if (item.OrderKey != order.OrderKey)
                                    dl.updateOrder(item, StatusOrder.Irrelevant);
                            }
                        }
                        break;
                    case StatusOrder.Irrelevant: break;
                    default: break;
                }
            }
            catch (HostingException h)
            {
                throw h;
            }
        }

        //to call it in a thread
        public void updateOrder_privateCase_sentMail(Order order)
        {
            HostingUnit UnitInTheOrder = dl.getOnehostingUnit(order.HostingUnitKey); //the hostingUnit in the order
            GuestRequest GuestInTheOrder = dl.getOneguestRequest(order.GuestRequestKey); //the guest in order
            if (UnitInTheOrder.Owner.CollectionClearance == false)
                throw new HostingException("Order status cannot be changed without bank account authorization", int.Parse(UnitInTheOrder.Owner.HostId), "BL");
            dl.updateOrder(order.Clone(), StatusOrder.Mail_sent);
            MailMessage mail = new MailMessage();
            mail.To.Add(GuestInTheOrder.MailAddress);
            mail.From = new MailAddress("delipecan2020@gmail.com");
            mail.Subject = "Your request has been received successfully";
            mail.Body =
                "<b> <p style = 'color: red; font-size:25px'>" + "Hi " + GuestInTheOrder.Private_Name + " " + GuestInTheOrder.Family_Name + "</p> </b>" +
                "<b> <p style = 'color: red; font-size:20px'> !My Hosting unit fits your requirements and is waiting only for you </p> </b>" +
                "<b> <p style = 'font-size:18px'> Your vacation date: " + GuestInTheOrder.EntryDate.ToString("dd/MM") + " - " + GuestInTheOrder.ReleaseDate.ToString("dd/MM") + "</p> </b>" +
                "<b> <p style = 'font-size:18px'> My Unit Adderess: " + UnitInTheOrder.AllhostingUnitdetails.SubArea + ", Israel </p> </b>" +
                "<b> <p style = 'font-size:18px'> Number of people who can stay: " + (UnitInTheOrder.AllhostingUnitdetails.Adults + UnitInTheOrder.AllhostingUnitdetails.Children).ToString() + "</p>  </b> <BR>" +
                "<b> <p style = 'font-size:20px'> If you interested in my suggestion, please call me or send me returning email </b> </p>" +
                "<b> <p style = 'font-size:18px'> My phone number is: " + UnitInTheOrder.Owner.PhoneNumber + "</p>  </b>" +
                "<b> <p style = 'font-size:18px'> My mail address: " + UnitInTheOrder.Owner.MailAdress + "</p>  </b>" +
                "<b> <p style = 'font-size:18px'> I would love to host you:), <BR>" + UnitInTheOrder.Owner.PrivateName + " " + UnitInTheOrder.Owner.FamilyName + " -the hoster </p> </b>" +
                "<b> <p style = 'color: red; font-size:20px'>" + "Deli-Pecan" +
                "<img width = 60 height = 60 src = https://i.imagesup.co/images2/db837a42efb8c8d35a45790fb3cb43df037752ee.jpg>";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("delipecan2020@gmail.com", "64725499");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Order> getIfBigger(int days)
        {
            var v = from item in dl.getOrders()
                    where numOfDaysBetween(item.CreateDate) > days 
                    select item;
            return v.ToList();
        }

        public Order getOneOrder(int key)
        {
            try
            {
                return dl.getOneOrder(key);
            }
            catch (HostingException h)
            {
                throw h;
            }
        }

        public List<Order> getOrders(Func<Order, bool> predicat = null)
        {
            return dl.getOrders(predicat);
         }

        public void updateCommission(int commisionush)
        {
            dl.updateCommission(commisionush); 
        }
        #endregion

        #region BankBranch
        public List<BankBranch> getBankBranches()
        {
            return dl.getBankBranches();
        }
        public IEnumerable<IGrouping<int ,BankBranch>> GroupByBankNumberOfBranch(Func<BankBranch, bool> p = null)
        {
            var v = from item in dl.getBankBranches()
            group item by item.BankNumber into b
                    select b;
            var g = v.OrderBy(item => item.Key);
            return g;
        }

        #endregion

        #region Group

        public IEnumerable<IGrouping<AreaINCountry, GuestRequest>> groupByArea(Func<GuestRequest, bool> p = null)
        {
            var group = dl.getGuestRequests(p).GroupBy(item => item.Allguestrequestdetails.Area).OrderBy(item => item.Key);
            return group;
        }

        public IEnumerable<IGrouping<int, GuestRequest>> groupByNumOfPeople(Func<GuestRequest, bool> p = null)
        {
            var v = from item in dl.getGuestRequests(p)
                    group item by item.Allguestrequestdetails.Adults + item.Allguestrequestdetails.Children into list
                    select list;
            var g = v.OrderBy(item => item.Key);
            return g;
        }

        public IEnumerable<IGrouping<StatusCostumer, GuestRequest>> groupByStatus(Func<GuestRequest, bool> p = null)
        {
            var group = dl.getGuestRequests(p).GroupBy(item => item.Status).OrderBy(item => item.Key);
            return group;
        }

        public IEnumerable<IGrouping<int, Host>> GroupByNumOfUnit()
        {
            List<HostingUnit> hostingUnits = dl.getHostingUnits();
            var groupunit = from item in hostingUnits
                            group item by item.Owner.HostId into g
                            select g.ToList();
            var groug = from item in groupunit
                        group item[0].Owner by item.Count() into g
                        select g;
            return groug;
        }

        public IEnumerable<IGrouping<int, HostingUnit>> GroupHostingUnitByNumOfPeople(Func<HostingUnit, bool> p = null)
        {
            var v = from item in dl.getHostingUnits(p)
                    group item by item.AllhostingUnitdetails.Adults + item.AllhostingUnitdetails.Children into list
                    select list;
            var g = v.OrderBy(item => item.Key);
            return g;
        }

        public IEnumerable<IGrouping<AreaINCountry, HostingUnit>> GroupByAreaOfUnit(Func<HostingUnit,bool> p = null)
        {
            List<HostingUnit> hostingUnits = dl.getHostingUnits(p);
            var group = from item in hostingUnits
                        group item by item.AllhostingUnitdetails.Area into g
                        select g;
            var n = group.OrderBy(item => item.Key);
            return n;
        }

        public IEnumerable<IGrouping<int, HostingUnit>> GroupByNumOfClosedOrders(Func<HostingUnit, bool> p = null)
        {
            var group = from item in dl.getHostingUnits(p)
                        group item by numOfOrdersWhichClosed(item) into g
                        select g;
            var n = group.OrderBy(item => item.Key);
            return n;
        }

        public IEnumerable<IGrouping<TypeOfUnit, HostingUnit>> GroupByTypeOfUnits(Func<HostingUnit, bool> p = null)
        {
            var v = from item in dl.getHostingUnits(p)
                    group item by item.AllhostingUnitdetails.Type into g
                    select g;
            var n = v.OrderBy(item => item.Key);
            return n;
        }
        #endregion

        #region Surveys

        public double PercentageOfCustomerWhoNecessarilyWantPool()
        {
            List<GuestRequest> guestRequests = dl.getGuestRequests();
            if (guestRequests.Count == 0)
                return 0;
            var v = guestRequests.FindAll(item => item.Allguestrequestdetails.Pool == Option.Necessary);
            return v.Count() / guestRequests.Count() * 100;
        }

        public double PercentageOfCustomerWhoNecessarilyWantJacuzzi()
        {
            List<GuestRequest> guestRequests = dl.getGuestRequests();
            if (guestRequests.Count == 0)
                return 0;
            var v = guestRequests.FindAll(item => item.Allguestrequestdetails.Jacuzzi == Option.Necessary);
            return v.Count() / guestRequests.Count() * 100;
        }

        public double PercentageOfCustomerWhoNecessarilyWantGarden()
        {
            List<GuestRequest> guestRequests = dl.getGuestRequests();
            if (guestRequests.Count == 0)
                return 0;
            var v = guestRequests.FindAll(item => item.Allguestrequestdetails.Garden == Option.Necessary);
            return v.Count() / guestRequests.Count() * 100;
        }

        public double PercentageOfCustomerWhoNecessarilyWantChildrensAttractions()
        {
            List<GuestRequest> guestRequests = dl.getGuestRequests();
            if (guestRequests.Count == 0)
                return 0;
            var v = guestRequests.FindAll(item => item.Allguestrequestdetails.ChildrensAttractions == Option.Necessary);
            return v.Count() / guestRequests.Count() * 100;
        }

        public double PercentageOfCustomerWhoNecessarilyWantWifi()
        {
            List<GuestRequest> guestRequests = dl.getGuestRequests();
            if (guestRequests.Count == 0)
                return 0;
            var v = guestRequests.FindAll(item => item.Allguestrequestdetails.IsWifi == true);
            return v.Count() / guestRequests.Count() * 100;
        }

        public double PercentageOfCustomerWhoNecessarilyWantAirConditioner()
        {
            List<GuestRequest> guestRequests = dl.getGuestRequests();
            if (guestRequests.Count == 0)
                return 0;
            var v = guestRequests.FindAll(item => item.Allguestrequestdetails.IsAirConditioner == true);
            return v.Count() / guestRequests.Count() * 100;
        }
        #endregion

    }
}