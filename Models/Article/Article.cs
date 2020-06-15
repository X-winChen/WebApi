using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.Models.Article
{
	public class Article
	{
		[Key]
		public int ID { get; set; }

		public string Name { get; set; }

		public string Path { get; set; }

		public string Title { get; set; }

		public int Type { get; set; }

		public string CreateDate { get; set; }

		public string DateTime { get; set; }

		public string Author { get; set; }


		public string Text { get; set; }
    }
}
