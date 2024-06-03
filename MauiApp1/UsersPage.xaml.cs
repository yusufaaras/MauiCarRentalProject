using Microsoft.Data.SqlClient;
using MauiApp1.Entity;
using System;
using System.Data;

namespace MauiApp1
{
    public partial class UsersPage : ContentPage
    {
        public UsersPage()
        {
            InitializeComponent();
            UsersListView.ItemSelected += UsersListView_ItemSelected;

        }
        SqlConnection Con = new SqlConnection(@"Data Source=.;TrustServerCertificate=true;Initial Catalog=AracKiralaDb;Integrated Security=True;Connect Timeout=30");

        DataTable dt = new DataTable();
        private void populate()
        {
            Con.Open();
            string query = "select * from userEntities";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            var dt = new DataTable();
            da.Fill(dt);

            var userList = new List<User>();
            foreach (DataRow row in dt.Rows)
            {
                var user = new User
                {
                    UserId = Convert.ToInt32(row["UserId"]),
                    Uname = row["Uname"].ToString(),
                    Upass = row["Upass"].ToString(),
                    Gender = row["Gender"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Address = row["Address"].ToString()
                };

                userList.Add(user);
            }


            UsersListView.ItemsSource = userList;
            Con.Close();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            populate();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("Id",typeof(int)),
                new DataColumn("Name", typeof(String)),
                new DataColumn("Password", typeof(String)),
                new DataColumn("Phone", typeof(String)),
                new DataColumn("Address", typeof(String))
             });
        }

        private void AddClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(PassEntry.Text) || string.IsNullOrWhiteSpace(PhoneEntry.Text) || string.IsNullOrWhiteSpace(AddressEntry.Text))
            {
                DisplayAlert("Error", "Missing information", "OK");
            }
            else
            {
                try
                {
                    Con.Open(); 
                    string query = "INSERT INTO userEntities (Uname, Upass, Phone, Address) VALUES ('" + NameEntry.Text + "', '" + PassEntry.Text + "',  '" + PhoneEntry.Text + "', '" + AddressEntry.Text + "')";

                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    DisplayAlert("Error", "User Successfully Added", "OK");
                    Con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    DisplayAlert("Error", Myex.Message, "OK");
                }
            }
        }

        private void UpdateClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(PassEntry.Text) || string.IsNullOrWhiteSpace(PhoneEntry.Text) || string.IsNullOrWhiteSpace(AddressEntry.Text) )
            {
                DisplayAlert("Error", "Missing information", "OK");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "UPDATE userEntities SET Uname='" + NameEntry.Text + "', Upass='" + PassEntry.Text +  "', Phone='" + PhoneEntry.Text + "', Address='" + AddressEntry.Text + "' WHERE UserId=" + IdEntry.Text + ";";


                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    DisplayAlert("Error", "User Successfully Update", "OK");
                    Con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    DisplayAlert("Error", Myex.Message, "OK");
                }

            }
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
             if (IdEntry.Text == "")
            {
                DisplayAlert("Error", "Missing Information", "OK");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from userEntities where UserId=" + IdEntry.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    DisplayAlert("Error", "User Deleted Successfully", "OK");
                    Con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    DisplayAlert("Error", Myex.Message, "OK");
                }
            }
        }

        private void UsersListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is DataRowView rowView)
            {
                IdEntry.Text = rowView.Row["Id"].ToString();
                NameEntry.Text = rowView.Row["Name"].ToString();
                PassEntry.Text = rowView.Row["Password"].ToString();
                PhoneEntry.Text = rowView.Row["Phone"].ToString();
                AddressEntry.Text = rowView.Row["Address"].ToString();
            }
        }
        private void UsersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedItem = e.SelectedItem as User;

                var itemId = selectedItem.UserId;
                var itemName = selectedItem.Uname;
                var itemPassword = selectedItem.Upass;
                var itemGender = selectedItem.Gender;
                var itemPhone = selectedItem.Phone;
                var itemAddress = selectedItem.Address;

                IdEntry.Text = itemId.ToString();
                NameEntry.Text = itemName;
                PassEntry.Text = itemPassword;
                PhoneEntry.Text = itemPhone;
                AddressEntry.Text = itemAddress;

                // ...
            }
        }

        private void BackClicked(object sender, EventArgs e)
        {
            var nextPage = new HomePage();
            var navigationPage = new NavigationPage(nextPage);
            Application.Current.MainPage = navigationPage;
        }

        private void ClearClicked(object sender, EventArgs e)
        {
            IdEntry.Text = "";
            NameEntry.Text = "";
            PassEntry.Text = "";
            PhoneEntry.Text = "";
            AddressEntry.Text = "";

        }
    }
}
