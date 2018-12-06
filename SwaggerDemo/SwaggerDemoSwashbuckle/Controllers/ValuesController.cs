using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SwaggerDemo.Controllers
{
    public enum TestEnumValue
    {
        /// <summary>
        /// Value1
        /// </summary>
        Value1,
        /// <summary>
        /// Value2
        /// </summary>
        Value2,
        /// <summary>
        /// Value3
        /// </summary>
        Value3
    }
    /// <summary>
    /// This is Value Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// This is demo XML documentation: Get method
        /// </summary>
        /// <returns>List of string</returns>
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// This is demo XML documentation: Post method
        /// </summary>
        /// <param name="value">Value that inserted</param>
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value,TestEnumValue enumValue)
        {
        }

        /// <summary>
        /// This is demo XML documentation: Put method
        /// </summary>
        /// <param name="id"> Value that updated </param>
        /// <param name="value"> New value</param>
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// This is demo XML documentation: Delete method
        /// </summary>
        /// <param name="id"> value that delete</param>
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
