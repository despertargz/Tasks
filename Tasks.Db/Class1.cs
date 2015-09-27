using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Db
{
    // db models

    public static class DbFactory
    {
        public static TaskDb Create()
        {
            return new TaskDb("data source=localhost\\sqlexpress; initial catalog=TaskDb; integrated security=true;");
        }
    }

    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }
        public DateTime? Due { get; set; }
        public DateTime Updated { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TaskDb : DbContext
    {
        public TaskDb()
        {

        }

        public TaskDb(string conStr) : base(conStr)
        {

        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
