using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CollectDataAP
{
    class DeviceState
    {
        [DllImport(@"WMIO2.dll")]
        public static extern bool SetDevice(byte uiValue);

        [DllImport(@"WMIO2.dll")]
        public static extern bool GetDevice(out byte uiValue);

        [DllImport(@"WMIO2.dll")]
        public static extern bool SetDevice2(byte uiValue);

        [DllImport(@"WMIO2.dll")]
        public static extern bool GetDevice2(out byte uiValue);


        private enum deviceState { };

        //0: The device power off     1: The device power on
        //lowwer byte
        //Bit7      Bit6        Bit5        Bit4        Bit3        Bit2    Bit1            Bit0
        //AllLED    WebCamRear  Bluetooth   -           -           GPS     Gobi3G          Wifi
        //upper byte
        //Bit7      Bit6        Bit5        Bit4        Bit3        Bit2    Bit1            Bit0
        //-         -           Expand COM  Expand USB  GPS Antenna RFID    WebCam Front    Barcode
        public uint GetDeviceState()
        {
            byte temp;
            uint devicestate = 0;

            GetDevice(out temp);
            devicestate =  temp;

            GetDevice2(out temp);
            devicestate += (((uint)temp) << 8);

            return devicestate;
        }

        //TODO...
        public string ParseDeviceStateCode(uint data)
        {
            string deviceState = "";

            if ((data & 0x0001) == 0x0001) deviceState += "";


                return deviceState;
        }

        //TODO: do more personalize
        public bool SetDeviceState(uint data)
        {
            byte data1 = (byte)data;
            byte data2 = (byte)(data>>8);

            if(SetDevice(data1) == false) return false;
            if(SetDevice2(data2) == false) return false;

            return true;
        }

    }
}
