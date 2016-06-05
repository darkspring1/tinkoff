using Business;
using Business.Dal;
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
                .Is("bitly");

            For<RandomStringGenerator>()
                .Use<RandomStringGenerator>()
                .Singleton();

            For(typeof(IRepository<>)).Use(typeof(EfRepository<>));
        }
    }
}
