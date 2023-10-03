﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static HotTab_Device2.DeviceState;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HotTab_Device2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();


        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                App.AppServiceConnected += MainPage_AppServiceConnected;
                App.AppServiceDisconnected += MainPage_AppServiceDisconnected;
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
            }
        }
         
        /// <summary>
        /// When the desktop process is disconnected, reconnect if needed
        /// </summary>
        private async void MainPage_AppServiceDisconnected(object sender, EventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // disable UI to access the connection
                //btnRegKey.IsEnabled = false;

                // ask user if they want to reconnect
                Reconnect();
            });
        }

        /// <summary>
        /// Ask user if they want to reconnect to the desktop process
        /// </summary>
        private async void Reconnect()
        {
            if (App.IsForeground)
            {
                MessageDialog dlg = new MessageDialog("Connection to desktop process lost. Reconnect?");
                UICommand yesCommand = new UICommand("Yes", async (r) =>
                {
                    await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
                });
                dlg.Commands.Add(yesCommand);
                UICommand noCommand = new UICommand("No", (r) => { });
                dlg.Commands.Add(noCommand);
                await dlg.ShowAsync();
            }
        }

        /// <summary>
        /// When the desktop process is connected, get ready to send/receive requests
        /// </summary>
        private async void MainPage_AppServiceConnected(object sender, AppServiceTriggerDetails e)
        {
            App.Connection.RequestReceived += AppServiceConnection_RequestReceived;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                //enable UI to access the connection
                //btnRegKey.IsEnabled = true;
            });
        }

        private async void AppServiceConnection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            //double d1 = (double)args.Request.Message["D1"];
            //double d2 = (double)args.Request.Message["D2"];
            //double result = d1 + d2;

            //ValueSet response = new ValueSet();
            //response.Add("RESULT", result);
            //await args.Request.SendResponseAsync(response);

            //// log the request in the UI for demo purposes
            //await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            //{
            //    //tbRequests.Text += string.Format("Request: {0} + {1} --> Response = {2}\r\n", d1, d2, result);
            //});
            uint deviceState = (uint)args.Request.Message["deviceStateAll"];

            ValueSet response = new ValueSet();
            response.Add("RESULT", "");
            await args.Request.SendResponseAsync(response);

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {/*
                if ((deviceState & (uint)Modules.Wifi) == (uint)Modules.Wifi)
                {
                    btn_wifi.Background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(@"/Assets/device/enable/G_Wi-Fi.bmp", UriKind.RelativeOrAbsolute)),
                        Stretch = Stretch.Uniform
                    };
                }
                else if ((deviceState & (uint)Modules.Gobi3G) == (uint)Modules.Gobi3G)
                {
                    btn_gobi3G.Background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(@"/Assets/device/enable/G_3G.bmp", UriKind.RelativeOrAbsolute)),
                        Stretch = Stretch.Uniform
                    };
                }
                else if ((deviceState & (uint)Modules.GPS) == (uint)Modules.GPS)
                {
                    btn_GPS.Background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(@"/Assets/device/enable/G_GPS.bmp", UriKind.RelativeOrAbsolute)),
                        Stretch = Stretch.Uniform
                    };
                }
                else if ((deviceState & (uint)Modules.Bluetooth) == (uint)Modules.Bluetooth)
                {
                    btn_bluetooth.Background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(@"/Assets/device/enable/G_blueTooth.bmp", UriKind.RelativeOrAbsolute)),
                        Stretch = Stretch.Uniform
                    };
                }*/
            });

            // else btn_wifi.Content = "Wifi " + "disable"; 

            //ValueSet response = new ValueSet();
            //response.Add("RESULT", "");
            //await args.Request.SendResponseAsync(response);
        }

        private async void btn_wifi_Click(object sender, RoutedEventArgs e)
        {
            ValueSet request = new ValueSet();
            request.Add("deviceConfig", "wifi");
            AppServiceResponse response = await App.Connection.SendMessageAsync(request);

            // display the response key/value pairs
            //tbResult.Text = "";
           // foreach (string key in response.Message.Keys)
           // {
               btn_wifi.Content =  "Wifi " + response.Message["res_wifi"].ToString();

            btn_wifi.Background = null;
           // }
        }
    }
}
