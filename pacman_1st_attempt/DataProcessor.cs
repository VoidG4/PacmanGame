using System;
using System.Collections.Generic;
using System.Linq;

namespace PrReviewApp
{
    public class data_processor 
    {
        public List<string> ProcessNames(List<string> Names)
        {
            List<string> result = new List<string>();

            try
            {
                var filtered = Names.Where(n => n.StartsWith("A"));
                var count = filtered.Count();

                if (count > 0)
                {
                    foreach (var name in filtered)
                    {
                        result.Add(name.ToLower());
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }
    }
}
