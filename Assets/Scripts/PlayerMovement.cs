using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float laneOffset = 2f;
    public float laneMoveSpeed = 8f;
    private int currentLane = 1;
    private float currentX;
    private float targetX;
    public float jumpForce = 8f;
    public float gravity = 20f;
    private float jumpVelocity;
    public float jumpHeight;
    private float baseY;
    public bool IsJumping => jumpHeight > 0f;
    public int CurrentLane => currentLane;

    private void Start()
    {
        baseY = transform.position.y;
        currentX = transform.position.x;
        targetX = currentX;
    }

    private void Update()
    {
        Move();
        Jump();

        currentX += (targetX - currentX) * Mathf.Clamp01(laneMoveSpeed * Time.deltaTime);
        transform.position = new Vector3(currentX,baseY + jumpHeight,transform.position.z);
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentLane--;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentLane++;
        }
        currentLane = Mathf.Clamp(currentLane, 0, 2);
        targetX = (currentLane - 1) * laneOffset;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpHeight == 0f)
        {
            jumpVelocity = jumpForce;
        }

        if (jumpHeight > 0f || jumpVelocity > 0f)
        {
            jumpHeight += jumpVelocity * Time.deltaTime;

            jumpVelocity -= gravity * Time.deltaTime;

            if (jumpHeight < 0f)
            {
                jumpHeight = 0f;
                jumpVelocity = 0f;
            }
        }
    }
}
