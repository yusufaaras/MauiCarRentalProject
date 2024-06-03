
using Microsoft.Data.SqlClient;
using MauiApp1;
using System.Data;

namespace MauiApp1;

public partial class Login : ContentPage
{
//    private readonly DataContext _context;
//    private readonly UserController _userController;
//    private readonly User _user;

//    public Login(DataContext context, UserController userController, User _user)
//    {
//        _context = context;
//        _userController = userController;
//        _user = _user;
        
//    }
    public Login()
    {
        InitializeComponent();
    }

    SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-S1QPNRR;TrustServerCertificate=true;Initial Catalog=AracKiralaDb;Integrated Security=True;Connect Timeout=30");

    DataTable dt = new DataTable();
    private void LoginClicked(object sender, EventArgs e)
    {
        string username = Uname.Text;
        string password = PassTb.Text;

        // Kullanıcı adı ve şifre boş olup olmadığını kontrol et
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            DisplayAlert("Error", "Username and password cannot be empty", "OK");
            return; // Giriş işlemine devam etme
        }

        string connectionString = @"Data Source=.;Initial Catalog=AracKiralaDb;Integrated Security=True;Connect Timeout=30;TrustServerCertificate=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT COUNT(*) FROM userEntities WHERE Uname = @Username AND Upass = @Password";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                int result = (int)command.ExecuteScalar();

                if (result == 1)
                {
                    var nextPage = new HomePage();
                    var navigationPage = new NavigationPage(nextPage);
                    Application.Current.MainPage = navigationPage;
                }
                else
                {
                    DisplayAlert("Error", "Username ve Password alanı boş geçilemez", "OK");
                }
            }
        }
    }



    private void SignClicked(object sender, EventArgs e)
    {
        var nextPage = new SignUpPage();
        var navigationPage = new NavigationPage(nextPage);
        Application.Current.MainPage = navigationPage;
    }
    private void ClearClicked(object sender, EventArgs e)
    {
        Uname.Text = "";
        PassTb.Text = "";
    }
}

