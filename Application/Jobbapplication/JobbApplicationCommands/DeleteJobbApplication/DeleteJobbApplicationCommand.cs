using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jobbapplication.JobbApplicationCommands.DeleteJobbApplication
{
    public class DeleteJobbApplicationCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }

}
