using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UserCommands.UpdateUser
{
    
        public class UpdateUserByIdCommand : IRequest<IdentityResult>
        {
            public string UserId { get; set; } 
            public string? Username { get; set; } 
           public string? Email { get; set; } 
           public string? Password { get; set; }
        }
    }

