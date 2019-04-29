using System;
using System.Windows.Input;

namespace FlightSimulator.Model
{
    public class CommandHandler : ICommand

    {
        private Action action;

        public CommandHandler(Action a)

        {
          action = a;

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
        

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)

        {
            action();

        }
 
    }
}
