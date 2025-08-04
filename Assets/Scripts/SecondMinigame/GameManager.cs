using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace SecondMinigame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Camera cameraComponent;
            
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        
        public IEnumerator MoveToNextScene()
        {
            float elapsedTime = 0;
            while (elapsedTime < 1)
            {
                float newFOV = Mathf.Lerp(60f, 0.01f, elapsedTime / 1);
                cameraComponent.fieldOfView = newFOV;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            cameraComponent.fieldOfView = 0.01f;
            
            SceneManager.LoadScene("Scenes/ThirdMinigame");
        }
    }
}
