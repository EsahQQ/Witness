using System;
using UnityEngine;
using System.Collections;

namespace ThirdMinigame
{
    public class FallableTile : MonoBehaviour
    {
        [SerializeField] private float fallDuration = 1f;

        private void OnTriggerExit(Collider other)
        {
            StartCoroutine(TileFall());
        }

        private IEnumerator TileFall()
        {
            float elapsedTime = 0;
            while (elapsedTime < fallDuration)
            {
                float newY = Mathf.Lerp(0.25f, -3, elapsedTime / fallDuration);
                float newScale = Mathf.Lerp(1f, 0.01f, elapsedTime / fallDuration);
                float newScaleY = newScale / 2;
                transform.localScale = new Vector3(newScale, newScaleY, newScale);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = new Vector3(transform.position.x, -3, transform.position.z);
            Destroy(gameObject);
        }
    }
}
