using API.DataAccess.Comm;
using API.Models.Article;
using API.Models.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace API.DataAccess.Home
{
    public class HomeDataAccess
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Menu> GetMune()
        {
            string strSql = @"select * from sys_Menu";

            ObservableCollection<Menu> MenuModel = DataBase.Query<Menu>(strSql);

            return MenuModel;
        }
        public static ObservableCollection<Article> GetArticleTop5()
        {
            string strSql = @"select top(5) * from mb_Article";

            ObservableCollection<Article> ArticleModel = DataBase.Query<Article>(strSql);

            foreach (var item in ArticleModel)
            {
                item.DateTime = DateTime.Parse(item.CreateDate).ToString("YYYY-MM-DD");

                item.Text= File.ReadAllText(@"." + item.Path + ".txt");

                item.Text = item.Text.Replace("\r\n", "</p> <p>");

                item.Text = item.Text.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

                item.Text = "<p>" + item.Text + "</P>";
            }

            return ArticleModel;

        }
        public static string GetArticleText()
        {
            string strSql = @"select top(5) * from mb_Article";

            ObservableCollection<Article> ArticleModel = DataBase.Query<Article>(strSql);

            string text = string.Empty;

            foreach (var item in ArticleModel)
            {
                //FileStream fileStream = new FileStream("."+item.Path+".txt",FileMode.Open);
                 text = System.IO.File.ReadAllText(@"." + item.Path + ".txt");
                //fileStream.Read();
                //fileStream.Seek(0,SeekOrigin.Begin);

                //fileStream.Read(bt, 0, 100);

               ///Decoder decoder = Encoding.UTF8.GetDecoder();

                //decoder.GetChars(bt, 0, bt.Length, Chartext, 0);

                //text =new string(Chartext);

            }
            return text;
        }

    }
}
