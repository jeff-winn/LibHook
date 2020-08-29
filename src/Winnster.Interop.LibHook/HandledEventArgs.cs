using System;

namespace Winnster.Interop.LibHook
{
    /// <summary>
    /// Provides event data for events that can be handled.
    /// </summary>
    public class HandledEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandledEventArgs"/> class.
        /// </summary>
        public HandledEventArgs()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the event was handled.
        /// </summary>
        public bool Handled { get; set; }
    }
}