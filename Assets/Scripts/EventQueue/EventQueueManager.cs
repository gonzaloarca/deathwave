using System.Collections.Generic;
using Commands;
using UnityEngine;
using UI;

namespace EventQueue
{
    public class EventQueueManager : MonoBehaviour
    {
        public static EventQueueManager Instance;

        public Queue<ICommand> Events => _events;
        private Queue<ICommand> _events = new Queue<ICommand>();

        private void Awake()
        {
            if (Instance != null) Destroy(this);
            Instance = this;
        }

        private void Update()
        {
            if (PauseManager.GameIsPaused) return;

            while (!IsQueueEmpty())
                _events.Dequeue().Execute();

            _events.Clear();
        }

        public void AddCommand(ICommand command)
        {
            if (PauseManager.GameIsPaused) return;

            _events.Enqueue(command);
        }

        private bool IsQueueEmpty() => _events.Count <= 0;
    }
}