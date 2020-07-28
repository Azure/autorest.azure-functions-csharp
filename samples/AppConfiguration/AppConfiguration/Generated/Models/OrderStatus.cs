// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace AppConfiguration.Models
{
    /// <summary> Order Status. </summary>
    public readonly partial struct OrderStatus : IEquatable<OrderStatus>
    {
        private readonly string _value;

        /// <summary> Determines if two <see cref="OrderStatus"/> values are the same. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public OrderStatus(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string PlacedValue = "placed";
        private const string ApprovedValue = "approved";
        private const string DeliveredValue = "delivered";

        /// <summary> placed. </summary>
        public static OrderStatus Placed { get; } = new OrderStatus(PlacedValue);
        /// <summary> approved. </summary>
        public static OrderStatus Approved { get; } = new OrderStatus(ApprovedValue);
        /// <summary> delivered. </summary>
        public static OrderStatus Delivered { get; } = new OrderStatus(DeliveredValue);
        /// <summary> Determines if two <see cref="OrderStatus"/> values are the same. </summary>
        public static bool operator ==(OrderStatus left, OrderStatus right) => left.Equals(right);
        /// <summary> Determines if two <see cref="OrderStatus"/> values are not the same. </summary>
        public static bool operator !=(OrderStatus left, OrderStatus right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="OrderStatus"/>. </summary>
        public static implicit operator OrderStatus(string value) => new OrderStatus(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is OrderStatus other && Equals(other);
        /// <inheritdoc />
        public bool Equals(OrderStatus other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
