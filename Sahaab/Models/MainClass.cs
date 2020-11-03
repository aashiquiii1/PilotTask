using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Sahaab.Models
{
    public class Product
    {
       

        public static ProductViewModel Retrieve()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            
            try
            {
                ProductViewModel model = new ProductViewModel();

                model.ProductList = Product.RetrieveAllProducts();
                model.ProductTypeList = Product.RetrieveAllProductsType();

                return model;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in. " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

            }
        }
        public static List<ProductViewModel> RetrieveAllProducts()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ProductViewModel model = new ProductViewModel();
            ProductDbContext dbContext = new ProductDbContext();
            model.ProductList = new List<ProductViewModel>();
           
            try
            {
                ds = dbContext.RetrieveAllProducts();
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProductViewModel productviewmodel = new ProductViewModel();

                        productviewmodel.Id = Convert.ToInt32(dr["Id"]);
                        productviewmodel.Name = Convert.ToString(dr["Name"]);
                        productviewmodel.Description = Convert.ToString(dr["Description"]);
                        productviewmodel.ItemCount = Convert.ToInt32(dr["ItemCount"]);
                        productviewmodel.ItemPrice = Math.Round(Convert.ToDecimal(dr["ItemPrice"]), 2); 
                        
                        productviewmodel.AvailabilityStatus = Convert.ToBoolean(dr["AvailabilityStatus"]);
                        productviewmodel.ProductTypeName = Convert.ToString(dr["ProductTypeName"]);

                        model.ProductList.Add(productviewmodel);
                    }

                }
                return model.ProductList;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in. " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

            }
        }

        public static List<ProductViewModel> RetrieveAllProductsbySearch(string search)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ProductViewModel model = new ProductViewModel();
            ProductDbContext dbContext = new ProductDbContext();
            model.ProductList = new List<ProductViewModel>();

            try
            {
                ds = dbContext.RetrieveAllProductsbySearch(search);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProductViewModel productviewmodel = new ProductViewModel();

                        productviewmodel.Id = Convert.ToInt32(dr["Id"]);
                        productviewmodel.Name = Convert.ToString(dr["Name"]);
                        productviewmodel.Description = Convert.ToString(dr["Description"]);
                        productviewmodel.ItemCount = Convert.ToInt32(dr["ItemCount"]);
                        productviewmodel.ItemPrice = Math.Round(Convert.ToDecimal(dr["ItemPrice"]), 2);
                        productviewmodel.AvailabilityStatus = Convert.ToBoolean(dr["AvailabilityStatus"]);
                        productviewmodel.ProductTypeName = Convert.ToString(dr["ProductTypeName"]);

                        model.ProductList.Add(productviewmodel);
                    }

                }
                return model.ProductList;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in. " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

            }
        }


        public static ProductViewModel RetrieveProductbyId(int id)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ProductViewModel model = new ProductViewModel();
            ProductDbContext dbContext = new ProductDbContext();
            
            try
            {
                ds = dbContext.RetrieveProductbyId(id);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    model.Name = Convert.ToString(dt.Rows[0]["Name"]);
                    model.Description = Convert.ToString(dt.Rows[0]["Description"]);
                    model.ItemCount = Convert.ToInt32(dt.Rows[0]["ItemCount"]);
                    model.ItemPrice = Math.Round(Convert.ToDecimal(dt.Rows[0]["ItemPrice"]), 2);
                    model.AvailabilityStatus = Convert.ToBoolean(dt.Rows[0]["AvailabilityStatus"]);
                    model.ProductTypeId = Convert.ToInt32(dt.Rows[0]["ProductTypeId"]);


                }
                model.ProductTypeList = Product.RetrieveAllProductsType();
                return model;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in. " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

            }
        }

        public static List<ProductViewModel> AddProduct(ProductViewModel model)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ProductViewModel model1 = new ProductViewModel();
            ProductDbContext dbContext = new ProductDbContext();
            model.ProductList = new List<ProductViewModel>();
           
            try
            {
                
                dbContext.AddNew(model);
                model.ProductList = Product.RetrieveAllProducts();
                return model.ProductList;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in. " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

            }
        }

        public static List<ProductViewModel> UpdateProduct(ProductViewModel model)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ProductViewModel model1 = new ProductViewModel();
            ProductDbContext dbContext = new ProductDbContext();
            model.ProductList = new List<ProductViewModel>();

            try
            {
                dbContext.UpdateProduct(model);
                model.ProductList = Product.RetrieveAllProducts();
                return model.ProductList;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in. " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

            }
        }

        public static List<ProductsTypeModel> RetrieveAllProductsType()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ProductsTypeModel model = new ProductsTypeModel();
            ProductDbContext dbContext = new ProductDbContext();
            model.List = new List<ProductsTypeModel>();
            try
            {
                ds = dbContext.RetrieveAllProductsType();
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProductsTypeModel productsTypeModel = new ProductsTypeModel();

                        productsTypeModel.Id = Convert.ToInt32(dr["Id"]);
                        productsTypeModel.Name = Convert.ToString(dr["Name"]);
                        model.List.Add(productsTypeModel);
                    }

                }
                return model.List;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in. " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

            }
        }


        public static List<ProductViewModel> Delete(int id)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ProductViewModel model = new ProductViewModel();
            ProductDbContext dbContext = new ProductDbContext();
            

            try
            {
                dbContext.Delete(id);
                model.ProductList = Product.RetrieveAllProducts();
                return model.ProductList;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in. " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

            }
        }


        public static List<ProductViewModel> Search(string search)
        {
            ProductViewModel model = new ProductViewModel();
            ProductDbContext dbContext = new ProductDbContext();

            try
            {
               
                model.ProductList = Product.RetrieveAllProductsbySearch(search);
                return model.ProductList;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in. " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

            }
        }


    }

    public class Customers
    {
        public static CustomerOrderViewModel RetrieveOrderDetails(int Id)
        {

            try
            {
                CustomerOrderViewModel model = new CustomerOrderViewModel();

                model.customers = Customers.RetrieveCustomerDetailsbyId(Id);
                model.customers.Orderlist = Customers.RetrieveAllOrderbyCustomer(Id);
               

                return model;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in. " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {

            }
        }


        public static CustomerModel RetrieveCustomerDetailsbyId(int Id)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            CustomerModel model = new CustomerModel();
            CustomerDbContext dbContext = new CustomerDbContext();

            try
            {
                ds = dbContext.RetrieveCustomerbyId(Id);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    model.id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    model.name = Convert.ToString(dt.Rows[0]["Name"]);
                    model.email = Convert.ToString(dt.Rows[0]["Email"]);
                }
               


                return model;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in. " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }

        public static List<OrderModel> RetrieveAllOrderbyCustomer(int Id)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            DataTable dtOrderDetails = new DataTable();
            DataSet dsOrderDetails = new DataSet();
            CustomerDbContext dbContext = new CustomerDbContext();
            List<OrderModel> orderlist=  new List<OrderModel>();

            try
            {
                ds = dbContext.RetrieveOrderbyCustomer(Id);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        OrderModel orderModel = new OrderModel();

                        orderModel.id = Convert.ToInt32(dr["Id"]);
                        orderModel.orderNumber = Convert.ToString(dr["OrderNumber"]);
                        DateTime date = Convert.ToDateTime(dr["OrderDate"]);

                        orderModel.orderDate = date.ToString("dd/MM/yyyy");

                        orderModel.ProductList=new List<OrderDetailsModel>();
                        

                        dsOrderDetails = dbContext.RetrieveOrderDetailsbyOrder(orderModel.id);
                        if (dsOrderDetails != null && dsOrderDetails.Tables.Count > 0)
                        {
                            dtOrderDetails = dsOrderDetails.Tables[0];
                        }

                        if (dtOrderDetails != null && dtOrderDetails.Rows.Count > 0)
                        {
                            foreach (DataRow drOrderDetails in dtOrderDetails.Rows)
                            {
                                OrderDetailsModel orderdetailsModel = new OrderDetailsModel();

                                orderdetailsModel.name= Convert.ToString(drOrderDetails["Name"]);
                                orderdetailsModel.quantity= Convert.ToInt32(drOrderDetails["Quantity"]);
                               
                                orderModel.ProductList.Add(orderdetailsModel);
                            }
                        }
                        orderlist.Add(orderModel);
                    }

                }
                return orderlist;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in. " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message);
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (dtOrderDetails != null)
                {
                    dtOrderDetails.Dispose();
                    dtOrderDetails = null;
                }
                if (dsOrderDetails != null)
                {
                    dsOrderDetails.Dispose();
                    dsOrderDetails = null;
                }
            }
        }
    }
}