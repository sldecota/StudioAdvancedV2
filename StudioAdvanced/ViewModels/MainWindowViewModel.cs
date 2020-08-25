using Microsoft.TeamFoundation.MVVM;
using Prism.Commands;
using StudioAdvanced.Helpers;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudioAdvanced.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            //this.CurrentView = new FamilyView();
            SwitchViewsCommand = new RelayCommand((parameter)
                => CurrentView = (UserControl)Activator.CreateInstance(parameter as Type));
        }

        public ICommand SwitchViewsCommand { get; private set; }

        private UserControl _currentView;

        public UserControl CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                if (value.Equals(_currentView)) return;
                _currentView = value;
                RaisePropertyChanged("CurrentView");
            }
        }

        public DelegateCommand<string> ExpandedCommand { get; set; }

        public void Expanded(UserControl parameter)
        {
            CurrentView = parameter;
        }

        
    }
}
