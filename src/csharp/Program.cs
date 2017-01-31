using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;


namespace EnergyStatusServer
{
    public class Program 
    {
        private NancyHost webServer;
        private StatusReceiver statusReceiver;

        public Program()
        {
            StatusCacheResource.init();

            var webModuleHelper = typeof( StatusWebModule);

            HostConfiguration nancyHostConfig = new HostConfiguration();
            nancyHostConfig.UrlReservations = new UrlReservations() { CreateAutomatically = true };

            webServer = new NancyHost(nancyHostConfig, new Uri("http://localhost:1234"));
            
            statusReceiver = new StatusReceiver();
        }

        ~Program()
        {
            webServer.Stop();
        }
        
        public void startServer()
        {
            statusReceiver.startLoop();
            webServer.Start();
        }

       
        static void Main(string[] args)
        {
            Console.WriteLine("Starting the Server");

            Program theProgram = new Program();

            theProgram.startServer();

            Console.ReadKey();
        }
    }
}
