﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringRepresentationTestScenarios{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.CodeGen.ModelObject.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.CodeGen.ModelObject.Recipes
{
    using System;
    using System.Collections.Generic;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Specifies various scenarios for string representation tests.
    /// </summary>
    /// <typeparam name="T">The type of the object being tested.</typeparam>
#if !OBeautifulCodeCodeGenRecipesProject
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.CodeGen.ModelObject.Recipes", "See package version number")]
    internal
#else
    public
#endif
    class StringRepresentationTestScenarios<T>
        where T : class
    {
        private readonly object lockScenarios = new object();

        private readonly List<Lazy<StringRepresentationTestScenario<T>>> scenarios = new List<Lazy<StringRepresentationTestScenario<T>>>();

        /// <summary>
        /// Adds the specified scenario to the list of scenarios.
        /// </summary>
        /// <param name="scenario">The scenario to add.</param>
        /// <returns>
        /// This object.
        /// </returns>
        public StringRepresentationTestScenarios<T> AddScenario(
            StringRepresentationTestScenario<T> scenario)
        {
            new { scenario }.AsTest().Must().NotBeNull();

            this.AddScenario(() => scenario);

            return this;
        }

        /// <summary>
        /// Adds the specified scenario to the list of scenarios.
        /// </summary>
        /// <param name="scenarioFunc">A func that returns the scenario to add.</param>
        /// <returns>
        /// This object.
        /// </returns>
        public StringRepresentationTestScenarios<T> AddScenario(
            Func<StringRepresentationTestScenario<T>> scenarioFunc)
        {
            new { scenarioFunc }.AsTest().Must().NotBeNull();

            lock (this.lockScenarios)
            {
                var lazyScenario = new Lazy<StringRepresentationTestScenario<T>>(scenarioFunc);

                this.scenarios.Add(lazyScenario);
            }

            return this;
        }

        /// <summary>
        /// Adds the specified scenarios to the list of scenarios.
        /// </summary>
        /// <param name="stringRepresentationTestScenarios">The scenarios to add.</param>
        /// <returns>
        /// This object.
        /// </returns>
        public StringRepresentationTestScenarios<T> AddScenarios(
            StringRepresentationTestScenarios<T> stringRepresentationTestScenarios)
        {
            new { stringRepresentationTestScenarios }.AsTest().Must().NotBeNull();

            lock (this.lockScenarios)
            {
                this.scenarios.AddRange(stringRepresentationTestScenarios.scenarios);
            }

            return this;
        }

        /// <summary>
        /// Removes all scenarios.
        /// </summary>
        /// <returns>
        /// This object.
        /// </returns>
        public StringRepresentationTestScenarios<T> RemoveAllScenarios()
        {
            lock (this.lockScenarios)
            {
                this.scenarios.Clear();
            }

            return this;
        }

        /// <summary>
        /// Validates the scenarios and prepares them for testing.
        /// </summary>
        /// <returns>
        /// The validated/prepared scenarios.
        /// </returns>
        public IReadOnlyList<ValidatedStringRepresentationTestScenario<T>> ValidateAndPrepareForTesting()
        {
            lock (this.lockScenarios)
            {
                var typeCompilableString = typeof(T).ToStringCompilable();

                var becauseNoScenarios = new[]
                {
                    Invariant($"Use a static constructor on your test class to add scenarios by calling {nameof(StringRepresentationTestScenarios<object>)}.{nameof(StringRepresentationTestScenarios<object>.AddScenario)}(...)."),
                    Invariant($"If you need to force the consuming unit tests to pass and you'll write your own unit tests, clear all scenarios by calling {nameof(StringRepresentationTestScenarios<object>)}.{nameof(StringRepresentationTestScenarios<object>.RemoveAllScenarios)}() and then add {nameof(StringRepresentationTestScenario<object>)}<{typeCompilableString}>.{nameof(StringRepresentationTestScenario<object>.ForceGeneratedTestsToPassAndWriteMyOwnScenario)}."),
                };

                this.scenarios.AsTest(Invariant($"{nameof(StringRepresentationTestScenarios<object>)}.{nameof(StringRepresentationTestScenarios<object>.scenarios)}")).Must().NotBeEmptyEnumerable(because: string.Join(Environment.NewLine, becauseNoScenarios), applyBecause: ApplyBecause.SuffixedToDefaultMessage);

                var result = new List<ValidatedStringRepresentationTestScenario<T>>();

                var scenariosCount = this.scenarios.Count;

                for (var x = 0; x < scenariosCount; x++)
                {
                    var scenario = this.scenarios[x].Value;

                    var scenarioNumber = x + 1;

                    var scenarioName = string.IsNullOrWhiteSpace(scenario.Name) ? "<Unnamed Scenario>" : scenario.Name;

                    var scenarioId = Invariant($"{scenarioName} ({nameof(StringRepresentationTestScenario<object>)} #{scenarioNumber} of {scenariosCount}):");

                    var validatedScenario = new ValidatedStringRepresentationTestScenario<T>(
                        scenarioId,
                        scenario.SystemUnderTestExpectedStringRepresentationFunc);

                    result.Add(validatedScenario);
                }

                return result;
            }
        }
    }
}
