using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_landlyst_v_0_01.Models
{
    public class ReservationModel
    {
        //This is not done yet
        private int reservationId;
        private int customerId;
        private int roomId;
        private bool pets;
        private int golf;
        private DateTime arriving;
        private DateTime departing;
        private int guestCount;
        private int erlyCheckIn;
        private int conferenceParticipants;
        private decimal price;


        public int ReservationId
        {
            get { return ReservationId; }
            set { ReservationId = value; }
        }
        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public int RoomId
        {
            get { return roomId; }
            set { roomId = value; }
        }
        public bool Pets
        {
            get { return pets; }
            set { pets = value; }
        }
        public int Golf
        {
            get { return golf; }
            set { golf = value; }
        }
        public DateTime DateTime
        {
            get { return arriving; }
            set { arriving = value; }
        }
        public DateTime Departing
        {
            get { return departing; }
            set { departing = value; }
        }
        public int GuestCount
        {
            get { return guestCount; }
            set { guestCount = value; }
        }
        public int ErlyCheckIn
        {
            get { return erlyCheckIn; }
            set { erlyCheckIn = value; }
        }
        public int ConferenceParticipants
        {
            get { return conferenceParticipants; }
            set { conferenceParticipants = value; }
        }
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }


        public ReservationModel()
        {

        }

        public ReservationModel
            (
        int reservationId,
        int customerId,
        int roomId,
        bool pets,
        int golf,
        DateTime arriving,
        DateTime departing,
        int guestCount,
        int erlyCheckIn,
        int conferenceParticipants,
        decimal price
            )
        {
            this.reservationId = reservationId;
            this.customerId = customerId;
            this.roomId = roomId;
            this.pets = pets;
            this.golf = golf;
            this.arriving = arriving;
            this.departing = departing;
            this.guestCount = guestCount;
            this.erlyCheckIn = erlyCheckIn;
            this.conferenceParticipants = conferenceParticipants;
            this.price = price;
        }













    }
}
