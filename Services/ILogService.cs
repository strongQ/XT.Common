using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Models.SignalR;

namespace XT.Common.Services
{
    public interface ILogService
    {
        public event EventHandler<string> AddLogEvent;
        void Log(string info);

        void LogHeart(string info);
        void LogError(string info, Exception ex);

        void LogError(string error);

        Task LogRemote(RemoteLog remoteLog);




        virtual List<string> GetLogs()
        {
            return new List<string>();
        }

    }
}
