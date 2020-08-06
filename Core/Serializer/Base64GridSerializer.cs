using Core.Data;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Numerics;
using System.Text;

namespace Core.Serializer
{
    internal class Base64GridSerializer : IGridSerializer
    {
        private readonly IGridSerializer _innerConverter = new HodokuGridSerializer();

        public IGrid Deserialize(string text)
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
            foreach( var pos in Position.All )
            {
                sb.Append(grid.GetValue(pos));
            }
            var bigInt = BigInteger.Parse(sb.ToString());
            var bytes = bigInt.ToByteArray();
            return WebEncoders.Base64UrlEncode(bytes);
        }
    }
}
