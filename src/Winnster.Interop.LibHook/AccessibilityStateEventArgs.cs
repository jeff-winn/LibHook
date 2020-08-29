using System;

namespace Winnster.Interop.LibHook
{
    /// <summary>
    /// Provides event data for accessibility state events.
    /// </summary>
    public class AccessibilityStateEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessibilityStateEventArgs"/> class.
        /// </summary>
        public AccessibilityStateEventArgs()
        {
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public AccessibilityStateEnum State { get; internal set; }
    }
}
