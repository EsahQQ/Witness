using System;
using UnityEngine;

namespace ThirdMinigame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject playerCamera;
        
        private Vector3 _playerStartPosition;
        
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _playerStartPosition = player.transform.position;
        }

        public void MoveToNextLevel()
        {
            Invoke(nameof(Move), 1);
        }

        // ИЗМЕНИТЬ ЛОГИКУ ПЕРЕХОДА
        
        private void Move()
        {
            player.transform.position = new Vector3(_playerStartPosition.x - 20, _playerStartPosition.y, _playerStartPosition.z);
            _playerStartPosition  = player.transform.position;
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x - 20, playerCamera.transform.position.y, playerCamera.transform.position.z);
        }
    }
}
