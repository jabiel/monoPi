using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace SerialPortTest
{
    /// <summary>
    /// Comunicatation between rpi and arduino connected threw usb cable
    /// </summary>
    class Program
    {
        static SerialPort sp;
        public static void Main(string[] args)
        {
            sp = new SerialPort("/dev/ttyUSB0", 9600, Parity.None, 8, StopBits.One);
            sp.Open();

            Thread.Sleep(200);

            Console.WriteLine(">" + ReadLine());

            for (var i = 0; i < 3; i++)
            {
                string result = string.Format("Test {0}", i);
                sp.WriteLine(result);
                Console.WriteLine("<" + result);


                Thread.Sleep(200);

                var ss = ReadLine();
                Console.WriteLine(">" + ss);
            }

            sp.Close();
        }

        public static string ReadLine()
        {
            string rxString = "";
            byte prevb = 0;
            var b = (byte)sp.ReadByte();

            while (b != 10 || prevb != 13) // 1310 EOL
            {
                //Console.WriteLine(":"+b+" " + (char)b);
                rxString += ((char)b);
                prevb = b;
                b = (byte)sp.ReadByte();
            }

            return rxString;
        }
		
		/// <summary>
        /// From http://stackoverflow.com/questions/434494/serial-port-rs232-in-mono-for-multiple-platforms
        /// </summary>
        /// <returns></returns>
        private static List<string> GetPortNames()
        {
            int p = (int)Environment.OSVersion.Platform;
            List<string> serial_ports = new List<string>();

            // Are we on Unix?
            if (p == 4 || p == 128 || p == 6)
            {
                string[] ttys = System.IO.Directory.GetFiles("/dev/", "tty*");
                foreach (string dev in ttys)
                {
                    //Arduino MEGAs show up as ttyACM due to their different USB<->RS232 chips
                    if (dev.StartsWith("/dev/ttyS") || dev.StartsWith("/dev/ttyUSB") || dev.StartsWith("/dev/ttyACM"))
                    {
                        serial_ports.Add(dev);
                    }
                }
            }
            else
            {
                serial_ports.AddRange(SerialPort.GetPortNames());
            }

            return serial_ports;
        }
        


    }
}
