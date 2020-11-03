using Newtonsoft.Json;
using Sahaab.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Sahaab.Controllers
{
    public class MainController : Controller
    {
        public string apiBaseAddress = "http://localhost:58339/";
        // GET: Main
        public ActionResult Products()
        {
            try
            {
                ProductViewModel model = new ProductViewModel();
                // model.ProductList = Employee.RetrieveAllEmployee();


                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string authInfo = WebConfigurationManager.AppSettings["BasicAuthUsername"] + ":" + WebConfigurationManager.AppSettings["BasicAuthPassword"];
                    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                    HttpResponseMessage response = client.GetAsync("api/allproducts").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var result = response.Content.ReadAsStringAsync().Result;
                        model = JsonConvert.DeserializeObject<ProductViewModel>(result);
                    }
                    else
                    {
                        var errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                        throw new Exception(errorMessage);
                    }
                    return View("Products", model);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ActionResult> Create(ProductViewModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    
                    var json = JsonConvert.SerializeObject(model);
                    HttpContent content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    string authInfo = WebConfigurationManager.AppSettings["BasicAuthUsername"] + ":" + WebConfigurationManager.AppSettings["BasicAuthPassword"];
                    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);

                    HttpResponseMessage response = await client.PostAsync("api/addproducts", content);
                    if (response.IsSuccessStatusCode)
                    {
                        // Storing the response details recieved from web api
                        var result = response.Content.ReadAsStringAsync().Result;
                        model.ProductList = JsonConvert.DeserializeObject<List<ProductViewModel>>(result);

                        return PartialView("_ProductList", model);
                    }
                    else
                    {
                        var errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                        var data1 = new { datamsg = "Error", Msg = errorMessage };
                        return Json(data1, JsonRequestBehavior.AllowGet);

                        //throw new Exception(errorMessage);
                    }

                   
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public ActionResult Details(int Id)
        {
            try
            {
                ProductViewModel model = new ProductViewModel();
                // model.ProductList = Employee.RetrieveAllEmployee();


                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string authInfo = WebConfigurationManager.AppSettings["BasicAuthUsername"] + ":" + WebConfigurationManager.AppSettings["BasicAuthPassword"];
                    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                    HttpResponseMessage response = client.GetAsync("api/details/"+Id+"").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var result = response.Content.ReadAsStringAsync().Result;
                        model = JsonConvert.DeserializeObject<ProductViewModel>(result);
                        return PartialView("_ProductEdit", model);
                    }
                    else
                    {
                        var errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                        throw new Exception(errorMessage);
                    }
                    
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<ActionResult> Update(ProductViewModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    
                    var json = JsonConvert.SerializeObject(model);
                    HttpContent content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    string authInfo = WebConfigurationManager.AppSettings["BasicAuthUsername"] + ":" + WebConfigurationManager.AppSettings["BasicAuthPassword"];
                    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);

                    HttpResponseMessage response = await client.PostAsync("api/updateproducts", content);
                    if (response.IsSuccessStatusCode)
                    {
                        // Storing the response details recieved from web api
                        var result = response.Content.ReadAsStringAsync().Result;
                        model.ProductList = JsonConvert.DeserializeObject<List<ProductViewModel>>(result);

                        return PartialView("_ProductList", model);
                    }
                    else
                    {
                        var errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                        var data1 = new { datamsg = "Error", Msg = errorMessage };
                        return Json(data1, JsonRequestBehavior.AllowGet);

                        //throw new Exception(errorMessage);
                    }


                }



            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public ActionResult Delete(int Id)
        {
            try
            {
                ProductViewModel model = new ProductViewModel();

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string authInfo = WebConfigurationManager.AppSettings["BasicAuthUsername"] + ":" + WebConfigurationManager.AppSettings["BasicAuthPassword"];
                    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                    HttpResponseMessage response = client.GetAsync("api/delete/"+Id+"").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        model.ProductList = JsonConvert.DeserializeObject<List<ProductViewModel>>(result);
                        return PartialView("_ProductList", model);
                    }
                    else
                    {
                        var errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                        throw new Exception(errorMessage);
                    }
                    
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public  ActionResult Search(string search)
        {
            try
            {
                ProductViewModel model = new ProductViewModel();

                string apilink= "api/searchproduct/"+search+"" ;
                if (search == "")
                {
                    apilink = "api/allproducts";
                }

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string authInfo = WebConfigurationManager.AppSettings["BasicAuthUsername"] + ":" + WebConfigurationManager.AppSettings["BasicAuthPassword"];
                    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                    HttpResponseMessage response = client.GetAsync(apilink).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        if (search == "")
                        {
                            var result = response.Content.ReadAsStringAsync().Result;
                            model = JsonConvert.DeserializeObject<ProductViewModel>(result);
                            return PartialView("_ProductList", model);
                        }
                        else
                        {
                            var result = response.Content.ReadAsStringAsync().Result;
                            model.ProductList = JsonConvert.DeserializeObject<List<ProductViewModel>>(result);
                            return PartialView("_ProductList", model);
                        }
                       


                        
                    }
                    else
                    {
                        var errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                        throw new Exception(errorMessage);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }



        }
    }
}