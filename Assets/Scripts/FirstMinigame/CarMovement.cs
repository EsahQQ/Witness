using UnityEngine;

namespace FirstMinigame
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;

        public bool canMove;
        private void Update()
        {
            if (!canMove) return;
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("CarReseter")) return;
            canMove = false;
            transform.position = new Vector3(12, transform.position.y, transform.position.z);
        }
    
    }
}
