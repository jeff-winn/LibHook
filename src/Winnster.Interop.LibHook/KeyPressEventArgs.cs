using System;

namespace Winnster.Interop.LibHook
{
    /// <summary>
    /// Provides event data for key press events.
    /// </summary>
    public class KeyPressEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyPressEventArgs"/> class.
        /// </summary>
        public KeyPressEventArgs()
        {
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public VirtualKeyEnum Key { get; internal set; }

        /// <summary>
        /// Gets the hardware scan code for the key.
        /// </summary>
        public int ScanCode { get; internal set; }

        /// <summary>
        /// Gets a pointer to extra information provided by the hardware.
        /// </summary>
        public IntPtr ExtraInfo { get; internal set; }

        /// <summary>
        /// Gets the timestamp at which the event occurred.
        /// </summary>
        public DateTime Timestamp { get; internal set; }
    }
}