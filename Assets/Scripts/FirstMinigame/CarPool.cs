using UnityEngine;
using System.Collections.Generic;

namespace FirstMinigame
{
    public class CarPool : MonoBehaviour
    {
        [SerializeField] private List<GameObject> carPrefabs;
        [SerializeField] private int poolSize;
        
        private List<GameObject> _pooledObjects;
        public static CarPool Instance {get; private set;}
        
        private void Awake()
        {
            Instance = this;
            
            _pooledObjects = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                int randomPrefabIndex = Random.Range(0, carPrefabs.Count);
                GameObject car = Instantiate(carPrefabs[randomPrefabIndex]);
                car.SetActive(false);
                _pooledObjects.Add(car);
                car.transform.SetParent(this.transform);
            }
        }
        
        public GameObject GetPooledObject()
        {
            if (_pooledObjects.Count > 0)
            {
                int randomIndex = Random.Range(0, _pooledObjects.Count);
                var car = _pooledObjects[randomIndex];
                _pooledObjects.RemoveAt(randomIndex);
                return car;
            }
            
            Debug.LogWarning("No free cars");
            return null;
        }
        
        public void ReturnObjectToPool(GameObject car)
        {
            _pooledObjects.Add(car);
            car.SetActive(false);
        }
    }
}
