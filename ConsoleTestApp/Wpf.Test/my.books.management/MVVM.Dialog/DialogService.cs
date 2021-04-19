using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace Wpf.Test.my.books.management.MVVM.Dialog
{
    public class DialogService : IDialogService
    {
        // The owning window of the dialogs
        private readonly Window owner;

        public DialogService(Window owner)
        {
            this.owner = owner;
            Mappings = new Dictionary<Type, Type>();
        }

        public IDictionary<Type, Type> Mappings { get; }

        // store mappings of viewmodels to their associated views
        public void Register<TViewModel, TView>() where TViewModel : IDialogRequestClose
                                                  where TView : IDialog
        {
            if (Mappings.ContainsKey(typeof(TViewModel)))
            {
                throw new ViewModelMappingException($"Type {typeof(TViewModel)} is already mapped to type {typeof(TView)}");
            }

            Mappings.Add(typeof(TViewModel), typeof(TView));
        }

        public void HandleCloseRequestedEvent(object sender, DialogCloseRequestedEventArgs args)
        {

        }
        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose
        {
            // Find the view type. The key of the Mappings dictionary is a viewmodel.
            Type viewType = Mappings[typeof(TViewModel)];

            // create a new instance of the view.
            IDialog dialog = (IDialog)Activator.CreateInstance(viewType);

            EventHandler<DialogCloseRequestedEventArgs> handler = null;

            handler = (sender, e) =>
            {
                viewModel.CloseRequested -= handler;

                if (e.DialogCloseInformation.DialogResult.HasValue)
                {
                    dialog.DialogResult = e.DialogCloseInformation.DialogResult;
                }
                else
                {
                    dialog.Close();
                }
            };

            viewModel.CloseRequested += handler;

            dialog.DataContext = viewModel;
            dialog.Owner = owner;

            return dialog.ShowDialog();
        }
    }
}
