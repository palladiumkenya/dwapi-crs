using System;

namespace Dwapi.Crs.SharedKernel.Utils
{
   public static class Extentions
    {
        /// <summary>
        /// Determines if a nullable Guid (Guid?) is null or Guid.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this Guid? guid)
        {
            return !guid.HasValue || guid.Value == Guid.Empty;
        }

        /// <summary>
        /// Determines if Guid is Guid.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static string HasToEndWith(this string value, string end)
        {
            return value.EndsWith(end) ? value : $"{value}{end}";
        }
        public static bool IsSameAs(this string value, string end)
        {
            if((null!=value)&&(null!=end))
                return value.ToLower().Trim() == end.ToLower().Trim();
            return false;
        }
        
        public static string ToDateFormat(this DateTime? guid)
        {
            if (guid.HasValue)
                return guid.Value.ToString("yyyy-MM-dd");
            
            return string.Empty;
        }
        
        public static string ToStringFormat(this string? guid)
        {
            if (null == guid)
                return "";
            if (string.IsNullOrWhiteSpace(guid))
                return "";
            
            return string.Empty;
        }
    }
}
