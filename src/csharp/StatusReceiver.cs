using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Energy;

namespace EnergyStatusServer
{
    class StatusReceiver
    {
        private StatusCacheWriter cache;
        private Thread receiverThread;
        private volatile bool shouldStop = false;

        public StatusReceiver( )
        {
            cache = StatusCacheResource.getWriter();
        }

        ~StatusReceiver()
        {
            shouldStop = true;
        }

        public void startLoop()
        {
            shouldStop = true;
            receiverThread = new Thread(this.readStatusInfoFromClients);

            shouldStop = false;
            receiverThread.Start();
        }

        public void readStatusInfoFromClients()
        {
            using (var netMQServer = new NetMQ.Sockets.RouterSocket("@tcp://*:5559"))
            {
                Int32 i = 0;

                while (!shouldStop)
                {
                    var identity = NetMQ.ReceivingSocketExtensions.ReceiveFrameBytes(netMQServer);
                    var envelope = NetMQ.ReceivingSocketExtensions.ReceiveFrameString(netMQServer);
                    var payload = NetMQ.ReceivingSocketExtensions.ReceiveFrameBytes(netMQServer);
                    Energy.Status status = Energy.Status.Parser.ParseFrom(payload);

                    var identityString = System.Text.Encoding.Default.GetString(identity);

                    cache.insertItem(status.Id, status);

                    //Console.WriteLine(identity + " has sent id " + status.Id);

                    NetMQ.OutgoingSocketExtensions.SendMoreFrame(netMQServer, identity);
                    NetMQ.OutgoingSocketExtensions.SendMoreFrame(netMQServer, "");
                    NetMQ.OutgoingSocketExtensions.SendFrame(netMQServer, "work harder");

                    i++;

                    if (i % 10000 == 0)
                    {
                        Console.WriteLine("Received " + i + " Messages");
                    }
                }

            }
        }


    }



}
