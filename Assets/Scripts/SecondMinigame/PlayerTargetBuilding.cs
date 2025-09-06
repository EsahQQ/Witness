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
                GameManager.Instance.MinigameWin();
            }
            else
            {
                nextTarget.tag = "OpenBuilding";
                var eButton = nextTarget.transform.Find("E");
                eButton.gameObject.SetActive(true);
                UIManager.Instance.ChangeCurrentTargetBuilding(nextTargetName, noteText);
            }
            tag = "ClosedBuilding";
        }
    }
}
