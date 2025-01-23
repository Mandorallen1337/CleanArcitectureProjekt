using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.JobbApplicationCommands
{
    public class DeleteJobbApplicationCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public DeleteJobbApplicationCommand(Guid id)
        {
            Id = id;
        }
    }


}
