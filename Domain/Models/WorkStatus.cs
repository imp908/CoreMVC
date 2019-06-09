namespace chat.Domain.APImodels{
    public interface IWorkStatus
    {
        string Status {get;}
    }

    public class WorkQueued : IWorkStatus
    {
        public string Status { get; } = "Queued";
    }    
    public class WorkStarted : IWorkStatus
    {
        public string Status {get;} = "Started";
    } 
    public class WorkFinished : IWorkStatus
    {
        public string Status { get; } = "Finished";
    }  
}