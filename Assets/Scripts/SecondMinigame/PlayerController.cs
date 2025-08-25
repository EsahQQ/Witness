using System;
using TreeEditor;
using UnityEngine;

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
        }

        private void OnTriggerExit(Collider other)
        {
            _currentInteractable = null;
        }
    }
}
