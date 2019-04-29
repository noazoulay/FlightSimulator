using FlightSimulator.Model;
using FlightSimulator.Views.Windows;
using System.ComponentModel;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private Server modServ;
        private ICommand setCom;
        private ICommand connCom;

        public FlightBoardViewModel(Server server)
        {
   
            this.modServ = server;
            modServ.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        public ICommand SettingsCommand

        {
            get
            {

                return setCom ?? (setCom = new CommandHandler(() => SettingsClick()));
            }
        }

        public double Lon
        {
            get { return modServ.Lon; }
        }
        public double Lat
        {

            get { return modServ.Lat; }

        }
        private void SettingsClick()

        {

            var settingWin = new Settings();
            settingWin.ShowDialog();
        }
        public ICommand ConnectComm
        {
            get
            {
                return connCom ?? (connCom = new CommandHandler(() => ConnectClick()));
            }
        }
        private void ConnectClick()
        {
            Server.Instance.connectServer();
            ConnectCommand.Instance.ConnectAsClient();
       }
    }

}