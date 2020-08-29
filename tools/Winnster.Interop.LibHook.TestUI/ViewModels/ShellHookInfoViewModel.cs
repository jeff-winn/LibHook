using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Winnster.Interop.LibHook.TestUI.ViewModels
{
    public class ShellHookInfoViewModel : HookViewModelBase<ShellHook>
    {
        public ShellHookInfoViewModel()
        {
            if (!this.IsInDesignMode)
            {
                this.hook.AccessibilityStateChanged += hook_AccessibilityStateChanged;
                this.hook.AppCommandReceived += this.hook_AppCommandReceived;
            }
        }

        public override void Cleanup()
        {
            if (this.hook != null)
            {
                this.hook.AccessibilityStateChanged -= this.hook_AccessibilityStateChanged;
                this.hook.AppCommandReceived -= this.hook_AppCommandReceived;
            }

            base.Cleanup();
        }

        private AppCommandEventArgs _lastAppCommandEventData;

        public AppCommandEventArgs LastAppCommandEventData
        {
            get
            {
                return this._lastAppCommandEventData;
            }

            set
            {
                if (this._lastAppCommandEventData != value)
                {
                    this._lastAppCommandEventData = value;
                    this.RaisePropertyChanged(() => this.LastAppCommandEventData);
                }
            }
        }

        private void hook_AccessibilityStateChanged(object sender, AccessibilityStateEventArgs e)
        {
        }

        private void hook_AppCommandReceived(object sender, AppCommandEventArgs e)
        {
            this.LastAppCommandEventData = e;
        }
    }
}