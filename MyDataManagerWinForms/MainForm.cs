using DataLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyDataModels;
using System.Diagnostics;

namespace MyDataManagerWinForms
{
    public partial class MainForm : Form
    {
        private static IConfigurationRoot _configuration;
        private static DbContextOptionsBuilder<DataDbContext> _optionsBuilder;

        private IList<Category> Categories = new List<Category>();
        private IList<Item> Items = new List<Item>();

        public MainForm()
        {
            InitializeComponent();
        }

        static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MyDataManagerData"));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BuildOptions();

            //load categories
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                Categories = db.Categories.OrderBy(x => x.Name).ToList();
                Items = db.Items.ToList();
                cboCategories.DataSource = Categories;
            }
        }

        private void cboCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cboBox = sender as ComboBox;
            var selItem = cboBox.SelectedItem as Category;

            LoadGrid(selItem);
        }

        private void LoadGrid(Category selectedItem)
        {
            Debug.WriteLine($"Selected Item {selectedItem.Id}| {selectedItem.Name}");
            var curData = Items.Where(x => x.CategoryId == selectedItem.Id).OrderBy(x => x.Name).ToList();
            dgItems.DataSource = curData;
        }
    }
}