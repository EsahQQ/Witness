using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SecondMinigame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Camera cameraComponent;
        
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        
        public void MinigameWin()
        {
            SceneTransitionManager.Instance.LoadScene("Scenes/ThirdMinigame");
        }
    }
}
