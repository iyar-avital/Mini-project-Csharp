using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;

namespace DAL
{
    class Dal_XML_imp : IDAL
    {
        XElement HostRoot;
        XElement GuestRequestRoot;
        XElement OrderRoot;
        XElement ConfigRoot;
        XElement BankRoot;
        const string HostFile = @"Hostush.xml";
        const string HostingUnitFile = @"HostingUnitush.xml";
        const string GuestFile = @"Guestush.xml";
        const string OrderFile = @"Orderush.xml";
        const string ConfigFile = @"Configush.xml";
        const string xmlLocalPath = @"atm.xml";
        List<HostingUnit> HostingUnits = new List<HostingUnit>();
        BackgroundWorker worker = new BackgroundWorker();
        bool IsDownloadFileComplete = false;
        internal Dal_XML_imp()
        {
            try
            {
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
                if (!File.Exists(HostFile))
                {
                    HostRoot = new XElement("allHost");
                    HostRoot.Save(HostFile);
                }
                else
                    HostRoot = XElement.Load(HostFile);

                if (!File.Exists(HostingUnitFile))
                {
                    List<HostingUnit> h = getHostingUnits();
                    SaveToXML(h, HostingUnitFile);
                }
                else
                    HostingUnits = LoadFromXML(HostingUnitFile);
                 
                if (!File.Exists(GuestFile))
                {
                    GuestRequestRoot = new XElement("allGuestRequest");
                    GuestRequestRoot.Save(GuestFile);
                }
                else
                    GuestRequestRoot = XElement.Load(GuestFile);

                if (!File.Exists(OrderFile))
                {
                    OrderRoot = new XElement("allOrder");
                    OrderRoot.Save(OrderFile);
                }
                else
                    OrderRoot = XElement.Load(OrderFile);

                if (!File.Exists(ConfigFile))
                {
                    ConfigRoot = new XElement("allConfig", new XElement("guestRequestKey", 10000000), new XElement("hostingUnitKey", 10000000),
                                 new XElement("orderKey", 20000000), new XElement("commission", 10), new XElement("lastDateThread", new DateTime(2020,01,01)));
                    ConfigRoot.Save(ConfigFile);
                }
                else
                    ConfigRoot = XElement.Load(ConfigFile);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (IsDownloadFileComplete == false)
            {
                WebClient wc = new WebClient();
                try
                {
                    string xmlServerPath =
                    @"https://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);
                    IsDownloadFileComplete = true;
                }
                catch (Exception)
                {
                    try
                    {
                        string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                        wc.DownloadFile(xmlServerPath, xmlLocalPath);
                        IsDownloadFileComplete = true;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(2000);
                    }         
                }
                finally
                {
                    wc.Dispose();
                }
            }     
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BankRoot = XElement.Load(xmlLocalPath);
            var allBranchByAtm = from item in BankRoot.Elements()
                                 select new XElement("BankBranch", new XElement("bankNumber", item.Element("קוד_בנק").Value),
                                 new XElement("bankName", item.Element("שם_בנק").Value), new XElement("branchNumber", item.Element("קוד_סניף").Value),
                                 new XElement("branchAddress", item.Element("כתובת_ה-ATM").Value), new XElement("branchCity", item.Element("ישוב").Value));
            BankRoot = new XElement("allBranch");
            foreach (var item in allBranchByAtm)
            {
                BankRoot.Add(item);
            }
            BankRoot.Save(xmlLocalPath);
        }

        #region GuestRequest

        public void addGuestRequest(GuestRequest guest)
        {
            int Key = int.Parse(ConfigRoot.Element("guestRequestKey").Value) + 1;
            guest.GuestRequestKey = Key;
            ConfigRoot.Element("guestRequestKey").Value = Key.ToString();
            ConfigRoot.Save(ConfigFile);
            guest.RegistrationDate = DateTime.Now;
            GuestRequestRoot.Add(new XElement("guest", new XElement("guestRequestKey", guest.GuestRequestKey),new XElement("guestId",guest.GuestId),
                new XElement("name",new XElement("privateName",guest.Private_Name),new XElement("familyName",guest.Family_Name)),new XElement("mailAddress",guest.MailAddress),
                new XElement("status",guest.Status),new XElement("registrationDate",guest.RegistrationDate.ToString("dd/MM/yyyy")),new XElement("entryDate",guest.EntryDate.ToString("dd/MM/yyyy")),
                new XElement("releaseDate",guest.ReleaseDate.ToString("dd/MM/yyyy")),new XElement("details",new XElement("area",guest.Allguestrequestdetails.Area),
                new XElement("subArea",guest.Allguestrequestdetails.SubArea),new XElement("type",guest.Allguestrequestdetails.Type),
                new XElement("adults",guest.Allguestrequestdetails.Adults),new XElement("children", guest.Allguestrequestdetails.Children),
                new XElement("pool",guest.Allguestrequestdetails.Pool),new XElement("jacuzzi",guest.Allguestrequestdetails.Jacuzzi),
                new XElement("garden",guest.Allguestrequestdetails.Garden),new XElement("childrenAttractions",guest.Allguestrequestdetails.ChildrensAttractions),
                new XElement("isWifi", guest.Allguestrequestdetails.IsWifi), new XElement("isAirConditioner", guest.Allguestrequestdetails.IsAirConditioner))));
            GuestRequestRoot.Save(GuestFile);
        }

        public void updateGuestRequest(GuestRequest guest, StatusCostumer statusCostumer)
        {
            XElement myguest = GuestRequestRoot.Elements().FirstOrDefault(item => Convert.ToInt32(item.Element("guestRequestKey").Value) == guest.GuestRequestKey);
            if (myguest == null)
                throw new HostingException("This object isn't exist, can't make a change", guest.GuestRequestKey, "DAL");
            myguest.Element("status").Value = statusCostumer.ToString();
            GuestRequestRoot.Save(GuestFile);
        }

        public GuestRequest getOneguestRequest(int key)
        {
            XElement myguest = GuestRequestRoot.Elements().FirstOrDefault(item => Convert.ToInt32(item.Element("guestRequestKey").Value) == key);
            if (myguest == null)
                throw new HostingException("This object isn't exist", key, "DAL");
            GuestRequest g = new GuestRequest();
            g.Allguestrequestdetails = new Details();
            g.GuestRequestKey = Convert.ToInt32(myguest.Element("guestRequestKey").Value);
            g.GuestId = myguest.Element("guestId").Value;
            g.Private_Name = myguest.Element("name").Element("privateName").Value;
            g.Family_Name = myguest.Element("name").Element("familyName").Value;
            g.MailAddress = myguest.Element("mailAddress").Value;
            g.Status = (StatusCostumer)Enum.Parse(typeof(StatusCostumer) ,myguest.Element("status").Value);
            g.RegistrationDate = Convert.ToDateTime(myguest.Element("registrationDate").Value);
            g.EntryDate = Convert.ToDateTime(myguest.Element("entryDate").Value);
            g.ReleaseDate = Convert.ToDateTime(myguest.Element("releaseDate").Value);
            g.Allguestrequestdetails.Area = (AreaINCountry)Enum.Parse(typeof(AreaINCountry),myguest.Element("details").Element("area").Value);
            g.Allguestrequestdetails.SubArea = myguest.Element("details").Element("subArea").Value;
            g.Allguestrequestdetails.Type = (TypeOfUnit)Enum.Parse(typeof(TypeOfUnit) ,myguest.Element("details").Element("type").Value);
            g.Allguestrequestdetails.Adults = Convert.ToInt32(myguest.Element("details").Element("adults").Value);
            g.Allguestrequestdetails.Children = Convert.ToInt32(myguest.Element("details").Element("children").Value);
            g.Allguestrequestdetails.Pool = (Option)Enum.Parse(typeof(Option), myguest.Element("details").Element("pool").Value);
            g.Allguestrequestdetails.Jacuzzi = (Option)Enum.Parse(typeof(Option), myguest.Element("details").Element("jacuzzi").Value);
            g.Allguestrequestdetails.Garden = (Option)Enum.Parse(typeof(Option), myguest.Element("details").Element("garden").Value);
            g.Allguestrequestdetails.ChildrensAttractions = (Option)Enum.Parse(typeof(Option), myguest.Element("details").Element("childrenAttractions").Value);
            g.Allguestrequestdetails.IsWifi = bool.Parse(myguest.Element("details").Element("isWifi").Value);
            g.Allguestrequestdetails.IsAirConditioner = bool.Parse(myguest.Element("details").Element("isAirConditioner").Value);
            return g.Clone();
        }

        public List<GuestRequest> getGuestRequests(Func<GuestRequest, bool> predicat = null)
        {
            if (predicat == null)
                return GuestRequestRoot.Elements().Select(item => getOneguestRequest(Convert.ToInt32(item.Element("guestRequestKey").Value))).ToList();
            var v = from item in GuestRequestRoot.Elements()
                    let Guester = getOneguestRequest(Convert.ToInt32(item.Element("guestRequestKey").Value))
                    where predicat(Guester) == true
                    select Guester.Clone();
            return v.ToList();
        }
        #endregion

        #region Host
        public void addHost(Host host)
        {
            HostRoot.Add(new XElement("host", new XElement("id", host.HostId), new XElement("name", new XElement("privateName", host.PrivateName), new XElement("familyName", host.FamilyName)),
                new XElement("phoneNumber", host.PhoneNumber), new XElement("mailAddress", host.MailAdress), new XElement("bankBranchDetails", new XElement("bankNumber", host.BankBranchDetails.BankNumber), new XElement("bankName", host.BankBranchDetails.BankName),
                new XElement("branchNumber", host.BankBranchDetails.BranchNumber), new XElement("branchAddress", host.BankBranchDetails.BranchAddress), new XElement("branchCity", host.BankBranchDetails.BranchCity)),
                new XElement("bankAccountNumber", host.BankAccountNumber),
                new XElement("collectionClearance", host.CollectionClearance), new XElement("language", host.Languages), new XElement("isInPlace", host.IsInPlace)));
            HostRoot.Save(HostFile);
        }

        public bool isDownloadFinished()
        {
            return IsDownloadFileComplete;
        }

        public Host getOnehost(string id)
        {
            XElement myhost = HostRoot.Elements().FirstOrDefault(item => item.Element("id").Value == id);
            if (myhost == null)
                throw new HostingException("This object isn't exist", int.Parse(id), "DAL");
            Host h = new Host();
            h.BankBranchDetails = new BankBranch();
            h.HostId = myhost.Element("id").Value;
            h.PrivateName = myhost.Element("name").Element("privateName").Value;
            h.FamilyName = myhost.Element("name").Element("familyName").Value;
            h.PhoneNumber = myhost.Element("phoneNumber").Value;
            h.MailAdress = myhost.Element("mailAddress").Value;
            h.BankBranchDetails.BankNumber = int.Parse(myhost.Element("bankBranchDetails").Element("bankNumber").Value);
            h.BankBranchDetails.BankName = myhost.Element("bankBranchDetails").Element("bankName").Value;
            h.BankBranchDetails.BranchNumber = int.Parse(myhost.Element("bankBranchDetails").Element("branchNumber").Value);
            h.BankBranchDetails.BranchAddress = myhost.Element("bankBranchDetails").Element("branchAddress").Value;
            h.BankBranchDetails.BranchCity = myhost.Element("bankBranchDetails").Element("branchCity").Value;
            h.BankAccountNumber = int.Parse(myhost.Element("bankAccountNumber").Value);
            h.CollectionClearance = bool.Parse(myhost.Element("collectionClearance").Value);
            h.Languages = myhost.Element("language").Value;
            h.IsInPlace = bool.Parse(myhost.Element("isInPlace").Value);
            return h.Clone();
        }

        public List<Host> getHosts(Func<Host, bool> predicat = null)
        {
            if (predicat == null)
                return HostRoot.Elements().Select(item => getOnehost(item.Element("id").Value)).ToList();
            var v = from item in HostRoot.Elements()
                    let Hoster = getOnehost(item.Element("id").Value)
                    where predicat(Hoster) == true
                    select Hoster.Clone();
            return v.ToList();
        }
        #endregion

        #region HostingUnit

        public static void SaveToXML(List<HostingUnit> source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSer = new XmlSerializer(source.GetType());
            xmlSer.Serialize(file, source);
            file.Close();
        }
        public static List<HostingUnit> LoadFromXML(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<HostingUnit>));
            List<HostingUnit> result = (List<HostingUnit>)xmlSer.Deserialize(file);
            file.Close();
            return result;
        }

        public void addHostingUnit(HostingUnit hostingUnit)
        {
            int Key = Convert.ToInt32(ConfigRoot.Element("hostingUnitKey").Value) + 1;
            hostingUnit.HostingUnitKey = Key;
            ConfigRoot.Element("hostingUnitKey").Value = Key.ToString();
            ConfigRoot.Save(ConfigFile);
            HostingUnits.Add(hostingUnit.Clone());
            SaveToXML(HostingUnits, HostingUnitFile);
        }

        public void deleteHostingUnit(HostingUnit hostingUnit)
        {
            bool exists = HostingUnits.Exists(item => item.HostingUnitKey == hostingUnit.HostingUnitKey);
            if (exists == false)
                throw new HostingException("This object isn't exist, Cannot delete", hostingUnit.HostingUnitKey, "DAL");
            HostingUnits.Remove(HostingUnits.Find(item => hostingUnit.HostingUnitKey == item.HostingUnitKey));
            SaveToXML(HostingUnits, HostingUnitFile);
        }

        public void updateHostingUnit(HostingUnit hostingUnit)
        {
            int index = HostingUnits.FindIndex(item => item.HostingUnitKey == hostingUnit.HostingUnitKey);
            if (index == -1)
                throw new HostingException("This object isn't exist, can't make a change", hostingUnit.HostingUnitKey, "DAL");
            HostingUnits[index] = hostingUnit.Clone();
            SaveToXML(HostingUnits, HostingUnitFile);
        }

        public HostingUnit getOnehostingUnit(int key)
        {
            HostingUnit myhosting = HostingUnits.FirstOrDefault(item => item.HostingUnitKey == key);
            if (myhosting == null)
                throw new HostingException("This object isn't exist", key, "DAL");
            return myhosting.Clone();
        }

        public List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> predicat = null)
        {
            if (predicat == null)
                return HostingUnits.Select(item => item.Clone()).ToList();
            var v = from item in HostingUnits
                    where predicat(item) == true
                    select item.Clone();
            return v.ToList();
        }
        #endregion

        #region Order
        public void addOrder(Order order)
        {
            int Key = int.Parse(ConfigRoot.Element("orderKey").Value) + 1;
            order.OrderKey = Key;
            ConfigRoot.Element("orderKey").Value = Key.ToString();
            ConfigRoot.Save(ConfigFile);
            OrderRoot.Add(new XElement("order", new XElement("orderKey", order.OrderKey), new XElement("hostingUnitKey", order.HostingUnitKey), new XElement("guestRequestKey", order.GuestRequestKey),
                new XElement("createDate", DateTime.Now.ToString("dd/MM/yyyy")), new XElement("status", order.Status), new XElement("bookingFee", order.BookingFee)));
            OrderRoot.Save(OrderFile);
        }

        public void updateOrder(Order order, StatusOrder statusOrder)
        {
            XElement myorder = OrderRoot.Elements().FirstOrDefault(item => int.Parse(item.Element("orderKey").Value) == order.OrderKey);
            if (myorder == null)
                throw new HostingException("This object isn't exist, can't make a change", order.OrderKey, "DAL");
            myorder.Element("status").Value = statusOrder.ToString();
            OrderRoot.Save(OrderFile);
        }

        public void saveTheDays(Order order)
        {
            HostingUnit hosting = HostingUnits.FirstOrDefault(item => item.HostingUnitKey == order.HostingUnitKey);
            XElement guestRequest = GuestRequestRoot.Elements().FirstOrDefault(item => int.Parse(item.Element("guestRequestKey").Value) == order.GuestRequestKey);
            for (DateTime date = Convert.ToDateTime(guestRequest.Element("entryDate").Value); date < Convert.ToDateTime(guestRequest.Element("releaseDate").Value); date = date.AddDays(1))
                 hosting[date] = true;
            SaveToXML(HostingUnits, HostingUnitFile);
        }

        public DateTime getLastDate()
        {
            return Convert.ToDateTime(ConfigRoot.Element("lastDateThread").Value);
        }

        public void setLastDate(DateTime last)
        {
            ConfigRoot.Element("lastDateThread").Value = last.ToString("dd/MM/yyyy");
            ConfigRoot.Save(ConfigFile);
        }

        public void updateBookingFee(Order order, int commission)
        {
            if (commission <= 0)
                throw new HostingException("A commission cannot be negative", order.OrderKey, "DAL");
            XElement myorder = OrderRoot.Elements().FirstOrDefault(item => int.Parse(item.Element("orderKey").Value) == order.OrderKey);
            myorder.Element("bookingFee").Value = Convert.ToString(commission);
            OrderRoot.Save(OrderFile);
        }

        public void updateCommission(int commissionush)
        {
            ConfigRoot.Element("commission").Value = commissionush.ToString();
            ConfigRoot.Save(ConfigFile);
        }

        public Order getOneOrder(int key)
        {
            XElement myorder = OrderRoot.Elements().FirstOrDefault(item => int.Parse(item.Element("orderKey").Value) == key);
            if (myorder == null)
                throw new HostingException("This object isn't exist", key, "DAL");
            Order order = new Order();
            order.GuestRequestKey = int.Parse(myorder.Element("guestRequestKey").Value);
            order.HostingUnitKey = int.Parse(myorder.Element("hostingUnitKey").Value);
            order.OrderKey = int.Parse(myorder.Element("orderKey").Value);
            order.Status = (StatusOrder)Enum.Parse(typeof(StatusOrder), myorder.Element("status").Value);
            order.CreateDate = Convert.ToDateTime(myorder.Element("createDate").Value);
            order.BookingFee = int.Parse(myorder.Element("bookingFee").Value);
            return order.Clone();
        }

        public List<Order> getOrders(Func<Order, bool> predicat = null)
        {
            if (predicat == null)
                return OrderRoot.Elements().Select(item => getOneOrder(int.Parse(item.Element("orderKey").Value))).ToList();
            var v = from item in OrderRoot.Elements()
                    let Orderr = getOneOrder(int.Parse(item.Element("orderKey").Value))
                    where predicat(Orderr) == true
                    select Orderr.Clone();
            return v.ToList();
        }

        #endregion

        #region BankBranch

        public List<BankBranch> getBankBranches()
        {
            List<BankBranch> banks = new List<BankBranch>();
            List<BankBranch> Distindebanks = new List<BankBranch>();
            foreach (var item in BankRoot.Elements())
            {
                BankBranch branch = new BankBranch();
                branch.BankNumber = Convert.ToInt32(item/*.Element("BankBranch")*/.Element("bankNumber").Value);
                branch.BankName = item/*.Element("BankBranch")*/.Element("bankName").Value;
                branch.BranchNumber = Convert.ToInt32(item/*.Element("BankBranch")*/.Element("branchNumber").Value);
                branch.BranchAddress = item/*.Element("BankBranch")*/.Element("branchAddress").Value;
                branch.BranchCity = item/*.Element("BankBranch")*/.Element("branchCity").Value;
                banks.Add(branch);
            }
            for (int i = 0; i < banks.Count; i++)
            {
                bool exsist = false;
                for (int j = 0; j < Distindebanks.Count; j++)
                {
                    if (banks[i].CompareTo(Distindebanks[j]) == 0)
                    {
                        exsist = true;  //to know dont add this item 
                        j = Distindebanks.Count;   //to stop the loop
                    }
                }
                if (exsist == false)
                {
                    if(banks[i].BankName.StartsWith("בנק"))
                    {
                        banks[i].BankName = banks[i].BankName.Substring(4, banks[i].BankName.Length - 9);
                        banks[i].BankName = banks[i].BankName.Replace("לישראל", "");
                    }
                    else   //just one bank dont start with "בנק"- יובנק
                        banks[i].BankName = banks[i].BankName.Substring(0, banks[i].BankName.Length - 5);
                    Distindebanks.Add(banks[i]);
                }
            }
            return Distindebanks;
        }

        #endregion
    }
}
