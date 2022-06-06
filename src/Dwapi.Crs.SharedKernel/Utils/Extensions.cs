using System;
using System.Collections.Generic;
using System.Linq;

namespace Dwapi.Crs.SharedKernel.Utils
{
   public static class Extensions
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
        
        public static string ToDateFormat(this DateTime? value)
        {
            if (value.HasValue)
                return value.Value.ToString("yyyy-MM-dd");
            
            return string.Empty;
        }
        
        public static string ToStringFormat(this string guid)
        {
            if (null == guid)
                return "";
            if (string.IsNullOrWhiteSpace(guid))
                return "";
            
            return string.Empty;
        }
        
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength); 
        }
        
        public static string Transfrom(this string value,string category="Marital")
        {
            
            if (category == "Marital")
            {
                var opts = new string[]
                {
                    "Single", "Married-monogamous", "Married-polygamous", "Separated", "Divorced", "Widowed",
                    "Cohabiting","Unknown"
                };
                
                if (null == value)
                    return "Unknown";
                if (string.IsNullOrWhiteSpace(value))
                    return "Unknown";
                if (value == "Unkown")
                    return "Unknown";
                if (!value.IsInOptions(opts.ToList()))
                    return "Unknown";
                return value;
            }
            
            if (null == value)
                return "";
            if (string.IsNullOrWhiteSpace(value))
                return "";
            
            return string.Empty;
        }
        
        public static bool IsInOptions(this string value, List<string> list)
        {
            var nmlist = new List<string>();
            
            foreach (var i in list) 
                nmlist.Add(i.ToLower());

            return nmlist.Any(x => nmlist.Contains(value.Trim().ToLower()));
        }
    }
}
