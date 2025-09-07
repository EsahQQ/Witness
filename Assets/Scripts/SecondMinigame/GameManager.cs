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

        private void Start()
        {
            EnemyNav.Instance.OnPlayerDeath += ReloadScene;
        }
        
        public void MinigameWin()
        {
            SceneTransitionManager.Instance.LoadScene("Scenes/2nd3rdText");
        }
        
        private void ReloadScene(object sender, EventArgs e)
        {
            SceneTransitionManager.Instance.LoadScene("Scenes/Main", 0.25f);
        }
    }
}
