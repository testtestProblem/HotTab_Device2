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

            Console.WriteLine("Choose the number you want:\n" +
                "[1] Get device state\n" +
                "[2] Send device state to UWP\n" +
                "[3] Set device state\n" +
                "[4] Parse device state code\n" +
                "[0] Exit");

            uint deviceStateCode = deviceState.GetDeviceStatePower();
            Console.WriteLine("Device state code: " + deviceStateCode.ToString());
            Console.WriteLine(deviceState.ParseDeviceStatePowerCode());
            

            while ((choice = Console.ReadLine()) != "0") 
            {
                if(choice=="1")
                {
                    deviceStateCode = deviceState.GetDeviceStatePower();
                    Console.WriteLine("Device state code: " + deviceStateCode.ToString());
                    Console.WriteLine(deviceState.ParseDeviceStatePowerCode());
                }
                if (choice == "2")
                {
                    deviceStateCode = deviceState.GetDeviceStatePower();
                    Console.WriteLine("Device state code: " + deviceStateCode.ToString());
                    Console.WriteLine(deviceState.ParseDeviceStatePowerCode());

                    connect2UWP.SendData2UWP(deviceStateCode);
                }
                else if(choice == "3")
                {
                    string temp = Console.ReadLine();

                    deviceState.SetDeviceStatePower((uint)Convert.ToInt32(temp));
                    Console.WriteLine("OK");
                }
                else if (choice == "4")
                {
                    Console.WriteLine(deviceState.ParseDeviceStatePowerCode());
                }
            }
        }

        
    }

    
}
