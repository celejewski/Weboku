using Core.Data;
using Core.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Numerics;
using System.Text;

namespace Core.Serializers
{
    internal class Base64GridSerializer : IGridSerializer
    {
        private readonly IGridSerializer _innerConverter = new HodokuGridSerializer();

        public IGrid Deserialize(string text)
        {
            try
            {
                var bytes = WebEncoders.Base64UrlDecode(text);
                var bigInt = new BigInteger(bytes);
                var parsed = bigInt.ToString().Substring(1);

                if( parsed.Length != 81 )
                {
                    throw new ArgumentException();
                }

                return _innerConverter.Deserialize(parsed);
            }

            catch( Exception ex )
            {
                throw new GridSerializationException($"Exception in {nameof(Base64GridSerializer)} occured during {nameof(Deserialize)} with value \"{text}\" ", ex);
            }
        }

        public bool IsValidFormat(string text)
        {
            try
            {
                Deserialize(text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Serialize(IGrid grid)
        {
            var sb = new StringBuilder();
            sb.Append("1");
            foreach( var pos in Position.Positions )
            {
                sb.Append(grid.GetValue(pos));
            }
            var bigInt = BigInteger.Parse(sb.ToString());
            var bytes = bigInt.ToByteArray();
            return WebEncoders.Base64UrlEncode(bytes);
        }
    }
}
