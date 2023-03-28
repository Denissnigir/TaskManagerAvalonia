﻿using System;
using System.Collections.Generic;

namespace TaskManager.Model;

public partial class ProjectUser
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public int UserId { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
