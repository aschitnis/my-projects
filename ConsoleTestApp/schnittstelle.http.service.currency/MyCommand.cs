using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace schnittstelle.http.service.currency
{
    // siehe : https://zamjad.wordpress.com/2011/08/04/implement-icommand-interface/

    public class CurrencyConversionCommand : ICommand
    {
        private Action<object> _executeHandler;
        private Func<object,bool> _canExecuteHandler;

        public CurrencyConversionCommand(Action<object> execute,
                                        Func<object,bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Execute cannot be null");
            _executeHandler = execute;
            _canExecuteHandler = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteHandler == null)
            {
                return true;
            }

            return _canExecuteHandler(parameter);
        }

        public void Execute(object parameter)
        {
            _executeHandler(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
