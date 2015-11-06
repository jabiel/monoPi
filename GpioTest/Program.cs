using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raspberry.IO.GeneralPurpose;

namespace GpioTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var led1 = ConnectorPin.P1Pin07.Output();

            using (var connection = new GpioConnection(led1))
            {
                for (var i = 0; i < 100; i++)
                {
                    Console.Write(".");
                    connection.Toggle(led1);
                    System.Threading.Thread.Sleep(2000);
                }

                connection.Close();
            }
        }
    }
}
