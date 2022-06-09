using System;
using Humanizer;

namespace Dwapi.Crs.SharedKernel.Custom
{
    public class AppProgress
    {
        public string Action { get; private set; }
        public double PercentComplete { get;  private set;}

        public string Report => $"{Action} {PercentComplete:F}%";

        public DateTime When { get; private set; }
        public string WhenAgo => When.Humanize(false);

        private AppProgress()
        {
        }
        private AppProgress(string action,double percentComplete )
        {
            PercentComplete = percentComplete;
            Action = action;
            When=DateTime.Now;
        }

        public static AppProgress New(string action,double percentComplete)
        {
            return new AppProgress(action, percentComplete);
        }
        
        public void Update(string action)
        {
            Action = action;
            When=DateTime.Now;
        }

        
        public void Update(int count, int total)
        {
            PercentComplete = (total == 0 || count == 0) ? 100 : (count / total) * 100;
            When=DateTime.Now;
        }

        public void Update(string action, int count, int total)
        {
            Action = action;
            PercentComplete = (total == 0 || count == 0) ? 100 : ((double) count / (double)total) * 100;
            When=DateTime.Now;
        }
        
        public void UpdateDone(string action)
        {
            Action = action;
            PercentComplete = 100;
            When=DateTime.Now;
        }

        public override string ToString()
        {
            return Report;
        }
    }
}