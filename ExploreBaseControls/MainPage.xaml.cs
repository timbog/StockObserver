using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ExploreBaseControls.Resources;
using Microsoft.Phone.Net.NetworkInformation;
using System.Collections.ObjectModel;
using Microsoft.Phone.Tasks;
using System.Windows.Threading;

namespace ExploreBaseControls
{
    public partial class MainPage : PhoneApplicationPage
    {
        private CurrencyClient client;
        private CrudeOilClient clientOil;
        private bool messageShown;
        private DispatcherTimer dispatcherTimer;

        public ObservableCollection<string> Changes { get; set; }

        public ObservableCollection<string> NetworkInterfaces { get; set; }

        // Конструктор
        public MainPage()
        {
            InitializeComponent();
            messageShown = false;
            client = new CurrencyClient(this);
            clientOil = new CrudeOilClient(this);
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(30);
            dispatcherTimer.Start();
        }

        public void UpdateCurrency()
        {
            float rublesInDollar = client.Parse("USD-RUB");
            float yuanesInDollar = client.Parse("USD-CNY");
            float jenesInDollar = client.Parse("USD-JPY");
            float dollarsInEuro = client.Parse("EUR-USD");
            float dollarsInPound = client.Parse("GBP-USD");
            Deployment.Current.Dispatcher.BeginInvoke(() => dollarBox.Text = (rublesInDollar.ToString()));
            Deployment.Current.Dispatcher.BeginInvoke(() => euroBox.Text = ((client.Parse("EUR-USD") * rublesInDollar).ToString()));
            Deployment.Current.Dispatcher.BeginInvoke(() => yuanBox.Text = (yuanesInDollar.ToString()));
            Deployment.Current.Dispatcher.BeginInvoke(() => jenaBox.Text = (jenesInDollar.ToString()));
            Deployment.Current.Dispatcher.BeginInvoke(() => dollarBox2.Text = (dollarsInEuro.ToString()));
            Deployment.Current.Dispatcher.BeginInvoke(() => dollarBox2.Text = (dollarsInEuro.ToString()));
            Deployment.Current.Dispatcher.BeginInvoke(() => dollarBox3.Text = (dollarsInPound.ToString()));
        }
        public void UpdateOil()
        {
            float WTIInDollar = clientOil.Parse("CL1:COM");
            float BrentInDollar = clientOil.Parse("CO1:COM");
            Deployment.Current.Dispatcher.BeginInvoke(() => WTIBox.Text = (WTIInDollar.ToString()));
            Deployment.Current.Dispatcher.BeginInvoke(() => BrentBox.Text = (BrentInDollar.ToString()));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            client.UpdateInfo();
            clientOil.UpdateInfo();
        }

        public void ShowError()
        {
            if (!messageShown)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show("Нет подключения к Internet",
                    "Ошибка", MessageBoxButton.OKCancel));
            }
            messageShown = !messageShown;
        }

        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Update();
        }
    }
}