using Domain.Models;
using Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.JobbApplicationQuerys
{
    public class GetJobbApplicationByIdQuery : IRequest<JobApplicationViewModel>
    {
        public Guid Id { get; }

        public GetJobbApplicationByIdQuery(Guid id)
        {
            Id = id;
        }
    }

}
