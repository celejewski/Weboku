using Core.Data;
using Core.Serializers;
using System;
using System.Linq;

namespace Application.Data
{
    public class ShareManager
    {
        public static IGrid TransformGrid(IGrid input, SharedFields sharedFields)
        {
            var output = input.Clone();
            if( sharedFields == SharedFields.Everything ) return output;

            output.ClearAllCandidates();
            if( sharedFields == SharedFields.GivensAndInputs ) return output;

            foreach( var position in Position.Positions.Where(position => !output.GetIsGiven(position)) )
            {
                output.SetValue(position, Value.None);
            }
            return output;
        }

        public static string SerializeGridToShareableFormat(IGrid grid, SharedConverter sharedConverter, string baseUri)
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
