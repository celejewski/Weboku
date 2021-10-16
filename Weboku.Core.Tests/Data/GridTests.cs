using FluentAssertions;
using System;
using Weboku.Core.Data;
using Weboku.Core.Serializers;
using Xunit;

namespace Weboku.Core.Tests.Data
{
    public class GridTests
    {
        private Position _position = new Position(0, 0);

        [Fact]
        public void Initial_grid_should_have_no_values_or_candidates()
        {
            var sut = new Grid();

            foreach (var position in Position.Positions)
            {
                sut.GetValue(position).Should().Be(Value.None);
                sut.GetCandidates(position).Should().Be(Candidates.None);
            }
        }

        [Fact]
        public void SetValue_changes_state_of_grid()
        {
            var grid = new Grid();
            var position = _position;
            var value = Value.One;

            grid.SetValue(position, value);

            grid.GetValue(position).Should().Be(value);
        }

        [Fact]
        public void SetValue_should_remove_candidates()
        {
            var sut = new Grid();
            var position = _position;
            sut.AddCandidate(position, Value.One);

            sut.SetValue(position, Value.Nine);

            sut.GetCandidates(position).Should().Be(Candidates.None);
        }

        [Fact]
        public void SetGiven_should_change_set_is_given()
        {
            var sut = new Grid();
            sut.SetValue(_position, Value.One);

            sut.SetIsGiven(_position, true);

            sut.GetIsGiven(_position).Should().BeTrue();
        }

        [Fact]
        public void SetGiven_should_throw_exception_if_value_is_set_to_none()
        {
            var sut = new Grid();
            sut.SetValue(_position, Value.None);

            Action action = () => sut.SetIsGiven(_position, true);
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void SetGiven_should_allow_to_set_false_even_when_value_is_none()
        {
            var sut = new Grid();
            sut.SetValue(_position, Value.None);

            Action action = () => sut.SetIsGiven(_position, false);
            action.Should().NotThrow();
        }

        [Fact]
        public void AddCandidate_should_set_candidate()
        {
            var sut = new Grid();
            var position = _position;

            sut.AddCandidate(position, Value.One);

            sut.GetCandidatesCount(position).Should().Be(1);
            sut.GetCandidates(position).Should().Be(Candidates.One);
        }

        [Fact]
        public void AddCandidate_should_set_candidate_if_called_multiple_times_with_same_value()
        {
            var sut = new Grid();
            var position = _position;

            sut.AddCandidate(position, Value.One);
            sut.AddCandidate(position, Value.One);
            sut.AddCandidate(position, Value.One);

            sut.GetCandidatesCount(position).Should().Be(1);
            sut.GetCandidates(position).Should().Be(Candidates.One);
        }

        [Fact]
        public void AddCandidate_multiple_calls_should_combine_various_candidates()
        {
            var sut = new Grid();
            var position = _position;

            sut.AddCandidate(position, Value.One);
            sut.AddCandidate(position, Value.Five);
            sut.AddCandidate(position, Value.Eight);

            sut.GetCandidatesCount(position).Should().Be(3);
            sut.HasCandidate(position, Value.One).Should().BeTrue();
            sut.HasCandidate(position, Value.Five).Should().BeTrue();
            sut.HasCandidate(position, Value.Eight).Should().BeTrue();
        }

        [Fact]
        public void ToggleCandidate_should_add_candidate_if_it_is_missing()
        {
            var sut = new Grid();

            sut.ToggleCandidate(_position, Value.One);

            sut.GetCandidatesCount(_position);
            sut.GetCandidates(_position).Should().Be(Candidates.One);
        }

        [Fact]
        public void ToggleCandidate_should_turn_off_candidate_if_it_is_present()
        {
            var sut = new Grid();
            sut.AddCandidate(_position, Value.One);

            sut.ToggleCandidate(_position, Value.One);

            sut.GetCandidatesCount(_position).Should().Be(0);
            sut.GetCandidates(_position).Should().Be(Candidates.None);
        }

        [Fact]
        public void RemoveCandidate_should_remove_existing_candidate()
        {
            var sut = new Grid();
            sut.AddCandidate(_position, Value.One);

            sut.RemoveCandidate(_position, Value.One);

            sut.GetCandidates(_position).Should().Be(Candidates.None);
            sut.GetCandidatesCount(_position).Should().Be(0);
        }

        [Fact]
        public void RemoveCandidate_should_not_throw_exception_when_trying_to_remove_not_existing_candidate()
        {
            var sut = new Grid();

            sut.RemoveCandidate(_position, Value.One);

            sut.GetCandidates(_position).Should().Be(Candidates.None);
            sut.GetCandidatesCount(_position).Should().Be(0);
        }

        [Fact]
        public void GetCandidatesCount_should_return_correct_value_if_there_are_multiple_candidates()
        {
            var sut = new Grid();
            sut.AddCandidate(_position, Value.Five);
            sut.AddCandidate(_position, Value.One);

            sut.GetCandidatesCount(_position).Should().Be(2);
        }

        [Fact]
        public void GetCandidatesCount_should_return_0_if_there_are_no()
        {
            var sut = new Grid();

            sut.GetCandidatesCount(_position).Should().Be(0);
        }

        [Fact]
        public void HasCandidate_should_return_true_only_for_present_candidates()
        {
            var sut = new Grid();
            sut.AddCandidate(_position, Value.One);
            sut.AddCandidate(_position, Value.Two);

            sut.HasCandidate(_position, Value.One).Should().BeTrue();
            sut.HasCandidate(_position, Value.Two).Should().BeTrue();
            sut.HasCandidate(_position, Value.Three).Should().BeFalse();
            sut.HasCandidate(_position, Value.Four).Should().BeFalse();
            sut.HasCandidate(_position, Value.Five).Should().BeFalse();
            sut.HasCandidate(_position, Value.Six).Should().BeFalse();
            sut.HasCandidate(_position, Value.Seven).Should().BeFalse();
            sut.HasCandidate(_position, Value.Eight).Should().BeFalse();
            sut.HasCandidate(_position, Value.Nine).Should().BeFalse();
        }

        [Fact]
        public void ClearAllCandidates_should_remove_candidates_from_position()
        {
            var sut = new Grid();
            sut.AddCandidate(_position, Value.One);
            sut.AddCandidate(_position, Value.Five);

            sut.ClearCandidates(_position);

            sut.GetCandidates(_position).Should().Be(Candidates.None);
            sut.GetCandidatesCount(_position).Should().Be(0);
        }

        [Fact]
        public void ClearCandidatesEverywhere_should_remove_all_candidates_from_all_positions()
        {
            var sut = new Grid();
            var position00 = new Position(0, 0);
            var position88 = new Position(8, 8);
            sut.AddCandidate(position00, Value.One);
            sut.AddCandidate(position00, Value.Two);
            sut.AddCandidate(position88, Value.One);
            sut.AddCandidate(position88, Value.Two);

            sut.ClearCandidatesEverywhere();

            sut.GetCandidates(position00).Should().Be(Candidates.None);
            sut.GetCandidates(position88).Should().Be(Candidates.None);
        }

        [Fact]
        public void HasValue_return_false_if_value_is_none()
        {
            var sut = new Grid();
            sut.SetValue(_position, Value.None);

            sut.HasValue(_position).Should().BeFalse();
        }

        [Fact]
        public void HasValue_return_true_if_value_is_different_than_none()
        {
            var sut = new Grid();
            sut.SetValue(_position, Value.One);

            sut.HasValue(_position).Should().BeTrue();
        }

        [Fact]
        public void IsCandidateLegal_is_false_when_position_shares_block_with_set_value()
        {
            var sut = new Grid();
            var position11 = new Position(1, 1);
            var value = Value.One;
            sut.SetValue(position11, value);

            var position00 = new Position(0, 0);
            sut.IsCandidateLegal(position00, value).Should().BeFalse();
        }

        [Fact]
        public void IsCandidateLegal_is_false_when_position_shares_col_with_set_value()
        {
            var sut = new Grid();
            var position08 = new Position(0, 8);
            var value = Value.One;
            sut.SetValue(position08, value);

            var position00 = new Position(0, 0);
            sut.IsCandidateLegal(position00, value).Should().BeFalse();
        }

        [Fact]
        public void IsCandidateLegal_is_false_when_position_shares_row_with_set_value()
        {
            var sut = new Grid();
            var position08 = new Position(8, 0);
            var value = Value.One;
            sut.SetValue(position08, value);

            var position00 = new Position(0, 0);
            sut.IsCandidateLegal(position00, value).Should().BeFalse();
        }

        [Fact]
        public void IsCandidateLegal_is_true_if_candidate_does_not_share_house_with_set_value()
        {
            var sut = new Grid();
            sut.IsCandidateLegal(_position, Value.One).Should().BeTrue();
        }

        [Fact]
        public void IsCandidateLegal_is_true_no_matter_what_for_value_none()
        {
            var sut = new Grid();
            sut.SetValue(new Position(0, 1), Value.None);
            sut.SetValue(new Position(1, 1), Value.None);
            sut.SetValue(new Position(1, 0), Value.None);

            var actual = sut.IsCandidateLegal(new Position(0, 0), Value.None);
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsValueLegal_is_false_when_there_is_the_same_value_in_block()
        {
            var sut = new Grid();
            var position00 = new Position(0, 0);
            sut.SetValue(position00, Value.One);
            var position11 = new Position(1, 1);
            sut.SetValue(position11, Value.One);

            sut.IsValueLegal(position00).Should().BeFalse();
            sut.IsValueLegal(position11).Should().BeFalse();
        }

        [Fact]
        public void IsValueLegal_is_false_when_there_is_the_same_value_in_row()
        {
            var sut = new Grid();
            var position00 = new Position(0, 0);
            sut.SetValue(position00, Value.One);
            var position80 = new Position(8, 0);
            sut.SetValue(position80, Value.One);

            sut.IsValueLegal(position00).Should().BeFalse();
            sut.IsValueLegal(position80).Should().BeFalse();
        }

        [Fact]
        public void IsValueLegal_is_false_when_there_is_the_same_value_in_col()
        {
            var sut = new Grid();
            var position00 = new Position(0, 0);
            sut.SetValue(position00, Value.One);
            var position08 = new Position(0, 8);
            sut.SetValue(position08, Value.One);

            sut.IsValueLegal(position00).Should().BeFalse();
            sut.IsValueLegal(position08).Should().BeFalse();
        }

        [Fact]
        public void IsValueLegal_is_true_when_there_is_the_no_same_value_in_row_and_col_and_block()
        {
            var sut = new Grid();
            var position00 = new Position(0, 0);
            sut.SetValue(position00, Value.One);

            sut.IsValueLegal(position00).Should().BeTrue();
        }

        [Fact]
        public void Restart_should_remove_candidates()
        {
            var sut = new Grid();
            sut.AddCandidate(_position, Value.One);
            sut.AddCandidate(_position, Value.Two);
            sut.AddCandidate(_position, Value.Three);

            sut.Restart();

            sut.GetCandidatesCount(_position).Should().Be(0);
            sut.GetCandidates(_position).Should().Be(Candidates.None);
        }


        [Fact]
        public void Restart_should_remove_input_which_is_not_given()
        {
            var sut = new Grid();
            sut.SetValue(_position, Value.One);
            sut.SetIsGiven(_position, false);

            sut.Restart();

            sut.GetValue(_position).Should().Be(Value.None);
            sut.GetIsGiven(_position).Should().BeFalse();
        }

        [Fact]
        public void Restart_should_leave_input_which_is_given()
        {
            var sut = new Grid();
            sut.SetValue(_position, Value.One);
            sut.SetIsGiven(_position, true);

            sut.Restart();

            sut.GetValue(_position).Should().Be(Value.One);
            sut.GetIsGiven(_position).Should().BeTrue();
        }

        [Fact]
        public void IsSudokuSolved_should_return_false_if_it_is_not_finished()
        {
            var sut = new Grid();

            sut.IsSudokuSolved().Should().BeFalse();
        }

        [Fact]
        public void IsSudokuSolved_should_return_false_if_has_invalid_input()
        {
            var hodokuGridSerializer = new HodokuGridSerializer();
            var input = "017289435329514678485736219531492867278651394964873152896145723143927586752368941";
            var sut = hodokuGridSerializer.Deserialize(input);

            sut.SetValue(new Position(0, 0), Value.One); // Value.Six is solution

            sut.IsSudokuSolved().Should().BeFalse();
        }

        [Fact]
        public void IsSudokuSolved_should_return_true_if_has_all_inputs_valid()
        {
            var hodokuGridSerializer = new HodokuGridSerializer();
            var input = "017289435329514678485736219531492867278651394964873152896145723143927586752368941";
            var sut = hodokuGridSerializer.Deserialize(input);

            sut.SetValue(new Position(0, 0), Value.Six);

            sut.IsSudokuSolved().Should().BeTrue();
        }

        [Fact]
        public void FillAllLegalCandidates_should_show_only_valid_candidates()
        {
            var sut = new Grid();
            sut.SetValue(new Position(0, 6), Value.One);
            sut.SetValue(new Position(0, 3), Value.Two);
            sut.SetValue(new Position(1, 1), Value.Three);
            sut.SetValue(new Position(3, 0), Value.Four);
            sut.SetValue(new Position(6, 0), Value.Five);

            sut.FillAllLegalCandidates();

            var position = new Position(0, 0);
            sut.GetCandidatesCount(position).Should().Be(4);
            sut.HasCandidate(position, Value.One).Should().BeFalse();
            sut.HasCandidate(position, Value.Two).Should().BeFalse();
            sut.HasCandidate(position, Value.Three).Should().BeFalse();
            sut.HasCandidate(position, Value.Four).Should().BeFalse();
            sut.HasCandidate(position, Value.Five).Should().BeFalse();
            sut.HasCandidate(position, Value.Six).Should().BeTrue();
            sut.HasCandidate(position, Value.Seven).Should().BeTrue();
            sut.HasCandidate(position, Value.Eight).Should().BeTrue();
            sut.HasCandidate(position, Value.Nine).Should().BeTrue();
        }

        [Fact]
        public void Clone_should_have_same_inputs()
        {
            var sut = new Grid();
            sut.SetValue(_position, Value.One);

            var actual = sut.Clone();

            actual.GetValue(_position).Should().Be(Value.One);
        }

        [Fact]
        public void Clone_should_have_same_candidates()
        {
            var sut = new Grid();
            sut.AddCandidate(_position, Value.One);
            sut.AddCandidate(_position, Value.Two);
            sut.AddCandidate(_position, Value.Three);

            var actual = sut.Clone();

            actual.GetCandidatesCount(_position).Should().Be(3);
            actual.HasCandidate(_position, Value.One).Should().BeTrue();
            actual.HasCandidate(_position, Value.Two).Should().BeTrue();
            actual.HasCandidate(_position, Value.Three).Should().BeTrue();
        }
    }
}