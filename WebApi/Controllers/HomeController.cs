using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using API.Business.Home;
using API.DataAccess.Comm;
using API.Models.Article;
using API.Models.Core;
using API.Models.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebApi.Core;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        HomeBusiness _homeBusiness = new HomeBusiness();    
        

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public AjaxRspJson GetMenu()
        {
            ObservableCollection < Menu > model= _homeBusiness.GetMune();

            return new AjaxRspJson { RspCode = 1, RspMsg="成功", ObjectData = model };
        }

        [HttpPost]
        public AjaxRspJson GetArticleTop5()
        {
            ObservableCollection<Article> ArticleModel = _homeBusiness.GetArticleTop5();

            return new AjaxRspJson { RspCode = 1, RspMsg = "成功", ObjectData = ArticleModel };
        }



    }
}