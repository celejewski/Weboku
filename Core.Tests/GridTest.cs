using Core.Data;
using System;
using Xunit;

namespace Core.Tests
{
    public class GridTest
    {
        [Fact]
        public void Setters()
        {
            var grid = new Grid();
            grid.SetValue(0, 0, 1);

            var value = grid.GetValue(0, 0);

            Assert.Equal(1, value);
        }

        [Fact]
        public void IsLegalValue_IsTrue_ForUniqueValue()
        {
            var grid = new Grid();
            var actual = grid.IsLegalValue(0, 0, 1);
            Assert.True(actual);
        }

        [Fact]
        public void IsLegalValue_IsTrue_ForSameValueInDifferentHouse()
        {
            var grid = new Grid();
            grid.SetValue(0, 0, 1);
            var actual = grid.IsLegalValue(3, 3, 1);
            Assert.True(actual);
        }

        [Fact]
        public void IsLegalValue_IsTrue_ForDifferentValueInSameHouse()
        {
            var grid = new Grid();
            grid.SetValue(0, 0, 1);
            var actual = grid.IsLegalValue(1, 0, 2);
            Assert.True(actual);
        }

        [Fact]
        public void IsLegalValue_IsFalse_WhenSameValueInRow()
        {
            var grid = new Grid();
            grid.SetValue(0, 0, 1);
            var actual = grid.IsLegalValue(1, 0, 1);
            Assert.False(actual);
        }

        [Fact]
        public void IsLegalValue_IsFalse_WhenSameValueInCol()
        {
            var grid = new Grid();
            grid.SetValue(0, 0, 1);
            var actual = grid.IsLegalValue(0, 1, 1);
            Assert.False(actual);
        }

        [Fact]
        public void IsLegalValue_IsFalse_WhenSameValueInBlock()
        {
            var grid = new Grid();
            grid.SetValue(0, 0, 1);
            var actual = grid.IsLegalValue(2, 2, 1);
            Assert.False(actual);
        }

        [Fact]
        public void IsLegalValue_IsTrue_ForSameValueForSameCell()
        {
            var grid = new Grid();
            grid.SetValue(0, 0, 1);
            var actual = grid.IsLegalValue(0, 0, 1);
            Assert.True(actual);
        }

        [Fact]
        public void CtorWithInput_ProducesCorrectGrid()
        {
            var grid = new Grid("000000010400000000020000000000050407008000300001090000300400200050100000000806000");
            var actual_00 = grid.GetValue(0, 0);
            var actual_70 = grid.GetValue(7, 0);
            var actual_01 = grid.GetValue(0, 1);

            Assert.Equal(0, actual_00);
            Assert.Equal(1, actual_70);
            Assert.Equal(4, actual_01);
        }

        [Fact]
        public void ToString_WorksCorrectly()
        {
            var input = "000000010400000000020000000000050407008000300001090000300400200050100000000806000";
            var grid = new Grid(input);
            var actual = grid.ToString();
            Assert.Equal(input, actual);
        }
    }
}
