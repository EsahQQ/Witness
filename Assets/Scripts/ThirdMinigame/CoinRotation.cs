using UnityEngine;

namespace ThirdMinigame
{
    public class CoinRotation : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 5f;

        private void Update()
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}
