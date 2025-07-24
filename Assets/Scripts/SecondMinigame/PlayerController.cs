using System;
using TreeEditor;
using UnityEngine;

namespace SecondMinigame
{
    public class PlayerController : MonoBehaviour
    {
        private IInteractable _currentInteractable;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && _currentInteractable != null)
                _currentInteractable.Interact();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("OpenBuilding")) return;
            _currentInteractable = other.GetComponent<IInteractable>();
        }

        private void OnTriggerExit(Collider other)
        {
            _currentInteractable = null;
        }
    }
}
