using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FirstMinigame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Camera cameraComponent;
        [SerializeField] private float timeToWin = 60f;
        
        private float _elapsedTime = 0f;
        
        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= timeToWin)
                StartCoroutine(MoveToNextScene());
        }

        private IEnumerator MoveToNextScene()
        {
            float elapsedTime = 0;
            while (elapsedTime < 1)
            {
                float newOrthographicSize = Mathf.Lerp(4f, 0.01f, elapsedTime / 1);
                cameraComponent.orthographicSize = newOrthographicSize;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            cameraComponent.orthographicSize = 0.01f;
            
            SceneManager.LoadScene("Scenes/Main");
        }
    }
}
