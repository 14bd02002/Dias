using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using app.Models;

namespace app.DAL
{
    public class ArticleInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ArticleContext>
    {
        protected override void Seed(ArticleContext context)
        {
            var articles = new List<Article>
            {
            new Article{Title="Carson",Content="Alexander",CreatedDate=DateTime.Parse("2005-09-01")},
            new Article{Title="ccarson",Content="Alexander",CreatedDate=DateTime.Parse("2005-09-01")},
            new Article{Title="Csaarson",Content="Alexander",CreatedDate=DateTime.Parse("2005-09-01")},
            new Article{Title="Cdarson",Content="AlexanderAlexanderAlexander",CreatedDate=DateTime.Parse("2005-09-01")},
            new Article{Title="Cdarson",Content="AlexanderAlexanderAlexanderAlexander",CreatedDate=DateTime.Parse("2005-09-01")},
            new Article{Title="Cdarson",Content="AlexanderAlexanderAlexanderAlexanderAlexanderAlexander",CreatedDate=DateTime.Parse("2005-09-01")},
            new Article{Title="Cdarson",Content="AlexanderAlexanderAlexanderAlexanderAlexander",CreatedDate=DateTime.Parse("2005-09-01")},
            new Article{Title="Cdarson",Content="Alexander",CreatedDate=DateTime.Parse("2005-09-01")},
            };

            articles.ForEach(s => context.Articles.Add(s));
            context.SaveChanges();
            var authors = new List<Author>
            {
            new Author{FirstName="Dias" ,LastName="Chemistry",},
            new Author{FirstName="DDias" ,LastName="Chemistry",},
            new Author{FirstName="DDDias" ,LastName="Chemistry",},
            new Author{FirstName="Diias" ,LastName="Chemistry",},
            new Author{FirstName="DDiias" ,LastName="Chemistry",},
            new Author{FirstName="DDDiias" ,LastName="Chemistry",},
            new Author{FirstName="DDiiaas" ,LastName="Chemistry",},
            new Author{FirstName="DDiiias" ,LastName="Chemistry",},
            };
            authors.ForEach(s => context.Authors.Add(s));
            context.SaveChanges();
            var categories = new List<Category>
            {
            new Category{CategoryName="Comedy",},
            new Category{CategoryName="Drama",},
            new Category{CategoryName="Fantastic",},
            new Category{CategoryName="Comedy",},
            new Category{CategoryName="Horror",},
            new Category{CategoryName="Fantastic",},
            new Category{CategoryName="Fantastic",},
            new Category{CategoryName="Fantastic",},
            
            };
            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();
        }
    }
}