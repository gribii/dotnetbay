using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DotNetBay.Interfaces;
using DotNetBay.Model;

namespace DotNetBay.Data.EF
{
    public class EFMainRepository : IMainRepository
    {
        private readonly MainDbContext dbContext;

        public EFMainRepository()
        {
            this.dbContext = new MainDbContext();
        }

        public Database Database
        {
            get { return this.dbContext.Database; }
        }

        public IQueryable<Auction> GetAuctions()
        {
            return this.dbContext.Auctions.Include(i => i.Bids).Include(i => i.Seller);
        }

        public IQueryable<Member> GetMembers()
        {
            return this.dbContext.Members.Include(m => m.Auctions).Include(a => a.Bids);
        }

        public Auction Add(Auction auction)
        {
            this.dbContext.Auctions.Add(auction);
            return auction;
        }

        public Auction Update(Auction auction)
        {
            return auction;
        }

        public Bid Add(Bid bid)
        {
            this.dbContext.Bids.Add(bid);
            return bid;
        }

        public Bid GetBidByTransactionId(Guid transactionId)
        {
            return this.dbContext.Bids.FirstOrDefault(b => b.TransactionId == transactionId);
        }

        public Member Add(Member member)
        {
            this.dbContext.Members.Add(member);
            return member;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}