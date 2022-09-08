
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7013/api/");


httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));


var response = await httpClient.GetAsync("Parents");
/*if (response.StatusCode != System.Net.HttpStatusCode.OK)
    return;*/
/*if (!response.IsSuccessStatusCode)
    return;*/
response.EnsureSuccessStatusCode();
var parent = await response.Content.ReadFromJsonAsync<Parent>();
Console.WriteLine(JsonConvert.SerializeObject(parent));



response = await httpClient.PostAsJsonAsync("Users/login", new Credentials { UserName = "admin", Password = "nimda" });
response.EnsureSuccessStatusCode();

var token = await response.Content.ReadAsStringAsync();

Console.WriteLine(token);

httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));


response = await httpClient.GetAsync("Users");
response.EnsureSuccessStatusCode();

Console.WriteLine(await response.Content.ReadAsStringAsync());