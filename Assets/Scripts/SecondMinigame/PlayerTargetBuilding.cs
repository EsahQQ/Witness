using System;
using System.Collections;
using UnityEngine;


namespace SecondMinigame
{
    public class PlayerTargetBuilding : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject nextTarget;
        [SerializeField] private string nextTargetName;
        
        public void Interact()
        {
            if (!nextTarget)
            {
                Debug.Log("Win!");
                StartCoroutine(GameManager.Instance.MoveToNextScene());
            }
            else
            {
                nextTarget.tag = "OpenBuilding";
                Debug.Log("Next target is " + nextTargetName + "!");
            }
            tag = "ClosedBuilding";
        }

        
    }
}
