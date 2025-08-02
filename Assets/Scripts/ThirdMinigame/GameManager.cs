using System;
using UnityEngine;
using System.Collections;

namespace ThirdMinigame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject playerCamera;
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private int coinsOnLevel = 24;
        
        private Vector3 _playerStartPosition;   
        private PlayerMovement _playerMovement;
        private Camera _cameraComponent;
        private int _levelsCompleted;
        
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _playerMovement =  player.GetComponent<PlayerMovement>();
            _cameraComponent = playerCamera.GetComponent<Camera>();
        }

        private void Start()
        {
            _playerStartPosition = player.transform.position;
        }

        public void CompleteLevel(int coinsCollected)
        {
            if (coinsCollected == coinsOnLevel)
            {
                _levelsCompleted++;
                if (_levelsCompleted == 5)
                {
                    Debug.Log("Game Win");
                }
                else
                {
                    StartCoroutine(TransitionToNextLevel());
                    coinsOnLevel--;
                }
            }
            else
            {
                Debug.Log($"Not enough coins ({coinsCollected}, {coinsOnLevel})");
            }
        }
        
        private IEnumerator TransitionToNextLevel()
        {
            _playerMovement.CanMove = false;
            
            float originalFarClip = _cameraComponent.farClipPlane;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                float newFarClip = Mathf.Lerp(originalFarClip, 0f, elapsedTime / fadeDuration);
                _cameraComponent.farClipPlane = newFarClip;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _cameraComponent.farClipPlane = 0f;
            
            player.transform.position = new Vector3(_playerStartPosition.x - 20, _playerStartPosition.y, _playerStartPosition.z);
            _playerStartPosition = player.transform.position;
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x - 20, playerCamera.transform.position.y, playerCamera.transform.position.z);
            
            elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                float newFarClip = Mathf.Lerp(0f, originalFarClip, elapsedTime / fadeDuration);
                _cameraComponent.farClipPlane = newFarClip;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _cameraComponent.farClipPlane = originalFarClip;
            
            _playerMovement.CanMove = true;
        }
    }
}
