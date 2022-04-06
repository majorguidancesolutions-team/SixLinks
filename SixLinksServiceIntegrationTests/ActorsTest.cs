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
		private const int NUMACTORS = 405;

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
		[InlineData("Tim", "Robbins", 1)]
		[InlineData("Morgan", "Freeman", 2)]
		[InlineData("Marlo", "Brando", 3)]

		public async Task TestGetAllActors(string firstName, string lastName, int id)
		{
			using (var context = new DataDbContext(_options))
			{
				_service = new SixLinksData(context);
				var actors = await _service.GetActors();
				Assert.Equal(NUMACTORS, actors.Count);
				Assert.Equal(firstName, actors[id].FirstName);
				Assert.Equal(lastName, actors[id].LastName);

				actors.Count.ShouldBe(NUMACTORS);
				actors[id].FirstName.ShouldBe(firstName, StringCompareShould.IgnoreCase);
				actors[id].LastName.ShouldBe(lastName, StringCompareShould.IgnoreCase);
			}
		}

		[Theory]
		[InlineData("Tim", "Robbins", 1)]
		[InlineData("Morgan", "Freeman", 2)]
		[InlineData("Marlo", "Brando", 3)]

		public async Task TestGetOneActor(string firstName, string lastName, int id)
		{
			using (var context = new DataDbContext(_options))
			{
				_service = new SixLinksData(context);
				var actor = await _service.GetActorById(id);
				actor.FirstName.ShouldBe(firstName, StringCompareShould.IgnoreCase);
				actor.LastName.ShouldBe(lastName, StringCompareShould.IgnoreCase);
			}
		}


	}
}