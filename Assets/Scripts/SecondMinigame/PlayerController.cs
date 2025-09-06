using System;
using UnityEngine;
using System.Collections;

namespace SecondMinigame
{
    [RequireComponent(typeof(BoxCollider))]
    public class PlayerController : MonoBehaviour
    {
        private GameObject _currentInteractable;
        private BoxCollider _collider;

        public static PlayerController Instance { get; private set; }
        public event EventHandler OnPlayerHide;

        public bool IsPlayerHide;

        private void Awake()
        {
            Instance = this;
            _collider = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            if (!_currentInteractable) return;
            if (Input.GetKeyDown(KeyCode.E) && _currentInteractable.CompareTag("OpenBuilding"))
            {
                if (!EnemyNav.Instance.IsChasing)
                    _currentInteractable.GetComponent<IInteractable>().Interact();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                IsPlayerHide = !IsPlayerHide;
                _collider.enabled = !_collider.enabled;
                OnPlayerHide?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _currentInteractable = other.gameObject;

            if (_currentInteractable.CompareTag("ClosedBuilding"))
            {
                var button = _currentInteractable.GetComponentInChildren<SpriteRenderer>();
                StartCoroutine(ShowButton(button, button.color.a, 1));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_currentInteractable.CompareTag("ClosedBuilding"))
            {
                var button = _currentInteractable.GetComponentInChildren<SpriteRenderer>();
                StartCoroutine(ShowButton(button, button.color.a, 0));
            }
            
            _currentInteractable = null;
        }

        private IEnumerator ShowButton(SpriteRenderer button, float firstAlpha, float secondAlpha)
        {

            float elapsedTime = 0;
            while (elapsedTime < 1)
            {
                float newAlpha = Mathf.Lerp(firstAlpha, secondAlpha, elapsedTime / 1);
                button.color = new Color(1, 1, 1, newAlpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            button.color = new Color(1, 1, 1, secondAlpha);
        }
    }
}
