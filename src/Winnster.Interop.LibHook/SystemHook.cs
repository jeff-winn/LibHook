using System;
using System.IO;
using Winnster.Interop.LibHook.Internal;

namespace Winnster.Interop.LibHook
{
    /// <summary>
    /// Provides a base class for system hooks. This class must be inherited.
    /// </summary>
    public abstract class SystemHook : MarshalByRefObject, IDisposable
    {
        private readonly object SyncRoot = new object();
        private readonly NativeMethods.HookProc callback;

        private HookHandle handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemHook"/> class.
        /// </summary>
        /// <param name="hookId">The hook id to attach.</param>
        protected SystemHook(int hookId)
        {
            this.HookId = hookId;
            this.callback = new NativeMethods.HookProc(this.InternalCallback);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SystemHook"/> class.
        /// </summary>
        ~SystemHook()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the hook id.
        /// </summary>
        private int HookId { get; set; }

        /// <summary>
        /// Disposes of the object and releases any managed and unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Occurs when an error has occurred while processing an event.
        /// </summary>
        public event EventHandler<ErrorEventArgs> Error;

        /// <summary>
        /// Raises the <see cref="Error"/> event.
        /// </summary>
        /// <param name="e">An <see cref="ErrorEventArgs"/> containing event data.</param>
        protected void OnError(ErrorEventArgs e)
        {
            if (this.Error != null)
            {
                this.Error(this, e);
            }
        }

        /// <summary>
        /// Disposes of the object and releases any managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> if the managed resources should be released; otherwise, <b>false</b> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Detach();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the hook is attached.
        /// </summary>
        public bool IsAttached
        {
            get
            {
                return this.handle != null;
            }
        }

        /// <summary>
        /// Attaches the hook.
        /// </summary>
        public void Attach()
        {
            if (this.IsAttached)
            {
                return;
            }

            lock (this.SyncRoot)
            {
                if (this.IsAttached)
                {
                    return;
                }

                this.handle = SafeNativeMethods.InitHook(this.HookId, this.callback);
            }
        }

        /// <summary>
        /// Detaches the hook.
        /// </summary>
        public void Detach()
        {
            if (!this.IsAttached)
            {
                return;
            }

            lock (this.SyncRoot)
            {
                if (!this.IsAttached)
                {
                    return;
                }

                bool released = SafeNativeMethods.ReleaseHook(this.handle);
                if (released)
                {
                    this.handle = null;
                }
            }
        }

        /// <summary>
        /// Occurs when the callback is received for an event.
        /// </summary>
        /// <param name="nCode">The hook code.</param>
        /// <param name="wParam">A parameter containing event data.</param>
        /// <param name="lParam">A parameter containing event data.</param>
        /// <returns><b>true</b> if the event was handled, otherwise <b>false</b>.</returns>
        protected abstract bool OnCallback(int nCode, IntPtr wParam, IntPtr lParam);
        
        private IntPtr InternalCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            IntPtr result = NativeMethods.FALSE;

            try
            {
                bool handled = this.OnCallback(nCode, wParam, lParam);
                if (handled)
                {
                    result = NativeMethods.TRUE;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    this.OnError(new ErrorEventArgs(ex));
                }
                catch
                {
                    // Swallow any exceptions, do not let any issues transfer into the kernel!
                }
            }

            return result;
        }
    }
}