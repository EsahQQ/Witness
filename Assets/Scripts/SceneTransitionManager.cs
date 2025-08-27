using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private Image blackImage;
    [SerializeField] private float fadeDuration = 3f;
    
    public static  SceneTransitionManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn());
    }
    
    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        blackImage.gameObject.SetActive(true);
        
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            blackImage.color = new Color(0, 0, 0, newAlpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        blackImage.color = new Color(0, 0, 0, 1);
        
        SceneManager.LoadScene(sceneName);
    }
    
    private IEnumerator FadeIn()
    {
        blackImage.gameObject.SetActive(true);
        
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            blackImage.color = new Color(0, 0, 0, newAlpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        blackImage.color = new Color(0, 0, 0, 0);
        blackImage.gameObject.SetActive(false);
    }
}
