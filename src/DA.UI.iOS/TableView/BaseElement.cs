// <copyright file="BaseElement.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Runtime.CompilerServices;
using DA.UI.Events;

namespace DA.UI.TableView;

/// <summary>
/// Represents an abstract base element for table view cells, providing a generic way to handle cell data.
/// </summary>
/// <typeparam name="T">The type of the value this element represents.</typeparam>
public abstract class BaseElement<T> : Element, INotifyPropertyChanged
{
    private T value;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseElement{T}"/> class.
    /// </summary>
    /// <param name="value">The value of the type <typeparamref name="T"/> to be associated with this cell.</param>
    /// <param name="reuseIdentifier">A string used to identify the cell to be reused in the table view.</param>
    /// <param name="style">The cell style to be used. Defaults to <see cref="UITableViewCellStyle.Default"/>.</param>
    protected BaseElement(T value, string reuseIdentifier, UITableViewCellStyle style = UITableViewCellStyle.Default)
        : base(reuseIdentifier, style)
    {
        ArgumentNullException.ThrowIfNull(value);
        this.value = value;
    }

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    public event EventHandler<UpdatedValueEventArgs<T>>? UpdatedValue;

    /// <summary>
    /// Gets the value associated with this cell element.
    /// </summary>
    public T Value => this.value;

    /// <summary>
    /// Updates the value of the cell and triggers a layout update if the value has changed.
    /// </summary>
    /// <param name="value">The new value to be set.</param>
    /// <param name="layout">Relayout the subviews, defaults to true.</param>
    /// <remarks>
    /// This method checks if the provided value is different from the current value.
    /// If so, it updates the value and calls <see cref="UITableViewCellElement.Layout"/> to update the cell's layout.
    /// </remarks>
    public void UpdateValue(T value, bool layout = true)
    {
        ArgumentNullException.ThrowIfNull(value);

        var originalValue = this.value;

        // If updated, update the value and layout the cell.
        if (this.SetField(ref this.value, value))
        {
            if (layout)
            {
                this.Layout();
                this.LayoutSubviews();
            }

            this.UpdatedValue?.Invoke(this, new UpdatedValueEventArgs<T>(originalValue, value));
        }
    }

    /// <summary>
    /// Notifies listeners that a property value has changed.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    /// <remarks>
    /// This method is called to notify listeners, typically binding clients, that a property value has changed.
    /// The <paramref name="propertyName"/> parameter is optional because it's automatically filled by the compiler
    /// with the name of the caller member.
    /// </remarks>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Sets the field to a new value and notifies that the property has changed if the value is different.
    /// </summary>
    /// <typeparam name="T">The type of the field.</typeparam>
    /// <param name="field">The field to be updated.</param>
    /// <param name="value">The new value for the field.</param>
    /// <param name="propertyName">The name of the property that changed.</param>
    /// <returns>True if the field was updated; otherwise, false.</returns>
    /// <remarks>
    /// This method compares the current and new values of the field. If they are different,
    /// it updates the field and calls <see cref="OnPropertyChanged"/> to notify about the change.
    /// The <paramref name="propertyName"/> is automatically filled by the compiler.
    /// </remarks>
    private bool SetField<Y>(ref Y field, Y value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<Y>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        this.OnPropertyChanged(propertyName);
        return true;
    }
}