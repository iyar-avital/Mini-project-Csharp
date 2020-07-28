using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    class Dal_imp : IDAL
    {
        #region GuestRequest

        public void addGuestRequest(GuestRequest guest)
        {
            guest.GuestRequestKey = ++Configuration.GuestRequestKey;
            guest.RegistrationDate = DateTime.Now;
            DataSource.GuestRequests.Add(guest.Clone());
        }

        public void updateGuestRequest(GuestRequest guest, StatusCostumer statusCostumer)
        {
            GuestRequest request = DataSource.GuestRequests.FirstOrDefault(item => guest.GuestRequestKey == item.GuestRequestKey);
            if (request == null)
                throw new HostingException("This object isn't exist, can't make a change", guest.GuestRequestKey, "DAL");
            request.Status = statusCostumer;
        }

        public GuestRequest getOneguestRequest(int key)
        {
            GuestRequest myguest = DataSource.GuestRequests.FirstOrDefault(item => item.GuestRequestKey == key);
            if (myguest == null)
                throw new HostingException("This object isn't exist", key, "DAL");
            return myguest.Clone();
        }

        public List<GuestRequest> getGuestRequests(Func<GuestRequest, bool> predicat = null)
        {
            if (predicat == null)
                return DataSource.GuestRequests.Select(item => item.Clone()).ToList();
            var v = from item in DataSource.GuestRequests
                    where predicat(item) == true
                    select item.Clone();
            return v.ToList();
        }
        #endregion

        #region Host

        public void addHost(Host host)
        {
            DataSource.Hosts.Add(host.Clone());
        }

        public bool isDownloadFinished()
        {
            return true;
        }

        public Host getOnehost(string id)
        {
            Host myhost = DataSource.Hosts.FirstOrDefault(item => item.HostId == id);
            if (myhost == null)
                throw new HostingException("This object isn't exist", int.Parse(id), "DAL");
            return myhost.Clone();
        }

        public List<Host> getHosts(Func<Host, bool> predicat = null)
        {
            if (predicat == null)
                return DataSource.Hosts.Select(item => item.Clone()).ToList();
            var v = from item in DataSource.Hosts
                    where predicat(item) == true
                    select item.Clone();
            return v.ToList();
        }

        #endregion

        #region HostingUnit

        public void addHostingUnit(HostingUnit hostingUnit)
        {
            hostingUnit.HostingUnitKey = ++Configuration.HostingUnitKey;
            DataSource.HostingUnits.Add(hostingUnit.Clone());
        }

        public void deleteHostingUnit(HostingUnit hostingUnit)
        {
            bool exists = DataSource.HostingUnits.Exists(item => item.HostingUnitKey == hostingUnit.HostingUnitKey);
            if (exists == false)
                throw new HostingException("This object isn't exist, Cannot delete", hostingUnit.HostingUnitKey, "DAL");
            DataSource.HostingUnits.Remove(DataSource.HostingUnits.Find(item => hostingUnit.HostingUnitKey == item.HostingUnitKey));
        }

        public void updateHostingUnit(HostingUnit hostingUnit)
        {
            int index = DataSource.HostingUnits.FindIndex(item => item.HostingUnitKey == hostingUnit.HostingUnitKey);
            if (index == -1)
                throw new HostingException("This object isn't exist, can't make a change", hostingUnit.HostingUnitKey, "DAL");
            DataSource.HostingUnits[index] = hostingUnit.Clone();
        }

        public HostingUnit getOnehostingUnit(int key)
        {
            HostingUnit myhosting = DataSource.HostingUnits.FirstOrDefault(item => item.HostingUnitKey == key);
            if (myhosting == null)
                throw new HostingException("This object isn't exist", key, "DAL");
            return myhosting.Clone();
        }

        public List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> predicat = null)
        {
            if (predicat == null)
                return DataSource.HostingUnits.Select(item => item.Clone()).ToList();
            var v = from item in DataSource.HostingUnits
                    where predicat(item) == true
                    select item.Clone();
            return v.ToList();
        }
        #endregion

        #region Order

        public void addOrder(Order order)
        {
            order.OrderKey = ++Configuration.OrderKey;
            order.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DataSource.Orders.Add(order.Clone());
        }

        public void updateOrder(Order order, StatusOrder statusOrder)
        {
            Order myorder = DataSource.Orders.FirstOrDefault(item => item.OrderKey == order.OrderKey);
            if (myorder == null)
                throw new HostingException("This object isn't exist, can't make a change", order.OrderKey, "DAL");
            myorder.Status = statusOrder;
        }

        public void saveTheDays(Order order)
        {
            HostingUnit hosting = DataSource.HostingUnits.Find(item => item.HostingUnitKey == order.HostingUnitKey);
            GuestRequest guestRequest = DataSource.GuestRequests.Find(item => item.GuestRequestKey == order.GuestRequestKey);
            for (DateTime date = guestRequest.EntryDate; date < guestRequest.ReleaseDate; date = date.AddDays(1))
                hosting[date] = true;
        }

        public DateTime getLastDate()
        {
            return DateTime.Now;   //never happend but we need it in the DalXml 
        }

        public void setLastDate(DateTime last)
        {
            return;   //cana"l
        }

        public void updateBookingFee(Order order, int commission)
        {
            if (commission <= 0)
                throw new HostingException("A commission cannot be negative", order.OrderKey, "DAL");
            DataSource.Orders.Find(item => item.OrderKey == order.OrderKey).BookingFee = commission;
        }

        public void updateCommission(int commissionush)
        {
            Configuration.Commission = commissionush;
        }

        public Order getOneOrder(int key)
        {
            Order myorder = DataSource.Orders.FirstOrDefault(item => item.OrderKey == key);
            if (myorder == null)
                throw new HostingException("This object isn't exist", key, "DAL");
            return myorder.Clone();
        }

        public List<Order> getOrders(Func<Order, bool> predicat = null)
        {
            if (predicat == null)
                return DataSource.Orders.Select(item => item.Clone()).ToList();
            var v = from item in DataSource.Orders
                    where predicat(item) == true
                    select item.Clone();
            return v.ToList();
        }
        #endregion

        #region BankBranch

        public List<BankBranch> getBankBranches()
        {
            return new List<BankBranch>()
            {
                new BankBranch()
                {
                    BankNumber = ++Configuration.BankNumber,
                    BankName = "Sabalim",
                    BranchNumber = ++Configuration.BranchNumber,
                    BranchAddress = "yafo 298",
                    BranchCity = "Tel Aviv"
                },
                new BankBranch()
                {
                    BankNumber = ++Configuration.BankNumber,
                    BankName = "Yaalom",
                    BranchNumber = ++Configuration.BranchNumber,
                    BranchAddress = "Pointer 63",
                    BranchCity = "holon"
                },
                new BankBranch()
                {
                    BankNumber = ++Configuration.BankNumber,
                    BankName = "Nadneda",
                    BranchNumber = ++Configuration.BranchNumber,
                    BranchAddress = "yaarot dvash 2",
                    BranchCity = "sofganya"
                },
                new BankBranch()
                {
                    BankNumber = ++Configuration.BankNumber,
                    BankName = "Ahinoam",
                    BranchNumber = ++Configuration.BranchNumber,
                    BranchAddress = "donalnd 98",
                    BranchCity = "modiin"
                },
                new BankBranch()
                {
                    BankNumber = ++Configuration.BankNumber,
                    BankName = "Shalem",
                    BranchNumber = ++Configuration.BranchNumber,
                    BranchAddress = "agudat israel",
                    BranchCity = "lapid"
                }
            };
        }
        #endregion
    }
}