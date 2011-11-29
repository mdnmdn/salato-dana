using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueChips.DanaManager.MainApp.Libs
{
    public static class Tools
    {
        /// <summary>
        /// tries an action blocking every exceptions; returns the exception caught, or null on success
        /// </summary>
        /// <param name="action"></param>
        static public Exception TryThis(Action action)
        {
            try {
                action.Invoke();
                return null;
            } catch (Exception e) {
                return e;
            }
        }

        /// <summary>
        /// tries a function blocking every exceptions; returns the result on success, or the value provided on failure
        /// </summary>
        /// <param name="action"></param>
        static public TResult TryThis<TResult>(Func<TResult> action, TResult onFailure)
        {
            try {
                return action.Invoke();
            } catch {
                return onFailure;
            }
        }
    }
}
