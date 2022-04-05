using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary;
using MyDataModels;

namespace SixLinksDataService
{
	public interface ISixLinksData
	{
		Task<List<Actor>> GetActorsFromDB(Movie selectedMovie);
	}
}
