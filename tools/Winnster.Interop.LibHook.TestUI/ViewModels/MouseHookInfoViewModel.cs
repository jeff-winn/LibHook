using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Winnster.Interop.LibHook.TestUI.Entities;

namespace Winnster.Interop.LibHook.TestUI.ViewModels
{
    public class MouseHookInfoViewModel : HookViewModelBase<MouseHook>
    {
        public MouseHookInfoViewModel()
        {
            if (!this.IsInDesignMode)
            {
                this.hook.MouseMove += this.hook_MouseMove;
                this.hook.MouseUp += this.hook_MouseUp;
                this.hook.MouseDown += this.hook_MouseDown;
            }
        }

        private MouseButtonEnum _lastButton;

        public MouseButtonEnum LastButton
        {
            get
            {
                return this._lastButton;
            }

            set
            {
                if (this._lastButton != value)
                {
                    this._lastButton = value;
                    this.RaisePropertyChanged(() => this.LastButton);
                }
            }
        }

        private MouseButtonStateEnum _lastButtonState;

        public MouseButtonStateEnum LastButtonState
        {
            get
            {
                return this._lastButtonState;
            }

            set
            {
                if (this._lastButtonState != value)
                {
                    this._lastButtonState = value;
                    this.RaisePropertyChanged(() => this.LastButtonState);
                }
            }
        }

        private int _lastXMouseMove;

        public int LastXMouseMove
        {
            get
            {
                return this._lastXMouseMove;
            }

            set
            {
                if (this._lastXMouseMove != value)
                {
                    this._lastXMouseMove = value;
                    this.RaisePropertyChanged(() => this.LastXMouseMove);
                }
            }
        }

        private int _lastYMouseMove;

        public int LastYMouseMove
        {
            get
            {
                return this._lastYMouseMove;
            }

            set
            {
                if (this._lastYMouseMove != value)
                {
                    this._lastYMouseMove = value;
                    this.RaisePropertyChanged(() => this.LastYMouseMove);
                }
            }
        }

        public override void Cleanup()
        {
            if (this.hook != null)
            {
                this.hook.MouseMove -= this.hook_MouseMove;
                this.hook.MouseDown -= this.hook_MouseDown;
                this.hook.MouseUp -= this.hook_MouseUp;
            }

            base.Cleanup();
        }

        private void hook_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.LastButton = e.Button;
            this.LastButtonState = MouseButtonStateEnum.Pressed;
        }

        private void hook_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.LastButton = e.Button;
            this.LastButtonState = MouseButtonStateEnum.Released;
        }

        private void hook_MouseMove(object sender, MouseMoveEventArgs e)
        {
            this.LastXMouseMove = e.X;
            this.LastYMouseMove = e.Y;
        }
    }
}