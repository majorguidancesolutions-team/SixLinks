using Xunit;
using Shouldly;
using SixLinksDataService;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyDataModels;
using System.Linq;

namespace SixLinksServiceIntegrationTests
{
	public class ActorsTest
	{
		private ISixLinksData _service;
		DbContextOptions<DataDbContext> _options;
		private const int NUMACTORS = 4;

		public ActorsTest()
		{
			SetUpOptions();
			BuildDefaults();
		}

		private void SetUpOptions()
		{
			_options = new DbContextOptionsBuilder<DataDbContext>().UseInMemoryDatabase(databaseName: "SixLinksTestDB").Options;
		}

		private void BuildDefaults()
		{
			using (var context = new DataDbContext(_options))
			{
				var existingActors = Task.Run(() => context.Actors.ToListAsync()).Result;
				if (existingActors == null || existingActors.Count < 2)
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
				new Actor() { Id = 1, FirstName = "Tim", LastName = "Robbins"},
				new Actor() { Id = 2, FirstName = "Morgan", LastName = "Freeman" },
				new Actor() { Id = 3, FirstName = "Marlo", LastName = "Brando" },
				new Actor() { Id = 4, FirstName = "Kevin", LastName = "Bacon" }
			};

		}

		[Theory]
		[InlineData("Kevin", "Bacon", 4)]
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
				Assert.Equal(firstName, actors.FirstOrDefault(x => x.Id == id).FirstName);
				Assert.Equal(lastName, actors.FirstOrDefault(x => x.Id == id).LastName);

				actors.Count.ShouldBe(NUMACTORS);
				actors.FirstOrDefault(x => x.Id == id).FirstName.ShouldBe(firstName, StringCompareShould.IgnoreCase);
				actors.FirstOrDefault(x => x.Id == id).LastName.ShouldBe(lastName, StringCompareShould.IgnoreCase);
			}
		}

		[Theory]
		[InlineData("Kevin", "Bacon", 4)]
		[InlineData("Tim", "Robbins", 1)]
		[InlineData("Morgan", "Freeman", 2)]
		[InlineData("Marlo", "Brando", 3)]

		public async Task TestGetOneActor(string FirstName, string LastName, int id)
		{
			using (var context = new DataDbContext(_options))
			{
				_service = new SixLinksData(context);
				var actor = await _service.GetActorById(id);
				actor.FirstName.ShouldBe(FirstName, StringCompareShould.IgnoreCase);
				actor.LastName.ShouldBe(LastName, StringCompareShould.IgnoreCase);
			}
		}

		//_service = new StatesService(context);
		//var state = await _service.GetAsync(16);
		//state.ShouldNotBeNull();
        //        state.Name.ShouldBe("Iwoa", StringCompareShould.IgnoreCase);
        //        state.Name = "Iowa";// update name
        //        _service.AddOrUpdateAsync(state);// save change

		[Fact]
		public async Task UpdateActors()
		{
			using (var context = new DataDbContext(_options))
			{
				_service = new SixLinksData(context);
				var actor = await _service.GetActorById(3);
				actor.ShouldNotBeNull();
				actor.FirstName.ShouldBe("Marlo", StringCompareShould.IgnoreCase);
				actor.LastName.ShouldBe("Brando", StringCompareShould.IgnoreCase);
				actor.FirstName = "Marlon";
				actor.LastName = "Brando";
				await _service.UpdateActor(actor.Id, actor.FirstName, actor.LastName);
				var newActor = await _service.GetActorById(3);
				actor.FirstName.ShouldBe("Marlon", StringCompareShould.IgnoreCase);
				actor.LastName.ShouldBe("Brando", StringCompareShould.IgnoreCase);
			}
		}




		[Fact]
		public async Task AddAndDelete()
		{
			using (var context = new DataDbContext(_options))
			{
				_service = new SixLinksData(context);
				var state = new Actor() { Id = 900, FirstName = "Luke", LastName = "Skywalker" };//add a new state
				var newActor = await _service.AddNewActor(state);//update database
				var createdState = await _service.GetActorById(state.Id);//call state
				createdState.ShouldNotBeNull();
				createdState.FirstName.ShouldBe("Luke", StringCompareShould.IgnoreCase);
				await _service.DeleteActor(createdState); //delete state
				var deletedState = await _service.GetActorById(state.Id);
				deletedState.ShouldBeNull();
				//update database
				//check if state exists
			}
		}
	}
}
