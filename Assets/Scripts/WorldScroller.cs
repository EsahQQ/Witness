using UnityEngine;

public class WorldScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 5f;
    
    private void Update()
    {
        transform.position =  new Vector3(transform.position.x - scrollSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        if (transform.position.x <= -20)
        {
            float overshot = transform.position.x + 20;
            transform.position =  new Vector3(overshot + 20, transform.position.y, transform.position.z);
        }
            
    }
}
