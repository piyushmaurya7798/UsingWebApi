using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Security;
using System.Text;
using UsingWebApi.Models;

namespace UsingWebApi.Controllers
{
    public class EmpController : Controller
    {
        HttpClient client;
        public EmpController() 
        {

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
            client=new HttpClient(clientHandler);
        }
        public IActionResult Index()
        {
            List<Emp> emp=new List<Emp>();
            string url = "https://localhost:44374/api/Emp/GetEmps";
            HttpResponseMessage response=client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsondata = response.Content.ReadAsStringAsync().Result;
                emp=JsonConvert.DeserializeObject<List<Emp>>(jsondata);
               
            }
            return View(emp);
        }
        
        public IActionResult AddEmp()
        {
          
            return View();
        }
        [HttpPost]
        public IActionResult AddEmp(Emp e)
        {
            string url = "https://localhost:44374/api/Emp/AddEmp";
           var jsondata=JsonConvert.SerializeObject(e);
            StringContent content = new StringContent(jsondata,Encoding.UTF8,"application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            string url = $"https://localhost:44374/api/Emp/DelEmps/{id}";
           
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        
        public IActionResult Update(int id)
        {
            string url = $"https://localhost:44374/api/Emp/GetById/{id}";
           
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsondata = response.Content.ReadAsStringAsync().Result;
                var emp = JsonConvert.DeserializeObject<Emp>(jsondata);
            return View(emp);
            }
                return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(Emp e)
        {
            string url = $"https://localhost:44374/api/Emp/UpdateEmp";
            var jsondata = JsonConvert.SerializeObject(e);
            StringContent content = new StringContent(jsondata, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PatchAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
                return View();
        }
    }
}
