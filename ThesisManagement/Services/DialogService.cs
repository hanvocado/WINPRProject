using ThesisManagement.ViewModels;
using ThesisManagement.Views.Shared;

namespace ThesisManagement.Services
{
    public interface IDialogService
    {
        bool? ShowDialog(string title, string message);
    }

    public class DialogService : IDialogService
    {
        private ConfirmationDialogVM viewModel;
        private ConfirmationDialog dialog;

        public bool? ShowDialog(string title, string message)
        {
            viewModel = new ConfirmationDialogVM
            {
                Title = title,
                Message = message,
                YesCommand = new ViewModelCommand(ExecuteYesCommand),
                NoCommand = new ViewModelCommand(ExecuteNoCommand)
            };

            dialog = new ConfirmationDialog
            {
                DataContext = viewModel
            };
            dialog.ShowDialog();
            return viewModel.DialogResult ?? false;
        }

        private void ExecuteNoCommand(object obj)
        {
            viewModel.DialogResult = false;
            dialog.Close();
        }

        private void ExecuteYesCommand(object obj)
        {
            viewModel.DialogResult = true;
            dialog.Close();
        }
    }
}
