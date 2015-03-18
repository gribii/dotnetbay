using AutoMapper;
using DotNetBay.Model;
using DotNetBay.WebApp.Models;

namespace DotNetBay.WebApp.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<CreateAuctionDto, Auction>();
        }
    }
}