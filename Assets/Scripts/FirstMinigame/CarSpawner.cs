using UnityEngine;
using System.Collections;

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
            _carPool = CarPool.Instance;
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (true) 
            {
                float spawnCooldown = Random.Range(minSpawnCooldown, maxSpawnCooldown);
                yield return new WaitForSeconds(spawnCooldown);
                
                SpawnCar();
            }
        }

        private void SpawnCar()
        {
            GameObject carToSpawn = _carPool.GetPooledObject();
            if (carToSpawn == null) return;

            int randomLineIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomLineIndex];
            
            carToSpawn.transform.position = spawnPoint.position;
            carToSpawn.transform.rotation = spawnPoint.rotation;
            
            carToSpawn.SetActive(true);
        }
    }
}
