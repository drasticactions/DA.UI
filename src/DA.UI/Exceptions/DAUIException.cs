// <copyright file="DAUIException.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DA.UI.Exceptions;

/// <summary>
/// DAUI Exception.
/// </summary>
public class DAUIException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DAUIException"/> class.
    /// </
    /// <param name="ex">The inner exception.</param>
    public DAUIException(Exception ex)
        : base(string.Empty, ex)
    {
    }
}