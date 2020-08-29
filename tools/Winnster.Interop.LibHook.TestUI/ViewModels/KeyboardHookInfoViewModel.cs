using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winnster.Interop.LibHook.TestUI.ViewModels
{
    public class KeyboardHookInfoViewModel : HookViewModelBase<KeyboardHook>
    {
        public KeyboardHookInfoViewModel()
        {
            if (!this.IsInDesignMode)
            {
                this.hook.KeyDown += hook_KeyDown;
                this.hook.KeyUp += hook_KeyUp;
            }
        }

        private KeyPressEventArgs _lastKeyEventData;

        public KeyPressEventArgs LastKeyEventData
        {
            get
            {
                return this._lastKeyEventData;
            }

            set
            {
                if (this._lastKeyEventData != value)
                {
                    this._lastKeyEventData = value;
                    this.RaisePropertyChanged(() => this.LastKeyEventData);
                }
            }
        }

        private void hook_KeyUp(object sender, KeyPressEventArgs e)
        {
            this.LastKeyEventData = e;
        }

        private void hook_KeyDown(object sender, KeyPressEventArgs e)
        {
            this.LastKeyEventData = e;
        }
    }
}