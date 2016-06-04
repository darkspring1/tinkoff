using Dal;
using StructureMap;

namespace Api.Ioc
{    
    public class ApiRegistry : Registry
    {
        public ApiRegistry()
        {
            For<DataContext>()
                .Use<DataContext>()
                .Ctor<string>("connectionString")
                .Is("safetyButton");
        }
    }
}
