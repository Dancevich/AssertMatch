﻿using AssertMatch.Tests.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static AssertMatch.Match;

namespace AssertMatch.Tests
{
    [TestClass]
    public class Matcher_GetFailMessage_Tests
    {
        [TestMethod]
        public void Single_property_fail_check()
        {
            var person = new Person { Name = "Jack" };

            var msg = Expect(person).GetFailMessage(x => x.Name == "NotJack");

            Assert.AreEqual(@"
Match is failed:
    ✘ Name == ""NotJack"", Actual: ""Jack""
", msg);
        }

        [TestMethod]
        public void Single_property_fail_if_multiple_expected_values_were_specified()
        {
            var person = new Person { Name = "Jack", Age = 25};

            var msg = Expect(person).GetFailMessage(x => x.Name == "Jack" &&
                                                         x.Age == 26);

            Assert.AreEqual(@"
Match is failed:
    ✓ Name == ""Jack""
    ✘ Age == 26, Actual: 25
", msg);
        }

        [TestMethod]
        public void Navigation_property_fail_check()
        {
            var person = new Person { Pet = new Pet { Name = "Sharik" } };

            var msg = Expect(person).GetFailMessage(x => x.Pet.Name == "Jack");

            Assert.AreEqual(@"
Match is failed:
    ✘ Pet.Name == ""Jack"", Actual: ""Sharik""
", msg);
        }

        [TestMethod]
        public void Navigation_property_fail_when_nested_obj_is_null()
        {
            var person = new Person();

            var msg = Expect(person).GetFailMessage(x => x.Pet.Name == "Sharik");

            Assert.AreEqual(@"
Match is failed:
    ✘ Pet.Name == ""Sharik"", Actual: Pet is NULL
", msg);
        }

        [TestMethod]
        public void Single_property_fail_check_when_actual_is_null()
        {
            var person = new Person { Name = null };

            var msg = Expect(person).GetFailMessage(x => x.Name == "Jack");

            Assert.AreEqual(@"
Match is failed:
    ✘ Name == ""Jack"", Actual: NULL
", msg);
        }

        [TestMethod]
        public void Single_property_fail_check_when_expected_is_null()
        {
            var person = new Person { Name = "Jack" };

            var msg = Expect(person).GetFailMessage(x => x.Name == null);

            Assert.AreEqual(@"
Match is failed:
    ✘ Name == NULL, Actual: ""Jack""
", msg);
        }
    }
}