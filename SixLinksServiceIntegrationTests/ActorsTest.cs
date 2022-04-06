using Xunit;
using Shouldly;
using SixLinksDataService;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyDataModels;

namespace SixLinksServiceIntegrationTests
{
	public class ActorsTest
	{
		private ISixLinksData _service;
		DbContextOptions<DataDbContext> _options;

		public ActorsTest()
		{
			SetUpOptions();
			BuildDefaults();
		}

		private void SetUpOptions()
		{
			_options = new DbContextOptionsBuilder<DataDbContext>().UseInMemoryDatabase(databaseName: "SixLinksDB").Options;
		}

		private void BuildDefaults()
		{
			using (var context = new DataDbContext(_options))
			{
				var existingActors = Task.Run(() => context.Actors.ToListAsync()).Result;
				if (existingActors == null || existingActors.Count < 10)
				{
					var actors = GetActorsTestData();
					context.Actors.AddRange(actors);
					context.SaveChanges();
				}
			}
		}

		private List<Actor> GetActorsTestData()
		{
			return new List<Actor>()
			{
				new Actor() { Id = 1, FirstName = "Kevin", LastName = "Bacon", BaconRating = 0, ActorMovies = new List<Movie_Actor>() }
			};

		}

		[Theory]


		[Fact]
		public void Test1()
		{

		}
	}
}