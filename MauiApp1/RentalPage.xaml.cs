using Microsoft.Data.SqlClient;
using MauiApp1.Entity;
using System.Data;

namespace MauiApp1;

public partial class RentalPage : ContentPage
{
    public RentalPage()
    {
        InitializeComponent();
    }
    SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-S1QPNRR;TrustServerCertificate=true;Initial Catalog=AracKiralaDb;Integrated Security=True;Connect Timeout=30");

    DataTable dt = new DataTable();

    string connectionString = "Data Source=DESKTOP-S1QPNRR;TrustServerCertificate=true;Initial Catalog=AracKiralaDb;Integrated Security=True";
    private void FillCombo()
    {
        Con.Open();
        string query = "select RegNo from carEntites where Availability='" + "Yes" + "';";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataReader rdr;
        rdr = cmd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Columns.Add("RegNo", typeof(string));
        dt.Load(rdr);
        RentalListView.SelectedItem = "RegNo";
        RentalListView.SelectedItem = dt;
        Con.Close();
    }
    private void FillCustomer()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string query = "SELECT CustId FROM customerEntities";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    List<string> custIds = new List<string>();
                    while (rdr.Read())
                    {
                        custIds.Add(rdr.GetInt32(0).ToString());
                    }
                    CustIdPicker.ItemsSource = custIds;
                }
            }
        }
    }

    private void FetchCustName()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string query = "SELECT * FROM customerEntities WHERE CustId = @CustId";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@CustId", CustIdPicker.SelectedItem.ToString());

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    NameEntry.Text = dr["CustName"].ToString();
                }
            }
        }
    }
    private void Populate()
    {
        Con.Open();
        string query = "select * from rentalEntities";
        SqlDataAdapter da = new SqlDataAdapter(query, Con);
        var dt = new DataTable();
        da.Fill(dt);

        var RentalList = new List<Rental>();
        foreach (DataRow row in dt.Rows)
        {
            var rental = new Rental
            {
                RentId = Convert.ToInt32(row["RentId"]),
                CarReg = row["CarReg"].ToString(),
                CustId = Convert.ToInt32(row["CustId"]),
                CustName = row["CustName"].ToString(),
                RentFees = Convert.ToDecimal(row["RentFees"]),
                Date = Convert.ToDateTime(row["Date"]),
                ReturnDate = Convert.ToDateTime(row["ReturnDate"]),
            };

            RentalList.Add(rental);
        }

        RentalListView.ItemsSource = RentalList;
        Con.Close();
    }



    protected override void OnAppearing()
    {
        base.OnAppearing();

        FillCombo();
        FillCustomer();
        Populate();

        //DataTable dt = new DataTable();
        //dt.Columns.AddRange(new DataColumn[]
        //{
        //new DataColumn("RentId", typeof(int)),
        //new DataColumn("CarReg", typeof(string)),
        //new DataColumn("CustName", typeof(string)),
        //new DataColumn("Name", typeof(string)),
        //new DataColumn("RentFees", typeof(string))
        //});

        //RentalListView.ItemsSource = dt.DefaultView;
    }
    private void AddClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(NameEntry.Text) ||
            string.IsNullOrEmpty(FeesEntry.Text) ||
            CustIdPicker.SelectedItem == null ||
            CarRegPicker.SelectedItem == null)
        {
            DisplayAlert("Error", "Missing information. Please fill in all fields.", "OK");
        }
        else
        {
            try
            {
                Con.Open(); 
                string query = "INSERT INTO rentalEntities (CarReg, CustId, CustName, RentFees, Date, ReturnDate) VALUES (@CarReg, @CustId, @CustName, @RentFees, @Date, @ReturnDate)";


                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@RentId", Convert.ToInt32(IdEntry.Text));
                cmd.Parameters.AddWithValue("@CarReg", CarRegPicker.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@CustId", Convert.ToInt32(CustIdPicker.SelectedItem.ToString()));
                cmd.Parameters.AddWithValue("@CustName", NameEntry.Text);
                cmd.Parameters.AddWithValue("@RentFees", Convert.ToDecimal(FeesEntry.Text));
                cmd.Parameters.AddWithValue("@Date", DateEntry.Date);
                cmd.Parameters.AddWithValue("@ReturnDate", ReturnDateEntry.Date);

                cmd.ExecuteNonQuery();
                DisplayAlert("Success", "Car Successfully Rented", "OK");
                Con.Close();
                Populate();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                Con.Close();
            }
        }
    }
    private void UpdateClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(IdEntry.Text) ||
            string.IsNullOrEmpty(NameEntry.Text) ||
            string.IsNullOrEmpty(FeesEntry.Text) ||
            CustIdPicker.SelectedItem == null ||
            CarRegPicker.SelectedItem == null ||
            DateEntry.Date == null ||
            ReturnDateEntry.Date == null)
        {
            DisplayAlert("Error", "Missing information. Please fill in all fields.", "OK");
        }
        else
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE rentalEntities SET CarReg = @CarReg, CustName = @CustName,Date=@Date,ReturnDate=@ReturnDate, RentFees = @RentFees WHERE RentId = @RentId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CarReg", CarRegPicker.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@CustName", NameEntry.Text);
                        cmd.Parameters.AddWithValue("@RentFees", Convert.ToDecimal(FeesEntry.Text));
                        cmd.Parameters.AddWithValue("@RentId", Convert.ToInt32(IdEntry.Text));
                        cmd.Parameters.AddWithValue("@Date", DateEntry.Date);
                        cmd.Parameters.AddWithValue("@ReturnDate", ReturnDateEntry.Date);

                        cmd.ExecuteNonQuery();
                        DisplayAlert("Success", "Car Successfully Updated", "OK");
                        Populate();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(IdEntry.Text))
        {
            DisplayAlert("Error", "Please enter Rental ID to delete.", "OK");
        }
        else
        {
            try
            {
                Con.Open();
                string query = "DELETE FROM rentalEntities WHERE RentId = " + IdEntry.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                DisplayAlert("Success", "Rental Deleted Successfully", "OK");
                Con.Close();
                Populate();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                Con.Close();
            }
        }
    }


    private void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Rental selectedRental)
        {
            IdEntry.Text = selectedRental.RentId.ToString();
            NameEntry.Text = selectedRental.CustName;
            FeesEntry.Text = selectedRental.RentFees.ToString();

            // CarRegPicker
            if (CarRegPicker.ItemsSource is List<string> carRegs)
            {
                CarRegPicker.SelectedItem = carRegs.FirstOrDefault(reg => reg == selectedRental.CarReg);
            }

            // CustIdPicker
            FillCustomer(); // Önce müşteri verilerini yeniden doldurun
            CustIdPicker.SelectedItem = selectedRental.CustId.ToString();
            CarRegPicker.SelectedItem = selectedRental.CarReg.ToString();

            // DateEntry
            DateEntry.Date = selectedRental.Date.Date;
            ReturnDateEntry.Date = selectedRental.ReturnDate.Date;
        }
    }






    private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        //if (RentalListView.SelectedItem != null)
        //{
        //    var selectedItem = RentalListView.SelectedItem as RentalTb1;

        //    var itemId = selectedItem.RentId;
        //    var itemName = selectedItem.CustName;
        //    var itemCarReg = selectedItem.CarReg;
        //    var itemRentFees = selectedItem.RentFees;

        //    IdEntry.Text = itemId.ToString();
        //    NameEntry.Text = itemName;
        //    CarRegPicker.SelectedItem = itemCarReg;
        //    FeesEntry.Text = itemRentFees.ToString();
        //}
    }
    private void ClearClicked(object sender, EventArgs e)
    {
        IdEntry.Text = "";
        NameEntry.Text = "";
        CarRegPicker.SelectedItem = null;
        FeesEntry.Text = "";
    }
    private void BackClicked(object sender, EventArgs e)
    {
        var nextPage = new HomePage();
        var navigationPage = new NavigationPage(nextPage);
        Application.Current.MainPage = navigationPage;
    }
    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            var selectedRental = (Rental)e.SelectedItem;
            IdEntry.Text = selectedRental.CarReg.ToString();
            NameEntry.Text = selectedRental.CustName.ToString();
            FeesEntry.Text = selectedRental.RentFees.ToString();
        }

    }


}