using Application.Dtos;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.JobbApplicationQuerys
{
    public class GetJobbApplicationByIdQuery : IRequest<JobbApplicationDto>
    {
        public Guid Id { get; }

        public GetJobbApplicationByIdQuery(Guid id)
        {
            Id = id;
        }
    }

}
