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
        private float _elapsedTime;
        private bool _isTransitioning;
        
        private void Update()
        {
            if (_isTransitioning) return;
            
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= timeToWin)
            {
                _isTransitioning = true;
                SceneTransitionManager.Instance.LoadScene("Scenes/Main");
            }
        }
    }
}
