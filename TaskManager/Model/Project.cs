using System;
using System.Collections.Generic;

namespace TaskManager.Model;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProjectUser> ProjectUsers { get; } = new List<ProjectUser>();

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
