using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FirstMinigame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Camera cameraComponent;
        [SerializeField] private float timeToWin = 60f;
        [SerializeField] private Slider slider;
        private float _elapsedTime;
        private bool _isTransitioning;

        private void Start()
        {
            PlayerController.Instance.OnPlayerDeath += ReloadScene;
        }

        private void Update()
        {
            if (_isTransitioning) return;
            
            _elapsedTime += Time.deltaTime;
            slider.value = _elapsedTime;
            if (_elapsedTime >= timeToWin)
            {
                _isTransitioning = true;
                SceneTransitionManager.Instance.LoadScene("Scenes/1st2ndText");
            }
        }

        private void OnDestroy()
        {
            PlayerController.Instance.OnPlayerDeath -= ReloadScene;
        }

        private void ReloadScene(object sender, EventArgs e)
        {
            SceneTransitionManager.Instance.LoadScene("Scenes/FirstMinigame");
        }
    }
}
