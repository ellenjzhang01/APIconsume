using System;
using Microsoft.Extensions.Logging;
using API.Common.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace API.Common.Services
{
    public class APIServices
    {
        private readonly ILogger _logger;

        public static async Task<string> GeCustomerFormAPI()
        {
            string retJson = "";
            using (var client = new HttpClient())
            {
                string APIurl = "https://o6tsc4vwuf.execute-api.ap-southeast-2.amazonaws.com/Prod";
                client.BaseAddress = new Uri(APIurl); ;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                HttpResponseMessage response = await client.GetAsync(APIurl);
                if (response.IsSuccessStatusCode)
                {
                    var mediaType = response.Content.Headers.ContentType.MediaType;
                    if (mediaType == "application/json")
                    {
                        retJson = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return retJson;
        }
        public static List<Customer> GetCustomers()
        {
            var taskitem = System.Threading.Tasks.Task.Run(() => GeCustomerFormAPI());
            taskitem.Wait();

            string response = taskitem.Result;
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(response);
            foreach (var c in customers)
            {
                c.PhoneNumber = SetPhoneNumber(c);
            }
                return customers;
        }

        public static string SetPhoneNumber(Customer customer)
        {
                if (string.IsNullOrEmpty(customer.PhoneNumber))
                {
                    customer.PhoneNumber = "Missing or invalid";
                }
                else if (customer.PhoneNumber.Contains("(") && customer.PhoneNumber.Contains(")"))
                {
                    string phoneNum = customer.PhoneNumber.Substring(customer.PhoneNumber.IndexOf(")"));
                    if (phoneNum.Length < 8 || phoneNum.Length > 10)
                    {
                       customer.PhoneNumber = "Missing or invalid";
                    }
                }
                else
                {
                    customer.PhoneNumber = new string(customer.PhoneNumber.Where(char.IsNumber).ToArray());
                    if (customer.PhoneNumber.Length < 8 || customer.PhoneNumber.Length > 10)
                    {
                      customer.PhoneNumber = "Missing or invalid";
                    }
                    else
                    {
                        switch (customer.State)
                        {
                            case "NSW":
                                customer.PhoneNumber = "(02) " + customer.PhoneNumber;
                                break;
                            case "ACT":
                                customer.PhoneNumber = "(02) " + customer.PhoneNumber;
                                break;
                            case "QLD":
                                customer.PhoneNumber = "(07) " + customer.PhoneNumber;
                                break;
                            case "TAS":
                               customer.PhoneNumber = "(03) " + customer.PhoneNumber;
                                break;
                            case "VIC":
                                customer.PhoneNumber = "(03) " + customer.PhoneNumber;
                                break;
                            case "WA":
                                customer.PhoneNumber = "(08) " + customer.PhoneNumber;
                                break;
                            case "SA":
                                customer.PhoneNumber = "(08) " + customer.PhoneNumber;
                                break;
                            default:
                                customer.PhoneNumber = "(02) " + customer.PhoneNumber;
                                break;
                        }
                    }
                }
                          
            return customer.PhoneNumber;
        }
    }
}
