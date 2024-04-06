using System.ComponentModel.DataAnnotations;

namespace NBD6.Models
{

    public class StaffBid
    {
        //FK
        public int StaffID { get; set; }
        public Staff Staff { get; set; }

        public int BidID { get; set; }
        public Bid Bid { get; set; }
    }
}
