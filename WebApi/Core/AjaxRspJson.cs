using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Core
{
    public class AjaxRspJson
    {
        /// <summary>
        /// 返回代码 0失败 1成功
        /// </summary>
        public int RspCode { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string RspMsg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public string RsgData { get; set; }

        public object ObjectData { get; set; }
        public AjaxRspJson()
        {
            this.RspCode = 0;
            this.RspMsg = string.Empty;
            this.RsgData = string.Empty;
        }

        public AjaxRspJson(HttpContext httpContext)
        {
            this.RspCode = 0;
            this.RspMsg = string.Empty;
            this.RsgData = string.Empty;
        }

        /// <summary>
        /// 将对象转换为JSON字符串
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            this.RsgData = JsonConvert.SerializeObject(ObjectData);

            JObject jObject = new JObject();

            jObject.Add("RspCode", this.RspCode);

            jObject.Add("RspMsg", this.RspMsg);

            jObject.Add("RspData", this.RsgData);

            return JsonConvert.SerializeObject(jObject, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });


        }
    }
}
