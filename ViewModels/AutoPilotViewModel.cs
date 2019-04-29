using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify

    {
        private ICommand okCommand;
        private ICommand clearCommand;
        private String con = "";

        private bool isSend = false;

     
        public String Content
        {
          
            get {
                return con; }
            set
            {
                con = value;
                NotifyPropertyChanged("Content");
                NotifyPropertyChanged("BackgroundColor");
            }
       }
        public String BackgroundColor
        {
            get
            {
                if (con != "")
                {
                    if (isSend)
                    {
                        isSend = false;
                        return "White";
                    }
                   return "Pink";
                }
                else
                {
                    return "White";
                }
            }
        }
        
        public ICommand ClearCommand
        {
            get
            {
                 return clearCommand ?? (clearCommand = new CommandHandler(() => ClearClick()));
            }
        }


        private void ClearClick()

        {
            con = "";

            NotifyPropertyChanged("Content");

            NotifyPropertyChanged("BackgroundColor");

        }

        private void OKClick()

        {
            ConnectCommand.Instance.Send(con);
            isSend = true;
            NotifyPropertyChanged("BackgroundColor");
        }
     
        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new CommandHandler(() => OKClick()));
            }
        }
     
    }
}
