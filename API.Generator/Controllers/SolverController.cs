using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Weboku.Core.Serializers;
using Weboku.Core.Solvers;

namespace Weboku.Generator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolverController : ControllerBase
    {
        private IEnumerable<IGridSerializer> _serializers = new List<IGridSerializer>
        {
            GridSerializerFactory.Make(GridSerializerName.Base64),
            GridSerializerFactory.Make(GridSerializerName.Base64, GridSerializerMode.OnlyInputs),
            GridSerializerFactory.Make(GridSerializerName.Hodoku)
        };

        private readonly ISolver _solver = new BruteForceSolver();

        private bool IsValidFormat(string serializedGrid)
        {
            return _serializers.Any(serializer => serializer.IsValidFormat(serializedGrid));
        }

        private IGridSerializer SelectSerializer(string serializedGrid)
        {
            return _serializers.FirstOrDefault(serializer => serializer.IsValidFormat(serializedGrid));
        }

        [HttpGet]
        [Route("{serializedGrid}")]
        [Produces("text/plain")]
        public ActionResult<string> Get(string serializedGrid)
        {
            if (!IsValidFormat(serializedGrid)) return BadRequest();

            var serializer = SelectSerializer(serializedGrid);
            var grid = serializer.Deserialize(serializedGrid);
            var solvedGrid = _solver.SolveGivens(grid);
            return new OkObjectResult(serializer.Serialize(solvedGrid));
        }
    }
}