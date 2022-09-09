using Grpc.Core;
using Models;
using Services.Interfaces;

namespace GrpcService.Services
{
    public class GrpsUsersService : GrpcUsersService.GrpcUsers.GrpcUsersBase
    {
        private ICrudService<User> _service;

        public GrpsUsersService(ICrudService<User> service)
        {
            _service = service;
        }


        public override async Task<GrpcUsersService.Users> Read(GrpcUsersService.Void request, ServerCallContext context)
        {
            var users = await _service.ReadAsync();

            var result = new GrpcUsersService.Users();
            result.Collection.AddRange(users.Select(x => new GrpcUsersService.User() { Id = x.Id, Password = x.Password, Roles = (int)x.Role, Username = x.UserName }).ToList());

            return result;
        }


    }
}
