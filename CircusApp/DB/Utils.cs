namespace CircusApp.DB
{
    public static class Utils
    {
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
        public override string ToString()
        {
            return $"{surname} {name}";
        }
    }
}