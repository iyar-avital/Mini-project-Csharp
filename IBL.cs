using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        /// <summary>
        /// Adds Guest Request
        /// </summary>
        /// <param name="guest">The object we want to add</param>
        void addGuestRequest(GuestRequest guest);

        /// <summary>
        /// Switches customer status to an object
        /// </summary>
        /// <param name="guest">The object you want to change its status</param>
        /// <param name="statusCostumer">The new status</param>
        void updateGuestRequest(GuestRequest guest, StatusCostumer statusCostumer);

        /// <summary>
        /// cheacks the flag IsDownloadFileComplete
        /// </summary>
        /// <returns>true or false</returns>
        bool isDownloadFinished();

        /// <summary>
        /// Gets a host , and add
        /// </summary>
        /// <param name="hostingUnit">The object we want to add</param>
        void addHost(Host host);

        /// <summary>
        /// Adds Hosting Unit
        /// </summary>
        /// <param name="hostingUnit">The object we want to add</param>
        void addHostingUnit(HostingUnit hostingUnit);

        /// <summary>
        /// Delete Hosting Unit
        /// </summary>
        /// <param name="hostingUnit">The object we want to delete</param>
        void deleteHostingUnit(HostingUnit hostingUnit);

        /// <summary>
        /// Looking for guestr request, and replacing it
        /// </summary>
        /// <param name="hostingUnit">The object we want to change</param>
        void updateHostingUnit(HostingUnit hostingUnit);

        /// <summary>
        /// Adds order
        /// </summary>
        /// <param name="order">The object we want to add</param>
        void addOrder(Order order);

        /// <summary>
        /// Switches order status to an object
        /// </summary>
        /// <param name="order">The object you want to change its status</param>
        /// <param name="statusOrder">The new status</param>
        void updateOrder(Order order, StatusOrder statusOrder);

        /// <summary>
        /// we do a thread that send a mail, so we need it in a apart function
        /// </summary>
        /// <param name="order"></param>
        void updateOrder_privateCase_sentMail(Order order);

        /// <summary>
        /// Returns the number of orders sent to the Guest Request
        /// </summary>
        /// <param name="guest">The object we want to calculate is the number of orders sent to it</param>
        /// <returns>int- number of orders which sent to Guest Request</returns>
        int numOfOrdersWhichSent(GuestRequest guest);

        /// <summary>
        /// Finds all units that are vacant within a certain time range
        /// </summary>
        /// <param name="date">Date from which start to check</param>
        /// <param name="numOfDays">The number of days to review begins from the first date</param>
        /// <returns></returns>
        List<HostingUnit> getAvailableUnits(DateTime date, int numOfDays);

        /// <summary>
        /// Calculates the number of days between two dates
        /// </summary>
        /// <param name="firstdate">The first date</param>
        /// <param name="secondate">The second date</param>
        /// <returns>int- number of days between two dates</returns>
        int numOfDaysBetween(DateTime firstdate, DateTime? secondate);

        /// <summary>
        /// Returns the number of bookings sent to a hosting unit
        /// </summary>
        /// <param name="hosting">The hosting Unit we want to calculate is the number of orders sent for it</param>
        /// <returns>int-number of bookings sent to a hosting unit</returns>
        int numOfOrdersWhichsentToUnit(HostingUnit hosting);

        /// <summary>
        /// Number of bookings closed per hosting unit
        /// </summary>
        /// <param name="hosting">The hosting Unit we want to calculate is the number of closed orders</param>
        /// <returns>int-number of bookings sent to a hosting unit</returns>
        int numOfOrdersWhichClosed(HostingUnit hosting);

        void updateCommission(int commissionush);

        /// <summary>
        /// Group guest request by area
        /// </summary>
        /// <returns>Groups of guest requests grouped by area</returns>
        IEnumerable<IGrouping<AreaINCountry, GuestRequest>> groupByArea(Func<GuestRequest, bool> p = null);

        /// <summary>
        /// Group guest request by number of people
        /// </summary>
        /// <returns>Groups of guest requests grouped by number of people</returns>
        IEnumerable<IGrouping<int, GuestRequest>> groupByNumOfPeople(Func<GuestRequest, bool> p = null);

        /// <summary>
        /// Group guest request by Status
        /// </summary>
        /// <returns>Groups of guest requests grouped by status</returns>
        IEnumerable<IGrouping<StatusCostumer, GuestRequest>> groupByStatus(Func<GuestRequest, bool> p = null);

        /// <summary>
        /// Group Host by number of units
        /// </summary>
        /// <returns>>Groups of Hosts grouped by number of units</returns>
        IEnumerable<IGrouping<int, Host>> GroupByNumOfUnit();

        /// <summary>
        /// Group Hosting Unit by number of people
        /// </summary>
        /// <returns>Groups of Hosting Unit grouped by number of people</returns>
        IEnumerable<IGrouping<int, HostingUnit>> GroupHostingUnitByNumOfPeople(Func<HostingUnit, bool> p = null);

        /// <summary>
        /// Group Hosting unit by area
        /// </summary>
        /// <returns>>Groups of Hosting units grouped by area</returns>
        IEnumerable<IGrouping<AreaINCountry, HostingUnit>> GroupByAreaOfUnit(Func<HostingUnit, bool> p = null);

        /// <summary>
        /// Group Hosting unit by number of closed order
        /// </summary>
        /// <returns>>Groups of Hosting units grouped by number of closed order</returns>
        IEnumerable<IGrouping<int, HostingUnit>> GroupByNumOfClosedOrders(Func<HostingUnit, bool> p = null);

        /// <summary>
        /// Group Hosting unit by type of unit
        /// </summary>
        /// <returns>>Groups of Hosting units grouped by type of unit</returns>
        IEnumerable<IGrouping<TypeOfUnit, HostingUnit>> GroupByTypeOfUnits(Func<HostingUnit, bool> p = null);

        /// <summary>
        /// Group Hosting unit by type of unit
        /// </summary>
        /// <returns>>Groups of Hosting units grouped by type of unit</returns>
        IEnumerable<IGrouping<int, BankBranch>> GroupByBankNumberOfBranch(Func<BankBranch, bool> p = null);

        /// <summary>
        /// Calculate number of orders for one hosting unit, and checks who is most populare- he will be the first in the list
        /// </summary>
        /// <returns>Sort list by popular hosting unit</returns>
        List<HostingUnit> MostPopularHostingUnit();

        /// <summary>
        /// Calculates percentage of customer who necessarily want pool
        /// </summary>
        /// <returns>percentage of customer who necessarily want pool</returns>
        double PercentageOfCustomerWhoNecessarilyWantPool();

        /// <summary>
        /// Calculates percentage of customer who necessarily want jaccuzi
        /// </summary>
        /// <returns>percentage of customer who necessarily want jaccuzi</returns>
        double PercentageOfCustomerWhoNecessarilyWantJacuzzi();

        /// <summary>
        /// Calculates percentage of customer who necessarily want garden
        /// </summary>
        /// <returns>percentage of customer who necessarily want garden</returns>
        double PercentageOfCustomerWhoNecessarilyWantGarden();

        /// <summary>
        /// Calculates percentage of customer who necessarily want children attractions
        /// </summary>
        /// <returns>percentage of customer who necessarily want children attractions</returns>
        double PercentageOfCustomerWhoNecessarilyWantChildrensAttractions();

        /// <summary>
        /// Calculates percentage of customer who necessarily want wifi
        /// </summary>
        /// <returns>percentage of customer who necessarily want wifi</returns>
        double PercentageOfCustomerWhoNecessarilyWantWifi();

        /// <summary>
        /// Calculates percentage of customer who necessarily want air conditioner
        /// </summary>
        /// <returns>percentage of customer who necessarily want air conditioner</returns>
        double PercentageOfCustomerWhoNecessarilyWantAirConditioner();

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
        /// Checks which Hosts meet certain conditions
        /// </summary>
        /// <param name="predicat">conditions</param>
        /// <returns>list of Hosts which meet certain conditions</returns>
        List<Host> getHosts(Func<Host, bool> predicat = null);

        /// <summary>
        /// Checks which Hosting units meet certain conditions
        /// </summary>
        /// <param name="predicat">conditions</param>
        /// <returns>list of Hosting units which meet certain conditions</returns>
        List<HostingUnit> getHostingUnits(Func<HostingUnit, bool> predicat = null);

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

        void MonthThread();
    }
}
