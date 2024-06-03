using MauiApp1;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MauiApp1;

public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
    }
    SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-S1QPNRR;TrustServerCertificate=true;Initial Catalog=AracKiralaDb;Integrated Security=True;Connect Timeout=30");

    DataTable dt = new DataTable();
    private void populate()
    {
        Con.Open();
        string query = "select * from userEntities";
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        SqlCommandBuilder builder = new SqlCommandBuilder(da);
        var ds = new DataSet();
        da.Fill(ds);
        Con.Close();
    }
    private void AddClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(PassEntry.Text) || string.IsNullOrWhiteSpace(PhoneEntry.Text) || string.IsNullOrWhiteSpace(AddressEntry.Text) || GenderPicker.SelectedIndex < 0)
        {
            DisplayAlert("Error", "Mandatory Fields cannot be left blank", "OK");
        }
        else
        {
            try
            {
                Con.Open();
                string query = "INSERT INTO userEntities (Uname, Upass, Gender, Phone, Address) VALUES ('" + NameEntry.Text + "', '" + PassEntry.Text + "', '" + GenderPicker.SelectedItem + "', '" + PhoneEntry.Text + "', '" + AddressEntry.Text + "')";

                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                DisplayAlert("Success", "User Successfully Added", "OK");
                Con.Close();
                populate();
                var nextPage = new Login();
                var navigationPage = new NavigationPage(nextPage);
                Application.Current.MainPage = navigationPage;
            }
            catch (Exception Myex)
            {
                DisplayAlert("Error", Myex.Message, "OK");
            }
        }
    }
    private void OnPhoneEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            if (!e.NewTextValue.All(char.IsDigit))
            {
                PhoneEntry.Text = e.OldTextValue;
            }
            if (e.NewTextValue.Length > 11)
            {
                PhoneEntry.Text = e.OldTextValue;
            }
        }
    }

    private void OnGenderPickerSelectedIndexChanged(object sender, EventArgs e)
    {
    }
    private void BackClicked(object sender, EventArgs e)
    {
        var nextPage = new Login();
        var navigationPage = new NavigationPage(nextPage);
        Application.Current.MainPage = navigationPage;
    }
    private void ClearClicked(object sender, EventArgs e)
    {
        IdEntry.Text = "";
        NameEntry.Text = "";
        PassEntry.Text = "";
    }
}