using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TaskManager.Model;

namespace TaskManager.View.Windows;

public partial class AuthWindow : Window
{
    private TaskManagerContext db = new TaskManagerContext();
    private TextBox userLogin;
    private TextBox userPassword;
    public static User curUser { get; set; }
    public AuthWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        userLogin = this.FindControl<TextBox>("LoginTb");
        userPassword = this.FindControl<TextBox>("PasswordTb");
    }

    private void Auth(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(userLogin.Text) && !string.IsNullOrWhiteSpace(userPassword.Text))
        {
            var user = db.Users.FirstOrDefault(p => p.Login == userLogin.Text && p.Password == userPassword.Text);
            if (user != null)
            {
                curUser = user;
                ProjectWindow projectWindow = new ProjectWindow();
                projectWindow.Show();
                this.Close();
            }
        }
        else
        {
            MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("ПОШЛИ НАХУЙ","asd").Show();
        }
    }
}