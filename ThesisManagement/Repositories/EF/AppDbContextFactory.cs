using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ThesisManagement.Repositories.EF
{
    class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ThesisManagement;Integrated Security=True";
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionBuilder.Options);
        }
    }
}
