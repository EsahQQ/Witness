using System;
using UnityEngine;

namespace SecondMinigame
{
    [RequireComponent(typeof(Animator))]
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private GameObject skeletal;
        private static readonly int Running = Animator.StringToHash("Running");
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            PlayerMovement.Instance.OnPlayerTurn += OnPlayerTurn;
        }

        private void Update()
        {
            _animator.SetBool(Running, PlayerMovement.Instance.IsRunning);
        }
        
        private void OnPlayerTurn(object sender, EventArgs e)
        {
            skeletal.transform.Rotate(0f, 180f, 0f);
        }
    }
}
