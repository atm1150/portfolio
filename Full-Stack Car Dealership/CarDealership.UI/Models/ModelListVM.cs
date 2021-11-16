using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{
    public class ModelListVM
    {
        public Model Model { get; set; }
        public List<ModelListItem> ModelList { get; set; }
        public List<SelectListItem> MakeList { get; set; }
        public int SelectedMakeId { get; set; }
        public ModelListVM()
        {
            Model = new Model();
            MakeList = new List<SelectListItem>();
            SelectedMakeId = new int();        
        }

        public void SetMakeItems(IEnumerable<Make> makes)
        {
            foreach (var make in makes)
            {
                MakeList.Add(new SelectListItem()
                {
                    Value = make.MakeID.ToString(),
                    Text = make.MakeName
                });
            }
        }
    }
}