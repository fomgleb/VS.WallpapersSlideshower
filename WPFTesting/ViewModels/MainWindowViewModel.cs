using System.Windows;
using System.Windows.Input;
using WPFTesting.Infrastructure.Commands;

namespace WPFTesting.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title = "Super Program";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new ActionCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
        }
    }
}
