using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace TitanicService
{
    //Объявляем класс
    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            InvokeRequestResposeService().Wait();
        }

        //пишем функцию для работы с сервисом
        static async Task InvokeRequestResposeService()
        {
            using (var client = new HttpClient())
            {
                //запрос к сервису
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"Survived", "Pclass", "Sex", "Age", "SibSp"},
                                Values = new string[,] {  { "0", "3", "female", "45", "1" },  { "1", "2", "male", "32", "0" }, {"1", "1", "female", "22", "2" },  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                //введите свой apiKey
                const string apiKey = "";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                //введите свой адрес сервиса
                client.BaseAddress = new Uri("");
                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);

                }
                else //если ошибка
                {
                    Console.WriteLine(string.Format("Ошибка {0}", response.StatusCode));
                    Console.WriteLine(response.Headers.ToString());
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
            
            
        }
    }
}
