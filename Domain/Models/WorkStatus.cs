namespace chat.Domain.APImodels{

    using System;
    public interface IWork
    {
        DateTime EventDate {get;}
    }

    public interface IWorkStatus
    {
        string Status {get;}
    }

    public class BaseWork : IWork
    {
        public DateTime EventDate {get;} = DateTime.Now;
    }

    public class WorkQueued : BaseWork, IWorkStatus
    {
        public string Status { get; } = "Queued";
    }    
    public class WorkStarted : BaseWork, IWorkStatus
    {
        public string Status {get;} = "Started";
    } 
    public class WorkFinished : BaseWork, IWorkStatus
    {
        public string Status { get; } = "Finished";
    }  
}