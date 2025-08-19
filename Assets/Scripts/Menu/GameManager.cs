using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

namespace Menu
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Image blackImage;
        public void StartGame()
        {
            blackImage.gameObject.SetActive(true);
            StartCoroutine(TransitionToGame());
        }

        private IEnumerator TransitionToGame()
        {
            float elapsedTime = 0;
            while (elapsedTime < 1)
            {
                float newAlpha = Mathf.Lerp(0, 1, elapsedTime / 1);
                blackImage.color = new Color(0, 0, 0, newAlpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            blackImage.color = new Color(0, 0, 0, 1);
            
            SceneManager.LoadScene("Scenes/StartCutscene");
        }
    }
}
