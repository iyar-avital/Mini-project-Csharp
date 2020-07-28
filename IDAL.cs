using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL
    {
        /// <summary>
        ///Sets it a guest request key , set the registiration date for today's date and add the guest
        /// </summary>
        /// <param name="guest">The object we want to add</param>
        void addGuestRequest(GuestRequest guest);

        /// <summary>
        /// Update the status customer
        /// </summary>
        /// <param name="guest">The guest request we want to update its status</param>
        /// <param name="statusCostumer">The new status</param>
        void updateGuestRequest(GuestRequest guest, StatusCostumer statusCostumer);

        /// <summary>
        /// Gets a host , and add
        /// </summary>
        /// <param name="hostingUnit">The object we want to add</param>
        void addHost(Host host);

        /// <summary>
        /// cheacks the flag IsDownloadFileComplete
        /// </summary>
        /// <returns>true or false</returns>
        bool isDownloadFinished();

        /// <summary>
        /// Sets it a hosting unit key , and bank account number  and add the unit
        /// </summary>
        /// <param name="hostingUnit">The object we want to add</param>
        void addHostingUnit(HostingUnit hostingUnit);

        /// <summary>
        /// Deletes hosting unit
        /// </summary>
        /// <param name="hostingUnit">The object we want to delete</param>
        void deleteHostingUnit(HostingUnit hostingUnit);

        /// <summary>
        /// Gets a hosting unit and replaces it with another
        /// </summary>
        /// <param name="hostingUnit">The hosting unit we want to update</param>
        void updateHostingUnit(HostingUnit hostingUnit);

        /// <summary>
        ///Sets it a order key , set the create date for today's date and add the order
        /// </summary>
        /// <param name="order">The object we want to add</param>
        void addOrder(Order order);

        /// <summary>
        /// Update the status order
        /// </summary>
        /// <param name="order">The order we want to update</param>
        /// <param name="statusOrder">The new status</param>
        void updateOrder(Order order, StatusOrder statusOrder);

        /// <summary>
        /// Saves the booking dates in the calendar
        /// </summary>
        /// <param name="order">The order for which we want to keep a log of the order dates</param>
        void saveTheDays(Order order);

        DateTime getLastDate();

        void setLastDate(DateTime last);

        /// <summary>
        /// Gets an order and sets its booking fee
        /// </summary>
        /// <param name="order">The order for which we want to set its comission</param>
        /// <param name="commission">The sum of comission</param>
        void updateBookingFee(Order order, int commission);

        void updateCommission(int commissionush);

        /// <summary>
        /// Gets a key and looking for the guest request which the key is its guest request key
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The guest request which the key is its guest request key</returns>
        GuestRequest getOneguestRequest(int key);

        /// <summary>
        /// Gets a key and looking for the hosting unit which the key is its hosting unit key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The hosting unit which the key is its hosting unit key</returns>
        HostingUnit getOnehostingUnit(int key);

        /// <summary>
        /// Gets a id and looking for the hoster which the id is its id
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The host which the key is its host id</returns>
        Host getOnehost(string id);

        /// <summary>
        /// Gets a key and looking for the order which the key is its order key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The order which the key is its order key</returns>
        Order getOneOrder(int key);

        /// <summary>
        /// Checks which Hosting units meet certain conditions
        /// </summary>
        /// <param name="predicat">conditions</param>
        /// <returns>list of Hosting units which meet certain conditions</returns>
        List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> predicat = null);

        /// <summary>
        /// Checks which Hosts meet certain conditions
        /// </summary>
        /// <param name="predicat">conditions</param>
        /// <returns>list of Hosts which meet certain conditions</returns>
        List<Host> getHosts(Func<Host, bool> predicat = null);

        /// <summary>
        /// Checks which guest request meet certain conditions
        /// </summary>
        /// <param name="predicat">conditions</param>
        /// <returns>list of guest requests which meet certain conditions</returns>
        List<GuestRequest> getGuestRequests(Func<GuestRequest, bool> predicat = null);

        /// <summary>
        /// Checks which orders meet certain conditions
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns>list of orders which meet certain conditions</returns>
        List<Order> getOrders(Func<Order, bool> predicat = null);

        /// <summary>
        /// Creates banks and returns them
        /// </summary>
        /// <returns>list of bank branches</returns>
        List<BankBranch> getBankBranches();
    }
}





