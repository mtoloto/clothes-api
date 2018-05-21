using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clothes.Business.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }  
}
