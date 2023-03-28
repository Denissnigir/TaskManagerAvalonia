using System;
using System.Collections.Generic;

namespace TaskManager.Model;

public partial class Task
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? UserId { get; set; }

    public int? CategoryId { get; set; }

    public string? Description { get; set; }

    public int? ProjectId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Project? Project { get; set; }

    public virtual User? User { get; set; }
}
