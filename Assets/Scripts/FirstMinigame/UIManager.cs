using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FirstMinigame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<Image> hearts;
        [SerializeField] private List<Image> imagesToHide;
        [SerializeField] private float hideDelay = 3f;
        [SerializeField] private float hideTime = 2f;
        private int _currentHeartsCount = 3;
        
        private void Start()
        {
            PlayerController.Instance.OnPlayerTakeDamage += OnPlayerTakeDamage;
            StartCoroutine(HideNotes());
        }

        private void OnDestroy()
        {
            PlayerController.Instance.OnPlayerTakeDamage -= OnPlayerTakeDamage;
        }

        private void OnPlayerTakeDamage(object sender, EventArgs e)
        {
            hearts[_currentHeartsCount - 1].color = Color.dimGray;
            _currentHeartsCount--;
        }

        private IEnumerator HideNotes()
        {
            yield return new WaitForSeconds(hideDelay);
            float elapsedTime = 0f;
            while (elapsedTime < hideTime)
            {
                float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / hideTime);
                foreach (Image image in imagesToHide)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            foreach (Image image in imagesToHide)
            {
                image.gameObject.SetActive(false);
            }
        }
    }
}
