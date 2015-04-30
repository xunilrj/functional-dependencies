using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MachinaAurum.FunctionalDependencies.tests
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void MustBePossibleToGetFunctionsFromCollections()
        {
            var list = new List<int>();
            var functions = list.GetFunctions();

            var add = functions.Add;

            add(0);
            add(1);

            Assert.AreEqual(2, list.Count);

            var remove = functions.Remove;

            remove(0);

            Assert.AreEqual(1, list.Count);

            remove(1);

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void MustBePossibleToGetSeekFunctionsFromDictionary()
        {
            var dictionary = new Dictionary<int, string>();
            dictionary.Add(0, "value1");

            var functions = dictionary.GetFunctions();

            var value = functions.Seek(0);

            Assert.AreEqual("value1", value);
        }

        [TestMethod]
        public void MustBePossibleToCombineFunctions()
        {
            var originalHumanResource = new HumanResource() { Name = "NAME" };
            var dictionary = new Dictionary<int, HumanResource>();
            dictionary.Add(0, originalHumanResource);

            var seek = dictionary.GetFunctions().Seek;
            var toXml = Serializers.ToXml;
            var fromXml = Serializers.FromXml<HumanResource>();

            var serializedHumanResource = Functions.Combine(seek, toXml, fromXml);
            var humanResource = serializedHumanResource(0);

            Assert.AreEqual(originalHumanResource.Name, humanResource.Name);
        }
    }

    public class HumanResource
    {
        public string Name { get; set; }
    }
}
