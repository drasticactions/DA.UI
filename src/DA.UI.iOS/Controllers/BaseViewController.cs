// <copyright file="BaseViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DA.UI.Services;

namespace DA.UI.Controllers;

/// <summary>
/// Represents a base view controller that provides common functionality for all view controllers.
/// </summary>
public class BaseViewController : UIViewController
{
    /// <summary>
    /// The dispatcher used for application-wide operations.
    /// </summary>
    private IAppDispatcher dispatcher;

    /// <summary>
    /// The error handler used for managing application errors.
    /// </summary>
    private IErrorHandler errorHandler;

    /// <summary>
    /// The factory used for creating asynchronous commands.
    /// </summary>
    private IAsyncCommandFactory asyncCommandFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseViewController"/> class.
    /// </summary>
    /// <param name="dispatcher">The dispatcher used for application-wide operations.</param>
    /// <param name="errorHandler">The error handler used for managing application errors.</param>
    /// <param name="asyncCommandFactory">The factory used for creating asynchronous commands.</param>
    public BaseViewController(IAppDispatcher dispatcher, IErrorHandler errorHandler, IAsyncCommandFactory asyncCommandFactory)
    {
        this.dispatcher = dispatcher;
        this.errorHandler = errorHandler;
        this.asyncCommandFactory = asyncCommandFactory;
    }

    /// <summary>
    /// Gets the dispatcher used for application-wide operations.
    /// </summary>
    public IAppDispatcher Dispatcher => this.dispatcher;

    /// <summary>
    /// Gets the error handler used for managing application errors.
    /// </summary>
    public IErrorHandler ErrorHandler => this.errorHandler;

    /// <summary>
    /// Gets the factory used for creating asynchronous commands.
    /// </summary>
    public IAsyncCommandFactory AsyncCommandFactory => this.asyncCommandFactory;
}