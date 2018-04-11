﻿using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Helpers.Commands
{
    [Serializable]
    public class RelayCommand : ICommand
    {
        #region Fields

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        private bool v;

        #endregion // Fields

        #region Constructors

        /// <summary>
        ///   Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        { }

        /// <summary>
        ///   Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand()
        {
        }

        public RelayCommand(Action<object> execute, bool v) : this(execute)
        {
            this.v = v;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion // ICommand Members
    }
}