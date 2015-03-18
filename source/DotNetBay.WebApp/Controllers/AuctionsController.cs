using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;
using DotNetBay.Model;
using DotNetBay.WebApp.Models;

namespace DotNetBay.WebApp.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly IMainRepository mainRepository;
        private readonly AuctionService service;
        private readonly IMemberService memberService;

        public AuctionsController()
        {
            this.mainRepository = new EFMainRepository();
            this.memberService = new SimpleMemberService(this.mainRepository);
            this.service = new AuctionService(this.mainRepository, this.memberService);
        }

        // GET: Auctions
        public ActionResult Index()
        {
            return View(this.mainRepository.GetAuctions().ToList());
        }

        public ActionResult Image(int id)
        {
            var auction = this.mainRepository.GetAuctions().FirstOrDefault(a => a.Id == id);
            if (auction != null)
            {
                return File(auction.Image, "image/jpeg");
            }

            return this.HttpNotFound();
        }

        // GET: Auctions/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Auctions/Create
        public ActionResult Create()
        {
            return View(new CreateAuctionDto {StartDateTimeUtc = DateTime.UtcNow, EndDateTimeUtc = DateTime.UtcNow.AddDays(10)});
        }

        // POST: Auctions/Create
        [HttpPost]
        public ActionResult Create(CreateAuctionDto auctionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var auction = Mapper.Map<Auction>(auctionDto);
                auction.Seller = this.memberService.GetCurrentMember();

                if (auction.StartDateTimeUtc < DateTime.UtcNow.AddMinutes(1))
                    auction.StartDateTimeUtc = DateTime.UtcNow.AddMinutes(1);

                HttpPostedFileBase file = this.Request.Files["UploadedFile"];

                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    auction.Image = new byte[file.ContentLength];
                    file.InputStream.Read(auction.Image, 0, file.ContentLength);
                }

                this.service.Save(auction);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Auctions/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
