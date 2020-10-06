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


        internal RoomModel SearchRooms(SearchRoomsModel searchInput)
        {
            RoomModel room = new RoomModel();
            // #1.. Read the value from the appsettings.json and connect to DB
            string connstr = configuration.GetConnectionString("HotelLandlystDB");
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            // #2.. Create command and get the hands on the customerID
            //string query = "declare @searchInputDeparting as DATE " +
            //               "declare @searchInputDeparting as DATE " +
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
            room.RoomType = (reader["roomType"].ToString());
            room.ImageName = (reader["imageName"].ToString());
            room.Price = (reader["price"].ToString());
            room.MiniBar = (reader["miniBar"].ToString());
            room.Aircondition = (reader["aircondition"].ToString()); ;
            room.PetsPossible = (reader["petsPosible"].ToString());
            room.GolfPossible = (reader["golfPosible"].ToString());
            room.RoomDescription = (reader["roomDescription"].ToString());

            // #4.. Close the connection
            conn.Close();
            return room;
        }



        //internal RoomModel SearchRooms(SearchRoomsModel searchInput)
        //{
        //    RoomModel room = new RoomModel();
        //    // #1.. Read the value from the appsettings.json and connect to DB
        //    string connstr = configuration.GetConnectionString("HotelLandlystDB");
        //    SqlConnection conn = new SqlConnection(connstr);
        //    conn.Open();

        //    // #2.. Create command and get the hands on the customerID
        //    //string query = "declare @searchInputDeparting as DATE " +
        //    //               "declare @searchInputDeparting as DATE " +
        //    string query = "SELECT [ro].[roomType], [ro].[price], [ro].[miniBar], [ro].[aircondition], [ro].[petsPosible], [ro].[golfPosible], " +
        //        "[ro].[roomId], [ro].[imageName], [rd].[roomDescription]" +
        //        "FROM [dbo].[Rooms] [ro] " +
        //        "join [dbo].[RoomDescriptions] as [rd] "+
        //        "on [ro].[descriptionId] = [rd].[descriptionId] "+
        //        "where [ro].[roomId] not in(select [roomId] from [Reservations] "+
        //        "where @searchInputArriving between [arriving] and [departing] " +
        //        "and @searchInputDeparting between [arriving] and [departing]) ";

        //      SqlCommand cmd = new SqlCommand(query, conn);
        //    cmd.Parameters.AddWithValue("@searchInputArriving", searchInput.Arriving.ToString("yyyy/MM/dd"));
        //    cmd.Parameters.AddWithValue("@searchInputDeparting", searchInput.Departing.ToString("yyyy/MM/dd"));

        //    // #3.. Query the DB
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    reader.Read();
        //    room.RoomType = (reader["[ro].[roomType]"].ToString());
        //    room.ImageName = (reader["ro.imageName"].ToString());
        //    room.Price = (reader["ro.price"].ToString());
        //    room.MiniBar = (reader["ro.miniBar"].ToString());
        //    room.Aircondition = (reader["ro.aircondition"].ToString()); ;
        //    room.PetsPossible = (reader["ro.petsPossible"].ToString());
        //    room.GolfPossible = (reader["ro.golfPossible"].ToString());
        //    room.RoomDescription = (reader["ro.roomDescription"].ToString());

        //    // #4.. Close the connection
        //    conn.Close();
        //    return room;
        //}


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
