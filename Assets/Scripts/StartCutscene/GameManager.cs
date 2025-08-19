using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace StartCutscene
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Image blackImage;

        private void Start()
        {
            blackImage.color = new Color(0, 0, 0, 1);
            StartCoroutine(OpenScene());
        }

        private IEnumerator OpenScene()
        {
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

        public void NextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
