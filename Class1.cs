using BepInEx;
using UnboundLib;
using System;
using System.Runtime.InteropServices;

namespace TabIn
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class Class1 : BaseUnityPlugin
    {
        private const string ModId = "faith.mom-gay.TabIn";
        private const string ModName = "TabIn";
        public const string Version = "0.0.4";
        private bool flashing = false;
        private IntPtr hwnd;

        delegate bool EnumThreadDelegate(IntPtr Hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("Kernel32.dll")]
        static extern int GetCurrentThreadId();

        static IntPtr GetWindowHandle()
        {
            IntPtr returnHwnd = IntPtr.Zero;
            var threadId = GetCurrentThreadId();
            EnumThreadWindows(threadId,
                (hWnd, lParam) => {
                    if (returnHwnd == IntPtr.Zero) returnHwnd = hWnd;
                    return true;
                }, IntPtr.Zero);
            return returnHwnd;
        }

        private void Start()
        {
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.willis.rounds.unbound"))
            {
                Unbound.RegisterClientSideMod(ModId);
            }
            hwnd = GetWindowHandle();
        }

        private void Update()
        {
            if (UnityEngine.Application.isFocused)
            {
                flashing = false;
                return;
            }
            if (GameManager.instance.battleOngoing && GameManager.lockInput == false)
            {
                if (!flashing)
                {
                    flashing = true;
                    FlashWindow.Flash(hwnd);
                }
            } else if (GameManager.lockInput == false && GameManager.instance.isPlaying)
            {
                if (!flashing)
                {
                    flashing = true;
                    FlashWindow.Flash(hwnd);
                }
            } else 
            {
                flashing = false;
            }
            
        }
    }
}
