using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace schnittstelle.mysql.db.baeumer.services
{
    // siehe : https://zamjad.wordpress.com/2011/08/04/implement-icommand-interface/

    public class MyCommand : ICommand
    {
        private Action _Function;
        private Func<bool> _Predicate;
        public MyCommand(Action function)
        {
            _Function = function;
        }

        public MyCommand(Action function, Func<bool> predicate)
        {
            _Function = function;
            _Predicate = predicate;
        }

        public bool CanExecute(object parameter)
        {
            if (_Predicate != null)
            {
                return _Predicate();
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if (_Function != null)
            {
                _Function();
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
