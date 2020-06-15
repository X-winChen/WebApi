
using API.DataAccess.Comm;
using API.DataAccess.Home;
using API.Models.Article;
using API.Models.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace API.Business.Home
{
    public class HomeBusiness
    {
        public  ObservableCollection<Menu> GetMune()
        {
            return HomeDataAccess.GetMune();
        }

        public ObservableCollection<Article> GetArticleTop5()
        {
            return HomeDataAccess.GetArticleTop5();
        }
    }
}
