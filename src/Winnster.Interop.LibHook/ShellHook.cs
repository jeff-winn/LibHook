using System;
using Winnster.Interop.LibHook.Internal;

namespace Winnster.Interop.LibHook
{
    /// <summary>
    /// Provides a hook for shell notifications. This class cannot be inherited.
    /// </summary>
    /// <example>
    /// The following example shows how to attach a system hook and receive notifications for application commands.
    /// <code lang="C#">
    /// private ShellHook hook;
    /// 
    /// public void Attach()
    /// {
    ///     this.hook = new ShellHook();
    ///     this.hook.AppCommandReceived += this.hook_AppCommandReceived;
    ///     this.hook.Attach();
    /// }
    /// 
    /// private void hook_AppCommandReceived(object sender, AppCommandEventArgs e)
    /// {
    ///     // Do something useful.
    /// }
    /// </code>
    /// <code lang="VB.NET">
    /// Private hook As ShellHook
    /// 
    /// Public Sub Attach()
    ///     Me.hook = new ShellHook
    ///     AddHandler Me.hook.AppCommandReceived, Me.hook_AppCommandReceived
    ///     Me.hook.Attach()
    /// End Sub
    /// 
    /// Private Sub hook_AppCommandReceived(ByVal sender As Object, ByVal e As AppCommandEventArgs)
    ///     ' Do something useful.
    /// End Sub
    /// </code>
    /// </example>
    public sealed class ShellHook : SystemHook
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellHook"/> class.
        /// </summary>
        public ShellHook()
            : base(NativeMethods.WH_SHELL)
        {
        }

        /// <summary>
        /// Occurs when the window is notified of a user-generated application command event.
        /// </summary>
        public event EventHandler<AppCommandEventArgs> AppCommandReceived;

        /// <summary>
        /// Raises the <see cref="AppCommandReceived"/> event.
        /// </summary>
        /// <param name="e">An <see cref="AppCommandEventArgs"/> containing event data.</param>
        private void OnAppCommandReceived(AppCommandEventArgs e)
        {
            if (this.AppCommandReceived != null)
            {
                this.AppCommandReceived(this, e);
            }
        }

        /// <summary>
        /// Occurs when the accessibility state changes.
        /// </summary>
        public event EventHandler<AccessibilityStateEventArgs> AccessibilityStateChanged;

        /// <summary>
        /// Raises the <see cref="AccessibilityStateChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="AccessibilityStateEventArgs"/> containing event data.</param>
        private void OnAccessibilityStateChanged(AccessibilityStateEventArgs e)
        {
            if (this.AccessibilityStateChanged != null)
            {
                this.AccessibilityStateChanged(this, e);
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

            ////Debug.WriteLine("WHShellHook::OnCallback - {0}:{1}:{2}", nCode, wParam, lParam);

            switch (nCode)
            {
                case NativeMethods.HSHELL_APPCOMMAND:
                    AppCommandEnum cmd = (AppCommandEnum)SafeNativeMethods.GetAppCommand(lParam);
                    DeviceEnum uDevice = (DeviceEnum)SafeNativeMethods.GetDevice(lParam);
                    KeyModifierEnum dwKeys = (KeyModifierEnum)SafeNativeMethods.GetKeyState(lParam);

                    AppCommandEventArgs appCommandEventArgs = new AppCommandEventArgs() { Command = cmd, Device = uDevice, Modifier = dwKeys };
                    this.OnAppCommandReceived(appCommandEventArgs);

                    handled = appCommandEventArgs.Handled;
                    break;

                case NativeMethods.HSHELL_ACCESSIBILITYSTATE:
                    AccessibilityStateEventArgs accessibilityStateEventArgs = new AccessibilityStateEventArgs() { State = (AccessibilityStateEnum)wParam };
                    this.OnAccessibilityStateChanged(accessibilityStateEventArgs);
                    break;

                case NativeMethods.HSHELL_ACTIVATESHELLWINDOW:
                case NativeMethods.HSHELL_ENDTASK:
                case NativeMethods.HSHELL_GETMINRECT:
                case NativeMethods.HSHELL_LANGUAGE:
                case NativeMethods.HSHELL_MONITORCHANGED:
                case NativeMethods.HSHELL_REDRAW:
                case NativeMethods.HSHELL_SYSMENU:
                case NativeMethods.HSHELL_TASKMAN:
                case NativeMethods.HSHELL_WINDOWACTIVATED:
                case NativeMethods.HSHELL_WINDOWCREATED:
                case NativeMethods.HSHELL_WINDOWDESTROYED:
                case NativeMethods.HSHELL_WINDOWREPLACED:
                case NativeMethods.HSHELL_WINDOWREPLACING:
                    break;
            }

            return handled;
        }
    }
}