using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace CollectDataAP
{
    class Program
    {
        static void Main(string[] args)
        {
            Tommy.Tommy_Start();

            DeviceState deviceState = new DeviceState();
            Connect2UWP connect2UWP = new Connect2UWP();

            connect2UWP.InitializeAppServiceConnection();

            string choice = "";

            Console.WriteLine("Choose the number you want:\n[1] Get device state\n[2] Set device state\n[0] Exit");
            while ((choice = Console.ReadLine()) != "0") 
            {
                if(choice=="1")
                {
                    uint deviceStateCode = deviceState.GetDeviceStatePower();
                    Console.WriteLine("Device state code: " + deviceStateCode.ToString());
                    Console.WriteLine(deviceState.ParseDeviceStatePowerCode());

                    connect2UWP.SendData2UWP(deviceStateCode);
                }
                else if(choice == "2")
                {
                    string temp = Console.ReadLine();

                    deviceState.SetDeviceStatePower((uint)Convert.ToInt32(temp));
                    Console.WriteLine("OK");
                }
                else if (choice == "3")
                {
                    Console.WriteLine(deviceState.ParseDeviceStatePowerCode());
                }
            }
        }

        
    }

    
}
