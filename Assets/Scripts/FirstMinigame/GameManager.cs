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
        [SerializeField] private Image blackImage;
        private float _elapsedTime = 0f;

        private void Start()
        {
            blackImage.color = new Color(0, 0, 0, 1);
            StartCoroutine(OpenScene());
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= timeToWin)
                StartCoroutine(MoveToNextScene());
        }

        private IEnumerator MoveToNextScene()
        {
            blackImage.gameObject.SetActive(true);
            float elapsedTime = 0;
            while (elapsedTime < 3)
            {
                float newAlpha = Mathf.Lerp(0, 1, elapsedTime / 3);
                blackImage.color = new Color(0, 0, 0, newAlpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            blackImage.color = new Color(0, 0, 0, 1);
            blackImage.gameObject.SetActive(false);
            
            SceneManager.LoadScene("Scenes/Main");
        }
        
        private IEnumerator OpenScene()
        {
            blackImage.gameObject.SetActive(true);
            float elapsedTime = 0;
            while (elapsedTime < 3)
            {
                float newAlpha = Mathf.Lerp(1, 0, elapsedTime / 3);
                blackImage.color = new Color(0, 0, 0, newAlpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            blackImage.color = new Color(0, 0, 0, 0);
            blackImage.gameObject.SetActive(false);
        }
    }
}
