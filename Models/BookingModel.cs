using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_landlyst_v_0_01.Models
{
    //Logic to add Labels to the formfields, and to apply fieldcontrol to the fields
    public class BookingModel
    {
        #region Fields


        private int customerId;
        private int roomId;
        private string firstName = null;
        private string lastName = null;
        private string streetName = null;
        private int streetNumber;
        private int zipPostal;
        private string city = null;
        private string country = null;
        private string phoneNumber = null;
        private string email = null;
        private string confirmEmail = null;

        #endregion



        #region Properties

        [Display(Name = "Customer ID: ")]
        public int CustomerID
        {
            get { return customerId; }
            set { customerId = value; }
        }

        [Display(Name = "Room ID: ")]
        public int RoomId
        {
            get { return roomId; }
            set { roomId = value; }
        }

        [Display(Name = "Fornavn: ")]
        [Required(ErrorMessage = "Udfyld venligst Dit fornavn.")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [Display(Name = "Efternavn: ")]
        [Required(ErrorMessage = "Udfyld venligst Dit efternavn.")]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [Display(Name = "Vejnavn: ")]
        [Required(ErrorMessage = "Udfyld venligst vejnavn.")]
        public string StreetName
        {
            get { return streetName; }
            set { streetName = value; }
        }

        [Display(Name = "Husnummer: ")]
        [Required(ErrorMessage = "Udfyld venligst Husnummer.")]
        public int StreetNumber
        {
            get { return streetNumber; }
            set { streetNumber = value; }
        }

        [Display(Name = "Postnummer: ")]
        [Range(1000, 9999, ErrorMessage = "Ugyldigt postnummer.")]
        public int ZipPostal
        {
            get { return zipPostal; }
            set { zipPostal = value; }
        }

        [Display(Name = "By: ")]
        [Required(ErrorMessage = "Udfyld venligst bynavn.")]
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        [Display(Name = "Land: ")]
        [Required(ErrorMessage = "Udfyld venligst Land.")]
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        [Display(Name = "Telefon: ")]
        [Required(ErrorMessage = "Udfyld venligst telefonnummer.")]
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Udfyld venligst din email addresse.")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [Display(Name = "Bekræft Email: ")]
        [Compare("Email", ErrorMessage = "Email addressen stemmer ikke overens med den først indtastede.")]
        public string ConfirmEmail
        {
            get { return confirmEmail; }
            set { confirmEmail = value; }
        }



        #region Constructors
        public BookingModel()
        {

        }

        public BookingModel(
            int customerId,
            int roomId,
            string firstName,
            string lastName,
            string streetName,
            int streetNumber,
            int zipPostal,
            string city,
            string country,
            string phoneNumber,
            string email,
            string confirmEmail
            )
        {
            this.customerId = customerId;
            this.roomId = roomId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.streetName = streetName;
            this.streetNumber = streetNumber;
            this.zipPostal = zipPostal;
            this.city = city;
            this.country = country;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.confirmEmail = confirmEmail;
        }
        #endregion



        //------------------Password is not being used in this project, so it has been commented out----------------

        //[Display(Name ="Password")]
        //[Required(ErrorMessage = "Udfyld venligst password.")]
        //[DataType(DataType.Password)]
        //[StringLength(100, MinimumLength = 10, ErrorMessage ="Dit password skal minimum være 10 karakterer langt.")]
        //public string Password { get; set; }

        //[Display(Name ="Bekræft password")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage ="Passwords stemmer ikke overens")]
        //public string ConfirmPassword { get; set; }
        #endregion
    }
}
