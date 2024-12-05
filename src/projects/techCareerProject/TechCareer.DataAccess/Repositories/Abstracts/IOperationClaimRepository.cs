using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCareer.DataAccess.Repositories.Abstracts;

public interface IOperationClaimRepository : IAsyncRepository<OperationClaim,int>
{
}
