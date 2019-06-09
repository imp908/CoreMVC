using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;

namespace mvccoresb.Infrastructure.SignalR
{
    using chat.Domain.APImodels;

    public class SignalRWorks : Hub
    {
        public async Task StartWork(){
            await Clients.All.SendAsync("WorkStatus",new WorkQueued());
            
            await Clients.All.SendAsync("WorkStatus", new WorkStarted());

            Task t = BackGroundWork.FakeLongRunningTask();

            await t.ContinueWith(
                (r)=>{
                    Clients.All.SendAsync("WorkStatus", new WorkFinished());
                }
            );            
        }
    }
    public class BackGroundWork
    {
        public static async Task FakeLongRunningTask()
        {
            await Task.Factory.StartNew(() => Thread.Sleep(5000));
        }
    }

}