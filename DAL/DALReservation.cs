using Hotel_landlyst_v_0_01.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

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

        internal SearchListModel SearchRooms(SearchRoomsModel searchInput)
        {
            SearchListModel searchListModel = new SearchListModel();
           
            // #1.. Read the value from the appsettings.json and connect to DB
            string connstr = configuration.GetConnectionString("HotelLandlystDB");
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            // #2.. Create command and get the hands on the customerID
            string query = "SELECT [ro].[roomType], [ro].[price], [ro].[miniBar], [ro].[aircondition], [ro].[petsPosible], [ro].[golfPosible], " +
                "[ro].[roomId], [ro].[imageName], [rd].[roomDescription]" +
                "FROM [dbo].[Rooms] [ro] " +
                "join [dbo].[RoomDescriptions] as [rd] " +
                "on [ro].[descriptionId] = [rd].[descriptionId] " +
                "where [ro].[roomId] not in(select [roomId] from [Reservations] " +
                "where @searchInputArriving between [arriving] and [departing] " +
                "and @searchInputDeparting between [arriving] and [departing]) ";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@searchInputArriving", searchInput.Arriving.ToString("yyyy/MM/dd"));
            cmd.Parameters.AddWithValue("@searchInputDeparting", searchInput.Departing.ToString("yyyy/MM/dd"));

            // #3.. Query the DB
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            while (reader.Read())
            {
                RoomModel room = new RoomModel
                {
                    RoomType = (reader["roomType"].ToString()),
                    ImageName = (reader["imageName"].ToString()),
                    Price = (reader["price"].ToString()),
                    MiniBar = (reader["miniBar"].ToString()),
                    Aircondition = (reader["aircondition"].ToString()),
                    PetsPossible = (reader["petsPosible"].ToString()),
                    GolfPossible = (reader["golfPosible"].ToString()),
                    RoomDescription = (reader["roomDescription"].ToString())
                };
                searchListModel.AccessList().Add(room);

            }

            // #4.. Close the connection
            conn.Close();
            return searchListModel;
        }



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
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int reservationID = Convert.ToInt32(reader[0].ToString());

            // #4.. Close the connection
            conn.Close();

            return reservationID;
        }

        internal object getReservation(int sessionReservationID)
        {
            BookingModel reservation = new BookingModel();
            // #1.. Read the value from the appsettings.json and connect to DB
            string connstr = configuration.GetConnectionString("HotelLandlystDB");
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            // #2.. Create command and get the hands on the customerID
            string query = "SELECT [firstName],[lastName],[streetName],[streetNumber],[zipPostal],[city],[country],[phone],[email]" +
                           "FROM [dbo].[Customers] WHERE customerID = @sessionReservationID ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@sessionReservationID", sessionReservationID);


            // #3.. Query the DB
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            reservation.ReservationID = sessionReservationID;
            reservation.FirstName = (reader["firstName"].ToString());
            reservation.LastName = (reader["lastName"].ToString());
            reservation.StreetName = (reader["streetName"].ToString());
            reservation.StreetNumber = (Convert.ToInt32(reader["streetNumber"]));
            reservation.ZipPostal = (Convert.ToInt32(reader["zipPostal"]));
            reservation.City = (reader["city"].ToString());
            reservation.Country = (reader["country"].ToString());
            reservation.PhoneNumber = (reader["phone"].ToString());
            reservation.Email = (reader["email"].ToString());


            // #4.. Close the connection
            conn.Close();


            return reservation;
        }
    }
}
