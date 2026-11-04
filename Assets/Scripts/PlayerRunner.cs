using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerRunner : MonoBehaviour
{
    public float jumpPower;
    public float moveSpeed;
    public float laneDistance;
    public TextMeshProUGUI hpText;
    public SpriteRenderer spriteRenderer;

    public int currentLane;
    public bool isJumping;
    
    private AABBCollider ground;
    private float targetX;
    private float verticalSpeed;
    private float hp = 100f;
    private float regenTimer;
    private float hitTimer;

    private void Start()
    {
        currentLane = 1;
        targetX = transform.position.x;
        ground = GameObject.FindWithTag("Ground").GetComponent<AABBCollider>();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentLane > 0)
            {
                currentLane = currentLane - 1;
                targetX = targetX - laneDistance;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentLane < 2)
            {
                currentLane = currentLane + 1;
                targetX = targetX + laneDistance;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJumping == false)
            {
                verticalSpeed = jumpPower;
                isJumping = true;
            }
        }

        float newX = Mathf.MoveTowards(transform.position.x, targetX, moveSpeed * Time.deltaTime);
        
        verticalSpeed = verticalSpeed - (9.81f * Time.deltaTime);
        float newY = transform.position.y + (verticalSpeed * Time.deltaTime);

        if (newY <= ground.max.y)
        {
            newY = ground.max.y;
            verticalSpeed = 0f;
            isJumping = false;
        }

        transform.position = new Vector3(newX, newY, transform.position.z);

        regenTimer = regenTimer + Time.deltaTime;
        if (regenTimer >= 1f)
        {
            hp = hp + 1f;
            regenTimer = 0f;
        }

        if (hp > 100f)
        {
            hp = 100f;
        }

        hpText.text = "HP: " + hp;

        if (hitTimer > 0f)
        {
            spriteRenderer.color = Color.red;
            hitTimer = hitTimer - Time.deltaTime;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void TakeDamage()
    {
        hp = hp - 5f;
        hitTimer = 0.5f;

        if (hp <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}