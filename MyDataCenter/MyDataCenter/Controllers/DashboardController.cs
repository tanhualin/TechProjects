using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using MyDataCenter.Common.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyDataCenter.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : ControllerBaseEx
    {
        [Authorize(Roles = "Developer,Admin,Operate,View,Approval")]
        [HttpGet]
        public IActionResult GetLoadMenu()
        {
            SmartHttpResult<List<Models.JsonTreeNode>> result = new SmartHttpResult<List<Models.JsonTreeNode>>();
            try
            {
                var entityList = DAL.SystemManage.SmartMenu.getMenuByUserName(HttpContext.User.Identity.Name);
                List<Models.JsonTreeNode> menuList = new List<Models.JsonTreeNode>();
                foreach (var entity in entityList)
                {
                    if (entity.ParentIdx == null)
                    {
                        Models.JsonTreeNode node = new Models.JsonTreeNode();
                        //node.Idx = entity.Idx;
                        node.text = entity.ModuleName;
                        node.link = entity.Link;
                        node.icon = entity.Icon;
                        Common.Helper.SmartMenuTreeHelper.LoadTree(entityList, node, entity.Idx);
                        menuList.Add(node);
                    }
                }
                result.Set(true, menuList);
            }
            catch (Exception err)
            {
                result.Set(false, err.Message);
            }
            return JsonIgnoreNull(result);
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
