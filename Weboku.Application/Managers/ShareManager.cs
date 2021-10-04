using System;
using System.Linq;
using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.Core.Serializers;

namespace Weboku.Application.Managers
{
    internal sealed class ShareManager
    {
        private Grid _sourceGrid;
        private Grid _transformedGrid;
        private bool _isDirty = true;
        private SharedFields _sharedFields = SharedFields.Everything;
        private readonly string _baseUri;

        public ShareManager(string baseUri)
        {
            _baseUri = baseUri;
            _sourceGrid = new Grid();
            _transformedGrid = new Grid();
        }

        public Grid Grid
        {
            get
            {
                if (_isDirty)
                {
                    _transformedGrid = TransformGrid(_sourceGrid, SharedFields);
                    _isDirty = false;
                }

                return _transformedGrid;
            }
        }

        public void UpdateGrid(Grid grid)
        {
            _sourceGrid = grid;
            _isDirty = true;
        }

        public SharedConverter SharedConverter { get; set; } = SharedConverter.MyLink;

        public SharedFields SharedFields
        {
            get { return _sharedFields; }
            set
            {
                _sharedFields = value;
                _isDirty = true;
            }
        }

        public string SharedOutput
        {
            get => SerializeGridToShareableFormat(Grid, SharedConverter, _baseUri);
        }

        private static Grid TransformGrid(Grid input, SharedFields sharedFields)
        {
            var output = input.Clone();
            if (sharedFields == SharedFields.Everything) return output;

            output.ClearAllCandidates();
            if (sharedFields == SharedFields.GivensAndInputs) return output;

            foreach (var position in Position.Positions.Where(position => !output.GetIsGiven(position)))
            {
                output.SetValue(position, Value.None);
            }

            return output;
        }

        private static string SerializeGridToShareableFormat(Grid grid, SharedConverter sharedConverter, string baseUri)
        {
            var gridSerializer = sharedConverter switch
            {
                SharedConverter.Hodoku => GridSerializerFactory.Make(GridSerializerName.Hodoku),
                SharedConverter.MyFormat => GridSerializerFactory.Make(GridSerializerName.Base64),
                SharedConverter.MyLink => GridSerializerFactory.Make(GridSerializerName.Base64),
                _ => throw new ArgumentException($"Incorrect option: {nameof(sharedConverter)} = {sharedConverter}")
            };

            var serialized = gridSerializer.Serialize(grid);
            return sharedConverter == SharedConverter.MyLink
                ? $"{baseUri}paste/{serialized}"
                : serialized;
        }
    }
}