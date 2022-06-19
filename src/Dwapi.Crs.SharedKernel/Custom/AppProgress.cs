using System;
using Dwapi.Crs.SharedKernel.Enums;
using Humanizer;

namespace Dwapi.Crs.SharedKernel.Custom
{
    public class AppProgress
    {
        public Area Area { get; private set; }
        public string AreaName => $"{Area}";
        public string Action { get; private set; }
        public double PercentComplete { get;  private set;}

        public double PercentCompleteInt => (int)PercentComplete;

        public string Report => $"{Action} {PercentComplete:F}%";

        public DateTime When { get; private set; }
        public string WhenAgo => When.Humanize(false);

        private AppProgress()
        {
        }
        private AppProgress(Area area, string action,double percentComplete )
        {
            Area = area;
            PercentComplete = percentComplete;
            Action = action;
            When=DateTime.Now;
        }

        public static AppProgress New(Area area,string action,double percentComplete)
        {
            return new AppProgress(area,action, percentComplete);
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