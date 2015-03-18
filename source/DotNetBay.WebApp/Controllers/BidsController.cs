using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;
using DotNetBay.WebApp.Infrastructure;
using DotNetBay.WebApp.Models;

namespace DotNetBay.WebApp.Controllers
{
    public class BidsController : Controller
    {
        private readonly IMainRepository mainRepository;
        private readonly IMemberService memberService;
        private readonly AuctionService service;

        public BidsController()
        {
            this.mainRepository = new EFMainRepository();
            this.memberService = new SimpleMemberService(this.mainRepository);
            this.service = new AuctionService(this.mainRepository, this.memberService);
        }

        // GET: Bids/Create
        public ActionResult Create(int id)
        {
            var auction = this.mainRepository.GetAuctions().FirstOrDefault(a => a.Id == id);
            if (auction == null)
            {
                return this.HttpNotFound();
            }

            return View(new CreateBidDto
            {
                AuctionTitle = auction.Title,
                AuctionDescription = auction.Description,
                StartPrice =
                    auction.StartPrice,
                CurrentPrice = auction.CurrentPrice,
                Bid = auction.CurrentPrice + 1
            });
        }

        // POST: Bids/Create
        [HttpPost]
        public ActionResult Create(int id, CreateBidDto bidDto)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View();
                }

                var auction = this.mainRepository.GetAuctions().FirstOrDefault(a => a.Id == id);
                if (auction == null) return this.HttpNotFound();
                if (!auction.IsRunning) return new HttpForbiddenResult();
                if (bidDto.Bid <= auction.CurrentPrice) return new HttpForbiddenResult(); // TODO: Notify user about bid that is too low

                this.service.PlaceBid(this.memberService.GetCurrentMember(), auction, bidDto.Bid);

                return RedirectToAction("Index", "Auctions");
            }
            catch
            {
                return View();
            }
        }
    }
}
