// <copyright file="ResultExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Exceptions;

namespace DA.UI;

/// <summary>
/// Result Extensions.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Throw DAUIException if Result is an Exception object.
    /// </summary>
    /// <typeparam name="T">Base object.</typeparam>
    /// <param name="result">Result object.</param>
    /// <returns>Result object if success.</returns>
    /// <exception cref="DAUIException">Thrown if the result is an Exception object.</exception>
    public static T HandleUIResult<T>(this UIResult<T> result)
    {
        if (result.IsT1)
        {
            throw new DAUIException(result.AsT1);
        }

        return result.AsT0;
    }

    /// <summary>
    /// Deconstructs the Result object into its value and error components.
    /// </summary>
    /// <typeparam name="T">The type of the value component.</typeparam>
    /// <param name="result">The Result object to deconstruct.</param>
    /// <param name="value">The value component of the Result object.</param>
    /// <param name="error">The error component of the Result object.</param>
    public static void Deconstruct<T>(this UIResult<T> result, out T? value, out Exception? error)
    {
        value = result.IsT0 ? result.AsT0 : default;
        error = result.IsT1 ? result.AsT1 : default;
    }
}