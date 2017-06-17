using HealthTracker.Data;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using System;
using System.IO;

namespace HealthTracker.Test
{
    [TestFixture]
    public class PersonTest
    {
        private IRepository<Person> persons;

        [SetUp]
        public void CreateSchema()
        {
            var testRoot = TestContext.CurrentContext.WorkDirectory;
            Directory.SetCurrentDirectory(testRoot);
            Console.WriteLine(Environment.CurrentDirectory);
            var schema = new SchemaExport(Database.Config);
            schema.Execute(true, true, false);
            persons = new PersonRepository();
        }

        [Test]
        public void CanSavePerson()
        {
            persons.Save(new Person());
            Assert.AreEqual(1, persons.RowCount());
        }

        [Test]
        public void CanGetPerson()
        {
            var person = new Person();
            persons.Save(person);
            Assert.AreEqual(1, persons.RowCount());
            person = persons.Get(person.Id);
            Assert.IsNotNull(person);
        }

        [Test]
        public void CanUpdatePerson()
        {
            var person = new Person();
            persons.Save(person);
            Assert.AreEqual(1, persons.RowCount());
            person = persons.Get(person.Id);
            person.FirstName = "Test";
            persons.Update(person);
            Assert.AreEqual(1, persons.RowCount());
            Assert.AreEqual("Test", persons.Get(person.Id).FirstName);
        }

        [Test]
        public void CanDeletePerson()
        {
            var person = new Person();
            persons.Save(person);
            Assert.AreEqual(1, persons.RowCount());
            persons.Delete(person);
            Assert.AreEqual(0, persons.RowCount());
        }
    }
}