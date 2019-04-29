using FlightSimulator.ViewModels;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace FlightSimulator.Model
{
    public class Server : BaseNotify

    {
        TcpClient client;
        TcpListener listener;
        double lon, lat;
        private static Server inst = null;
        public static Server Instance
        {
            get
            {
                if (inst == null)
                {
                    inst = new Server();
                }
                return inst;
            }
        }

        private Server()
        {
            isStop = false;
        }

        public bool isStop
        {
            set;
            get;
        }

        public double Lon
        {
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
            get { return lon; }
        }

        public double Lat
        {
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
            get { return lat; }

        }
        public void connectServer()

        {

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
                ApplicationSettingsModel.Instance.FlightInfoPort);
            listener = new TcpListener(ep);
            listener.Start();
            client = listener.AcceptTcpClient();
            Thread thread = new Thread(() => listen(client, listener));
            thread.Start();

        }


        public void listen(TcpClient client, TcpListener listener)

        {

            Byte[] bytes;

            NetworkStream ns = client.GetStream();
            while (!isStop)
            {
                if (client.ReceiveBufferSize > 0)

                {
                    bytes = new byte[client.ReceiveBufferSize];
                    ns.Read(bytes, 0, client.ReceiveBufferSize);
                    string msg = Encoding.ASCII.GetString(bytes);
                    Console.WriteLine("msg" + msg + "\n");
                    splitMessage(msg);
                }
            }
            ns.Close();
            client.Close();
            listener.Stop();

        }

        public void splitMessage(string msg)
        {
            string[] splitMs = msg.Split(',');

            if (msg.Contains(","))
            {
                Lon = double.Parse(splitMs[0]);
                Lat = double.Parse(splitMs[1]);

            }

        }



        public void DisConnect()

        {
            isStop = true;
        }

        public bool isConnected()

        {

            return (listener != null);

        }



    }
}

    


