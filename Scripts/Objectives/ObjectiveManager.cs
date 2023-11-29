using System;
using System.Collections.Generic;

namespace Core.Gameplay.Objectives
{
    public class ObjectiveManager
    {
        public Action<Objective> OnObjectiveAdded;

        public List<Objective> Objectives { get; } = new();
        private readonly Dictionary<string, List<Objective>> _objectiveMap = new();
        
        // Adds an objective to the objective manager. 
        // If the objective has an EventTrigger, it's progress will be incremented by AddProgress when the event is triggered.
        // Multiple objectives can have the same EventTrigger (i.e. MobKilled, ItemCollected, etc)
        public void AddObjective(Objective objective)
        {
            Objectives.Add(objective);
            if (!string.IsNullOrEmpty(objective.EventTrigger))
            {
                if (!_objectiveMap.ContainsKey(objective.EventTrigger))
                {
                    _objectiveMap.Add(objective.EventTrigger, new List<Objective>());
                }

                _objectiveMap[objective.EventTrigger].Add(objective);
            }

            OnObjectiveAdded?.Invoke(objective);
        }
        
        public void AddProgress(string eventTrigger, int value)
        {
            if (!_objectiveMap.ContainsKey(eventTrigger)) return;
            foreach (var objective in _objectiveMap[eventTrigger])
            {
                objective.AddProgress(value);
            }
        }
    }
}