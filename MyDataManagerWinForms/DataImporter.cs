using DataLibrary;
using MyDataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyDataManagerWinForms
{
    public class DataImporter
    {
        private static readonly HttpClient client = new HttpClient();
        public async Task ImportData()
        {
            await GetData();
            
        }

        private void InsertData()
        {
            using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
            {

            }
        }
        private async Task GetData()
        {
            var response = await client.GetAsync("https://imdb-api.com/en/API/Top250Movies/k_ttuui8u4");
            string json = await response?.Content?.ReadAsStringAsync() ?? string.Empty;

            if (string.IsNullOrEmpty(json))
            {
                return;
            }

            try
            {

                ImdbData data = JsonConvert.DeserializeObject<ImdbData>(json);
                
                List<Movie> ourMovies = new List<Movie>();
                List<Actor> ourActors = new List<Actor>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            
        }
    }
}
