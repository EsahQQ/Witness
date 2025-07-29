using System;
using UnityEngine;

namespace ThirdMinigame
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject _currentTile;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                Destroy(other.gameObject);
            }

            if (other.gameObject.CompareTag("Flour"))
            {
                if (_currentTile != null)
                {
                    Destroy(_currentTile);
                }
                _currentTile = other.gameObject;
            }
            
            if (other.gameObject.CompareTag("Exit"))
            {
                Debug.Log("Win");
                GameManager.Instance.MoveToNextLevel();
            }
        }
    }
}
