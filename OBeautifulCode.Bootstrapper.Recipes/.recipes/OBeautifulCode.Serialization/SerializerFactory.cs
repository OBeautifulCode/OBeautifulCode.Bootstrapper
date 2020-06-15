﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializerFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Serialization.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Serialization.Recipes
{
    using System;
    using System.Collections.Concurrent;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Compression;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Serialization.Bson;
    using OBeautifulCode.Serialization.Json;
    using OBeautifulCode.Serialization.PropertyBag;

    using static System.FormattableString;

    /// <summary>
    /// Default implementation of <see cref="ISerializerFactory" />.
    /// </summary>
#if !OBeautifulCodeSerializationRecipesProject
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Serialization.Recipes", "See package version number")]
    internal
#else
    public
#endif
    sealed class SerializerFactory : SerializerFactoryBase
    {
        private static readonly SerializerFactory InternalInstance = new SerializerFactory();

        private static readonly ConcurrentDictionary<SerializerRepresentation, ConcurrentDictionary<AssemblyMatchStrategy, ISerializer>>
            SerializerCache = new ConcurrentDictionary<SerializerRepresentation, ConcurrentDictionary<AssemblyMatchStrategy, ISerializer>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBagSerializerFactory"/> class.
        /// </summary>
        /// <param name="compressorFactory">Optional compressor factory to use.  DEFAULT is to use OBeautifulCode.Compression.Recipes.CompressorFactory.Instance.</param>
        public SerializerFactory(
            ICompressorFactory compressorFactory = null)
            : base(compressorFactory)
        {
        }

        /// <summary>
        /// Gets the singleton entry point to the code.
        /// </summary>
        public static ISerializerFactory Instance => InternalInstance;

        /// <inheritdoc />
        public override ISerializer BuildSerializer(
            SerializerRepresentation serializerRepresentation,
            AssemblyMatchStrategy assemblyMatchStrategy = AssemblyMatchStrategy.AnySingleVersion)
        {
            new { serializerRepresentation }.AsArg().Must().NotBeNull();

            ISerializer result;

            if (SerializerCache.TryGetValue(serializerRepresentation, out ConcurrentDictionary<AssemblyMatchStrategy, ISerializer> assemblyMatchStrategyToSerializerMap))
            {
                if (assemblyMatchStrategyToSerializerMap.TryGetValue(assemblyMatchStrategy, out result))
                {
                    return result;
                }
            }

            // ReSharper disable once RedundantArgumentDefaultValue
            var configurationType = serializerRepresentation.SerializationConfigType?.ResolveFromLoadedTypes(assemblyMatchStrategy, throwIfCannotResolve: true);

            ISerializer serializer;

            switch (serializerRepresentation.SerializationKind)
            {
                case SerializationKind.Bson:
                    serializer = new ObcBsonSerializer(configurationType?.ToBsonSerializationConfigurationType());
                    break;
                case SerializationKind.Json:
                    serializer = new ObcJsonSerializer(configurationType?.ToJsonSerializationConfigurationType());
                    break;
                case SerializationKind.PropertyBag:
                    serializer = new ObcPropertyBagSerializer(configurationType?.ToPropertyBagSerializationConfigurationType());
                    break;
                default:
                    throw new NotSupportedException(Invariant($"{nameof(serializerRepresentation)} from enumeration {nameof(SerializationKind)} of {serializerRepresentation.SerializationKind} is not supported."));
            }

            result = this.WrapInCompressingSerializerIfAppropriate(serializer, serializerRepresentation.CompressionKind);

            SerializerCache.TryAdd(serializerRepresentation, new ConcurrentDictionary<AssemblyMatchStrategy, ISerializer>());

            SerializerCache[serializerRepresentation].TryAdd(assemblyMatchStrategy, result);

            return result;
        }
    }
}