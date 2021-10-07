using System;
using Weboku.Application.Enums;
using Weboku.Application.Filters;
using Weboku.Core.Data;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private Tool _tool = Tool.Marker;
        public event EventHandler<Tool> OnToolChanged;

        private Value _selectedValue = Value.One;
        public event EventHandler<Value> OnValueChanged;

        private Color PrimaryColor { get; set; } = Color.First;
        private Color SecondaryColor { get; set; } = Color.Second;

        public void UsePrimaryTool(Position position)
        {
            _historyManager.Save(Grid);

            switch (_tool)
            {
                case Tool.Marker:
                    _toolManager.UseMarker(Grid, position, _selectedValue);
                    break;
                case Tool.Pencil:
                    _toolManager.UsePencil(Grid, position, _selectedValue);
                    break;
                case Tool.Eraser:
                    _toolManager.UseEraser(Grid, position);
                    break;
                case Tool.Brush:
                    _toolManager.UseBrush(_colorManager, position, PrimaryColor);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            GridChanged();
        }

        public void UseSecondaryTool(Position position)
        {
            _historyManager.Save(Grid);

            switch (_tool)
            {
                case Tool.Marker:
                    _toolManager.UsePencil(Grid, position, _selectedValue);
                    break;
                case Tool.Pencil:
                    _toolManager.UseMarker(Grid, position, _selectedValue);
                    break;
                case Tool.Eraser:
                    _toolManager.UseEraser(Grid, position);
                    break;
                case Tool.Brush:
                    _toolManager.UseBrush(_colorManager, position, SecondaryColor);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            GridChanged();
        }

        public void SelectValue(Value value)
        {
            _selectedValue = value;
            var selectedValueFilter = new SelectedValueFilter(value);
            SetFilter(selectedValueFilter);
            OnValueChanged?.Invoke(this, value);
        }

        public void SelectTool(Tool tool)
        {
            _tool = tool;
            OnToolChanged?.Invoke(this, tool);
        }

        public void SelectPrimaryColor(Color color)
        {
            PrimaryColor = color;
        }

        public void SelectSecondaryColor(Color color)
        {
            SecondaryColor = color;
        }
    }
}