using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public MovingStatus MoveStatus { get; set; }

    private void Awake()
    {
        MoveStatus = new MovingStatus(agent);
    }

    public void Move(EnvironmentObject.ClickData data, System.Action onStart = null, System.Action onStop = null)
    {
        MoveStatus.Move(data, onStart, onStop);
    }

    private void Update()
    {
        MoveStatus.Update();
    }

    public class MovingStatus
    {
        public NavMeshAgent agent;

        public MovingStatus(NavMeshAgent agent)
        {
            this.agent = agent;
        }

        public bool Moving { get; set; }
        public Vector3 Target { get; set; }
        public System.Action<Vector3> OnStartMove { get; set; }
        public System.Action OnStopMove { get; set; }

        private System.Action singleStop { get; set; }

        public void Move(EnvironmentObject.ClickData data, System.Action onStart = null, System.Action onStop = null)
        {
            if (!Moving)
            {
                onStart?.Invoke();
            }
            Target = data.position;
            OnStartMove?.Invoke(data.position);
            singleStop = onStop;
            agent.SetDestination(data.position);
            Moving = true;
        }

        public void Update()
        {
            if (!Moving || agent.pathPending || agent.remainingDistance > agent.stoppingDistance) return;
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) Stop();
        }

        public void Stop()
        {
            Moving = false;
            OnStopMove?.Invoke();
            singleStop?.Invoke();
            singleStop = null;
        }
    }
}