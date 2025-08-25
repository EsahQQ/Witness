using System;
using System.Collections;
using UnityEngine;


namespace SecondMinigame
{
    public class PlayerTargetBuilding : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject nextTarget;
        [SerializeField] private string nextTargetName;
        [SerializeField] private string noteText;
        
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
                UIManager.Instance.ChangeCurrentTargetBuilding(nextTargetName, noteText);
            }
            tag = "ClosedBuilding";
        }
    }
}
