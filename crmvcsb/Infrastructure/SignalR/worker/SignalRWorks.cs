using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;

namespace crmvcsb.Infrastructure.SignalR
{
    using chat.Domain.APImodels;

    public class SignalRWorks : Hub
    {
        public async Task StartWork(){
            await Clients.All.SendAsync("WorkStatus",new WorkQueued());
            await BackGroundWork.FakeLongRunningTask(2000);
            await Clients.All.SendAsync("WorkStatus", new WorkStarted());
            await BackGroundWork.FakeLongRunningTask(3000);
            await Clients.All.SendAsync("WorkStatus", new WorkFinished());       
        }
    }
    
    public class BackGroundWork
    {       
        public static async Task FakeLongRunningTask()
        {
            await Task.Factory.StartNew(() => Thread.Sleep(5000));
        }

        public static async Task FakeLongRunningTask(int ms)
        {
            await Task.Factory.StartNew(() => Thread.Sleep(ms));
        }
    }

}