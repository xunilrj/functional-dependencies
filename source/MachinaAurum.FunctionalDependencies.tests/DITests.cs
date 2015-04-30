using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachinaAurum.FunctionalDependencies.tests
{
    [TestClass]
    public class DITests
    {
        [TestMethod]
        public void MyTestMethod()
        {
            //Simulating real application

            var unity = new UnityContainer();

            unity.RegisterType<Func<int, HumanResource>>(new InjectionFactory(x =>
                {
                    var repository = new FakeRepository<HumanResource>();
                    return new Func<int, HumanResource>(repository.GetById);
                }));

            var sut = unity.Resolve<Sut>();

            // Simulating test

            var fakeDictionary = new Dictionary<int, HumanResource>();
            var seek = fakeDictionary.GetFunctions().Seek;

            var sut2 = new Sut(seek);
        }
    }

    public interface IAbstractRepository<T>
    {
        T GetById(int key);
    }

    public class FakeRepository<T> : IAbstractRepository<T>
    {

        public T GetById(int key)
        {
            return default(T);
        }
    }

    public class Sut
    {
        Func<int, HumanResource> Seek;

        public Sut(Func<int, HumanResource> seek)
        {
            Seek = seek;
        }

        public void SomeTransaction(int id)
        {
            var item = Seek(id);
        }
    }
}
