﻿using System;
using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private Tool _tool = Tool.Marker;
        public event EventHandler<Tool> OnToolChanged;

        private Value _selectedValue = Value.One;
        public event EventHandler<Value> OnValueChanged;

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
                default:
                    throw new InvalidOperationException();
            }

            GridChanged();
        }

        public void SelectValue(Value value)
        {
            if (value == Value.None) throw new ArgumentException(nameof(value));

            _selectedValue = value;
            OnValueChanged?.Invoke(this, value);
        }

        public void SelectTool(Tool tool)
        {
            _tool = tool;
            OnToolChanged?.Invoke(this, tool);
        }
    }
}