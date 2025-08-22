using System;
using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

namespace SecondMinigame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI targetText;
        
        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void ChangeCurrentTargetBuilding(string nextTargetName)
        {
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
