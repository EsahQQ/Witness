using System;
using UnityEngine;
using System.Collections;

namespace ThirdMinigame
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject _currentTile;
        private int _coinsCollected;
        [SerializeField] private AudioSource coinPickUp;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                _coinsCollected++;
                coinPickUp.Play();
                Destroy(other.gameObject);
            }

            if (other.gameObject.CompareTag("Flour"))
            {
                if (_currentTile != null)
                {
                    StartCoroutine(TileFall(_currentTile));
                }
                _currentTile = other.gameObject;
            }
            
            if (other.gameObject.CompareTag("Exit"))
            {
                GameManager.Instance.CompleteLevel(_coinsCollected);
                _coinsCollected = 0;
            }
        }

        private IEnumerator TileFall(GameObject tile)
        {
            float elapsedTime = 0;
            while (elapsedTime < 2)
            {
                float newY = Mathf.Lerp(0.25f, -5, elapsedTime / 2);
                tile.transform.position = new Vector3(tile.transform.position.x, newY, tile.transform.position.z);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            tile.transform.position = new Vector3(tile.transform.position.x, -5, tile.transform.position.z);
            Destroy(tile);
        }
    }
}
