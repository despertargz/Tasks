﻿using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tasks.Db;

namespace Tasks.Api
{


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
                    Name = newTask.Name,
                    CategoryId = 4,
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

        [HttpGet("/categories")]
        public Category[] GetCategories()
        {
            using (var db = DbFactory.Create())
            {
                return db.Categories.ToArray();
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