using NBD6.Models;
using System.ComponentModel.DataAnnotations;

namespace NBD6.Views.Bids
{

    public class StaffBid
    {
        // Foreign keys
        public int StaffID { get; set; }
        public Staff Staff { get; set; }

        public int BidID { get; set; }
        public Bid Bid { get; set; }
    }
}
