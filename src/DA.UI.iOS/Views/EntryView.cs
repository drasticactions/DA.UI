// <copyright file="EntryView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Masonry;

namespace DA.UI.Views;

public sealed class EntryView : UIView
{
    private UILabel caption;
    private UITextField textField;

    public EntryView(string caption, string value = "", bool isPassword = false)
    {
        this.caption = new UILabel()
        { Font = UIFont.SystemFontOfSize(15, UIFontWeight.Semibold), TextColor = UIColor.Gray };
        this.caption.Text = caption;
        this.textField = new UITextField();
        this.textField.BorderStyle = UITextBorderStyle.RoundedRect;
        this.textField.Text = value;
        this.textField.SecureTextEntry = isPassword;
        this.AddSubview(this.caption);
        this.AddSubview(this.textField);
        this.caption.MakeConstraints(x =>
        {
            x.Top.EqualTo(this).Offset(10);
            x.Leading.EqualTo(this).Offset(10);
            x.Trailing.EqualTo(this).Offset(-10);
        });
        this.textField.MakeConstraints(x =>
        {
            x.Top.EqualTo(this.caption.Bottom()).Offset(10);
            x.Leading.EqualTo(this).Offset(10);
            x.Trailing.EqualTo(this).Offset(-10);
            x.Bottom.EqualTo(this).Offset(-10);
        });
    }

    public UITextField TextField => this.textField;

    public UILabel Caption => this.caption;
}