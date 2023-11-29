using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XT.Common.Services
{
    public interface IWindowService
    {
        Task Start();
        Task Stop();
    }
}
