using System;
using System.Runtime.InteropServices;
using Winnster.Interop.LibHook.Internal;

namespace Winnster.Interop.LibHook
{
    /// <summary>
    /// Provides a hook for keyboard notifications. This class cannot be inherited.
    /// </summary>
    /// <example>
    /// The following example shows how to attach a keyboard hook and receive key press notifications.
    /// <code lang="C#">
    /// private KeyboardHook hook;
    /// 
    /// public void Attach()
    /// {
    ///     this.hook = new KeyboardHook();
    ///     this.hook.KeyDown += this.hook_KeyDown;
    ///     this.hook.KeyUp += this.hook_KeyUp;
    ///     this.hook.Attach();
    /// }
    /// 
    /// private void hook_KeyDown(object sender, KeyPressEventArgs e)
    /// {
    ///     // Do something useful.
    /// }
    /// 
    /// private void hook_KeyUp(object sender, KeyPressEventArgs e)
    /// {
    ///     // Do something useful.
    /// }
    /// </code>
    /// <code lang="VB.NET">
    /// Private hook As KeyboardHook
    /// 
    /// Public Sub Attach()
    ///     Me.hook = new KeyboardHook
    ///     AddHandler Me.hook.KeyDown, Me.hook_KeyDown
    ///     AddHandler Me.hook.KeyUp, Me.hook_KeyUp
    ///     Me.hook.Attach()
    /// End Sub
    /// 
    /// Private Sub hook_KeyDown(ByVal sender As Object, ByVal e As KeyPressEventArgs)
    ///     ' Do something useful.
    /// End Sub
    /// 
    /// Private Sub hook_KeyUp(ByVal sender As Object, ByVal e As KeyPressEventArgs)
    ///     ' Do something useful.
    /// End Sub
    /// </code>
    /// </example>
    public sealed class KeyboardHook : SystemHook
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardHook"/> class.
        /// </summary>
        public KeyboardHook()
            : base(NativeMethods.WH_KEYBOARD_LL)
        {
        }

        /// <summary>
        /// Occurs when a key is pressed.
        /// </summary>
        public event EventHandler<KeyPressEventArgs> KeyDown;

        /// <summary>
        /// Raises the <see cref="KeyDown"/> event.
        /// </summary>
        /// <param name="e">An <see cref="KeyPressEventArgs"/> containing event data.</param>
        private void OnKeyDown(KeyPressEventArgs e)
        {
            if (this.KeyDown != null)
            {
                this.KeyDown(this, e);
            }
        }

        /// <summary>
        /// Occurs when a key is released.
        /// </summary>
        public event EventHandler<KeyPressEventArgs> KeyUp;

        /// <summary>
        /// Raises the <see cref="KeyUp"/> event.
        /// </summary>
        /// <param name="e">An <see cref="KeyPressEventArgs"/> containing event data.</param>
        private void OnKeyUp(KeyPressEventArgs e)
        {
            if (this.KeyUp != null)
            {
                this.KeyUp(this, e);
            }
        }

        /// <summary>
        /// Occurs when the callback is received for an event.
        /// </summary>
        /// <param name="nCode">The hook code.</param>
        /// <param name="wParam">A parameter containing event data.</param>
        /// <param name="lParam">A parameter containing event data.</param>
        /// <returns><b>true</b> if the event was handled, otherwise <b>false</b>.</returns>
        protected override bool OnCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            bool handled = false;

            ////Debug.WriteLine("KeyBoardHook::OnCallback - {0}:{1}:{2}", nCode, wParam, lParam);

            switch (nCode)
            {
                case NativeMethods.HC_ACTION:
                    NativeMethods.KBDLLHOOKSTRUCT info = (NativeMethods.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(NativeMethods.KBDLLHOOKSTRUCT));

                    VirtualKeyEnum key = VirtualKeyEnum.VK_UNKNOWN;
                    if (Enum.IsDefined(typeof(VirtualKeyEnum), info.vkCode))
                    {
                        key = (VirtualKeyEnum)info.vkCode;
                    }

                    KeyPressEventArgs e = new KeyPressEventArgs()
                        {
                            Key = key,
                            ScanCode = info.scanCode,
                            ExtraInfo = info.dwExtraInfo,
                            Timestamp = DateTime.Today.AddMilliseconds(info.time)
                        };

                    int identifier = wParam.ToInt32();
                    switch (identifier)
                    {
                        case NativeMethods.WM_KEYDOWN:
                        case NativeMethods.WM_SYSKEYDOWN:
                            this.OnKeyDown(e);
                            break;

                        case NativeMethods.WM_KEYUP:
                        case NativeMethods.WM_SYSKEYUP:
                            this.OnKeyUp(e);
                            break;
                    }

                    break;
            }

            return handled;
        }
    }
}