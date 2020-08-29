namespace Winnster.Interop.LibHook.Internal
{
    /// <summary>
    /// Represents a hook handle.
    /// </summary>
    internal class HookHandle : Microsoft.Win32.SafeHandles.CriticalHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Releases the handle.
        /// </summary>
        /// <returns><b>true</b> if the handle is released; otherwise, <b>false</b>.</returns>
        protected override bool ReleaseHandle()
        {
            return SafeNativeMethods.ReleaseHook(this);
        }
    }
}