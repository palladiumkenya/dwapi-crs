using System;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;

namespace Dwapi.Crs.Service.App.Filters
{
    public class ProlongExpirationTimeAttribute : JobFilterAttribute, IApplyStateFilter
    {
        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = TimeSpan.FromDays(270);
        }

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = TimeSpan.FromDays(270);
        }
    }
}
