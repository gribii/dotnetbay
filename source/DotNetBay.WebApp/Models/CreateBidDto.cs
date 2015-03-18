using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetBay.WebApp.Models
{
    public class CreateBidDto
    {
        [DisplayName("Title")]
        public string AuctionTitle { get; set; }

        [DisplayName("Description")]
        public string AuctionDescription { get; set; }

        [DisplayName("Start price")]
        public double StartPrice { get; set; }

        [DisplayName("Current price")]
        public double CurrentPrice { get; set; }

        [DisplayName("Your bid")]
        [DataType(DataType.Currency)]
        public double Bid { get; set; }
    }
}