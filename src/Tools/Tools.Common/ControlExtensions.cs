using System.ComponentModel;
using System.Windows.Forms;

namespace BoardGame.Tools.Common
{
    /// <summary>
    /// Contains extensions for the control class.
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// Helps with cross-threading call problems. Invokes action if required.
        /// </summary>
        /// <param name="obj">The control the action has to be called on.</param>
        /// <param name="action">The action called on the control.</param>
        public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
        {
            if (obj.InvokeRequired)
            {
                var args = new object[0];
                obj.Invoke(action, args);
            }
            else
            {
                action();
            }
        }
    }
}
