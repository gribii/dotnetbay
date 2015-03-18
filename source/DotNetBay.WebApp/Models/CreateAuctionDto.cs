using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetBay.WebApp.Models
{
    public class CreateAuctionDto
    {
        [StringLength(60, MinimumLength = 3)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Start Price")]
        [Range(1, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal CurrentPrice { get; set; }

        [DisplayName("Description")]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        public string Description { get; set; }

        [DisplayName("Start Date and Time (UTC)")]
        [DataType(DataType.DateTime)]
        public DateTime StartDateTimeUtc { get; set; }

        [DisplayName("End Date and Time (UTC)")]
        [DataType(DataType.DateTime)]
        public DateTime EndDateTimeUtc { get; set; }
    }
}