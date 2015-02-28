using System;
using System.IO;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Model;
using Microsoft.Win32;

namespace DotNetBay.WPF
{
    /// <summary>
    ///     Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView
    {
        public SellView()
        {
            this.InitializeComponent();
        }

        private void ButtonAddAuction_OnClick(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            var memberService = new SimpleMemberService(app.MainRepository);
            var auctionService = new AuctionService(app.MainRepository, memberService);

            var title = this.TextBoxTitle.Text;
            var desc = this.TextBoxDescription.Text;
            var startPrice = double.Parse(this.TextBoxStartPrice.Text);
            var startDate = this.DatePickerStart.Value.Value;
            var endDate = this.DatePickerEnd.Value.Value;
            var imagePath = this.TextBoxImagePath.Text;
            var imageData = this.ReadImage(imagePath);

            auctionService.Save(new Auction
            {
                Title = title,
                Description = desc,
                StartPrice = startPrice,
                StartDateTimeUtc = startDate,
                EndDateTimeUtc = endDate,
                Image = imageData,
                IsClosed = false,
                IsRunning = false,
                Seller = memberService.GetCurrentMember()
            });

            this.Close();
        }

        private byte[] ReadImage(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }
            catch
            {
                return null;
            }
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonImageMore_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog {CheckFileExists = true, CheckPathExists = true};
            if (!string.IsNullOrEmpty(this.TextBoxImagePath.Text))
            {
                dialog.InitialDirectory = Path.GetDirectoryName(this.TextBoxImagePath.Text);
            }

            if (dialog.ShowDialog() == null)
            {
                this.TextBoxImagePath.Text = dialog.SafeFileName;
            }
        }
    }
}