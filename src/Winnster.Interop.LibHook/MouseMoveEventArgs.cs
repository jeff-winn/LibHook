using System;

namespace Winnster.Interop.LibHook
{
    /// <summary>
    /// Provides event data for mouse movement events.
    /// </summary>
    public class MouseMoveEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseMoveEventArgs"/> class.
        /// </summary>
        public MouseMoveEventArgs()
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
    }
}