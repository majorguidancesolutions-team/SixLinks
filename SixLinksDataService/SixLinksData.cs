using DataLibrary;
using Microsoft.EntityFrameworkCore;
using MyDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixLinksDataService
{
	public class SixLinksData : ISixLinksData
	{
        private DataDbContext _context;
        public SixLinksData(DataDbContext context)
        {
            _context = context;
        }

        public async Task<List<Actor>> GetActorsFromDB(Movie selectedMovie)
		{
			var movieData = await _context.Movies
				.Include(x => x.MovieActors)
				.ThenInclude(y => y.Actor)
				.Select(x => new
				{
					Id = x.Id,
					Title = x.Title,
					Actors = x.MovieActors.Select(y => y.Actor)
				})
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == selectedMovie.Id);

			return movieData?.Actors?.ToList() ?? new List<Actor>();
		}
	}
}
