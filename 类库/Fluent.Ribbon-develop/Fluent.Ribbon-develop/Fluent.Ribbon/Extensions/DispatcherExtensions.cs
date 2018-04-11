﻿namespace Fluent.Extensions
{
    using System;
    using System.Windows.Threading;

    /// <summary>
    /// Class with extension methods for <see cref="Dispatcher"/> and <see cref="DispatcherObject"/>.
    /// </summary>
    internal static class DispatcherExtensions
    {
        public static void RunInDispatcherAsync(this DispatcherObject dispatcher, Action action, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (dispatcher == null)
            {
                action();
                return;
            }

            dispatcher.Dispatcher.RunInDispatcherAsync(action, priority);
        }

        public static void RunInDispatcherAsync(this Dispatcher dispatcher, Action action, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (dispatcher == null)
            {
                action();
            }
            else
            {
                dispatcher.BeginInvoke(priority, action);
            }
        }

        public static void RunInDispatcher(this DispatcherObject dispatcher, Action action, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (dispatcher == null)
            {
                action();
                return;
            }

            dispatcher.Dispatcher.RunInDispatcher(action, priority);
        }

        public static void RunInDispatcher(this Dispatcher dispatcher, Action action, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (dispatcher == null
                || dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.Invoke(priority, action);
            }
        }
    }
}