using System;
using UnityEngine;
using System.Collections;

namespace ThirdMinigame
{
    public class FallableTile : MonoBehaviour
    {
        [SerializeField] private float fallDuration = 2f;

        private void OnTriggerExit(Collider other)
        {
            StartCoroutine(TileFall());
        }

        private IEnumerator TileFall()
        {
            float elapsedTime = 0;
            while (elapsedTime < 2)
            {
                float newY = Mathf.Lerp(0.25f, -5, elapsedTime / 2);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = new Vector3(transform.position.x, -5, transform.position.z);
            Destroy(this);
        }
    }
}
