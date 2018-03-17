using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using REST_API.Models;

namespace REST_API.Controllers
{
    [Produces("application/json")]
    [Route("api/Values")]
    public class ValuesController : Controller
    {
        private static readonly TestModel _model = new TestModel();

        [HttpGet]
        public IActionResult GetAll()
        {
            if (_model.GetAll() == null)
                return NotFound();
            
            return new OkObjectResult(_model.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (_model.Get(id) == null)
                return new NotFoundObjectResult(id);

            return new OkObjectResult(_model.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] TestData item)
        {
            _model.Add(item);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] TestData item)
        {
            item.Id = id;
            if (!_model.Update(item))
                return new NotFoundObjectResult(id);

            return new OkObjectResult(id);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (_model.Get(id) == null)
                return new NotFoundObjectResult(id);

            _model.Remove(id);
            return new OkObjectResult(id);
        }
    }
}