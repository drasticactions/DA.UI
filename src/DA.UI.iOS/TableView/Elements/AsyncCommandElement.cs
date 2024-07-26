// <copyright file="AsyncCommandElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Buttons;
using DA.UI.Commands;

namespace DA.UI.TableView.Elements;

public sealed class AsyncCommandElement : UIButtonElement
{
    public AsyncCommandElement(AsyncCommandButton button)
        : base(button, "AsyncCommandElement", UITableViewCellStyle.Default)
    {
    }

    public AsyncCommandButton AsyncButton => (AsyncCommandButton)this.Button;

    public static AsyncCommandElement Create(AsyncCommandButton button)
        => new AsyncCommandElement(button);

    public static AsyncCommandElement Create(IAsyncCommand command)
        => new AsyncCommandElement(new AsyncCommandElementButton(command));
}