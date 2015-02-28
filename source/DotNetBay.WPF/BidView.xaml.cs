using System.Windows;
using DotNetBay.Core;
using DotNetBay.Model;

namespace DotNetBay.WPF
{
    /// <summary>
    ///     Interaction logic for BidView.xaml
    /// </summary>
    public partial class BidView : Window
    {
        public BidView(Auction auction)
        {
            this.InitializeComponent();

            this.DataContext = auction;
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonPlaceBid_OnClick(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            var memberService = new SimpleMemberService(app.MainRepository);
            var auctionService = new AuctionService(app.MainRepository, memberService);

            var amount = double.Parse(this.TextBoxBid.Text);
            var member = memberService.GetCurrentMember();
            var auction = this.DataContext as Auction;

            auctionService.PlaceBid(member, auction, amount);
            
            this.Close();
        }
    }
}