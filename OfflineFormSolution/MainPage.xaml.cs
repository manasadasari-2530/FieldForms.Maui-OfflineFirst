using OfflineFormApp.Views;
using Microsoft.Maui.Networking;
using OfflineFormApp.Utils;
using OfflineFormApp.Services;
using System.Threading.Tasks;



namespace OfflineFormSolution
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
            UpdateStatusBanner();
            _ = CheckAndSync();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= OnConnectivityChanged;
        }
        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                UpdateStatusBanner();
                _ = CheckAndSync();
            });
        }

        private async void CheckAndSyncOnAppear()
        {
            await CheckAndSync();
        }

        private async Task CheckAndSync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await SyncService.SyncPendingFormsAsync(); 
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = CheckAndSync();
        }
        private void UpdateStatusBanner()
        {
            bool isOnline = NetworkUtils.IsOnline();
            StatusBanner.Text = isOnline ? "✅ Connected (Online)" : "⚠️ You are Offline";
            StatusBanner.BackgroundColor = isOnline ? Color.FromHex("#DCF7DC") : Color.FromHex("#FFE1E1");
            StatusBanner.TextColor = isOnline ? Color.FromHex("#285228") : Color.FromHex("#872F2F");
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}

        private async void OnStartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DynamicFormPage());
        }

    }

}
