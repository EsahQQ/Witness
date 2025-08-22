using System;
using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace SecondMinigame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI targetText;
        [SerializeField] private GameObject notePanel;
         
        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void ChangeCurrentTargetBuilding(string nextTargetName)
        {
            notePanel.SetActive(true);
            while (!Input.GetKeyDown(KeyCode.Space))
            {
                
            }
            notePanel.SetActive(false);
            StartCoroutine(ChangeTargetBuildingText(nextTargetName));
        }

        private IEnumerator ChangeTargetBuildingText(string nextTargetName)
        {
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
