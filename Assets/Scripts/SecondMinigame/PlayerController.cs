using System;
using TreeEditor;
using UnityEngine;

namespace SecondMinigame
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject _currentInteractable;

        public static PlayerController Instance { get; private set; }
        public event EventHandler OnPlayerHide;

        public bool IsPlayerHide;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (!_currentInteractable) return;
            if (Input.GetKeyDown(KeyCode.E) && _currentInteractable.CompareTag("OpenBuilding"))
            {
                _currentInteractable.GetComponent<IInteractable>().Interact();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("PlayerHideInvoke");
                IsPlayerHide = !IsPlayerHide;
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
