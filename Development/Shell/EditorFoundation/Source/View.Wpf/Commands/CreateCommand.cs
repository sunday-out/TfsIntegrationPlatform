﻿// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System.Windows;
using System.Windows.Input;
using Microsoft.TeamFoundation.Migration.Shell.Controller;
using Microsoft.TeamFoundation.Migration.Shell.Model;

namespace Microsoft.TeamFoundation.Migration.Shell.View
{
    /// <summary>
    /// Provides a command that creates a new data model.
    /// </summary>
    /// <typeparam name="TController">The type of the controller.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class CreateCommand<TController, TModel> : ViewModelCommand<TController, TModel>
        where TController : ControllerBase<TModel>, new ()
        where TModel : ModelRoot, new ()
    {
        #region Fields
        private readonly Window owner;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommand&lt;TController, TModel&gt;"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public CreateCommand (ViewModel<TController, TModel> viewModel)
            : this (viewModel, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommand&lt;TController, TModel&gt;"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="owner">The owner window.</param>
        public CreateCommand (ViewModel<TController, TModel> viewModel, Window owner)
            : base (viewModel)
        {
            this.owner = owner;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the view model property that determines whether this command can be executed.
        /// </summary>
        /// <value></value>
        protected override string ViewModelPropertyName
        {
            get
            {
                return "CanCreate";
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public override bool CanExecute (object parameter)
        {
            return CanExecute (this.viewModel, parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public override void Execute (object parameter)
        {
            Execute (this.viewModel, this.owner, parameter);
        }

        /// <summary>
        /// Determines whether a data model can be created for the specified <see cref="ViewModel&lt;TController, TModel&gt;"/>.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>
        /// 	<c>true</c> if a data model can be created; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanCreate (ViewModel<TController, TModel> viewModel)
        {
            return viewModel.CanCreate;
        }

        /// <summary>
        /// Creates a data model for the specified <see cref="ViewModel&lt;TController, TModel&gt;"/>.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>
        ///     <c>true</c> if a data model is created; otherwise, <c>false</c>.
        /// </returns>
        public static bool Create (ViewModel<TController, TModel> viewModel)
        {
            return viewModel.Create ();
        }
        #endregion

        #region Internal Methods
        internal static bool CanExecute (ViewModel<TController, TModel> viewModel, object parameter)
        {
            return CreateCommand<TController, TModel>.CanCreate (viewModel);
        }

        internal static void Execute (ViewModel<TController, TModel> viewModel, Window owner, object parameter)
        {
            CreateCommand<TController, TModel>.Create (viewModel);
        }
        #endregion
    }

    /// <summary>
    /// Provides a <see cref="CommandBinding"/> that delegates the <see cref="ApplicationCommands.New"/> routed command to the <see cref="CreateCommand&lt;TController, TModel&gt;"/>.
    /// </summary>
    /// <typeparam name="TController">The type of the controller.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class NewCommandBinding<TController, TModel> : CommandBinding
        where TController : ControllerBase<TModel>, new ()
        where TModel : ModelRoot, new ()
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NewCommandBinding&lt;TController, TModel&gt;"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public NewCommandBinding (ViewModel<TController, TModel> viewModel)
            : this (viewModel, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewCommandBinding&lt;TController, TModel&gt;"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="owner">The owner window.</param>
        public NewCommandBinding (ViewModel<TController, TModel> viewModel, Window owner)
        {
            this.Command = ApplicationCommands.New;

            this.CanExecute += delegate (object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = CreateCommand<TController, TModel>.CanExecute (viewModel, e.Parameter);
            };

            this.Executed += delegate (object sender, ExecutedRoutedEventArgs e)
            {
                CreateCommand<TController, TModel>.Execute (viewModel, owner, e.Parameter);
            };
        }
        #endregion
    }
}
