using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyDataCenter.Common.Mvc
{
    public class ControllerBaseEx:ControllerBase
    {
        public ControllerBaseEx() : base()
        {
        }
        //public JsonResult JsonSmart(SmartHttpResult result)
        //{
        //    return new JsonResult(result);
        //}
        public JsonResult JsonEx(SmartHttpResult data)
        {
            return new JsonResult(data);
        }

        public JsonResult JsonIgnoreNull(SmartHttpResult data)
        {
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.NullValueHandling = NullValueHandling.Ignore;
            return new JsonResult(data, jsetting);
        }
    }
}
