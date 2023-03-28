﻿using System;
using System.Collections.Generic;

namespace TaskManager.Model;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
