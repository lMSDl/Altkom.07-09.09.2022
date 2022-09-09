using Grpc.Core;
using StreamService;

namespace GrpcService.Services
{
    public class GrpcStreamService : StreamService.GrpcStream.GrpcStreamBase
    {

        public override async Task FromServer(Request request, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            var text = "ala ma kota i dwa psy";

            foreach (var item in text)
            {
                await responseStream.WriteAsync(new Response { Text = item.ToString() });
            }
        }

        public override async Task<Response> ToServer(IAsyncStreamReader<Request> requestStream, ServerCallContext context)
        {
            var response = new Response();

            await foreach (var request in requestStream.ReadAllAsync())
            {
                response.Text += request.Text;
            }

            return response;
        }

        public override async Task FromToServer(IAsyncStreamReader<Request> requestStream, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            await foreach (var request in requestStream.ReadAllAsync())
            {
                await responseStream.WriteAsync(new Response { Text = request.Text });
            }
        }
    }
}
