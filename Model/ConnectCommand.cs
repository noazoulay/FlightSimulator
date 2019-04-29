using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class ConnectCommand
    {
        TcpClient client;
        private bool isConnected;
        private Mutex mutex;
        private static ConnectCommand instance = null;
        public static ConnectCommand Instance

        {
            get
            {
                if (instance == null)
                {
                    instance = new ConnectCommand();
                }
         
                return instance;
            }
        }


        public bool IsConnected

        {

            get;

            set;

        }



        private ConnectCommand()

        {

            isConnected = false;

            mutex = new Mutex();

        }


        public void ConnectAsClient()

        {

            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),

                ApplicationSettingsModel.Instance.FlightCommandPort);

            client = new TcpClient();

            client.Connect(iPEndPoint);

            isConnected = true;

            Console.WriteLine("Connection Succeeded!");



        }

        public void Send(string message)

        {

            string[] commands = ParseMessage(message);

            mutex.WaitOne();

            Thread thread = new Thread(() => RunSend(commands, client));

            thread.Start();

            mutex.ReleaseMutex();


        }

        public void DisConnect()

        {

            isConnected = false;

            client.Close();

        }

        private string[] ParseMessage(string message)

        {

            string[] commands;

            return commands = message.Split(new[] { Environment.NewLine },

            StringSplitOptions.None);

        }



        private void RunSend(string[] commands, TcpClient client)

        {

            if (!isConnected) return;

            NetworkStream stream = client.GetStream();



            foreach (string command in commands)

            {

                Byte[] data = Encoding.ASCII.GetBytes(command + "\r\n");

                stream.Write(data, 0, data.Length);

                Thread.Sleep(2000);

            }

        }



        

    }
}

