using System.Collections.Generic;

namespace Core.Data
{
    public readonly struct Value
    {
        private readonly int _value;
        private Value(int value) => _value = value;

        public static readonly Value None = new Value(0);
        public static readonly Value One = new Value(1);
        public static readonly Value Two = new Value(2);
        public static readonly Value Three = new Value(3);
        public static readonly Value Four = new Value(4);
        public static readonly Value Five = new Value(5);
        public static readonly Value Six = new Value(6);
        public static readonly Value Seven = new Value(7);
        public static readonly Value Eight = new Value(8);
        public static readonly Value Nine = new Value(9);

        public static readonly IReadOnlyCollection<Value> NonEmpty = new[]
        {
            One, Two, Three, Four, Five, Six, Seven, Eight, Nine
        };

        private static readonly IReadOnlyList<Value> _values = new[]
        {
            None, One, Two, Three, Four, Five, Six, Seven, Eight, Nine
        };

        public static implicit operator int(Value value) => value._value;
        public static implicit operator Value(int index) => _values[index];

        public override int GetHashCode() => _value;

        public override string ToString() => _value.ToString();

        public override bool Equals(object obj) => Equals((Value) obj);

        public bool Equals(Value other) => _value == other._value;
    }
}
