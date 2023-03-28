using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Microsoft.EntityFrameworkCore;
using TaskManager.Model;

namespace TaskManager.View.Windows;

public partial class ProjectWindow : Window
{
    private TaskManagerContext db = new TaskManagerContext();
    private ListBox projectsList;
    private TextBox projectName;
    public ProjectWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        UpdateData();
    }


    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        projectsList = this.FindControl<ListBox>("ProjectLb");
        projectName = this.FindControl<TextBox>("ProjectTb");
    }

    private void UpdateData()
    {
        var projectsUsers = db.ProjectUsers.ToList()
            .Where(p => p.UserId == AuthWindow.curUser.Id)
            .ToList();
        List<Project> projects = new List<Project>();
        foreach (var projectsUser in projectsUsers)
        {
            projects.Add(db.Projects.FirstOrDefault(p => p.Id == projectsUser.ProjectId));
        }
        projectsList.Items = projects;
    }
    
    private void AddProject(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(projectName.Text))
        {
            Project project = new Project();
            project.Name = projectName.Text;
            project.Id = db.Projects.OrderBy(p => p.Id)
                .Last().Id + 1;
            db.Projects.Add(project);
            db.SaveChanges();
            ProjectUser projectUser = new ProjectUser();
            projectUser.UserId = AuthWindow.curUser.Id;
            projectUser.ProjectId = project.Id;
            projectUser.Id = db.ProjectUsers.OrderBy(p => p.Id)
                .Last().Id + 1;
            db.ProjectUsers.Add(projectUser);
            db.SaveChanges();

            UpdateData();
        }
    }

    private void ToTasks(object? sender, RoutedEventArgs e)
    {
        var context = (sender as Button).DataContext as Project;
        TaskWindow taskWindow = new TaskWindow(context.Id);
        taskWindow.Show();
        this.Close();
    }
}