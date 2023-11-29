using Core.Gameplay.Objectives;
using NUnit.Framework;

namespace Editor.Tests.Core.Gameplay.Objectives
{
    [TestFixture]
    public class ObjectiveTests
    {
        [Test]
        public void Objective_InitializesCorrectly()
        {
            var objective = new Objective("trigger", "Progress: {0}/{1}", 10);

            Assert.AreEqual(0, objective.CurrentValue);
            Assert.AreEqual(10, objective.MaxValue);
            Assert.IsFalse(objective.IsComplete);
            Assert.AreEqual("Progress: 0/10", objective.GetStatusText());
        }

        [Test]
        public void AddProgress_IncreasesCurrentValue()
        {
            var objective = new Objective("trigger", "Progress: {0}/{1}", 10);
            objective.AddProgress(5);

            Assert.AreEqual(5, objective.CurrentValue);
            Assert.IsFalse(objective.IsComplete);
            Assert.AreEqual("Progress: 5/10", objective.GetStatusText());
        }

        [Test]
        public void AddProgress_DoesNotExceedMaxValue()
        {
            var objective = new Objective("trigger", "Progress: {0}/{1}", 10);
            objective.AddProgress(15);

            Assert.AreEqual(10, objective.CurrentValue);
            Assert.IsTrue(objective.IsComplete);
            Assert.AreEqual("Progress: 10/10", objective.GetStatusText());
        }

        [Test]
        public void OnComplete_TriggeredWhenCompleted()
        {
            bool triggered = false;
            var objective = new Objective("trigger", "Progress: {0}/{1}", 10);
            objective.OnComplete += () => triggered = true;

            objective.AddProgress(10);

            Assert.IsTrue(triggered);
        }

        [Test]
        public void AddProgress_DoesNothingWhenComplete()
        {
            var objective = new Objective("trigger", "Progress: {0}/{1}", 10);
            objective.AddProgress(10); // Complete the objective
            objective.AddProgress(5);  // Try to add more progress

            Assert.AreEqual(10, objective.CurrentValue);
            Assert.IsTrue(objective.IsComplete);
            Assert.AreEqual("Progress: 10/10", objective.GetStatusText());
        }
    }
}
