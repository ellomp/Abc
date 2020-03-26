using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests
{

    public class AssemblyTests //kontrollib, kas kõik klassid on testitud
    {

        private static string IsNotTested => "<{0}> is not tested";
        private static string NoClassesInAssembly =>
            "No classes found in assembly {0}";
        private static string NoClassesInNamespace =>
            "No classes found in namespace {0}";
        private static string TestAssembly => "Abc.Tests";
        private static string Assembly => "Abc";
        private static char GenericsChar => '`';
        private static char InternalClass => '+';
        private static string DisplayClass => "<>";
        private static string Shell32 => "Shell32.";
        private List<string> _list;

        [TestInitialize]
        public void CreateList()
        {
            _list = new List<string>();
        }

        protected virtual string Namespace(string name)
        {
            return $"{Assembly}.{name}";
        }

        protected void IsAllTested(string assemblyName,
            string namespaceName = null)
        {
            var l = GetAssemblyClasses(assemblyName);
            RemoveInterfaces(l);
            _list = ToClassNamesList(l);
            RemoveNotInNamespace(namespaceName);
            RemoveSurrogateClasses();
            RemoveTested();

            if (_list.Count == 0) return;

            Report(IsNotTested, _list[0]);
        }

        private static void Report(string message, params object[] parameters)
        {
            Assert.Fail(message, parameters);
        }

        private static List<Type> GetAssemblyClasses(string assemblyName)
        {
            var l = GetSolution.TypesForAssembly(assemblyName);
            if (l.Count == 0) Report(NoClassesInAssembly, assemblyName);

            return l;
        }

        private static void RemoveInterfaces(IList<Type> types)
        {
            for (var i = types.Count; i > 0; i--)
            {
                var e = types[i - 1];

                if (!e.IsInterface) continue;

                types.Remove(e);
            }
        }

        private static List<string> ToClassNamesList(List<Type> l)
        {
            return l.Select(o => o.FullName).ToList();
        }

        private void RemoveNotInNamespace(string namespaceName)
        {
            if (string.IsNullOrEmpty(namespaceName)) return;

            _list.RemoveAll(o => !o.StartsWith(namespaceName + '.'));

            if (_list.Count > 0) return;

            Report(NoClassesInNamespace, namespaceName);
        }

        private void RemoveSurrogateClasses()
        {
            _list.RemoveAll(o => o.Contains(Shell32));
            _list.RemoveAll(o => o.Contains(InternalClass));
            _list.RemoveAll(o => o.Contains(DisplayClass));
            _list.RemoveAll(o => o.Contains("<"));
            _list.RemoveAll(o => o.Contains(">"));
            _list.RemoveAll(o => o.Contains("Migrations"));
        }

        private void RemoveTested()
        {
            var tests = GetTestClasses();

            for (var i = _list.Count; i > 0; i--)
            {
                var className = _list[i - 1];
                var testName = ToTestName(className);
                var t = tests.Find(o => o.EndsWith(testName));

                if (ReferenceEquals(null, t)) continue;

                _list.RemoveAt(i - 1);
            }
        }

        private List<string> GetTestClasses()
        {
            var l = new List<string>();
            var tests = GetSolution.TypeNamesForAssembly(TestAssembly);

            foreach (var t in tests)
            {
                var n = RemoveGenericsChars(t);
                l.Add(n);
            }

            return l;
        }

        private static string ToTestName(string className)
        {
            className = RemoveAssemblyName(className);
            className = RemoveGenericsChars(className);

            return className + "Tests";
        }

        private static string RemoveGenericsChars(string className)
        {
            var idx = className.IndexOf(GenericsChar);
            if (idx > 0) className = className.Substring(0, idx);

            return className;
        }

        private static string RemoveAssemblyName(string className)
        {
            return className.Substring(Assembly.Length);
        }

    }

}
