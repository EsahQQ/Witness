using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class TextTypeEffect : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] private string text;

    [SerializeField] private string sceneToLoad;

    [SerializeField] private TextMeshProUGUI pressHint;
    private TextMeshProUGUI _textPanel;
    private bool _isTextTypeEnd;

    private void Awake()
    {
        _textPanel = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartCoroutine(WriteText());
    }

    private void Update()
    {
        if (!_isTextTypeEnd)
            return;
        
        if (Input.anyKeyDown)
            SceneTransitionManager.Instance.LoadScene(sceneToLoad);
    }

    private IEnumerator WriteText()
    {
        foreach (char c in text)
        {
            _textPanel.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        _isTextTypeEnd = true;
        StartCoroutine(ShowHint());
    }

    private IEnumerator ShowHint()
    {
        float elapsedTime = 0;
        while (elapsedTime < 2)
        {
            float newAlpha = Mathf.Lerp(0, 1, elapsedTime / 2);
            pressHint.color = new Color(1, 1, 1, newAlpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
