using CSA.WebService.Common.Authorization;
using Sahaab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Sahaab.API
{
    public class WebApiController : ApiController
    {
        [BasicAuthorization]
        [Route("api/allproducts")]
        [HttpGet]
        public IHttpActionResult ListProducts()
        {
            try
            {

                var list = Product.Retrieve();
                return Ok(list);

            }
            catch (Exception ex)
            {
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.InternalServerError, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message));

            }

        }

        [BasicAuthorization]
        [Route("api/addproducts")]
        [HttpPost]
        public IHttpActionResult AddProduct([FromBody] ProductViewModel model)
        {
            try
            {
               
                    
                    var result = Product.AddProduct(model);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occured while AddProduct."));
                    }
            }
            catch (Exception ex)
            {
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.InternalServerError, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message));
            }

        }


        [BasicAuthorization]
        [Route("api/updateproducts")]
        [HttpPost]
        public IHttpActionResult UpdateProduct([FromBody] ProductViewModel model)
        {
            try
            {
                var result = Product.UpdateProduct(model);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occured while updating product."));
                }

            }
            catch (Exception ex)
            {
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.InternalServerError, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message));
            }

        }


        [BasicAuthorization]
        [Route("api/delete/{id}")]
        [HttpGet]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var list = Product.Delete(id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.InternalServerError, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message));
            }

        }


        [BasicAuthorization]
        [Route("api/details/{Id}")]
        [HttpGet]
        public IHttpActionResult ProductDetails(int Id)
        {
            try
            {
                var list = Product.RetrieveProductbyId(Id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.InternalServerError, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message));
            }

        }




        [BasicAuthorization]
        [Route("api/searchproduct/{search}")]
        [HttpGet]
        public IHttpActionResult Search(string search)
        {
            try
            {
                var list = Product.Search(search);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.InternalServerError, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message));
            }

        }

        [BasicAuthorization]
        [Route("api/CustomerOrder/{Id}")]
        [HttpGet]
        public IHttpActionResult ListProducts(int Id)
        {
            try
            {
                var list = Customers.RetrieveOrderDetails(Id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.InternalServerError, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " Error Message: " + ex.Message));
            }

        }
    }



}

