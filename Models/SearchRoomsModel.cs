using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_landlyst_v_0_01.Models
{
    public class SearchRoomsModel
    {
        //field is planned for later use to ensure correct date
        //private int addYear = 3;

        [Display(Name = "Ankomst")]
        [Required(ErrorMessage = "Vælg venligst Ankomstdato")]
        public DateTime Arriving { get; set; }

        [Display(Name = "Afrejse")]
        [Required(ErrorMessage = "Vælg venligst afrejse dato")]
        public DateTime Departing { get; set; }

        //public SearchRoomsModel()
        //{
        //}
        // public SearchRoomsModel(int addYear)
        //{
        //    this.addYear = addYear;
        //}

    }

}