using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sahaab.Models
{
    public class CustomVariables
    {
        public static string DbConnectionString = "DBConnectionString";
    }
    public class DbContextCustomError : ApplicationException
    {
        public DbContextCustomError(string message) : base(message)
        {
        }
    }

    public class ProductDbContext : IDisposable
    {
        private string conndb = string.Empty;

        public ProductDbContext()
        {
            string conndb = string.Empty;
            try
            {
                ConnectionStringSettingsCollection connstrings = ConfigurationManager.ConnectionStrings;
                string connstrvalue = CustomVariables.DbConnectionString;
                foreach (ConnectionStringSettings cs in connstrings)
                {
                    if (cs.Name.ToLower() == connstrvalue.ToLower())
                        conndb = cs.ConnectionString;
                }
                if (conndb == string.Empty)
                    // FIRE Custom Error
                    throw (new DbContextCustomError("Connection string '" + CustomVariables.DbConnectionString + "' not found in web.config file"));
                else
                {
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in connection string." + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);
            }
        }
        public bool AddNew(ProductViewModel model)
        {
            SqlConnection connSQL;
            SqlCommand cmdSQL = new SqlCommand();
            connSQL = new SqlConnection(conndb);
            cmdSQL = new SqlCommand();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[CustomVariables.DbConnectionString].ConnectionString;
                connSQL = new SqlConnection(conStr);

                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "Sp_Products";

                cmdSQL.Parameters.AddWithValue("@action", "insert");
                cmdSQL.Parameters.AddWithValue("@Name", model.Name);
                cmdSQL.Parameters.AddWithValue("@Description", model.Description);
                cmdSQL.Parameters.AddWithValue("@ItemCount", model.ItemCount);
                cmdSQL.Parameters.AddWithValue("@ItemPrice", model.ItemPrice);
                cmdSQL.Parameters.AddWithValue("@AvailabilityStatus", model.AvailabilityStatus);
                cmdSQL.Parameters.AddWithValue("@ProductTypeId", model.ProductTypeId);
                connSQL.Open();
                cmdSQL.ExecuteNonQuery();

                return true;
            }
            catch (SqlException ex)
            {
                throw new ArgumentException("Error Occured in Add New Product ." + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);

            }
            finally
            {
                // Release all Opened Objects from the memory
                if (cmdSQL != null)
                    cmdSQL.Dispose();
                cmdSQL = null;
                if (connSQL.State != ConnectionState.Closed | (connSQL != null))
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }

        public bool UpdateProduct(ProductViewModel model)
        {
            SqlConnection connSQL;
            SqlCommand cmdSQL = new SqlCommand();
            connSQL = new SqlConnection(conndb);
            cmdSQL = new SqlCommand();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[CustomVariables.DbConnectionString].ConnectionString;
                connSQL = new SqlConnection(conStr);

                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "Sp_Products";

                cmdSQL.Parameters.AddWithValue("@action", "update");
                cmdSQL.Parameters.AddWithValue("@id",model.Id);
                cmdSQL.Parameters.AddWithValue("@Name", model.Name);
                cmdSQL.Parameters.AddWithValue("@Description", model.Description);
                cmdSQL.Parameters.AddWithValue("@ItemCount", model.ItemCount);
                cmdSQL.Parameters.AddWithValue("@ItemPrice", model.ItemPrice);
                cmdSQL.Parameters.AddWithValue("@AvailabilityStatus", model.AvailabilityStatus);
                cmdSQL.Parameters.AddWithValue("@ProductTypeId", model.ProductTypeId);
                connSQL.Open();
                cmdSQL.ExecuteNonQuery();

                return true;
            }
            catch (SqlException ex)
            {
                throw new ArgumentException("Error Occured in Add New Product ." + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);

            }
            finally
            {
                // Release all Opened Objects from the memory
                if (cmdSQL != null)
                    cmdSQL.Dispose();
                cmdSQL = null;
                if (connSQL.State != ConnectionState.Closed | (connSQL != null))
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }
        public bool Delete(int id)
        {
            SqlConnection connSQL;
            SqlCommand cmdSQL = new SqlCommand();
            connSQL = new SqlConnection(conndb);
            cmdSQL = new SqlCommand();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[CustomVariables.DbConnectionString].ConnectionString;
                connSQL = new SqlConnection(conStr);

                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "Sp_Products";

                cmdSQL.Parameters.AddWithValue("@action", "delete");
                cmdSQL.Parameters.AddWithValue("@id", id);


                connSQL.Open();
                cmdSQL.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in Delete Product ." + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);

            }
            finally
            {
                // Release all Opened Objects from the memory
                if (cmdSQL != null)
                    cmdSQL.Dispose();
                cmdSQL = null;
                if (connSQL.State != ConnectionState.Closed | connSQL != null)
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }

       

        public DataSet RetrieveAllProducts()
        {
            SqlConnection connSQL;
            SqlCommand cmdSQL = new SqlCommand();
            connSQL = new SqlConnection(conndb);
            cmdSQL = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[CustomVariables.DbConnectionString].ConnectionString;
                connSQL = new SqlConnection(conStr);

                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "Sp_Products";

                cmdSQL.Parameters.AddWithValue("@action", "selectallproducts");

                connSQL.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmdSQL);
                adapter.SelectCommand = cmdSQL;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in Retrieve All Products. " + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);
            }
            // Return False
            finally
            {
                // Release all Opened Objects from the memory
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }
                if (cmdSQL != null)
                {
                    cmdSQL.Dispose();
                    cmdSQL = null;
                }
                if (connSQL.State != ConnectionState.Closed | (connSQL != null))
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }

        public DataSet RetrieveAllProductsbySearch(string search)
        {
            SqlConnection connSQL;
            SqlCommand cmdSQL = new SqlCommand();
            connSQL = new SqlConnection(conndb);
            cmdSQL = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[CustomVariables.DbConnectionString].ConnectionString;
                connSQL = new SqlConnection(conStr);

                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "Sp_Products";

                cmdSQL.Parameters.AddWithValue("@action", "selectproductbySearch");
                cmdSQL.Parameters.AddWithValue("@search", search);

                connSQL.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmdSQL);
                adapter.SelectCommand = cmdSQL;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in Retrieve All Products. " + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);
            }
            // Return False
            finally
            {
                // Release all Opened Objects from the memory
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }
                if (cmdSQL != null)
                {
                    cmdSQL.Dispose();
                    cmdSQL = null;
                }
                if (connSQL.State != ConnectionState.Closed | (connSQL != null))
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }

        public DataSet RetrieveProductbyId(int Id)
        {
            SqlConnection connSQL;
            SqlCommand cmdSQL = new SqlCommand();
            connSQL = new SqlConnection(conndb);
            cmdSQL = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[CustomVariables.DbConnectionString].ConnectionString;
                connSQL = new SqlConnection(conStr);

                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "Sp_Products";

                cmdSQL.Parameters.AddWithValue("@action", "selectproductbyId");
                cmdSQL.Parameters.AddWithValue("@id", Id);

                connSQL.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmdSQL);
                adapter.SelectCommand = cmdSQL;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in Retrieve All Products. " + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);
            }
            // Return False
            finally
            {
                // Release all Opened Objects from the memory
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }
                if (cmdSQL != null)
                {
                    cmdSQL.Dispose();
                    cmdSQL = null;
                }
                if (connSQL.State != ConnectionState.Closed | (connSQL != null))
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }

        public DataSet RetrieveAllProductsType()
        {
            SqlConnection connSQL;
            SqlCommand cmdSQL = new SqlCommand();
            connSQL = new SqlConnection(conndb);
            cmdSQL = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[CustomVariables.DbConnectionString].ConnectionString;
                connSQL = new SqlConnection(conStr);

                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "Sp_Products";

                cmdSQL.Parameters.AddWithValue("@action", "selectallProductsTpe");

                connSQL.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmdSQL);
                adapter.SelectCommand = cmdSQL;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in Retrieve All Products. " + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);
            }
            // Return False
            finally
            {
                // Release all Opened Objects from the memory
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }
                if (cmdSQL != null)
                {
                    cmdSQL.Dispose();
                    cmdSQL = null;
                }
                if (connSQL.State != ConnectionState.Closed | (connSQL != null))
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }


        //public DataSet RetrieveAllDesignation()
        //{
        //    SqlConnection connSQL;
        //    SqlCommand cmdSQL = new SqlCommand();
        //    connSQL = new SqlConnection(conndb);
        //    cmdSQL = new SqlCommand();
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string conStr;
        //        conStr = ConfigurationManager.ConnectionStrings[CustomVariables.DbConnectionString].ConnectionString;
        //        connSQL = new SqlConnection(conStr);

        //        cmdSQL.Connection = connSQL;
        //        cmdSQL.CommandType = CommandType.StoredProcedure;
        //        cmdSQL.CommandText = "SPTest";

        //        cmdSQL.Parameters.AddWithValue("@action", "selectalldesignastion");

        //        connSQL.Open();
        //        ds = new DataSet();
        //        adapter = new SqlDataAdapter(cmdSQL);
        //        adapter.SelectCommand = cmdSQL;
        //        adapter.Fill(ds);
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ArgumentException("Error Occured in RetrieveAll all Employee " + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);
        //    }
        //    // Return False
        //    finally
        //    {
        //        // Release all Opened Objects from the memory
        //        if (ds != null)
        //        {
        //            ds.Dispose();
        //            ds = null;
        //        }
        //        if (adapter != null)
        //        {
        //            adapter.Dispose();
        //            adapter = null;
        //        }
        //        if (cmdSQL != null)
        //        {
        //            cmdSQL.Dispose();
        //            cmdSQL = null;
        //        }
        //        if (connSQL.State != ConnectionState.Closed | (connSQL != null))
        //        {
        //            connSQL.Close();
        //            connSQL = null;
        //        }
        //    }
        //}


        ~ProductDbContext()
        {

        }

        private bool disposedValue;


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                    conndb = string.Empty;
            }
            this.disposedValue = true;
        }


        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    public class CustomerDbContext : IDisposable
    {
        private string conndb = string.Empty;

        public CustomerDbContext()
        {
            string conndb = string.Empty;
            try
            {
                ConnectionStringSettingsCollection connstrings = ConfigurationManager.ConnectionStrings;
                string connstrvalue = CustomVariables.DbConnectionString;
                foreach (ConnectionStringSettings cs in connstrings)
                {
                    if (cs.Name.ToLower() == connstrvalue.ToLower())
                        conndb = cs.ConnectionString;
                }
                if (conndb == string.Empty)
                    // FIRE Custom Error
                    throw (new DbContextCustomError("Connection string '" + CustomVariables.DbConnectionString + "' not found in web.config file"));
                else
                {
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in connection string." + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);
            }
        }
       
   
     
       
        
        public DataSet RetrieveCustomerbyId(int CustomerId)
        {
            SqlConnection connSQL;
            SqlCommand cmdSQL = new SqlCommand();
            connSQL = new SqlConnection(conndb);
            cmdSQL = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[CustomVariables.DbConnectionString].ConnectionString;
                connSQL = new SqlConnection(conStr);

                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "Sp_Customer";

                cmdSQL.Parameters.AddWithValue("@action", "RetrieveCustomerDetails");
                cmdSQL.Parameters.AddWithValue("@CustomerId", CustomerId);

                connSQL.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmdSQL);
                adapter.SelectCommand = cmdSQL;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in Retrieve All Products. " + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);
            }
            // Return False
            finally
            {
                // Release all Opened Objects from the memory
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }
                if (cmdSQL != null)
                {
                    cmdSQL.Dispose();
                    cmdSQL = null;
                }
                if (connSQL.State != ConnectionState.Closed | (connSQL != null))
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }

        public DataSet RetrieveOrderbyCustomer(int CustomerId)
        {
            SqlConnection connSQL;
            SqlCommand cmdSQL = new SqlCommand();
            connSQL = new SqlConnection(conndb);
            cmdSQL = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[CustomVariables.DbConnectionString].ConnectionString;
                connSQL = new SqlConnection(conStr);

                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "Sp_Customer";

                cmdSQL.Parameters.AddWithValue("@action", "RetrieveOrdersbyCustomerId");
                cmdSQL.Parameters.AddWithValue("@CustomerId", CustomerId);

                connSQL.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmdSQL);
                adapter.SelectCommand = cmdSQL;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in Retrieve All Products. " + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);
            }
            // Return False
            finally
            {
                // Release all Opened Objects from the memory
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }
                if (cmdSQL != null)
                {
                    cmdSQL.Dispose();
                    cmdSQL = null;
                }
                if (connSQL.State != ConnectionState.Closed | (connSQL != null))
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }

        public DataSet RetrieveOrderDetailsbyOrder(int OrderId)
        {
            SqlConnection connSQL;
            SqlCommand cmdSQL = new SqlCommand();
            connSQL = new SqlConnection(conndb);
            cmdSQL = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                string conStr;
                conStr = ConfigurationManager.ConnectionStrings[CustomVariables.DbConnectionString].ConnectionString;
                connSQL = new SqlConnection(conStr);

                cmdSQL.Connection = connSQL;
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.CommandText = "Sp_Customer";

                cmdSQL.Parameters.AddWithValue("@action", "RetrieveOrderDetailsbyOrderId");
                cmdSQL.Parameters.AddWithValue("@OrderId", OrderId);

                connSQL.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmdSQL);
                adapter.SelectCommand = cmdSQL;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Occured in Retrieve All Products. " + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Message: " + ex.Message);
            }
            // Return False
            finally
            {
                // Release all Opened Objects from the memory
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }
                if (cmdSQL != null)
                {
                    cmdSQL.Dispose();
                    cmdSQL = null;
                }
                if (connSQL.State != ConnectionState.Closed | (connSQL != null))
                {
                    connSQL.Close();
                    connSQL = null;
                }
            }
        }


        ~CustomerDbContext()
        {

        }

        private bool disposedValue;


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                    conndb = string.Empty;
            }
            this.disposedValue = true;
        }


        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}