using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Model;

namespace DotNetBay.WPF
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly App app = Application.Current as App;
        private readonly ObservableCollection<Auction> auctions = new ObservableCollection<Auction>();
        private readonly IAuctionService auctionService;
        private readonly object auctionsLock = new object();

        public MainWindow()
        {
            // Make sure collection can be accessed from other threads
            // This is used because we will update the collection after an event was raised.
            BindingOperations.EnableCollectionSynchronization(this.auctions, this.auctionsLock);

            var memberService = new SimpleMemberService(this.app.MainRepository);
            this.auctionService = new AuctionService(this.app.MainRepository, memberService);

            this.InitializeComponent();
            this.DataContext = this;

            this.UpdateAuctions();

            // register to auctioneer events
            this.app.AuctionRunner.Auctioneer.BidAccepted += this.AuctioneerOnBidAccepted;
            this.app.AuctionRunner.Auctioneer.BidDeclined += this.AuctioneerOnBidDeclined;
            this.app.AuctionRunner.Auctioneer.AuctionStarted += this.AuctioneerOnAuctionStarted;
            this.app.AuctionRunner.Auctioneer.AuctionEnded += this.AuctioneerOnAuctionEnded;
        }

        private void AuctioneerOnAuctionEnded(object sender, AuctionEventArgs auctionEventArgs)
        {
            Debug.WriteLine("Auction ended");
            this.UpdateAuctions();
        }

        public ObservableCollection<Auction> Auctions
        {
            get { return this.auctions; }
        }

        private void AuctioneerOnAuctionStarted(object sender, AuctionEventArgs auctionEventArgs)
        {
            Debug.WriteLine("Auction started");
            this.UpdateAuctions();
        }

        private void AuctioneerOnBidDeclined(object sender, ProcessedBidEventArgs processedBidEventArgs)
        {
        }

        private void AuctioneerOnBidAccepted(object sender, ProcessedBidEventArgs processedBidEventArgs)
        {
            this.UpdateAuctions();
        }

        private void UpdateAuctions()
        {
            lock (this.auctionsLock)
            {
                this.auctions.Clear();
                foreach (var a in this.auctionService.GetAll()) this.auctions.Add(a);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var view = new BidView(((FrameworkElement) sender).DataContext as Auction);
            view.ShowDialog();
        }

        private void ButtonAddAuction_OnClick(object sender, RoutedEventArgs e)
        {
            var view = new SellView();
            view.ShowDialog();
            this.UpdateAuctions();
        }
    }
}