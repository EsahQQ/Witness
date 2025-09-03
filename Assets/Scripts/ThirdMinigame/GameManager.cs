using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ThirdMinigame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject playerCamera;
        
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private float levelTransitionOffset = 20f;
        [SerializeField] private int totalLevels = 5;
        
        private Vector3 _playerStartPosition;   
        private PlayerMovement _playerMovement;
        private Camera _cameraComponent;

        private int _coinsOnLevel = 24;
        private int _coinsCollected;
        private int _levelsCompleted;
        private bool _isTransitioning;
        
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
            LevelExit.OnPlayerExit += HandlePlayerExit;
            Coin.OnCoinCollected += HandleCoinCollected;
        }

        private void OnDestroy()
        {
            LevelExit.OnPlayerExit -= HandlePlayerExit;
            Coin.OnCoinCollected -= HandleCoinCollected;
        }

        private void HandleCoinCollected(object sender, EventArgs e)
        {
            _coinsCollected++;
        }

        private void HandlePlayerExit(object sender, EventArgs e)
        {
            if (_coinsCollected == _coinsOnLevel)
            {
                CompleteLevel();
            }
            else
            {
                Debug.Log($"Not enough coins ({_coinsCollected}, {_coinsOnLevel})");
                RestartMinigame();
            }
        }

        private void CompleteLevel()
        {
            if (_isTransitioning) return;
            _levelsCompleted++;
            if (_levelsCompleted == totalLevels)
            {
                _isTransitioning = true;
                SceneTransitionManager.Instance.LoadScene("Scenes/EndCutscene");
            }
            else
            {
                _coinsOnLevel--;
                _coinsCollected = 0;
                StartCoroutine(TransitionToNextLevel());
            }
        }
        
        private IEnumerator TransitionToNextLevel()
        {
            _playerMovement.CanMove = false;
            yield return new WaitForSeconds(1);
            
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
            
            player.transform.position = new Vector3(_playerStartPosition.x - levelTransitionOffset, _playerStartPosition.y, _playerStartPosition.z);
            _playerStartPosition = player.transform.position;
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x - levelTransitionOffset, playerCamera.transform.position.y, playerCamera.transform.position.z);
            
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

        private void RestartMinigame()
        {
            _coinsCollected = 0;
            _coinsOnLevel = 24;
            SceneTransitionManager.Instance.LoadScene("Scenes/ThirdMinigame");
        }
    }
}
