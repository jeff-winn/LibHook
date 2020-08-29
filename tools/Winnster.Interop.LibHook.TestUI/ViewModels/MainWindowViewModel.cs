using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Winnster.Interop.LibHook.TestUI.Entities;

namespace Winnster.Interop.LibHook.TestUI.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            this.Loaded = new RelayCommand(this.OnLoaded);
            this.Closing = new RelayCommand<CancelEventArgs>(this.OnClosing);
            this.Exit = new RelayCommand<MainWindow>(this.OnExit);
        }

        public RelayCommand Loaded { get; set; }

        private void OnLoaded()
        {
            this.MessengerInstance.Send(new AppLoadedMessage() { IsLoaded = true });
        }

        public RelayCommand<CancelEventArgs> Closing { get; set; }

        private void OnClosing(CancelEventArgs e)
        {
            AppClosingMessage msg = new AppClosingMessage();
            this.MessengerInstance.Send(msg);

            e.Cancel = msg.Cancel;
        }

        public RelayCommand<MainWindow> Exit { get; set; }

        private void OnExit(MainWindow window)
        {
            window.Close();
        }

        private bool _isMouseHookEnabled;

        public bool IsMouseHookEnabled
        {
            get
            {
                return this._isMouseHookEnabled;
            }

            set
            {
                if (this._isMouseHookEnabled != value)
                {
                    this._isMouseHookEnabled = value;
                    this.RaisePropertyChanged(() => this.IsMouseHookEnabled);
                }
            }
        }

        private bool _isKeyboardHookEnabled;

        public bool IsKeyboardHookEnabled
        {
            get
            {
                return this._isKeyboardHookEnabled;
            }

            set
            {
                if (this._isKeyboardHookEnabled != value)
                {
                    this._isKeyboardHookEnabled = value;
                    this.RaisePropertyChanged(() => this.IsKeyboardHookEnabled);
                }
            }
        }

        private bool _isShellHookEnabled;

        public bool IsShellHookEnabled
        {
            get
            {
                return this._isShellHookEnabled;
            }

            set
            {
                if (this._isShellHookEnabled != value)
                {
                    this._isShellHookEnabled = value;
                    this.RaisePropertyChanged(() => this.IsShellHookEnabled);
                }
            }
        }
    }
}
