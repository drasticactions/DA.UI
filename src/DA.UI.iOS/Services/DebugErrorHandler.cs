// <copyright file="DebugErrorHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Services;

namespace DA.UI;

/// <summary>
/// Debug Error Handler.
/// </summary>
public class DebugErrorHandler : IErrorHandler
{
    /// <inheritdoc/>
    public void HandleError(Exception ex)
    {
        System.Diagnostics.Debug.WriteLine(ex);
    }
}