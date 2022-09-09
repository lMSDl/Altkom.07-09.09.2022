
using Client;
using Grpc.Net.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;


using (var grpcChannel = GrpcChannel.ForAddress("https://localhost:7044")) {
    var client = new GrpcService.Greeter.GreeterClient(grpcChannel);

    var result = await client.SayHelloAsync(new GrpcService.HelloRequest() { Name = "Paul" });

    Console.WriteLine(result.Message);
}


using (var grpcChannel = GrpcChannel.ForAddress("https://localhost:7044")) {
    var usersClient = new GrpcUsersService.GrpcUsers.GrpcUsersClient(grpcChannel);

    var users = await usersClient.ReadAsync(new GrpcUsersService.Void());
}
Console.Read();

static async Task WebAPI()
{
    var handler = new HttpClientHandler()
    {
        AutomaticDecompression = System.Net.DecompressionMethods.Brotli
    };

    var httpClient = new HttpClient(handler);

    httpClient.BaseAddress = new Uri("https://localhost:7013/api/");


    httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
    httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));


    var response = await httpClient.GetAsync("Parents");
    /*if (response.StatusCode != System.Net.HttpStatusCode.OK)
        return;*/
    /*if (!response.IsSuccessStatusCode)
        return;*/
    response.EnsureSuccessStatusCode();
    //var parent = await response.Content.ReadFromJsonAsync<Parent>();
    //Console.WriteLine(JsonConvert.SerializeObject(parent));
    Console.WriteLine(await response.Content.ReadAsStringAsync());


    response = await httpClient.PostAsJsonAsync("Users/login", new Credentials { UserName = "admin", Password = "nimda" });
    response.EnsureSuccessStatusCode();

    var token = await response.Content.ReadAsStringAsync();

    Console.WriteLine(token);

    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));


    response = await httpClient.GetAsync("Users");
    response.EnsureSuccessStatusCode();

    Console.WriteLine(await response.Content.ReadAsStringAsync());

    response = await httpClient.DeleteAsync("Users/1");
    response.EnsureSuccessStatusCode();


    response = await httpClient.GetAsync("Users");
    response.EnsureSuccessStatusCode();

    Console.WriteLine(await response.Content.ReadAsStringAsync());



    var openApiHttpClient = new HttpClient();
    openApiHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
    var openApiClient = new openapiClient("https://localhost:7013", openApiHttpClient);

    var shoppingLists = await openApiClient.ShoppingListsAllAsync();

    Console.WriteLine();
}

static Action<string> ConsoleWrite()
{
    return x => Console.WriteLine(x);
}

static async Task SignalR()
{
    var signalR = new HubConnectionBuilder()
                    .WithUrl("http://localhost:5299/signalR/Demo")
                    .WithAutomaticReconnect()
                    .Build();


    signalR.Reconnected += SignalR_Reconnected;
    signalR.Reconnecting += SignalR_Reconnecting;



    Task SignalR_Reconnecting(Exception? arg)
    {
        Console.WriteLine("Reconnecting...");
        return Task.CompletedTask;
    }

    Task SignalR_Reconnected(string? arg)
    {
        Console.WriteLine("Reconnected!");
        return Task.CompletedTask;
    }

    signalR.On<string>(nameof(HelloResponse), x => HelloResponse(x));
    signalR.On<string>("Echo", x => Console.WriteLine(x));

    signalR.On("Welcome", ConsoleWrite());
    signalR.On("NewConnection", ConsoleWrite());
    signalR.On("SendMessage", ConsoleWrite());

    await signalR.StartAsync();


    await signalR.SendAsync("SayHello");
    await signalR.SendAsync("SayHelloToMe", "Paul");

    if (DateTime.Now.Second % 2 == 0)
        await signalR.SendAsync("JoinGroup", "myFavouriteGroup");



    while (true)
    {
        var connectionId = Console.ReadLine();
        await signalR.SendAsync("Echo", connectionId);
        await signalR.SendAsync("SendMessage", connectionId, Console.ReadLine());
    }


    Console.ReadLine();

    void HelloResponse(string message)
    {
        Console.WriteLine(message);
    }
}