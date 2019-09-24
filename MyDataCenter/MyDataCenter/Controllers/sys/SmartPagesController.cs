using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDataCenter.Common.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyDataCenter.Controllers.sys
{
    [Route("api/[controller]")]
    public class SmartPagesController : ControllerBaseEx
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            SmartHttpResult<List<Models.SystemManage.SmartPagesModel>> result = new SmartHttpResult<List<Models.SystemManage.SmartPagesModel>>();
            result.status = true;
            result.data = DAL.SystemManage.SmartPages.GetPages();
            return JsonEx(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
