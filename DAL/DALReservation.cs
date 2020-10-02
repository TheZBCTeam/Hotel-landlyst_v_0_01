using Hotel_landlyst_v_0_01.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_landlyst_v_0_01.DAL
{
    public class DALReservation
    {
        #region Fields

        private IConfiguration configuration;
        #endregion




        #region Constructors
        public DALReservation(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion



        internal int addBooking(BookingModel reservation)
        {
            
            // #1.. Read the value from the appsettings.json and connect to DB
            string connstr = configuration.GetConnectionString("HotelLandlystDB");
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            // #2.. Create command and get the hands on the customerID
            string query = "INSERT INTO [dbo].[Customers]([firstName],[lastName],[streetName],[streetNumber],[zipPostal],[city],[country],[phone],[email])" +
                           "VALUES(@firstName,@lastName,@streetName,@streetNumber,@zipPostal,@city,@country,@phone,@email)select SCOPE_IDENTITY() as customerID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@firstName", reservation.FirstName);
            cmd.Parameters.AddWithValue("@lastName", reservation.LastName);
            cmd.Parameters.AddWithValue("@streetName", reservation.StreetName);
            cmd.Parameters.AddWithValue("@streetNumber", reservation.StreetNumber);
            cmd.Parameters.AddWithValue("@zipPostal", reservation.ZipPostal);
            cmd.Parameters.AddWithValue("@city", reservation.City);
            cmd.Parameters.AddWithValue("@country", reservation.Country);
            cmd.Parameters.AddWithValue("@phone", reservation.PhoneNumber);
            cmd.Parameters.AddWithValue("@email", reservation.Email);

            // #3.. Query the DB
            SqlDataReader reader =cmd.ExecuteReader();
            reader.Read();
            int customerID = Convert.ToInt32(reader[0].ToString());

            // #4.. Close the connection
            conn.Close();

            return customerID;
        }

        internal object getReservation(int sessionBookingID)
        {
            throw new NotImplementedException();
        }
    }
}
