using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks.Api
{
    public static class DbFactory
    {
        public static TaskDb Create()
        {
            return new TaskDb("data source=localhost\\sqlexpress; initial catalog=TaskDb; integrated security=true;");
        }
    }

    public class TaskController : Controller
    {
        [HttpGet("/tasks")]
        public object[] Get()
        {
            using (var db = DbFactory.Create())
            {
                return db.Tasks
                    .Select(o => new { Name = o.Name, Priority = o.Priority, Status = o.Status, Created = o.Created, Due = o.Due })
                    .ToArray();
            }
        }

        [HttpPost("/tasks")]
        public void Post()
        {
            using (var db = DbFactory.Create())
            {
                db.Tasks.Add(new Task()
                {
                    Priority = 3,
                    Status = 1,
                    Created = DateTime.Now,
                    Completed = null,
                    Updated = DateTime.Now,
                    Due = null,
                    Name = "New Task",
                });

                db.SaveChanges();
            }
        }
    }

    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }
        public DateTime? Due { get; set; }
        public DateTime Updated { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
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
        public TaskDb(string conStr) : base(conStr)
        {

        }

        public DbSet<Task> Tasks { get; set; }
    }
        
}

/*
task
	name
	description
	comments
	status
	created
	completed
	updated
	due
	alerts
	priority
	tags
	
	

create task	
filter tasks
single task
	add comment
	update status
	set due dat

    */