using NUnit.Framework;
using System;

namespace WeInvest.Domain.Tests {
    public class UtilityTests {
    
        [Test]
        public void GetProperty_WithValueNotBeingNull_ShouldReturnValueOfProperty() {
            var dynamicObject = new {
                Id = 5,
                Name = "Jäger",
                IsHuman = true
            };

            int id = dynamicObject.GetProperty<int>("Id");
            string name = dynamicObject.GetProperty<string>("Name");
            bool isHuman = dynamicObject.GetProperty<bool>("IsHuman");

            Assert.That(id, Is.EqualTo(5));
            Assert.That(name, Is.EqualTo("Jäger"));
            Assert.That(isHuman, Is.True);
        }

        [Test]
        public void GetProperty_WithPropertyOrObjectBeingNull_ShouldReturnDefault() {
            var obj1 = new {
                Name = "Test"
            };
            object obj2 = null;

            var result1 = obj1.GetProperty<string>("name");
            var result2 = obj2.GetProperty<int>("name");

            Assert.That(result1, Is.Null);
            Assert.That(result2, Is.EqualTo(default(int)));
        }

        [Test]
        public void ForceGetProperty_WithValueNotBeingNull_ShouldReturnValueOfProperty() {
            var dynamicObject = new {
                Id = 5,
                Name = "Jäger",
                IsHuman = true
            };

            int id = dynamicObject.ForceGetProperty<int>("Id");
            string name = dynamicObject.ForceGetProperty<string>("Name");
            bool isHuman = dynamicObject.ForceGetProperty<bool>("IsHuman");

            Assert.That(id, Is.EqualTo(5));
            Assert.That(name, Is.EqualTo("Jäger"));
            Assert.That(isHuman, Is.True);
        }

        [Test]
        public void ForceGetProperty_WithPropertyOrObjectBeingNull_ShouldReturnNullOrThrowException() {
            var dynamicObject = new {
                Name = "Test"
            };

            Assert.That(dynamicObject.ForceGetProperty<string>("name"), Is.Null);
            Assert.Throws<NullReferenceException>(() => dynamicObject.ForceGetProperty<int>("name"));
        }

        [Test]
        public void HasProperty_Always_ShouldReturnBool() {
            var dynamicObject = new {
                Name = "Test"
            };

            Assert.That(dynamicObject.HasProperty("Name"), Is.True);
            Assert.That(dynamicObject.HasProperty("Id"), Is.False);
        }

    }
}
