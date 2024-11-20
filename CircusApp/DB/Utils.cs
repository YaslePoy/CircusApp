using System.Collections.Generic;
using System.Linq;

namespace CircusApp.DB
{
    public static class Utils
    {
        public static List<T> Flat<T>(this IEnumerable<IEnumerable<T>> t)
        {
            var total = t.Sum(i => i.Count());
            var ls = new List<T>(total);
            foreach (var i in t)
                ls.AddRange(i.ToList());
            return ls;
        }
    }

    public partial class Specialization
    {
        public override string ToString()
        {
            return name;
        }
    }
    public partial class Skill
    {
        public override string ToString()
        {
            return name;
        }
    }
    
    public partial class User
    {
        public double Fill { get; set; }
        public override string ToString()
        {
            return $"{surname} {name}";
        }
    }
    
    public partial class Performance
    {
        public override string ToString()
        {
            return name;
        }
    }
    
    public partial class Event
    {
        public override string ToString()
        {
            return name;
        }
    }
}