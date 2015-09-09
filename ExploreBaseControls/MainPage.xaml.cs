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

namespace ExploreBaseControls
{
    public partial class MainPage : PhoneApplicationPage
    {
        private CurrencyClient client;

        // Конструктор
        public MainPage()
        {
            InitializeComponent();
            client = new CurrencyClient(this);
            // Пример кода для локализации ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        public void Update()
        {
            float rublesInDollar = client.Parse("RUB");
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            client.UpdateInfo();
        }



        // Пример кода для сборки локализованной панели ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Установка в качестве ApplicationBar страницы нового экземпляра ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Создание новой кнопки и установка текстового значения равным локализованной строке из AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Создание нового пункта меню с локализованной строкой из AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}