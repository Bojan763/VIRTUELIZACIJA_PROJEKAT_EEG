using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;

namespace Service
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(EEGService));
            host.Open();
            Console.WriteLine("EEG Service started, press any key to close it.");
            Console.ReadKey();

            host.Close();
            Console.WriteLine("EEG Service is closed");
        }
    }
}
