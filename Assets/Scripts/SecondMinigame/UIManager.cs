using System;
using TMPro;
using UnityEngine;

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
            targetText.text = nextTargetName;
        }
    }
}
