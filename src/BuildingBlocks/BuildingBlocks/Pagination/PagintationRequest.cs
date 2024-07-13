using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Pagination
{
    public record PagintationRequest(int PageIndex=0, int PageSize=10);
   
}
