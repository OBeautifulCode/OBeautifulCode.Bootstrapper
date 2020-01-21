﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleAbstractionBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CLAP;

    /// <summary>
    /// Instance for use in CLAP.
    /// </summary>
    public abstract class ConsoleAbstractionBase
    {
        /// <summary>
        /// Gets the exception types for which only the exception message should be displayed (omits the stack trace).
        /// </summary>
        public virtual IReadOnlyCollection<Type> MessageOnlyExceptionTypes => null;

        /// <summary>
        /// Error method to call from CLAP; a 1 will be returned as the exit code if this is entered since an exception was thrown.
        /// </summary>
        /// <param name="context">Context provided with details.</param>
        [Error]
#pragma warning disable CS3001 // Argument type is not CLS-compliant - needed for CLAP
        public void Error(
            ExceptionContext context)
#pragma warning restore CS3001 // Argument type is not CLS-compliant - needed for CLAP
        {
            // change color to red
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            // parser exception or
            if (context.Exception is CommandLineParserException)
            {
                Console.WriteLine("Failure parsing command line arguments.  Run the exe with the 'help' command for usage.");
                Console.WriteLine("   " + context.Exception.Message);
            }
            else if ((this.MessageOnlyExceptionTypes ?? new Type[0]).Any(_ => _ == context.Exception.GetType()))
            {
                Console.WriteLine("Failure during execution; configured to omit stack trace.");
                Console.WriteLine(string.Empty);
                Console.WriteLine("   " + context.Exception.Message);
            }
            else
            {
                Console.WriteLine("Failure during execution.");
                Console.WriteLine("   " + context.Exception.Message);
                Console.WriteLine(string.Empty);
                Console.WriteLine("   " + context.Exception);
            }

            // restore color
            Console.WriteLine();
            Console.ForegroundColor = originalColor;
        }

        /// <summary>
        /// Help method to call from CLAP.
        /// </summary>
        /// <param name="helpText">Generated help text to display.</param>
        [Empty]
        [Help(Aliases = "h,?,-h,-help")]
        [Verb(Aliases = "Help", IsDefault = true)]
        public void ShowUsage(
            string helpText)
        {
            Console.WriteLine("   Usage");
            Console.Write("   -----");

            // strip out the usage info about help, it's confusing
            helpText = string.Join(Environment.NewLine, helpText.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Skip(3));

            Console.WriteLine(helpText);
            Console.WriteLine();
        }
    }
}