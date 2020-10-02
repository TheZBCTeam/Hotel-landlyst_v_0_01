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

        [Display(Name = "Booking ID")]
        public int ReservationID { get; set; }

        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Udfyld venligst Dit fornavn.")]
        public string FirstName { get; set; }

        [Display(Name = "Efternavn")]
        [Required(ErrorMessage = "Udfyld venligst Dit efternavn.")]
        public string LastName { get; set; }

        [Display(Name = "Vejnavn")]
        [Required(ErrorMessage = "Udfyld venligst vejnavn.")]
        public string StreetName { get; set; }

        [Display(Name = "Husnummer")]
        [Required(ErrorMessage = "Udfyld venligst Husnummer.")]
        public int StreetNumber { get; set; }

        [Display(Name = "Postnummer")]
        [Range(1000, 9999, ErrorMessage = "Ugyldigt postnummer.")]
        public int ZipPostal { get; set; }

        [Display(Name = "By")]
        [Required(ErrorMessage = "Udfyld venligst bynavn.")]
        public string City { get; set; }

        [Display(Name = "Land")]
        [Required(ErrorMessage = "Udfyld venligst Land.")]
        public string Country { get; set; }

        [Display(Name = "Telefonnummer")]
        [Required(ErrorMessage = "Udfyld venligst telefonnummer.")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email addresse")]
        [Required(ErrorMessage = "Udfyld venligst din email addresse.")]
        public string Email { get; set; }

        [Display(Name = "Bekræft Email addresse")]
        [Compare("Email", ErrorMessage = "Email addressen stemmer ikke overens med den først indtastede.")]
        public string ConfirmEmail { get; set; }

      




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
    }
}
