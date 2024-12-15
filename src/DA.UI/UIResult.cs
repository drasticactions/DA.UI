// <copyright file="UIResult.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Tools;

namespace DA.UI;

/// <summary>
/// Represents a result that can either contain a value of type <typeparamref name="T"/> or an <see cref="Exception"/>.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public class UIResult<T> : Multiple<T, Exception>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UIResult{T}"/> class.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">Represents the type of value.</param>
    private UIResult(T value)
        : base(0, value, default)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UIResult{T}"/> class.
    /// </summary>
    /// <param name="value">Represents the type of value.</param>
    private UIResult(Exception? value)
        : base(1, default, value)
    {
    }

    public static implicit operator UIResult<T?>(T? t) => new(t);

    public static implicit operator UIResult<T?>(Exception? t) => new(t);
}