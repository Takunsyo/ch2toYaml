using System;
using System.Windows.Input;

namespace ch22yaml.Data
{
    public class CustonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action Command { get;}

        private bool _CanExecute = true;

        public void SetCanExecute(bool value)
        {
            _CanExecute = value;
        }

        public bool CanExecute(object parameter)
        {
            return _CanExecute;
        }

        public void Execute(object parameter)
        {
            Command();
        }

        public CustonCommand(Action cmd)
        {
            this.Command = cmd;
        }
    }
}
