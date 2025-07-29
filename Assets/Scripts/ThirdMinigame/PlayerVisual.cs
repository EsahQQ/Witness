using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ThirdMinigame
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private GameObject skeletal;
        
        private PlayerMovement _playerMovement;
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Start()
        {
            _playerMovement.OnPlayerMove += RotatePlayer;
        }

        private void RotatePlayer(object sender, Vector3 moveDirection)
        {
            skeletal.transform.rotation = Quaternion.LookRotation(moveDirection) * Quaternion.Euler(0, -90, 0);
        }
    }
}
