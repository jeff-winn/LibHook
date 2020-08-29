namespace Winnster.Interop.LibHook
{
    /// <summary>
    /// Provides event data for application command events.
    /// </summary>
    public class AppCommandEventArgs : HandledEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppCommandEventArgs"/> class.
        /// </summary>
        public AppCommandEventArgs()
        {
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        public AppCommandEnum Command { get; internal set; }

        /// <summary>
        /// Gets the device.
        /// </summary>
        public DeviceEnum Device { get; internal set; }

        /// <summary>
        /// Gets the key modifier.
        /// </summary>
        public KeyModifierEnum Modifier { get; internal set; }
    }
}