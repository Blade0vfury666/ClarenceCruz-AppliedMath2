using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public RunnerObstacle obstaclePrefab;
    public GameObject target;
    public float spawnInterval;
    public float obstacleSpeed;
    public float laneDistance;
    public float ground;

    private float spawnTimer;

    private void Update()
    {
        spawnTimer = spawnTimer + Time.deltaTime;
        
        if (spawnTimer >= spawnInterval)
        {
            SpawnObstacle();
            spawnTimer = 0f;
        }
    }

    private void SpawnObstacle()
    {
        int randomLane = Random.Range(0, 3);
        
        RunnerObstacle newObstacle = Instantiate(obstaclePrefab);
        newObstacle.laneIndex = randomLane;
        newObstacle.playerTarget = target;
        newObstacle.moveSpeed = obstacleSpeed;
        newObstacle.laneDistance = laneDistance;
        newObstacle.ground = ground;
    }
}