using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _1081759project.Models
{
    public class CVMFacFood
    {
        public List<TableFactorys1081759> factory { get; set; }
        public List<TableFoods1081759> food { get; set; }

        [DisplayName("菜單編號")]
        public string foodId { get; set; }
        [DisplayName("菜單名稱")]
        public string foodName { get; set; }
        [DisplayName("單價")]
        public string foodPrice { get; set; }
        [DisplayName("存貨量")]
        public string foodStock { get; set; }
      
    }
}