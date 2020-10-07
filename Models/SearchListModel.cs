using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_landlyst_v_0_01.Models
{
    public class SearchListModel
    {
        private List<RoomModel> searchResultList = new List<RoomModel>();

        public List<RoomModel> AccessList()
        {

            return searchResultList;
        }



        //Bidrag fra Aleander:
        // Attribute
        //private List<RoomModel> searchResultList = new List<RoomModel>();

        // Propperty
        //public List<RoomModel> SearchResultList
        //{
        //    get 
        //    { 
        //        return searchResultList; 
        //    }
        //    set
        //    {
        //        searchResultList = value;
        //    }
        //}
    }
}
