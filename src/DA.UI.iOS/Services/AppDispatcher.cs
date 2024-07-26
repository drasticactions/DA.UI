// <copyright file="AppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Services;

namespace DA.UI;

/// <summary>
/// App Dispatcher.
/// </summary>
public class AppDispatcher : NSObject, IAppDispatcher
{
    /// <inheritdoc/>
    public bool Dispatch(Action action)
    {
        if (NSThread.Current.IsMainThread)
        {
            action();
            return true;
        }

        NSOperationQueue.MainQueue.AddOperation(() => action());
        return true;
    }
}