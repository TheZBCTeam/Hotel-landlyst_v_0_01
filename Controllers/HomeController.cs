using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Configuration;
using Hotel_landlyst_v_0_01.Models;
using Hotel_landlyst_v_0_01.Data;
using Hotel_landlyst_v_0_01.DAL;
using Microsoft.AspNetCore.Http;

namespace Hotel_landlyst_v_0_01.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        #endregion



        #region Constructors
        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Golf()
        {
            return View();
        }

        public IActionResult Restaurant()
        {
            return View();
        }

        public IActionResult SearchRooms()
        {
            return View();
        }

        public IActionResult Booking()
        {
            return View();
        }

        public IActionResult BookingConfirmation(BookingModel reservation)
        {

            DALReservation dr = new DALReservation(configuration);

            int reservationID = dr.addBooking(reservation);

            reservation.ReservationID = reservationID;

            //save the bookingID to the session
            HttpContext.Session.SetString("reservationID", reservationID.ToString()); //This line writes to the session

            string stringReservationID = HttpContext.Session.GetString("reservationID"); //This is reading from session

            return View(reservation);
        }


        public IActionResult BookingOverview()
        {
            // Get the BookingID from the session
            int sessionReservationID = Convert.ToInt32(HttpContext.Session.GetString("reservationID")); //This is reading from session

            //Get the Booking object from the DB by using the DALReservation class (configuarion is set in top of this site)
            DALReservation dr = new DALReservation(configuration);
            BookingModel reservation = (BookingModel)dr.getReservation(sessionReservationID);
            
            //Send the results to the view
            return View(reservation);
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Conference()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
