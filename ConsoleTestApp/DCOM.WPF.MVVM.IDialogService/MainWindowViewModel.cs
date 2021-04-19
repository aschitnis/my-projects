namespace DCOM.WPF.MVVM.IDialogService
{
    using MVVM;
    using System.Windows.Input;

    public class MainWindowViewModel
    {
        private readonly IDialogService dialogService;

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            DisplayDialogWindowCommand = new ActionCommand(p => DisplayDialogWindow());
            DisplaySecondDialogWindowCommand = new ActionCommand(p => DisplaySecondDialogWindow());
        }

        public ICommand DisplayDialogWindowCommand { get; }
        public ICommand DisplaySecondDialogWindowCommand { get; }

        private void DisplayDialogWindow()
        {
            var viewModel = new DialogViewModel("Hello!");

            bool? result = dialogService.ShowDialog(viewModel);
            
            if (result.HasValue)
            {
                if (result.Value)
                {
                    // Accepted
                }
                else
                {
                    // Cancelled
                }
            }
        }

        private void DisplaySecondDialogWindow()
        {
            var viewModel = new DialogSecondViewModel("ZWEITE DIALOG");

            bool? result = dialogService.ShowDialog(viewModel);

            if (result.HasValue)
            {
                if (result.Value)
                {
                    // Accepted
                }
                else
                {
                    // Cancelled
                }
            }
        }
    }
}