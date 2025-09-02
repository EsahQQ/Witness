using System;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace SecondMinigame
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyNav : MonoBehaviour
    {
        [SerializeField] private float checkTimer = 5f;
        [SerializeField] private float minMoveDistance = 10f;
        [SerializeField] private float maxMoveDistance = 20f;
        [SerializeField] private float chasingDistance = 5f;
        private NavMeshAgent _agent;
        private PlayerController _player;
        private float _elapsedTime;
        private State _currentState;
        private bool _isTurnedRight = true;
        private bool _isIdle;
        
        public static EnemyNav Instance { get; private set; }
        public event EventHandler<State> OnStateSwitch;
        public event EventHandler OnEnemyTurn;
        public event EventHandler OnPlayerDeath;
        public bool IsChasing;

        public enum State
        {
            Roaming,
            Chasing,
            Idle
        }
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            Instance = this;
        }

        private void Start()
        {
            _elapsedTime = checkTimer;
            _player = PlayerController.Instance;
        }
        
        private void Update()
        {
            if (_agent.isStopped)
                return;
            CheckCurrentState();

            switch (_currentState)
            {
                case State.Roaming:
                    Roaming();
                    break;
                case State.Chasing:
                    Chasing();
                    break;
            }
            
            IsChasing = (_currentState == State.Chasing);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }

        private void CheckCurrentState()
        {
            if (Vector3.Distance(transform.position, _player.transform.position) < chasingDistance  && !_player.IsPlayerHide)
            {
                if (_currentState != State.Chasing)
                {
                    _currentState = State.Chasing;
                    OnStateSwitch?.Invoke(this, _currentState);
                }
            }
            else if (_currentState != State.Roaming)
            {
                _currentState = State.Roaming;
                OnStateSwitch?.Invoke(this, _currentState);
            }
        }

        private void Roaming()
        {
            if (_elapsedTime >= checkTimer)
            {
                SetDestination();
                _elapsedTime = 0;
            }
            //Debug.Log(_agent.remainingDistance);
            if (_agent.remainingDistance < 1 && _agent.remainingDistance != 0)
            {
                if (!_isIdle)
                {
                    _isIdle = true;
                    OnStateSwitch?.Invoke(this, State.Idle);
                }
            }
            else
            {
                if (_isIdle  && _agent.remainingDistance != 0)
                {
                    _isIdle = false;
                    OnStateSwitch?.Invoke(this, State.Idle);
                }
            }

            _elapsedTime += Time.deltaTime;
        }
        
        private void SetDestination()
        {
            for (int i = 0; i < 100; i++)
            {
                var distance = Random.Range(minMoveDistance, maxMoveDistance);
                Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * distance;
                randomDir += transform.position;

                if (!NavMesh.SamplePosition(randomDir, out NavMeshHit navHit, 5, NavMesh.AllAreas)) continue;
                NavMeshPath path = new NavMeshPath();
                _agent.CalculatePath(navHit.position, path);
                if (path.status != NavMeshPathStatus.PathComplete) continue;
                _agent.SetDestination(navHit.position);
                
                CheckEnemyTurn(navHit.position.x);
                    
                return;
            }
        }

        private void Chasing()
        {
            _agent.destination = _player.transform.position;
            CheckEnemyTurn(_player.transform.position.x);
        }

        private void CheckEnemyTurn(float xPosition)
        {
            if ((xPosition < transform.position.x && _isTurnedRight) || (xPosition > transform.position.x && !_isTurnedRight))
            {
                OnEnemyTurn?.Invoke(this, EventArgs.Empty);
                _isTurnedRight =  !_isTurnedRight;
            }
        }
    }
}
