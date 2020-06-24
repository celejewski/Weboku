using System.Collections.Generic;

namespace Core.Data
{
    public readonly struct InputValue
    {
        private readonly int _value;
        private InputValue(int value) => _value = value;

        public static readonly InputValue Empty = new InputValue(0);
        public static readonly InputValue One = new InputValue(1);
        public static readonly InputValue Two = new InputValue(2);
        public static readonly InputValue Three = new InputValue(3);
        public static readonly InputValue Four = new InputValue(4);
        public static readonly InputValue Five = new InputValue(5);
        public static readonly InputValue Six = new InputValue(6);
        public static readonly InputValue Seven = new InputValue(7);
        public static readonly InputValue Eight = new InputValue(8);
        public static readonly InputValue Nine = new InputValue(9);

        public static readonly IEnumerable<InputValue> NonEmpty = new[]
        {
            One, Two, Three, Four, Five, Six, Seven, Eight,Nine
        };

        public static readonly IReadOnlyList<InputValue> Values = new[]
        {
            Empty, One, Two, Three, Four, Five, Six, Seven, Eight, Nine
        };

        public static implicit operator int(InputValue value) => value._value;
        public static implicit operator InputValue(int index) => Values[index];

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals((InputValue) obj);
        }

        public bool Equals(InputValue other)
        {
            return _value == other._value;
        }
    }
}
