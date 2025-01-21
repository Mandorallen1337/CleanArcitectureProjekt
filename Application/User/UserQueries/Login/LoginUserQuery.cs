using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UserQueries.Login
{
    public class LoginUserQuery : IRequest<LoginUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserResponse
    {
        public bool IsSuccessful { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
}

