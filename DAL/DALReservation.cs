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
            string query = "SELECT [ro].[roomType], [ro].[roomNumber], [ro].[price], [ro].[miniBar], [ro].[aircondition], [ro].[petsPosible], [ro].[golfPosible], " +
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
                    RoomId = (reader["roomId"].ToString()),
                    RoomType = (reader["roomType"].ToString()),
                    RoomNumber = (reader["roomNumber"].ToString()),
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




        internal int AddCustomer(BookingModel customer)
        {

            // #1.. Read the value from the appsettings.json and connect to DB
            string connstr = configuration.GetConnectionString("HotelLandlystDB");
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            // #2.. Create command and get the hands on the customerID
            string query = "INSERT INTO [dbo].[Customers]([firstName],[lastName],[streetName],[streetNumber],[zipPostal],[city],[country],[phone],[email])" +
                           "VALUES(@firstName,@lastName,@streetName,@streetNumber,@zipPostal,@city,@country,@phone,@email)select SCOPE_IDENTITY() as customerID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@firstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@lastName", customer.LastName);
            cmd.Parameters.AddWithValue("@streetName", customer.StreetName);
            cmd.Parameters.AddWithValue("@streetNumber", customer.StreetNumber);
            cmd.Parameters.AddWithValue("@zipPostal", customer.ZipPostal);
            cmd.Parameters.AddWithValue("@city", customer.City);
            cmd.Parameters.AddWithValue("@country", customer.Country);
            cmd.Parameters.AddWithValue("@phone", customer.PhoneNumber);
            cmd.Parameters.AddWithValue("@email", customer.Email);

            // #3.. Query the DB
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int customerId = Convert.ToInt32(reader[0].ToString());

            // #4.. Close the connection
            conn.Close();

            return customerId;
        }




        //This part is not yet ready, but i have figured out how to do it as it is just to build upon the customercreation,
        //just with the reservationModel instead, and using the seesion variables to add to the reservation.
        internal int AddReservation(ReservationModel reservation)
        {

            // #1.. Read the value from the appsettings.json and connect to DB
            string connstr = configuration.GetConnectionString("HotelLandlystDB");
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            // #2.. Create command and get the hands on the customerID
            string query = "INSERT INTO [dbo].[Reservation]([customerId],[roomId],[pets],[golf],[arriving],[departing],[guestCount],[erlyCheckIn],[conferenceParticipants],[price])" +
                           "VALUES(@customerId,@roomId,@pets,@golf,@arriving,@departing,@guestCount,@erlyCheckIn,@conferenceParticipants,@price)select SCOPE_IDENTITY() as reservationId";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@customerId", reservation.CustomerId);
            cmd.Parameters.AddWithValue("@roomId", reservation.RoomId);
            cmd.Parameters.AddWithValue("@pets", reservation.Pets);
            cmd.Parameters.AddWithValue("@golf", reservation.Golf);
            cmd.Parameters.AddWithValue("@arriving", reservation.Arriving);
            cmd.Parameters.AddWithValue("@departing", reservation.Departing);
            cmd.Parameters.AddWithValue("@guestCount", reservation.GuestCount);
            cmd.Parameters.AddWithValue("@conferenceParticipants", reservation.ConferenceParticipants);
            cmd.Parameters.AddWithValue("@price", reservation.Price);

            // #3.. Query the DB
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int reservationId = Convert.ToInt32(reader[0].ToString());

            // #4.. Close the connection
            conn.Close();

            return reservationId;
        }
        



        internal object getReservation(int sessionCustomerId)
        {
            BookingModel customer = new BookingModel();
            // #1.. Read the value from the appsettings.json and connect to DB
            string connstr = configuration.GetConnectionString("HotelLandlystDB");
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            // #2.. Create command and get the hands on the customerID
            string query = "SELECT [firstName],[lastName],[streetName],[streetNumber],[zipPostal],[city],[country],[phone],[email]" +
                           "FROM [dbo].[Customers] WHERE customerID = @sessionCustomerID ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@sessionCustomerID", sessionCustomerId);


            // #3.. Query the DB
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            customer.CustomerID = sessionCustomerId;
            customer.FirstName = (reader["firstName"].ToString());
            customer.LastName = (reader["lastName"].ToString());
            customer.StreetName = (reader["streetName"].ToString());
            customer.StreetNumber = (Convert.ToInt32(reader["streetNumber"]));
            customer.ZipPostal = (Convert.ToInt32(reader["zipPostal"]));
            customer.City = (reader["city"].ToString());
            customer.Country = (reader["country"].ToString());
            customer.PhoneNumber = (reader["phone"].ToString());
            customer.Email = (reader["email"].ToString());


            // #4.. Close the connection
            conn.Close();


            return customer;
        }
    }
}
