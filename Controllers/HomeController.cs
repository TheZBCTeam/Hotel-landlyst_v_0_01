using Hotel_landlyst_v_0_01.DAL;
using Hotel_landlyst_v_0_01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

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

        public IActionResult SearchRoomsResults(SearchRoomsModel searchInput)
        {
            SearchListModel returnedList;
            DALReservation dr = new DALReservation(configuration);
            returnedList= dr.SearchRooms(searchInput);
            List<RoomModel> finalReturnedList = returnedList.AccessList();
            return View(finalReturnedList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Booking(string submit)
        {
            int roomId = Convert.ToInt32(submit);
            HttpContext.Session.SetString("roomId", roomId.ToString()); //This line writes to the session
            return View(roomId);
        }

        public IActionResult BookingConfirmation(BookingModel addCustomer)
        {

            DALReservation dr = new DALReservation(configuration);
            int sessionRoomId = Convert.ToInt32(HttpContext.Session.GetString("roomId")); //This is reading from session

            int customerId = dr.AddCustomer(addCustomer);

            addCustomer.CustomerID = customerId;

            //save the bookingID to the session

            HttpContext.Session.SetString("customerId", customerId.ToString()); //This line writes to the session

            string stringCustomerId = HttpContext.Session.GetString("customerId"); //This is reading from session and is not used in the connection

            return View(addCustomer);
        }


        public IActionResult BookingOverview()
        {
            // Get the BookingID from the session
            int sessionCustomerId = Convert.ToInt32(HttpContext.Session.GetString("customerId")); //This is reading from session

            //Get the Booking object from the DB by using the DALReservation class (configuarion is set in top of this site)
            DALReservation dr = new DALReservation(configuration);
            BookingModel customer = (BookingModel)dr.getReservation(sessionCustomerId);

            //Send the results to the view
            return View(customer);
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
