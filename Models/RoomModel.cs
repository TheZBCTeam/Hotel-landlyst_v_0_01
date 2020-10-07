using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_landlyst_v_0_01.Models
{

    public class RoomModel
    {
       

        [Display(Name = "Værelses ID: ")]
        public string RoomId { get; set; }

        [Display(Name = "Værelses type: ")]
        public string RoomType { get; set; }

        [Display(Name = "Værelsesnummer: ")]
        public string RoomNumber { get; set; }

        [Display(Name = "Pris pr. overnatning: ")]
        public string Price { get; set; }

        [Display(Name = "Rengjort: ")]
        public string Cleaned { get; set; }

        [Display(Name = "Minibar: ")]
        public string MiniBar { get; set; }

        [Display(Name = "Aircondition: ")]
        public string Aircondition { get; set; }

        [Display(Name = "Må kæledyr medbringes: ")]
        public string PetsPossible { get; set; }

        [Display(Name = "Kan bookes til, All in one Golf: ")]
        public string GolfPossible { get; set; }

        [Display(Name = "Galleri: ")]
        public string ImageName { get; set; }

        [Display(Name = "Beskrivelse: ")]
        public string RoomDescription { get; set; }
    }
}
