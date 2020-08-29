using System;
using System.Runtime.InteropServices;
using Winnster.Interop.LibHook.Internal;

namespace Winnster.Interop.LibHook
{
    /// <summary>
    /// Provides a hook for mouse notifications. This class cannot be inherited.
    /// </summary>
    /// <example>
    /// The following example shows how to attach a mouse hook and receive mouse change notifications.
    /// <code lang="C#">
    /// private MouseHook hook;
    /// 
    /// public void Attach()
    /// {
    ///     this.hook = new MouseHook();
    ///     this.hook.MouseMove += this.hook_MouseMove;
    ///     this.hook.Attach();
    /// }
    /// 
    /// private void hook_MouseMove(object sender, MouseMoveEventArgs e)
    /// {
    ///     // Do something useful.
    /// }
    /// </code>
    /// <code lang="VB.NET">
    /// Private hook As MouseHook
    /// 
    /// Public Sub Attach()
    ///     Me.hook = new MouseHook
    ///     AddHandler Me.hook.MouseMove, Me.hook_MouseMove
    ///     Me.hook.Attach()
    /// End Sub
    /// 
    /// Private Sub hook_MouseMove(ByVal sender As Object, ByVal e As MouseMoveEventArgs)
    ///     ' Do something useful.
    /// End Sub
    /// </code>
    /// </example>
    public sealed class MouseHook : SystemHook
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseHook"/> class.
        /// </summary>
        public MouseHook()
            : base(NativeMethods.WH_MOUSE_LL)
        {
        }

        /// <summary>
        /// Occurs when the mouse wheel is rotated.
        /// </summary>
        public event EventHandler<EventArgs> MouseWheel;

        /// <summary>
        /// Raises the <see cref="MouseWheel"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing event data.</param>
        private void OnMouseWheel(EventArgs e)
        {
            if (this.MouseWheel != null)
            {
                this.MouseWheel(this, e);
            }
        }

        /// <summary>
        /// Occurs when the mouse moves.
        /// </summary>
        public event EventHandler<MouseMoveEventArgs> MouseMove;

        /// <summary>
        /// Raises the <see cref="MouseMove"/> event.
        /// </summary>
        /// <param name="e">An <see cref="MouseMoveEventArgs"/> containing event data.</param>
        private void OnMouseMove(MouseMoveEventArgs e)
        {
            if (this.MouseMove != null)
            {
                this.MouseMove(this, e);
            }
        }

        /// <summary>
        /// Occurs when a mouse button is released.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseUp;

        /// <summary>
        /// Raises the <see cref="MouseUp"/> event.
        /// </summary>
        /// <param name="e">An <see cref="MouseButtonEventArgs"/> containing event data.</param>
        private void OnMouseUp(MouseButtonEventArgs e)
        {
            if (this.MouseUp != null)
            {
                this.MouseUp(this, e);
            }
        }

        /// <summary>
        /// Occurs when a mouse button is pressed.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseDown;

        /// <summary>
        /// Raises the <see cref="MouseDown"/> event.
        /// </summary>
        /// <param name="e">An <see cref="MouseButtonEventArgs"/> containing event data.</param>
        private void OnMouseDown(MouseButtonEventArgs e)
        {
            if (this.MouseDown != null)
            {
                this.MouseDown(this, e);
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

            ////Debug.WriteLine("MouseHook::OnCallback - {0}:{1}:{2}", nCode, wParam, lParam);

            switch (nCode)
            {
                case NativeMethods.HC_ACTION:
                    NativeMethods.MSLLHOOKSTRUCT info = (NativeMethods.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(NativeMethods.MSLLHOOKSTRUCT));

                    DateTime dt = DateTime.Today.AddMilliseconds(info.time);
                    ////Debug.WriteLine("dt: {0}", dt);

                    ////Debug.WriteLine("pt: {0}", info.pt);
                    ////Debug.WriteLine("mouseData: {0}", info.mouseData);
                    ////Debug.WriteLine("flags: {0}", info.flags);
                    ////Debug.WriteLine("time: {0}", info.time);
                    ////Debug.WriteLine("dwExtraInfo: {0}", info.dwExtraInfo);
                    ////Debug.WriteLine("");

                    int identifier = wParam.ToInt32();
                    switch (identifier)
                    {
                        case NativeMethods.WM_MOUSEMOVE:
                            this.OnMouseMove(new MouseMoveEventArgs()
                                {
                                    X = info.pt.x,
                                    Y = info.pt.y,
                                    Timestamp = dt
                                });
                            break;

                        case NativeMethods.WM_MOUSEWHEEL:
                        case NativeMethods.WM_MOUSEHWHEEL:
                            this.OnMouseWheel(EventArgs.Empty);
                            break;

                        case NativeMethods.WM_LBUTTONUP:
                        case NativeMethods.WM_MBUTTONUP:
                        case NativeMethods.WM_RBUTTONUP:
                        case NativeMethods.WM_XBUTTONUP:
                        case NativeMethods.WM_NCXBUTTONUP:
                            this.OnMouseUp(new MouseButtonEventArgs()
                                {
                                    ButtonState = MouseButtonStateEnum.Released,
                                    Button = ConvertToButtonEnum(identifier),
                                    X = info.pt.x,
                                    Y = info.pt.y,
                                    Timestamp = dt
                                });
                            break;

                        case NativeMethods.WM_LBUTTONDOWN:
                        case NativeMethods.WM_MBUTTONDOWN:
                        case NativeMethods.WM_RBUTTONDOWN:
                        case NativeMethods.WM_XBUTTONDOWN:
                        case NativeMethods.WM_NCXBUTTONDOWN:
                            this.OnMouseDown(new MouseButtonEventArgs()
                                {
                                    ButtonState = MouseButtonStateEnum.Released,
                                    Button = ConvertToButtonEnum(identifier),
                                    X = info.pt.x,
                                    Y = info.pt.y,
                                    Timestamp = dt
                                });
                            break;
                    }

                    break;
            }

            return handled;
        }

        private static MouseButtonEnum ConvertToButtonEnum(int button)
        {
            switch (button)
            {
                case NativeMethods.WM_LBUTTONDBLCLK:
                case NativeMethods.WM_LBUTTONDOWN:
                case NativeMethods.WM_LBUTTONUP:
                    return MouseButtonEnum.LButton;

                case NativeMethods.WM_MBUTTONDBLCLK:
                case NativeMethods.WM_MBUTTONDOWN:
                case NativeMethods.WM_MBUTTONUP:
                    return MouseButtonEnum.MButton;

                case NativeMethods.WM_RBUTTONDBLCLK:
                case NativeMethods.WM_RBUTTONDOWN:
                case NativeMethods.WM_RBUTTONUP:
                    return MouseButtonEnum.RButton;

                case NativeMethods.WM_XBUTTONDBLCLK:
                case NativeMethods.WM_XBUTTONDOWN:
                case NativeMethods.WM_XBUTTONUP:
                    return MouseButtonEnum.XButton1;

                case NativeMethods.WM_NCXBUTTONDBLCLK:
                case NativeMethods.WM_NCXBUTTONDOWN:
                case NativeMethods.WM_NCXBUTTONUP:
                    return MouseButtonEnum.XButton2;

                default:
                    return MouseButtonEnum.Unknown;
            }
        }
    }
}