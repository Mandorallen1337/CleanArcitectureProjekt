using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jobbapplication.JobbApplicationQuerys.GetAllJobbAplicationsQuery
{
    public class GetAllJobbApplicationsQuery : IRequest<IEnumerable<JobbApplication>>
    {
    }
}
