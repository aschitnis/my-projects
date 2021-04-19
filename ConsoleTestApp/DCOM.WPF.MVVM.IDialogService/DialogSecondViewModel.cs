using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCOM.WPF.MVVM.IDialogService
{
    using System;
    using System.Windows.Input;
    public class DialogSecondViewModel : IDialogRequestClose
    {
        public DialogSecondViewModel(string message)
        {
            Message = message;
            OkCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public string Message { get; }
        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }
    }
}

