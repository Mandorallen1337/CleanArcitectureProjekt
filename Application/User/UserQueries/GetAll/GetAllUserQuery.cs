using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UserQueries.GetAll
{
    public class GetAllUserQuery : IRequest<List<IdentityUser>> 
    {
    }
}
