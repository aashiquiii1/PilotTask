using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahaab.Models
{
    public class ProductViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        
        
        public string Search { get; set; }
        [Required(ErrorMessage = "*")]
        public string Description { get; set; }

        [Required(ErrorMessage = "*")]
        public int ItemCount { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal ItemPrice { get; set; }

        [Required(ErrorMessage = "*")]
        public bool AvailabilityStatus { get; set; }
      
        [Required(ErrorMessage = "*")]
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public List<ProductViewModel> ProductList { get; set; }

        public List<ProductsTypeModel> ProductTypeList { get; set; }
        
    }

    public class ProductsTypeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        public string Description { get; set; }
        public List<ProductsTypeModel> List { get; set; }
    }


 


    public class CustomerOrderViewModel
    {
        [JsonProperty(PropertyName = "customer")]
        public CustomerModel customers { get; set; }
        //[JsonProperty(PropertyName = "order")]
        //public OrderModel ordermodel { get; set; }
        //public List<OrderModel> OrderList { get; set; }
        //public List<OrderDetailsModel> OrderDetailsList { get; set; }
        
    }

    public class CustomerModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        [JsonProperty(PropertyName = "order")]
        public List<OrderModel> Orderlist { get; set; }
    }
    public class OrderModel
    {
        
        public int id { get; set; }
        public string orderNumber { get; set; }
        public string orderDate { get; set; }
        [JsonProperty(PropertyName = "products")]
        public List<OrderDetailsModel> ProductList { get; set; }

    }

    public class OrderDetailsModel
    {
        public string name { get; set; }
        public int quantity { get; set; }
    }

}