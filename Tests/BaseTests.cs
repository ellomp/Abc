using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests
{
    public class BaseTests
    {
        private const string NotTested = "<{0}> is not tested";
        private const string NotSpecified = "Class is not specified";
        private List<string> Members { get; set; }
        protected Type type;

        [TestMethod]

        public void IsTested()
        {
            if (type == null) Assert.Inconclusive(NotSpecified); //kui tüüpi pole, siis annab ta mulle teada, et sellist tüüpi polegi 
            var m = GetClass.Members(type, PublicBindingFlagsFor.DeclaredMembers);
            Members = m.Select(e => e.Name).ToList();
            RemoveTested();

            if (Members.Count == 0) return;
            Assert.Fail(NotTested, Members[0]);
        }

        private void RemoveTested()
        {
            var tests = GetType().GetMembers().Select(e => e.Name).ToList();

            for (var i = Members.Count; i > 0; i--)
            {
                var m = Members[i - 1] + "Test";
                var isTested = tests.Find(o => o == m);

                if (string.IsNullOrEmpty(isTested)) continue;
                Members.RemoveAt(i - 1);
            }
        }
        protected static void ArePropertyValuesEqualTest(object obj1, object obj2)
        {
            foreach (var property in obj1.GetType().GetProperties())
            {
                var name = property.Name;
                var p = obj2.GetType().GetProperty(name);
                Assert.IsNotNull(p, $"No property with name '{name}'found.");
                var expected = property.GetValue(obj1);
                var actual = p.GetValue(obj2);
                Assert.AreEqual(expected, actual, $"For property '{name}'.");
            }
        }
    }
}