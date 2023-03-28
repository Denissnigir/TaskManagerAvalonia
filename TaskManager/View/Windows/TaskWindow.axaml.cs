using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using TaskManager.Model;

namespace TaskManager.View.Windows;

public partial class TaskWindow : Window
{
    private TaskManagerContext db = new TaskManagerContext();
    private TextBox newTaskName;
    private Button profile;
    private TextBlock greetingText;
    private ListBox plannedLb;
    private int projectId;
    
    public TaskWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    public TaskWindow(int id)
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        profile.DataContext = AuthWindow.curUser;
        greetingText.Text = AuthWindow.curUser.Login;

        projectId = id;
        
        UpdateData(id);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        newTaskName = this.FindControl<TextBox>("NewTaskTextBox");
        profile = this.FindControl<Button>("ProfileBtn");
        greetingText = this.FindControl<TextBlock>("GreetingTb");
        plannedLb = this.FindControl<ListBox>("PlannedLb");
    }

    private void AddTask(object? sender, RoutedEventArgs e)
    {
        string name = newTaskName.Text.Trim();

        Task task = new Task();
        task.Id = db.Tasks.OrderBy(p => p.Id)
            .Last().Id + 1;
        task.ProjectId = projectId;
        task.CategoryId = 1;
        task.UserId = AuthWindow.curUser.Id;
        task.Name = newTaskName.Text;
        db.Tasks.Add(task);
        db.SaveChanges();
        
        UpdateData(projectId);
        
        newTaskName.Text = ""; 
    }

    private void UpdateData(int projectId)
    {
        var plannedTasks = db.Tasks.ToList()
            .Where(p => p.CategoryId == 1 && p.ProjectId == projectId)
            .ToList();
        plannedLb.Items = plannedTasks;
    }
}