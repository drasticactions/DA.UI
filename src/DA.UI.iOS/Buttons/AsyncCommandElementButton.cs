// <copyright file="AsyncCommandElementButton.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Commands;

namespace DA.UI.Buttons;

/// <summary>
/// Async Command Element Button.
/// </summary>
public class AsyncCommandElementButton : AsyncCommandButton
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncCommandElementButton"/> class.
    /// </summary>
    /// <param name="value">The command.</param>
    public AsyncCommandElementButton(IAsyncCommand value)
        : base(value)
    {
#if MACCATALYST
        this.PreferredBehavioralStyle = UIKit.UIBehavioralStyle.Pad;
#endif
    }

    /// <summary>
    /// Set the button configuration.
    /// </summary>
    /// <param name="isRunning">Is running.</param>
    /// <returns>UIButtonConfiguration.</returns>
    protected override UIButtonConfiguration? SetButtonConfiguration(bool isRunning)
    {
        var config = UIButtonConfiguration.FilledButtonConfiguration;
        config.CornerStyle = UIButtonConfigurationCornerStyle.Fixed;
        config.Background.CornerRadius = 0;
        config.ShowsActivityIndicator = isRunning;
        config.ImagePlacement = NSDirectionalRectEdge.Leading;
        config.ImagePadding = 10;
        return config;
    }
}