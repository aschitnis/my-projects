namespace Wpf.Test.my.books.management.MVVM.Dialog
{
    using System;

    public class DialogCloseRequestedEventArgs : EventArgs
    {
        public DialogCloseRequestedEventArgs(DialogCloseResult dialogResult)
        {
            DialogCloseInformation = dialogResult;
        }

        public DialogCloseResult DialogCloseInformation { get; }
    }

    public class DialogCloseResult
    {
        public DialogCloseResult(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }

        public string DialogResultMessage { get; set; } = null;
        public bool? DialogResult { get; }
    }
}