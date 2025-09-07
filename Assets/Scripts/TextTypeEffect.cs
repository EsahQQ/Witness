using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class TextTypeEffect : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] private string text;
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
            SceneTransitionManager.Instance.LoadScene("Scenes/Main");
    }

    private IEnumerator WriteText()
    {
        foreach (char c in text)
        {
            _textPanel.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        _isTextTypeEnd = true;
    }
}
