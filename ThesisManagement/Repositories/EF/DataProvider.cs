using Microsoft.EntityFrameworkCore;

namespace ThesisManagement.Repositories.EF
{
    public class DataProvider
    {
        private static DataProvider _instance;
        public static DataProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataProvider();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public AppDbContext Context { get; set; }

        private DataProvider()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(Properties.Settings.Default.ConnectionString);
            Context = new AppDbContext(optionsBuilder.Options);
        }
    }
}
