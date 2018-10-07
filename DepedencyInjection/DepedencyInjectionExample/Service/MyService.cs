namespace DepedencyInjectionExample.Service
{
    public interface IHelloWorldService
    {
        string SaysHello();
    }
    public class HelloWorldService : IHelloWorldService
    {
        public string SaysHello()
        {
            return "Hello ";
        }
    }

}
