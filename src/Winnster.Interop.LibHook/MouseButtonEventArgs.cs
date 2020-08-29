using System;

namespace Winnster.Interop.LibHook
{
    /// <summary>
    /// Provides event data for mouse button events.
    /// </summary>
    public class MouseButtonEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseButtonEventArgs"/> class.
        /// </summary>
        public MouseButtonEventArgs()
        {
        }

        /// <summary>
        /// Gets the X coordinate (in pixels).
        /// </summary>
        public int X { get; internal set; }

        /// <summary>
        /// Gets the Y coordinate (in pixels).
        /// </summary>
        public int Y { get; internal set; }

        /// <summary>
        /// Gets the timestamp at which the event occurred.
        /// </summary>
        public DateTime Timestamp { get; internal set; }

        /// <summary>
        /// Gets the button state.
        /// </summary>
        public MouseButtonStateEnum ButtonState { get; internal set; }

        /// <summary>
        /// Gets the button.
        /// </summary>
        public MouseButtonEnum Button { get; internal set; }
    }
}