using System;
using TMPro;
using UnityEngine;
using System.Collections;

namespace SecondMinigame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI targetText;
        [SerializeField] private GameObject notePanel;
        private TextMeshProUGUI _noteText;

        public event EventHandler OnNoteShow;
        
        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _noteText = notePanel.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ChangeCurrentTargetBuilding(string nextTargetName, string noteText)
        {
            StartCoroutine(ChangeTargetBuildingText(nextTargetName, noteText));
        }

        private IEnumerator ChangeTargetBuildingText(string nextTargetName, string noteText)
        {
            _noteText.text = noteText;
            notePanel.SetActive(true);
            OnNoteShow?.Invoke(this, EventArgs.Empty);
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            notePanel.SetActive(false);
            OnNoteShow?.Invoke(this, EventArgs.Empty);
            float elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / 1f);
                targetText.alpha = newAlpha;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            targetText.alpha = 0f;
            targetText.text = nextTargetName;
            elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / 1f);
                targetText.alpha = newAlpha;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            targetText.alpha = 1f;
        }
    }
}
