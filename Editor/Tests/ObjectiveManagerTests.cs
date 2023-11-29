using NUnit.Framework;
using Core.Gameplay.Objectives;

namespace Editor.Tests.Core.Gameplay.Objectives
{
    [TestFixture]
    public class ObjectiveManagerTests
    {
        [Test]
        public void AddObjective()
        {
            var objectiveManager = new ObjectiveManager();
            var objective = new Objective("Test", "Progress: {0}/{1}", 10);

            objectiveManager.AddObjective(objective);

            Assert.Contains(objective, objectiveManager.Objectives);
        }
        
        [Test]
        public void AddObjective_TriggersOnObjectiveAdded()
        {
            bool triggered = false;
            var objectiveManager = new ObjectiveManager();
            var objective = new Objective("Test", "Progress: {0}/{1}", 10);
            objectiveManager.OnObjectiveAdded += (obj) => triggered = true;

            objectiveManager.AddObjective(objective);

            Assert.IsTrue(triggered);
        }

        [Test]
        public void AddProgress_AddsProgressToRelevantObjectives()
        {
            var objectiveManager = new ObjectiveManager();
            var objective = new Objective("Test", "Progress: {0}/{1}", 10);
            objectiveManager.AddObjective(objective);

            objectiveManager.AddProgress(objective.EventTrigger, 5);

            Assert.AreEqual(5, objective.CurrentValue);
        }

        [Test]
        public void AddProgress_DoesNothingForNonexistentEventTrigger()
        {
            var objectiveManager = new ObjectiveManager();
            var objective = new Objective("Test", "Progress: {0}/{1}", 10);
            objectiveManager.AddObjective(objective);

            objectiveManager.AddProgress("Nonexistent", 5);

            Assert.AreEqual(0, objective.CurrentValue);
        }
        
        [Test]
        public void AddProgress_AddsProgressToObjectiveWithNoTrigger()
        {
            var objectiveManager = new ObjectiveManager();
            var objective = new Objective("Progress: {0}/{1}", 10);
            objectiveManager.AddObjective(objective);

            objectiveManager.AddProgress("", 5);

            Assert.AreEqual(0, objective.CurrentValue);
        }
    }
}