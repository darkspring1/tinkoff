using System.Linq;
using Business.Dal;
using Business.Entities;
using StructureMap;
using Test.Dal;


namespace Test.Ioc
{

    public class TestRegistry : Registry
    {
        public TestRegistry()
        {
            For<IRepository<Url>>().Use<TestRepository<Url>>();
        }
    }
}
