﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ExploreBaseControls.Resources;

namespace ExploreBaseControls
{
    public partial class MainPage : PhoneApplicationPage
    {
        private CurrencyClient client;
        private CrudeOilClient clientOil;

        // Конструктор
        public MainPage()
        {
            InitializeComponent();
            client = new CurrencyClient(this);
            clientOil = new CrudeOilClient(this);
            var thread = new System.Threading.Thread(BackGroundUpdate);
            thread.Start();
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

        public void BackGroundUpdate()
        {
            while (true)
            {
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(30)).Wait();
                Update();
            }
        }
    }
}