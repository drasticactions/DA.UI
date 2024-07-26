// <copyright file="EntryElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Events;
using DA.UI.Views;
using Masonry;

namespace DA.UI.TableView.Elements;

public sealed class EntryElement : BaseElement<string>
{
    private readonly EntryView holderView;

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

    public event EventHandler<EntryUpdatedEventArgs>? EntryChanged;

    public UITextField TextField => this.holderView.TextField;

    public bool IsEnabled
    {
        get => this.holderView.TextField.Enabled;
        set => this.holderView.TextField.Enabled = value;
    }

    public override void Layout()
    {
        this.holderView.TextField.Text = this.Value;
    }
}