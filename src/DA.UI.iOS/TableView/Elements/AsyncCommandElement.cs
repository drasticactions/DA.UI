// <copyright file="AsyncCommandElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Buttons;
using DA.UI.Commands;

namespace DA.UI.TableView.Elements;

/// <summary>
/// Represents an element that contains an <see cref="AsyncCommandButton"/> and inherits from <see cref="UIButtonElement"/>.
/// </summary>
public sealed class AsyncCommandElement : UIButtonElement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncCommandElement"/> class with the specified <see cref="AsyncCommandButton"/>.
    /// </summary>
    /// <param name="button">The <see cref="AsyncCommandButton"/> to associate with this element.</param>
    public AsyncCommandElement(AsyncCommandButton button)
        : base(button, "AsyncCommandElement", UITableViewCellStyle.Default)
    {
    }

    /// <summary>
    /// Gets the <see cref="AsyncCommandButton"/> associated with this element.
    /// </summary>
    public AsyncCommandButton AsyncButton => (AsyncCommandButton)this.Button;

    /// <summary>
    /// Creates a new instance of the <see cref="AsyncCommandElement"/> class with the specified <see cref="AsyncCommandButton"/>.
    /// </summary>
    /// <param name="button">The <see cref="AsyncCommandButton"/> to associate with the new element.</param>
    /// <returns>A new instance of the <see cref="AsyncCommandElement"/> class.</returns>
    public static AsyncCommandElement Create(AsyncCommandButton button)
        => new AsyncCommandElement(button);

    /// <summary>
    /// Creates a new instance of the <see cref="AsyncCommandElement"/> class with a new <see cref="AsyncCommandElementButton"/> created from the specified <see cref="IAsyncCommand"/>.
    /// </summary>
    /// <param name="command">The <see cref="IAsyncCommand"/> to associate with the new element.</param>
    /// <returns>A new instance of the <see cref="AsyncCommandElement"/> class.</returns>
    public static AsyncCommandElement Create(IAsyncCommand command)
        => new AsyncCommandElement(new AsyncCommandElementButton(command));
}