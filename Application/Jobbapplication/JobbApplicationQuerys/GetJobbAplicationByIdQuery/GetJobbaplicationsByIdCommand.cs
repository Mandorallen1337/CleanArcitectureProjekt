using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jobbapplication.JobbApplicationQuerys.GetJobbAplicationByIdQuery
{
    public class GetJobbApplicationByIdQuery : IRequest<JobbApplication>
    {
        public Guid Id { get; set; }
    }
}
