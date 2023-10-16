using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            //HasBeen_Click(0);

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

            uint deviceStateCode = (uint)args.Request.Message["deviceStateAll"];
            

            //ValueSet response = new ValueSet();
            //response.Add("RESULT", "");
            //await args.Request.SendResponseAsync(response);

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                CheckDeviceState_Button(deviceStateCode); 
            });

            // else btn_wifi.Content = "Wifi " + "disable"; 

            //ValueSet response = new ValueSet();
            //response.Add("RESULT", "");
            //await args.Request.SendResponseAsync(response);
        }

        private void CheckDeviceState_Button(uint deviceState)
        {
            if ((deviceState & (uint)Modules.Wifi) == (uint)Modules.Wifi)
            {
                btn_wifi.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
            }
            else 
            {
                btn_wifi.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            }

            if ((deviceState & (uint)Modules.Gobi3G) == (uint)Modules.Gobi3G)
            {
                btn_gobi3G.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
            }
            else
            {
                btn_gobi3G.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            }

            if ((deviceState & (uint)Modules.GPS) == (uint)Modules.GPS)
            {
                btn_GPS.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
            }
            else
            {
                btn_GPS.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            }

            if ((deviceState & (uint)Modules.Bluetooth) == (uint)Modules.Bluetooth)
            {
                btn_bluetooth.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
            }
            else
            {
                btn_bluetooth.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            }

            if ((deviceState & (uint)Modules.WebCamRear) == (uint)Modules.WebCamRear)
            {
                btn_webCamRear.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
            }
            else
            {
                btn_webCamRear.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            }

            if ((deviceState & (uint)Modules.AllLED) == (uint)Modules.AllLED)
            {
                btn_allLED.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
            }
            else
            {
                btn_allLED.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            }

            if ((deviceState & (uint)Modules.Barcode) == (uint)Modules.Barcode)
            {
                btn_barcode.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
            }
            else
            {
                btn_barcode.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            }

            if ((deviceState & (uint)Modules.RFID) == (uint)Modules.RFID)
            {
                btn_RFID.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
            }
            else
            {
                btn_RFID.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            }

            if ((deviceState & (uint)Modules.GPSAntenna) == (uint)Modules.GPSAntenna)
            {
                btn_GPSAntenna.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
            }
            else
            {
                btn_GPSAntenna.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            }

            /*
            foreach (uint m in Enum.GetValues(typeof(Modules)))
            {
                if ((deviceState & m) == (uint)Modules.Wifi)
                {
                    btn_wifi.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                }
                else if ((deviceState & m) == (uint)Modules.Gobi3G)
                {
                    btn_gobi3G.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                }
                else if ((deviceState & m) == (uint)Modules.GPS)
                {
                    btn_GPS.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                }
                else if ((deviceState & m) == (uint)Modules.Bluetooth)
                {
                    btn_bluetooth.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                }
                else if ((deviceState & m) == (uint)Modules.WebCamRear)
                {
                    btn_webCamRear.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                }
                else if ((deviceState & m) == (uint)Modules.AllLED)
                {
                    btn_allLED.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                }
                else if ((deviceState & m) == (uint)Modules.Barcode)
                {
                    btn_barcode.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                }
                else if ((deviceState & m) == (uint)Modules.RFID)
                {
                    btn_RFID.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                }
                else if ((deviceState & m) == (uint)Modules.GPSAntenna)
                {
                    btn_GPSAntenna.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
                }
            }
            */
        }

        private async void HasBeen_Click(uint data)
        {
            ValueSet request = new ValueSet();
            request.Add("deviceConfig", data);
            AppServiceResponse response = await App.Connection.SendMessageAsync(request);

            if(response.Message["res_deviceConfig"] as uint? != null)
            {
                CheckDeviceState_Button((uint)response.Message["res_deviceConfig"]);
            }
        }

        private void btn_wifi_Click(object sender, RoutedEventArgs e)
        {
            HasBeen_Click((uint)Modules.Wifi);



            // display the response key/value pairs
            //tbResult.Text = "";
            // foreach (string key in response.Message.Keys)
            // {
            //  btn_wifi.Content =  "Wifi " + response.Message["res_wifi"].ToString();

            //Image img = new Image();
            //BitmapImage bitmap = new BitmapImage(new Uri("ms-appx:///Assets/G_Wi-Fi.bmp"));
            //img.Source = bitmap;

            //btn_wifi.Background = img;

            //btn_wifi.Background = new ImageBrush
            //{
            //    ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/G_Wi-Fi.bmp")),
            //    Stretch = Stretch.Uniform
            //};

            // }
        }

        private void btn_gobi3G_Click(object sender, RoutedEventArgs e)
        {
            HasBeen_Click((uint)Modules.Gobi3G);
        }

        private void btn_GPS_Click(object sender, RoutedEventArgs e)
        {
            HasBeen_Click((uint)Modules.GPS);
        }

        private void btn_bluetooth_Click(object sender, RoutedEventArgs e)
        {
            HasBeen_Click((uint)Modules.Bluetooth);
        }

        private void btn_webCamRear_Click(object sender, RoutedEventArgs e)
        {
            HasBeen_Click((uint)Modules.WebCamRear);
        }
    }
}
