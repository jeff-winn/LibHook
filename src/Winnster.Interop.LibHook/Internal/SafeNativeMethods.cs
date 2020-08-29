using System;
using System.Runtime.InteropServices;

namespace Winnster.Interop.LibHook.Internal
{
    /// <summary>
    /// Contains the safe native API function declarations.
    /// </summary>
    internal static class SafeNativeMethods
    {
        private const string LibHookDll = "libhook.dll";

        /// <summary>
        /// Initializes the hook.
        /// </summary>
        /// <param name="idHook">The hook id to set.</param>
        /// <param name="lpfn">The callback.</param>
        /// <returns>If successful, the handle to the hook; otherwise, a null reference.</returns>
        [DllImport(LibHookDll, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern HookHandle InitHook(int idHook, NativeMethods.HookProc lpfn);

        /// <summary>
        /// Releases the hook.
        /// </summary>
        /// <param name="hhk">The hook to release.</param>
        /// <returns><b>true</b> if the hook was released successfully, otherwise <b>false</b>.</returns>
        [DllImport(LibHookDll, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReleaseHook(HookHandle hhk);

        [DllImport(LibHookDll, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern short GetAppCommand(IntPtr lParam);

        [DllImport(LibHookDll, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern short GetDevice(IntPtr lParam);

        [DllImport(LibHookDll, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern short GetKeyState(IntPtr lParam);

        [DllImport(LibHookDll, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern short GetWheelDelta(IntPtr value);
    }
}