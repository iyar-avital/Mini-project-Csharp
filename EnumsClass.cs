using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum TypeOfUnit { Hotel_room, Zimer, Encampment, Apartment, Sleeping_bag_on_the_sea }
    public enum AreaINCountry { All, North, South, Center }
    public enum StatusOrder { Active, Mail_sent, Closes_out_of_customer_disrespect, Closes_with_customer_response, Irrelevant }
    public enum StatusCostumer { Active, Closed_through_the_site, Closed_because_expired }
    public enum Option { Necessary, possible, uninterested }
    public enum YesOrNot { there_is, there_isnt }
    public enum UserControlProperty    { name, mail, area, number }
}
 