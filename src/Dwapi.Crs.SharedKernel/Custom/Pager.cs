using System;

namespace Dwapi.Crs.SharedKernel.Custom
{
    public class Pager
    {
        public static int PageCount(int batchSize, long totalRecords)
        {
            if (totalRecords > 0)
            {
                if (totalRecords < batchSize)
                {
                    return 1;
                }

                
                return (int) Math.Ceiling(totalRecords / (double) batchSize);
            }

            return 0;
        }
        
    }
}
