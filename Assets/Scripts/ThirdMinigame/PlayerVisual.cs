using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ThirdMinigame
{
    public class PlayerVisual : MonoBehaviour
    {
        private static readonly int Running = Animator.StringToHash("Running");
        [SerializeField] private GameObject skeletal;
        
        private Animator _animator;
        private PlayerMovement _playerMovement;
        
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _playerMovement.OnPlayerMove += MovePlayerVisual;
            _playerMovement.OnPlayerStop += StopPlayerVisual;
        }

        private void MovePlayerVisual(object sender, Vector3 moveDirection)
        {
            skeletal.transform.rotation = Quaternion.LookRotation(moveDirection) * Quaternion.Euler(0, -90, 0);
            
            _animator.SetBool(Running, true);
        }
        
        private void StopPlayerVisual(object sender, EventArgs e)
        {
            _animator.SetBool(Running, false);
        }
    }
}
