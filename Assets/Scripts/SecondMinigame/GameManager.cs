using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SecondMinigame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Camera cameraComponent;
        [SerializeField] private Image blackImage;
        
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            blackImage.color = new Color(0, 0, 0, 1);
            StartCoroutine(OpenScene());
        }

        public void MinigameWin()
        {
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
            
            SceneManager.LoadScene("Scenes/ThirdMinigame");
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
