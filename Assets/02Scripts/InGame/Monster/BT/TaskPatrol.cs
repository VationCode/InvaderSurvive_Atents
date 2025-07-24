//¼øÂû
using UnityEngine;
namespace BehaviorTree
{
    public class TaskPatrol : Node
    {
        private Transform ctransform;
        private Animator animator;
        private Transform[] waypoints;

        private int currentWaypointIndex = 0;
        
        private float waitTime = 1f;
        private float waitCounter = 0f;
        private bool waiting = false;

       public TaskPatrol(Transform transform, Transform[] _waypoints)
        {
            ctransform = transform;
            animator = transform.GetComponent<Animator>();
            waypoints = _waypoints;
        }

        public override ENodeState Evaluate()
        {
            if(waiting)
            {
                waitCounter += Time.deltaTime;
                if (waitCounter >= waitTime)
                {
                    waiting = false;
                    animator.SetBool("IsWalk", true);
                }
            }
            else
            {
                Transform wp = waypoints[currentWaypointIndex];
                if(Vector3.Distance(ctransform.position, wp.position) < 0.01f)
                {
                    ctransform.position = wp.position;
                    waitCounter = 0f;
                    waiting = true;

                    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                    animator.SetBool("IsWalk", false);
                }
                else
                {
                    ctransform.position = Vector3.MoveTowards(ctransform.position, wp.position, GuardBT.speed * Time.deltaTime);
                    ctransform.LookAt(wp.position);
                }
            }
            eState = ENodeState.RUNNING;
            return eState;
        }
    }
}
