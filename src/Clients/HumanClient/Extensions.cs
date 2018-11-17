using System;
using System.Windows.Forms;

namespace BoardGame.HumanClient
{
    public static class Extensions
    {
        public static void InvokeIfRequired<T>(this T control, Action<T> action) where T : Control
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action(control);
            }
        }
    }
}