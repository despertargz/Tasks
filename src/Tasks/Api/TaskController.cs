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
        public object[] Get(int? status)
        {
            using (var db = DbFactory.Create())
            {
                return db.Tasks
                    .Where(o => o.Status != 6 && (status == null || (o.Status == status)))
                    .Select(o => new { Description = o.Description, Id = o.Id, Name = o.Name, Priority = o.Priority, Status = o.Status, Created = o.Created, Due = o.Due })
                    .ToArray();
            }
        }

        [HttpPost("/tasks")]
        public void Post([FromBody]NewTask newTask)
        {
            using (var db = DbFactory.Create())
            {
                db.Tasks.Add(new Task()
                {
                    Priority = 3,
                    Status = 1,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Completed = null,
                    Due = null,
                    Name = newTask.Name
                });

                db.SaveChanges();
            }
        }

        [HttpPost("/tasks/{id}")]
        public void Update(int id, [FromBody]NewValue body)
        {
            using (var db = DbFactory.Create())
            {
                var task = db.Tasks.First(o => o.Id == id);
               
                if (body.Field == "Status")
                {
                    task.Status = int.Parse(body.Value);
                    if (body.Value == "3")
                    {
                        task.Completed = DateTime.Now;
                    }
                }
                else if (body.Field == "Priority")
                {
                    task.Priority = int.Parse(body.Value);
                }
                else if (body.Field == "Name")
                {
                    task.Name = body.Value;
                }
                else if (body.Field == "Description")
                {
                    task.Description = body.Value;
                }
                else if (body.Field == "Due")
                {
                    if (body.Value != null)
                    {
                        task.Due = DateTime.Parse(body.Value);
                    }
                }

                db.SaveChanges();
            }

            
        }
    }

    // http bodys

    public class NewValue
    {
        public string Field { get; set; }
        public string Value { get; set; }
    }

    public class NewTask
    {
        public string Name { get; set; }
    }

    // db models

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