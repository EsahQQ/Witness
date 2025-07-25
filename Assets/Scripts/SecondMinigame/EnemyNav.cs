using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.AI;

namespace SecondMinigame
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyNav : MonoBehaviour
    {
        [SerializeField] private float checkTimer = 5f;
        [SerializeField] private float minMoveDistance = 10f;
        [SerializeField] private float maxMoveDistance = 20f;
        private NavMeshAgent _agent;
        private float _elapsedTime;
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_elapsedTime >= checkTimer)
            {
                SetDestination();
                _elapsedTime = 0;
            }

            _elapsedTime += Time.deltaTime;
        }

        private void SetDestination()
        {
            for (int i = 0; i < 100; i++)
            {
                Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(minMoveDistance, maxMoveDistance);
                randomDir += transform.position;

                if (NavMesh.SamplePosition(randomDir, out NavMeshHit navHit, 5, NavMesh.AllAreas))
                {
                    NavMeshPath path = new NavMeshPath();
                    _agent.CalculatePath(navHit.position, path);
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        _agent.SetDestination(navHit.position);
                        return;
                    }
                        
                }
            }
            
            
            
        }
    }
}
