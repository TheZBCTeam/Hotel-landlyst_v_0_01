using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_landlyst_v_0_01.Models
{
    public class RoomModel
    {
        public string RoomId { get; set; }
        public string roomType { get; set; }
        public string RoomNumber { get; set; }
        public string Price { get; set; }
        public string Cleaned { get; set; }
        public string MiniBar { get; set; }
        public string Aircondition { get; set; }
        public string PetsPossible { get; set; }
        public string GolfPossible { get; set; }
        public string RoomDescription { get; set; }
    }
}
