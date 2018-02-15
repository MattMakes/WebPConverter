using System;
using System.Runtime.InteropServices;

namespace WebPConsole.ext
{
    public partial class NativeMethods
    {
        public static void WebPSafeFree(IntPtr toDeallocate)
        {
            WebPFree(toDeallocate);
        }

        [DllImport("libwebp", EntryPoint = "WebPFree", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WebPFree(IntPtr toDeallocate);
    }
}