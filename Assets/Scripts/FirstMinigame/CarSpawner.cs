using System.Collections.Generic;
using UnityEngine;

namespace FirstMinigame
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField] private float minSpawnCooldown = 1f;
        [SerializeField] private float maxSpawnCooldown = 3f;
        [SerializeField] private Transform[] spawnPoints;

        private float _elapsedtime;
        private float _currentSpawnCooldown;
        private CarPool _carPool;

        private void Start()
        {
            SetCooldown();
            _carPool = CarPool.Instance;
        }

        private void Update()
        {
            _elapsedtime +=  Time.deltaTime;
            if (_elapsedtime < _currentSpawnCooldown)
                return;

            SpawnCar();
            _elapsedtime = 0;
            SetCooldown();
        }

        private void SetCooldown()
        {
            _currentSpawnCooldown = Random.Range(minSpawnCooldown, maxSpawnCooldown);
        }

        private void SpawnCar()
        {
            GameObject carToSpawn = _carPool.GetPooledObject();
            
            int randomLineIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomLineIndex];
            
            carToSpawn.transform.position = spawnPoint.position;
            carToSpawn.transform.rotation = spawnPoint.rotation;
            
            carToSpawn.SetActive(true);
        }
    }
}
