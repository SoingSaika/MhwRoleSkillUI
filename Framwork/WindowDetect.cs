using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace MhwRoleSkill.Framwork
{
    internal class WindowDetect
    {

        public static bool IsCurrentTopProcWindow(string procName)
        {
            var windowPtr = GetForegroundWindow();
            var processStr = 0x0;
            GetWindowThreadProcessId(windowPtr, out uint processPtr);
            Process process = Process.GetProcessById((int) processPtr);
            return processStr.ToString(process.ProcessName).Equals(procName);
        }

        [DllImport("user32")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpString);

        [DllImport("user32")]
        private static extern bool EnumWindows(WndEnumProc lpEnumFunc, int lParam);

        [DllImport("user32")]
        private static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lptrString, int nMaxCount);

        [DllImport("user32")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32")]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref LPRECT rect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [StructLayout(LayoutKind.Sequential)]
        private readonly struct LPRECT
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;
        }

        public delegate bool WndEnumProc(IntPtr hWnd, int lparam);

        public readonly struct WindowInfo
        {
            public WindowInfo(IntPtr hWnd, string className, string title, bool isVisible, Rectangle bounds) : this()
            {
                Hwnd = hWnd;
                ClassName = className;
                Title = title;
                IsVisible = isVisible;
                Bounds = bounds;
            }

            /// <summary>
            /// 获取窗口句柄。
            /// </summary>
            public IntPtr Hwnd { get; }

            /// <summary>
            /// 获取窗口类名。
            /// </summary>
            public string ClassName { get; }

            /// <summary>
            /// 获取窗口标题。
            /// </summary>
            public string Title { get; }

            /// <summary>
            /// 获取当前窗口是否可见。
            /// </summary>
            public bool IsVisible { get; }

            /// <summary>
            /// 获取窗口当前的位置和尺寸。
            /// </summary>
            public Rectangle Bounds { get; }

            /// <summary>
            /// 获取窗口当前是否是最小化的。
            /// </summary>
            public bool IsMinimized => Bounds.Left == -32000 && Bounds.Top == -32000;
        }
    }
}
