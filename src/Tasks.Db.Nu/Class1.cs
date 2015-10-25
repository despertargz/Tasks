using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Db
{
    // db models

    public static class DbFactory
    {
        public static string ConStr;

        public static TaskDb Create()
        {
            if (ConStr == null)
            {
                throw new Exception("ConStr not set");
            }

            return new TaskDb(ConStr);
        }
    }

    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }
        public DateTime? Due { get; set; }
        public DateTime Updated { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public virtual List<Comment> Comments { get; set; } = new List<Comment>();
        public virtual List<Tag> Tags { get; set; } = new List<Tag>();
        public virtual List<Data> Data { get; set; } = new List<Data>();
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public int TaskId { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }

        public int TaskId { get; set; }
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
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Data> Data { get; set; }
    }
}
