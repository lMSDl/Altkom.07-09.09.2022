using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseWebSockets();
app.MapWhen(context => context.WebSockets.IsWebSocketRequest, webSocketApp =>
{
    webSocketApp.Run(async context =>
    {
        var websocket = await context.WebSockets.AcceptWebSocketAsync();
        var helloMessage = Encoding.UTF8.GetBytes("Hello from WebSocket!");

        await websocket.SendAsync(new ArraySegment<byte>(helloMessage, 0, helloMessage.Length), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);

        _ = Task.Run(async () =>
        {
            do
            {
                var message = Encoding.UTF8.GetBytes($"{DateTime.Now}");
                await websocket.SendAsync(new ArraySegment<byte>(message, 0, message.Length), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);

                await Task.Delay(5000);

            } while (!websocket.CloseStatus.HasValue);
        });

        do
        {
            byte[] buffer = new byte[1024];

            try
            {
                await Task.Delay(5000);
                var receive = await websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                await websocket.SendAsync(new ArraySegment<byte>(buffer, 0, receive.Count), receive.MessageType, receive.EndOfMessage, CancellationToken.None);
            }
            catch { }

        } while (!websocket.CloseStatus.HasValue);


        await websocket.CloseAsync(websocket.CloseStatus.Value, websocket.CloseStatusDescription, CancellationToken.None);
    });
});

app.UseOwin(pipe => pipe(environment => OwinResponse));

app.Run();


Task OwinResponse(IDictionary<string, object> environment)
{
    var path = (string)environment["owin.RequestPath"];

    string response = $"Hello from OWIN! Your path was: {path}";
    var responseBytes = Encoding.UTF8.GetBytes(response);

    var headers = (IDictionary<string, string[]>)environment["owin.ResponseHeaders"];

    headers["Content-Type"] = new string[] { "text/plain" };
    headers["Content-Length"] = new string[] { responseBytes.Length.ToString() };

    var stream = (Stream)environment["owin.ResponseBody"];

    return stream.WriteAsync(responseBytes, 0, responseBytes.Length);
}