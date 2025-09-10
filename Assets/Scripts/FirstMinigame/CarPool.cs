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

        private List<GameObject> _availableCars;
        private void Awake()
        {
            Instance = this;
            
            _availableCars = new List<GameObject>();
            
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
            _availableCars.Clear();
            foreach (GameObject car in _pooledObjects)
            {
                if (!car.activeInHierarchy)
                {
                    _availableCars.Add(car);
                }
            }
            
            if (_availableCars.Count > 0)
            {
                int randomIndex = Random.Range(0, _availableCars.Count);
                return _availableCars[randomIndex];
            }
            
            Debug.LogWarning("No free cars");
            return null;
        }
        
        public void ReturnObjectToPool(GameObject car)
        {
            car.SetActive(false);
        }
    }
}
