using UnityEngine;

public class RunnerObstacle : MonoBehaviour
{
    public float moveSpeed;
    public int laneIndex;
    public float laneDistance;
    public float ground;
    public GameObject playerTarget;
    
    private float currentZ;
    private float currentX;
    private bool hasHitPlayer;

    private void Start()
    {
        currentZ = 300f;
        hasHitPlayer = false;

        if (laneIndex == 0)
        {
            currentX = -laneDistance;
        }
        if (laneIndex == 1)
        {
            currentX = 0f;
        }
        if (laneIndex == 2)
        {
            currentX = laneDistance;
        }
    }

    private void Update()
    {
        currentZ = currentZ - (moveSpeed * Time.deltaTime);

        float perspective = CameraComponent.focalLenth / (CameraComponent.focalLenth + currentZ);
        
        float scaledX = currentX * perspective;
        float scaledY = ground * perspective;

        transform.localScale = new Vector3(perspective, perspective, 1f);
        transform.position = new Vector3(scaledX, scaledY, 0f);

        CheckHitPlayer();

        if (currentZ < -50f)
        {
            Destroy(gameObject);
        }
    }

    private void CheckHitPlayer()
    {
        if (hasHitPlayer == true)
        {
            return;
        }

        if (currentZ < 5f)
        {
            if (currentZ > -5f)
            {
                PlayerRunner player = playerTarget.GetComponent<PlayerRunner>();

                if (player.currentLane == laneIndex)
                {
                    if (player.isJumping == false)
                    {
                        player.TakeDamage();
                        hasHitPlayer = true;
                    }
                }
            }
        }
    }
}