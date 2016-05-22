using app.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace app.DAL
{
    public class ArticleContext : DbContext
    {

        public ArticleContext()
            : base("ArticleContext")
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
