using System.Collections;
using System.Collections.Generic;
using Core.Gameplay.Objectives;
using Core.Gameplay.State;
using UnityEngine;

namespace UI.Game.Objectives
{
    public class ObjectivePanel : MonoBehaviour
    {
        [SerializeField]
        private ObjectiveDisplay _objectiveDisplayPrefab;
        
        [SerializeField]
        private Transform _objectiveDisplayParent;
        
        private readonly List<ObjectiveDisplay> _listDisplay = new();

        void Start()
        {
            StartCoroutine(DelayedInit());
        }
        
        private IEnumerator DelayedInit()
        {
            while (GameManager.Instance == null || GameManager.Instance.Objectives == null)
            {
                yield return null;
            }
            foreach (var objective in GameManager.Instance.Objectives.Objectives)
            {
                AddObjective(objective);
            }
            
            GameManager.Instance.Objectives.OnObjectiveAdded += AddObjective;
        }

        private void AddObjective(Objective obj)
        {
            var display = Instantiate(_objectiveDisplayPrefab, _objectiveDisplayParent);
            display.Init(obj);
            _listDisplay.Add(display);
        }

        public void Reset()
        {
            for (var i = _listDisplay.Count - 1; i >= 0; i--)
            {
                Destroy(_listDisplay[i].gameObject);
            }
            _listDisplay.Clear();
        }
    }
}