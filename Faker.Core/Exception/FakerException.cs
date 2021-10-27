namespace Faker.Core.Exception
{
    public class FakerException : System.Exception
    {
        public FakerException(string? message)
            : base(message)
        {
        }
        
        public FakerException(string? message, System.Exception? cause)
            : base(message, cause)
        {
        }
    }
}
