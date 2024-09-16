// <copyright file="EntryElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Events;
using DA.UI.Views;
using Masonry;

namespace DA.UI.TableView.Elements;

/// <summary>
/// Represents an entry element that contains an <see cref="EntryView"/> and inherits from <see cref="BaseElement{T}"/> with a string type parameter.
/// </summary>
public sealed class EntryElement : BaseElement<string>
{
    private readonly EntryView holderView;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntryElement"/> class with the specified caption, value, and password flag.
    /// </summary>
    /// <param name="caption">The caption to display in the entry view.</param>
    /// <param name="value">The initial value of the entry view.</param>
    /// <param name="isPassword">A flag indicating whether the entry view is for password input.</param>
    public EntryElement(string caption, string value = "", bool isPassword = false)
        : base(value, "EntryElement", UITableViewCellStyle.Default)
    {
        this.holderView = new EntryView(caption, value, isPassword);
        this.ContentView.AddSubview(this.holderView);
        this.holderView.MakeConstraints(x =>
        {
            x.Top.EqualTo(this.ContentView).Offset(10);
            x.Leading.EqualTo(this.ContentView).Offset(10);
            x.Trailing.EqualTo(this.ContentView).Offset(-10);
            x.Bottom.EqualTo(this.ContentView).Offset(-10);
        });
        this.holderView.TextField.EditingChanged += (s, e) =>
        {
            this.UpdateValue(this.holderView.TextField.Text ?? string.Empty, true);
            this?.EntryChanged?.Invoke(this, new EntryUpdatedEventArgs(this.holderView.TextField.Text ?? string.Empty));
        };
        this.SelectionStyle = UITableViewCellSelectionStyle.None;
    }

    /// <summary>
    /// Occurs when the entry value is changed.
    /// </summary>
    public event EventHandler<EntryUpdatedEventArgs>? EntryChanged;

    /// <summary>
    /// Gets the <see cref="UITextField"/> associated with this entry element.
    /// </summary>
    public UITextField TextField => this.holderView.TextField;

    /// <summary>
    /// Gets or sets a value indicating whether the entry is enabled.
    /// </summary>
    public bool IsEnabled
    {
        get => this.holderView.TextField.Enabled;
        set
        {
            this.InvokeOnMainThread(() =>
            {
                this.holderView.TextField.Enabled = value;
            });
        }
    }

    /// <summary>
    /// Configures the layout of the entry element.
    /// </summary>
    public override void Layout()
    {
        this.holderView.TextField.Text = this.Value;
    }
}