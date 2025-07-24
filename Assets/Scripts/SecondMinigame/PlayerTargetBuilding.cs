using System;
using UnityEngine;

namespace SecondMinigame
{
    public class PlayerTargetBuilding : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Interacted");
        }
    }
}
