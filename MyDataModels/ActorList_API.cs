using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataModels
{
	public class ActorList_API
	{
		public string searchType { get; set; }
		public string expression { get; set; }
		public List<Result> results { get; set; }
		public string errorMessage { get; set; }
	}

	public class Result
	{
		public string id { get; set; }
		public string resultType { get; set; }
		public string image { get; set; }
		public string title { get; set; }
		public string description { get; set; }
	}
}
