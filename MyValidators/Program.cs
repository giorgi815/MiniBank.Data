using MyValidators.Attributes;

namespace MyValidators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User();

            user.Name = "Test";
            user.Age = null;
        }


        public class User
        {
            public string Name { get; set; }

            [MyRequired]
            public int? Age { get; set; }

        }

    }
}
