using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class BindJoystick
    {
        static BindJoystick instance = null;
        private BindJoystick() { }
        public static BindJoystick getInstance()
        {
            if (instance == null)
            {
                instance = new BindJoystick();
            }
            return instance;
        }
        public float RudderCommand
        {
            set
            {
                string rudderLine = "set controls/flight/rudder " + value + "\r\n";
                ConnectCommand.Instance.Send(rudderLine);
            }
        }
        public double ElevatorCommand
        {
            set
            {
                string ElevatorLine = "set /controls/flight/elevator " + value + "\r\n";
                ConnectCommand.Instance.Send(ElevatorLine);
            }
        }
        public float ThrottleCommand
        {
            set
            {
                string throttleLine = "set controls/engines/current-engine/throttle " + value + "\r\n";
                ConnectCommand.Instance.Send(throttleLine);
            }
        }
        
        public double AileronCommand
        {
            set
            {
                string AileronLine = "set /controls/flight/aileron " + value + "\r\n";
                ConnectCommand.Instance.Send(AileronLine);
            }
        }
        
    }
}
