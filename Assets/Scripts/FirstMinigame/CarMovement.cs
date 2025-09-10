using UnityEngine;

namespace FirstMinigame
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        
        private void Update()
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("CarReseter")) return;
            
            CarPool.Instance.ReturnObjectToPool(gameObject);
        }
    }
}
