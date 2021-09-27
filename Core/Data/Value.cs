using System;
using System.Collections.Generic;

namespace Weboku.Core.Data
{
    public readonly struct Value : IEquatable<Value>
    {
        private readonly byte _value;

        private Value(byte value) => _value = value;

        public static readonly Value None = new(0);
        public static readonly Value One = new(1);
        public static readonly Value Two = new(2);
        public static readonly Value Three = new(3);
        public static readonly Value Four = new(4);
        public static readonly Value Five = new(5);
        public static readonly Value Six = new(6);
        public static readonly Value Seven = new(7);
        public static readonly Value Eight = new(8);
        public static readonly Value Nine = new(9);

        public static IReadOnlyCollection<Value> NonEmpty { get; } = new[]
        {
            One, Two, Three, Four, Five, Six, Seven, Eight, Nine
        };

        public static IReadOnlyList<Value> All { get; } = new[]
        {
            None, One, Two, Three, Four, Five, Six, Seven, Eight, Nine
        };

        public static implicit operator int(Value value) => value._value;

        public static implicit operator Value(int index) => All[index];

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString() => _value.ToString();

        public override bool Equals(object obj)
        {
            return obj is Value other && Equals(other);
        }

        public bool Equals(Value other)
        {
            return _value == other._value;
        }

        public static bool operator ==(Value left, Value right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Value left, Value right)
        {
            return !left.Equals(right);
        }
    }
}