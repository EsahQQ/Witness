using System.Collections.Generic;
using UnityEngine;

namespace FirstMinigame
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField] private float spawnCooldown = 5f;
        [SerializeField] private List<CarMovement> spawnedCars;
        [SerializeField] private int linesAmount = 4;
        private float _elapsedTime;
    

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime < spawnCooldown) return;
            _elapsedTime = 0;
            int randomCarIndex = Random.Range(0, spawnedCars.Count);
            int randomLineIndex = Random.Range(0, linesAmount);
            var car = spawnedCars[randomCarIndex];
            car.canMove = true;
        }
    }
}
