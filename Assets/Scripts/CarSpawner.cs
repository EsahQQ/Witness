using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private float spawnCooldown = 5f;
    [SerializeField] private List<GameObject> spawnedCars;
    [SerializeField] private int linesAmount = 4;
    [SerializeField] private float moveAmount = 1.25f;
    private float _elapsedTime;
    

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime < spawnCooldown) return;
        _elapsedTime = 0;
        int randomCarIndex = Random.Range(0, spawnedCars.Count);
        int randomLineIndex = Random.Range(0, linesAmount);
        GameObject car = Instantiate(spawnedCars[randomCarIndex]);
        car.transform.position = new  Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z - randomLineIndex * moveAmount);
    }
}
