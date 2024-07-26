// <copyright file="UpdatedValueEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.Events;

/// <summary>
/// Represents the event arguments for an updated value event.
/// </summary>
/// <typeparam name="T">The type of the value being updated.</typeparam>
public class UpdatedValueEventArgs<T> : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatedValueEventArgs{T}"/> class.
    /// </summary>
    /// <param name="originalValue">The original value before the update.</param>
    /// <param name="value">The updated value.</param>
    public UpdatedValueEventArgs(T? originalValue, T? value)
    {
        this.OriginalValue = originalValue;
        this.UpdatedValue = value;
    }

    /// <summary>
    /// Gets the original value before the update.
    /// </summary>
    public T? OriginalValue { get; }

    /// <summary>
    /// Gets the updated value.
    /// </summary>
    public T? UpdatedValue { get; }
}