using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Winnster.Interop.LibHook.TestUI.Entities;

namespace Winnster.Interop.LibHook.TestUI.ViewModels
{
    public abstract class HookViewModelBase<THook> : ViewModelBase
        where THook : SystemHook, new()
    {
        protected THook hook;

        protected HookViewModelBase()
        {
            this.hook = new THook();

            this.MessengerInstance.Register<AppLoadedMessage>(this, (msg) => this.Attach());
            this.MessengerInstance.Register<AppClosingMessage>(this, (msg) => this.Cleanup());
        }

        public void Attach()
        {
            if (this.hook.IsAttached)
            {
                return;
            }

            this.hook.Attach();
        }

        public void Detach()
        {
            if (!this.hook.IsAttached)
            {
                return;
            }

            this.hook.Detach();
        }

        public override void Cleanup()
        {
            this.Detach();

            if (this.hook != null)
            {
                this.hook.Dispose();
                this.hook = null;
            }

            base.Cleanup();
        }
    }
}
