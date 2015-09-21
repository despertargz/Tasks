using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks.Api
{
    public class TaskController
    {

    }

    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public DateTime Created { get; set; }
        public DateTime Completed { get; set; }
        public DateTime Due { get; set; }
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