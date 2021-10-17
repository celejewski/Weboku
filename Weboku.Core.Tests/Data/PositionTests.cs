using FluentAssertions;
using System;
using System.Linq;
using Weboku.Core.Data;
using Xunit;

namespace Weboku.Core.Tests
{
    public class PositionTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 3, 0)]
        [InlineData(2, 6, 0)]
        [InlineData(3, 0, 3)]
        [InlineData(4, 3, 3)]
        [InlineData(5, 6, 3)]
        [InlineData(6, 0, 6)]
        [InlineData(7, 3, 6)]
        [InlineData(8, 6, 6)]
        public void TopLeftCornerOfBlock_should_return_valid_position(int block, int expectedX, int expectedY)
        {
            var actual = Position.TopLeftCornerOfBlock(block);
            actual.X.Should().Be(expectedX);
            actual.Y.Should().Be(expectedY);
        }

        [Fact]
        public void GetHouseOf_positions_with_same_x_share_row_house()
        {
            var actual = Position.GetHouseOf(new Position(0, 0), new Position(8, 0));
            actual.Should().Be(House.Row);
        }

        [Fact]
        public void GetHouseOf_positions_with_same_y_share_col_house()
        {
            var actual = Position.GetHouseOf(new Position(0, 8), new Position(0, 8));
            actual.Should().Be(House.Col);
        }

        [Fact]
        public void GetHouseOf_positions_with_same_block_share_block_house()
        {
            var actual = Position.GetHouseOf(new Position(0, 0), new Position(2, 2));
            actual.Should().Be(House.Block);
        }

        [Fact]
        public void GetHouseOf_positions_with_different_x_y_and_block_have_none_house()
        {
            var actual = Position.GetHouseOf(new Position(0, 0), new Position(8, 8));
            actual.Should().Be(House.None);
        }

        [Fact]
        public void GetHouseOf_should_throw_exception_for_empty_collection()
        {
            var positions = new Position[0];
            Action action = () => Position.GetHouseOf(positions);

            action.Should().Throw<Exception>();
        }

        [Fact]
        public void GetPositionsSeenBy_should_return_positions_which_share_x_or_y_or_block_with_input_position_but_not_input_position_itself()
        {
            var actual = Position.GetPositionsSeenBy(new Position(0, 0));
            actual.Should().HaveCount(20);

            // Block
            actual.Should().Contain(new Position(1, 0));
            actual.Should().Contain(new Position(2, 0));
            actual.Should().Contain(new Position(0, 1));
            actual.Should().Contain(new Position(1, 1));
            actual.Should().Contain(new Position(2, 1));
            actual.Should().Contain(new Position(0, 2));
            actual.Should().Contain(new Position(1, 2));
            actual.Should().Contain(new Position(2, 2));

            // Row
            actual.Should().Contain(new Position(1, 0));
            actual.Should().Contain(new Position(2, 0));
            actual.Should().Contain(new Position(3, 0));
            actual.Should().Contain(new Position(4, 0));
            actual.Should().Contain(new Position(5, 0));
            actual.Should().Contain(new Position(6, 0));
            actual.Should().Contain(new Position(7, 0));
            actual.Should().Contain(new Position(8, 0));

            // Col
            actual.Should().Contain(new Position(0, 1));
            actual.Should().Contain(new Position(0, 2));
            actual.Should().Contain(new Position(0, 3));
            actual.Should().Contain(new Position(0, 4));
            actual.Should().Contain(new Position(0, 5));
            actual.Should().Contain(new Position(0, 6));
            actual.Should().Contain(new Position(0, 7));
            actual.Should().Contain(new Position(0, 8));
        }

        [Fact]
        public void GetPositionsSeenByAll_should_return_empty_collection()
        {
            var positions = new[]
            {
                new Position(0, 0),
                new Position(2, 2),
                new Position(8, 8)
            };
            var actual = Position.GetPositionsSeenByAll(positions);
            actual.Should().BeEmpty();
        }

        [Fact]
        public void GetPositionsSeenByAll_should_return_positions_seen_by_all_input_positions()
        {
            var positions = new[]
            {
                new Position(0, 0),
                new Position(0, 1),
                new Position(0, 2)
            };
            var actual = Position.GetPositionsSeenByAll(positions);
            actual.Should().HaveCount(15); // 9 from block + 9 from column - 3 which are in block and column

            // Column
            actual.Should().Contain(new Position(0, 0));
            actual.Should().Contain(new Position(0, 1));
            actual.Should().Contain(new Position(0, 2));
            actual.Should().Contain(new Position(0, 3));
            actual.Should().Contain(new Position(0, 4));
            actual.Should().Contain(new Position(0, 5));
            actual.Should().Contain(new Position(0, 6));
            actual.Should().Contain(new Position(0, 7));
            actual.Should().Contain(new Position(0, 8));

            // Block
            actual.Should().Contain(new Position(0, 0));
            actual.Should().Contain(new Position(0, 1));
            actual.Should().Contain(new Position(0, 2));
            actual.Should().Contain(new Position(1, 0));
            actual.Should().Contain(new Position(1, 1));
            actual.Should().Contain(new Position(1, 2));
            actual.Should().Contain(new Position(2, 0));
            actual.Should().Contain(new Position(2, 1));
            actual.Should().Contain(new Position(2, 2));
        }

        [Fact]
        public void Equals_should_return_true_when_positions_have_same_x_and_y()
        {
            var sut = new Position(0, 0);

            sut.Equals(new Position(0, 0)).Should().BeTrue();
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        public void Equals_should_return_false_when_positions_have_different_x_or_y(int x, int y)
        {
            var sut = new Position(0, 0);
            sut.Equals(new Position(x, y)).Should().BeFalse();
        }

        [Fact]
        public void Equals_is_false_when_argumnet_is_not_position_or_ValueTuple()
        {
            var sut = new Position(0, 0);
            sut.Equals(new object()).Should().BeFalse();
        }

        [Fact]
        public void Equals_using_ValueTuple_should_return_true_when_positions_have_same_x_and_y()
        {
            var sut = new Position(0, 0);

            sut.Equals(new Position(0, 0)).Should().BeTrue();
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        public void Equals_using_ValueTuple_should_return_false_when_positions_have_different_x_or_y(int x, int y)
        {
            var sut = new Position(0, 0);
            sut.Equals((x, y)).Should().BeFalse();
        }

        [Theory]
        [InlineData(0, 0, "r1c1")]
        [InlineData(8, 8, "r9c9")]
        public void ToString_should_return_formated_x_y(int x, int y, string expected)
        {
            var sut = new Position(x, y);

            sut.ToString().Should().Be(expected);
        }
    }
}