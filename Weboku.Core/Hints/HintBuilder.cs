using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;

namespace Weboku.Core.Hints
{
    public class HintBuilder
    {
        public Grid Grid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public bool[] IsRowHighlighted = new bool[9];
        public bool[] IsColHighlighted = new bool[9];
        public bool[] IsBlockHighlighted = new bool[9];

        public Color[,] CellColors { get; set; }
        public Color[,] InputColors { get; set; }
        public Color[,,] CandidatesColors { get; set; }

        public HintBuilder(Grid grid)
        {
            Grid = grid;
        }

        public HintBuilder SetTitle(string key, params object[] args)
        {
            //Title = string.Format(LanguageContainerService.Keys[key], args);
            return this;
        }

        public HintBuilder SetDescription(string key, params object[] args)
        {
            //Description = string.Format(LanguageContainerService.Keys[key], args);
            return this;
        }

        public HintBuilder HighlightRow(Position position)
        {
            IsRowHighlighted[position.y] = true;
            return this;
        }

        public HintBuilder HighlightCol(Position position)
        {
            IsColHighlighted[position.x] = true;
            return this;
        }

        public HintBuilder HighlightBlock(Position position)
        {
            HighlightBlock(position.block);
            return this;
        }

        public HintBuilder HighlightBlock(int block)
        {
            IsBlockHighlighted[block] = true;
            return this;
        }

        public HintBuilder HighlightHouse(Position position, House house)
        {
            switch (house)
            {
                case House.None:
                    break;
                case House.Row:
                    HighlightRow(position);
                    break;
                case House.Col:
                    HighlightCol(position);
                    break;
                case House.Block:
                    HighlightBlock(position);
                    break;
            }

            return this;
        }

        public HintBuilder HighlightHouses(Position position, IEnumerable<House> houses)
        {
            foreach (var house in houses)
            {
                HighlightHouse(position, house);
            }

            return this;
        }

        public HintBuilder MarkCell(Color color, Position position)
        {
            CellColors[position.x, position.y] = color;
            return this;
        }

        public HintBuilder MarkCells(Color color, IEnumerable<Position> positions)
        {
            foreach (var position in positions)
            {
                MarkCell(color, position);
            }

            return this;
        }

        public HintBuilder MarkCandidate(Color color, Position position, Value value)
        {
            CandidatesColors[position.x, position.y, value] = color;
            return this;
        }

        public HintBuilder MarkCandidates(Color color, IEnumerable<Position> positions, Value value)
        {
            foreach (var position in positions)
            {
                MarkCandidate(color, position, value);
            }

            return this;
        }

        public HintBuilder MarkInput(Color color, Position position)
        {
            InputColors[position.x, position.y] = color;
            return this;
        }


        public HintBuilder Mark(Color color, Position position, Value value)
        {
            MarkCell(color, position);
            MarkCandidate(color, position, value);
            return this;
        }

        public HintBuilder Mark(Color color, IEnumerable<Position> positions, Value value)
        {
            MarkCells(color, positions);
            MarkCandidates(color, positions, value);
            return this;
        }

        public HintBuilder Mark(Color color, IEnumerable<Position> positions, IEnumerable<Value> values)
        {
            foreach (var value in values)
            {
                Mark(color, positions, value);
            }

            return this;
        }

        public HintBuilder MarkIfHasCandidate(Color color, IEnumerable<Position> positions, Value value)
        {
            Mark(color, positions.Where(pos => Grid.HasCandidate(pos, value)), value);
            return this;
        }

        public HintBuilder MarkIfHasCandidates(Color color, IEnumerable<Position> positions, IEnumerable<Value> values)
        {
            foreach (var value in values)
            {
                MarkIfHasCandidate(color, positions, value);
            }

            return this;
        }

        public HintBuilder MarkInputOrCandidate(Color color, IEnumerable<Position> positions, Value candidate)
        {
            foreach (var pos in positions)
            {
                if (!Grid.HasValue(pos))
                {
                    MarkCandidate(color, pos, candidate);
                }
                else
                {
                    MarkInput(color, pos);
                }
            }

            return this;
        }

        public HintBuilder SetValueFilter(Value input)
        {
            //SelectValue(input);
            return this;
        }

        public Hint Build()
        {
            return new Hint
            {
                CellColors = CellColors,
                InputColors = InputColors,
                Grid = Grid,
                CandidatesColors = CandidatesColors,
                Description = Description,
                IsBlockHighlighted = IsBlockHighlighted,
                IsColHighlighted = IsColHighlighted,
                IsRowHighlighted = IsRowHighlighted,
                Title = Title
            };
        }
    }
}