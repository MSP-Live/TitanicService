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


                const string apiKey = "Zzo/wtl7jk4PdxeJivOXzpnelCUBaLSiNTnHNjP1UXM2+kn8DyJjJbsK5smHxDZfrkk4lp4B1Be2zHU5uEFCkA==";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/0ae14adb92ab448d9c08cf25c19db4f0/services/6e8b91505d004995a2046a2752b773ef/execute?api-version=2.0&details=true ");
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
