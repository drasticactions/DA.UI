// <copyright file="AsyncCommandElementButton.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Commands;

namespace DA.UI.Buttons;

public class AsyncCommandElementButton : AsyncCommandButton
{
    public AsyncCommandElementButton(IAsyncCommand value)
        : base(value)
    {
    }

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