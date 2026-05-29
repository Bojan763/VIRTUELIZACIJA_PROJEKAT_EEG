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
            ServiceHost host = new ServiceHost(new EEGService());
            try
            {
                host.Open();
                Console.WriteLine("EEG Service started.");
                Console.ReadLine();
                host.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                host.Abort();
            }
        }
    }
}
