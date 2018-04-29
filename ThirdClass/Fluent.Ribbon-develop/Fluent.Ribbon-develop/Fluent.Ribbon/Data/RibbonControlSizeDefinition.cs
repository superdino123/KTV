﻿// ReSharper disable once CheckNamespace
namespace Fluent
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Fluent.Converters;

    /// <summary>
    /// Class to map from <see cref="RibbonGroupBoxState"/> to <see cref="RibbonControlSize"/>
    /// </summary>
    [TypeConverter(typeof(SizeDefinitionConverter))]
    public struct RibbonControlSizeDefinition : IEquatable<RibbonControlSizeDefinition>
    {
        private const int MaxSizeDefinitionParts = 3;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public RibbonControlSizeDefinition(RibbonControlSize large, RibbonControlSize middle, RibbonControlSize small)
            : this()
        {
            this.Large = large;
            this.Middle = middle;
            this.Small = small;
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public RibbonControlSizeDefinition(string sizeDefinition)
            : this()
        {
            if (string.IsNullOrEmpty(sizeDefinition))
            {
                this.Large = RibbonControlSize.Large;
                this.Middle = RibbonControlSize.Large;
                this.Small = RibbonControlSize.Large;
                return;
            }

            var splitted = sizeDefinition.Split(new[] { ' ', ',', ';', '-', '>' }, MaxSizeDefinitionParts, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (splitted.Count == 0)
            {
                this.Large = RibbonControlSize.Large;
                this.Middle = RibbonControlSize.Large;
                this.Small = RibbonControlSize.Large;
                return;
            }

            // Ensure that we got three sizes
            for (var i = splitted.Count; i < MaxSizeDefinitionParts; i++)
            {
                splitted.Add(splitted[splitted.Count - 1]);
            }

            this.Large = ToRibbonControlSize(splitted[0]);
            this.Middle = ToRibbonControlSize(splitted[1]);
            this.Small = ToRibbonControlSize(splitted[2]);
        }

        /// <summary>
        /// Gets or sets the value for large group sizes
        /// </summary>
        public RibbonControlSize Large { get; set; }

        /// <summary>
        /// Gets or sets the value for middle group sizes
        /// </summary>
        public RibbonControlSize Middle { get; set; }

        /// <summary>
        /// Gets or sets the value for small group sizes
        /// </summary>
        public RibbonControlSize Small { get; set; }

        /// <summary>
        /// Converts from <see cref="string"/> to <see cref="RibbonControlSizeDefinition"/>
        /// </summary>
        public static implicit operator RibbonControlSizeDefinition(string sizeDefinition)
        {
            return new RibbonControlSizeDefinition(sizeDefinition);
        }

        /// <summary>
        /// Converts from <see cref="RibbonControlSizeDefinition"/> to <see cref="string"/>
        /// </summary>
        public static implicit operator string(RibbonControlSizeDefinition sizeDefinition)
        {
            return sizeDefinition.ToString();
        }

        /// <summary>
        /// Converts from <see cref="string"/> to <see cref="RibbonControlSize"/>
        /// </summary>
        public static RibbonControlSize ToRibbonControlSize(string ribbonControlSize)
        {
            RibbonControlSize result;

            return Enum.TryParse(ribbonControlSize, true, out result)
                       ? result
                       : RibbonControlSize.Large;
        }

        /// <summary>
        /// Gets the appropriate <see cref="RibbonControlSize"/> from <see cref="Large"/>, <see cref="Middle"/> or <see cref="Small"/> depending on <paramref name="ribbonGroupBoxState"/>
        /// </summary>
        public RibbonControlSize GetSize(RibbonGroupBoxState ribbonGroupBoxState)
        {
            switch (ribbonGroupBoxState)
            {
                case RibbonGroupBoxState.Large:
                    return this.Large;

                case RibbonGroupBoxState.Middle:
                    return this.Middle;

                case RibbonGroupBoxState.Small:
                    return this.Small;

                case RibbonGroupBoxState.Collapsed:
                case RibbonGroupBoxState.QuickAccess:
                    return this.Large;

                default:
                    return RibbonControlSize.Large;
            }
        }

        #region Overrides of ValueType

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is RibbonControlSizeDefinition && this.Equals((RibbonControlSizeDefinition)obj);
        }

        #region Equality members

        /// <inheritdoc />
        public bool Equals(RibbonControlSizeDefinition other)
        {
            return this.Large == other.Large && this.Middle == other.Middle && this.Small == other.Small;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)this.Large;
                hashCode = (hashCode * 397) ^ (int)this.Middle;
                hashCode = (hashCode * 397) ^ (int)this.Small;
                return hashCode;
            }
        }

        /// <summary>Determines whether the specified object instances are considered equal.</summary>
        /// <param name="left">The first object to compare. </param>
        /// <param name="right">The second object to compare. </param>
        /// <returns>true if the objects are considered equal; otherwise, false. If both <paramref name="left" /> and <paramref name="right" /> are null, the method returns true.</returns>
        public static bool operator ==(RibbonControlSizeDefinition left, RibbonControlSizeDefinition right)
        {
            return left.Equals(right);
        }

        /// <summary>Determines whether the specified object instances are not considered equal.</summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if the objects are not considered equal; otherwise, false. If both <paramref name="left" /> and <paramref name="right" /> are null, the method returns false.</returns>
        public static bool operator !=(RibbonControlSizeDefinition left, RibbonControlSizeDefinition right)
        {
            return !left.Equals(right);
        }

        #endregion

        #endregion

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return $"{this.Large} {this.Middle} {this.Small}";
        }
    }
}