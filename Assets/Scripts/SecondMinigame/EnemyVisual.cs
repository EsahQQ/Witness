using System;
using UnityEngine;

namespace SecondMinigame
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyNav))]
    public class EnemyVisual : MonoBehaviour
    {
        private static readonly int Running = Animator.StringToHash("Running");
        private static readonly int Idle = Animator.StringToHash("Idle");
        [SerializeField] private GameObject skeletal;
        private Animator _animator;
        private EnemyNav _enemyNav;
        private bool _isIdle;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _enemyNav = GetComponent<EnemyNav>();
        }

        private void Start()
        {
            _enemyNav.OnStateSwitch += OnStateSwitch;
            _enemyNav.OnEnemyTurn += OnEnemyTurn;
        }

        private void OnDestroy()
        {
            _enemyNav.OnStateSwitch -= OnStateSwitch;
            _enemyNav.OnEnemyTurn -= OnEnemyTurn;
        }

        private void OnEnemyTurn(object sender, EventArgs e)
        {
            skeletal.transform.Rotate(0f, 180f, 0f);
        }

        private void OnStateSwitch(object sender, EnemyNav.State e)
        {
            switch (e)
            {
                case EnemyNav.State.Chasing:
                    _animator.SetBool(Running, true);
                    break;
                case EnemyNav.State.Roaming:
                    _animator.SetBool(Running, false);
                    break;
                case EnemyNav.State.Idle:
                    _isIdle = !_isIdle;
                    _animator.SetBool(Idle, _isIdle);
                    break;
            }
        }
    }
}
