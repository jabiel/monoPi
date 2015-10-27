﻿using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSelfHosting
{
    /// <summary>
    /// Runs http server using owin self hosting 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //string baseUrl = "http://localhost:5000"; // działa na win
            string baseUrl = "http://*:5000"; // działa na rpi
            using (WebApp.Start<Startup>(baseUrl))
            {
                Console.WriteLine("Press Enter to quit.");
                Console.ReadKey();

            }
        }
    }
}
