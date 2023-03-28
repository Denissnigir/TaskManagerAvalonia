using System;
using System.Collections.Generic;

namespace TaskManager.Model;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte[]? Photo { get; set; }

    public virtual ICollection<ProjectUser> ProjectUsers { get; } = new List<ProjectUser>();

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
