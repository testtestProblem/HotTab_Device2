using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace CollectDataAP
{
    class Program
    {
        static void Main(string[] args)
        {
            Tommy_Start();

            DeviceState deviceState = new DeviceState();
            uint deviceStateData = 0;
            string choice = "";

            Console.WriteLine("Choose the number you want:\n[1] Get device state\n[2] Set device state\n[0] Exit");
            while ((choice = Console.ReadLine()) != "0") 
            { 
                if(choice=="1")
                {
                    deviceStateData = deviceState.GetDeviceState();
                    Console.WriteLine("Device state code: " + deviceStateData.ToString());
                }
                else if(choice == "2")
                {
                    string temp = Console.ReadLine();

                    deviceState.SetDeviceState((uint)Convert.ToInt32(temp));
                    Console.WriteLine("OK");
                }
            }
        }

        static public void Tommy_Start()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("|   ========     =====     |\\   /|    |\\   /|    \\\\    //  |");
            Console.WriteLine("|      ||       //   \\\\    |\\   /|    |\\   /|     \\\\  //   |");
            Console.WriteLine("|      ||       ||   ||    | \\ / |    | \\ / |      \\\\//    |");
            Console.WriteLine("|      ||       \\\\   //    |  |  |    |  |  |       ||     |");
            Console.WriteLine("|      ||        =====     |  |  |    |  |  |       ||     |");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    class Connect2UWP
    {
        private AppServiceConnection connection = null;

        public async void InitializeAppServiceConnection()
        {
            connection = new AppServiceConnection();
            connection.AppServiceName = "SampleInteropService";
            connection.PackageFamilyName = Package.Current.Id.FamilyName;
            //connection.RequestReceived += Connection_RequestReceived;
            //connection.ServiceClosed += Connection_ServiceClosed;

            AppServiceConnectionStatus status = await connection.OpenAsync();
            if (status != AppServiceConnectionStatus.Success)
            {
                // something went wrong ...
                Console.WriteLine(status.ToString());
                Console.ReadLine();
                //this.IsEnabled = false;
            }
        }

        public async void SendData2UWP(string data)
        {
            // ask the UWP to calculate d1 + d2
            ValueSet request = new ValueSet();
            request.Add("D1", (string)data);
            //request.Add("D2", (double)2);
            AppServiceResponse response = await connection.SendMessageAsync(request);
            //double result = (double)response.Message["RESULT"];
        }


    }
}
