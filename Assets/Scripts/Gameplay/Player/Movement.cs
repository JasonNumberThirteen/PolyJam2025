using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float fallPower;

    bool isGrounded = false;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundCheckLayerMask;

    [SerializeField] private AnimationCurve jumpCurve;

    float currentJumpTime = 0;
    [SerializeField] float jumpTime = 1f;
    [SerializeField] float speedLimit = 20;
    [SerializeField] private Animator legsAnimator;

    Vector2 moveDirection;

    bool limitMovement = false;
    float radius = 0f;
    Vector2 limitLocation = Vector2.zero;
    float safetyDistance = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InputEvents.MoveAction += Move;
        InputEvents.JumpAction += Jump;
    }

    private void OnDisable()
    {
        InputEvents.MoveAction -= Move;
        InputEvents.JumpAction -= Jump;
    }
    

    public void SendImpulse(Vector2 impulse)
    {
        rb.AddForce(impulse, ForceMode2D.Impulse);
        currentJumpTime = jumpTime;
    }

    public void LimitMovement(bool setLimitMovement, Vector2 limitFrom, float setRadius = 0)
    {
        if(setLimitMovement)
        {
            limitMovement = true;
            radius = setRadius;
            limitLocation = limitFrom;
        }
        else
        {
            limitMovement = false;
        }
    }
    private void Jump(object sender, bool jumpPressed)
    {
        if(jumpPressed)
        {
            if(currentJumpTime < jumpTime)
            {
                float evaluateCurve = jumpCurve.Evaluate(currentJumpTime/jumpTime);
                float curvedJumpPower = Mathf.Lerp(jumpPower, 0,  evaluateCurve);

                rb.AddForce(Vector2.up * curvedJumpPower);
                currentJumpTime += Time.deltaTime;

				if(legsAnimator != null)
				{
					legsAnimator.SetBool("IsJumping", true);
				}
            }
            else
            {
                rb.AddForce(-Vector2.up * fallPower);
            }
        }
        else
        {
            if (!isGrounded)
            {
                currentJumpTime = jumpTime;
                rb.AddForce(-Vector2.up * fallPower);
            }
            else
            {
                currentJumpTime = 0;

				if(legsAnimator != null)
				{
					legsAnimator.SetBool("IsJumping", false);
				}
            }
        }
    }

    private void Move(object sender, Vector2 e)
    {
        moveDirection = new Vector2(e.x, moveDirection.y);

		if(legsAnimator != null)
		{
			legsAnimator.SetBool("IsMoving", e.x <= -0.1f || e.x >= 0.1f);
		}
    }

    private void FixedUpdate()
    {
        if(Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundCheckLayerMask))
        {
            isGrounded = true;

			if(legsAnimator != null)
			{
				legsAnimator.SetBool("IsJumping", false);
			}
        }
        else
        {
            isGrounded = false;

			if(legsAnimator != null)
			{
				legsAnimator.SetBool("IsJumping", true);
			}
        }

        rb.AddForce(moveDirection * moveSpeed);

        if (limitMovement)
        {
            Vector2 distance = limitLocation - (Vector2)transform.position;
            if (distance.magnitude > radius)
            {
                transform.position = (Vector2)transform.position + distance.normalized * (distance.magnitude - radius);
            }

            if(Vector2.Distance(limitLocation, transform.position) > radius + safetyDistance)
            {
                transform.position = transform.position.normalized * (distance.magnitude - radius);
            }
        }

        if(isGrounded)
        {
            if(rb.velocity.x > 5)
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
            }
            else if(rb.velocity.x < -5)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
            }
        }
        else
        {
            if(rb.velocity.x > speedLimit)
            {
                rb.velocity = new Vector2(speedLimit, rb.velocity.y);
            }
            else if(rb.velocity.x < -speedLimit)
            {
                rb.velocity = new Vector2(-speedLimit, rb.velocity.y);
            }
            else if(rb.velocity.y > speedLimit)
            {
                rb.velocity = new Vector2(rb.velocity.x, speedLimit);
            }
            else if (rb.velocity.y < -speedLimit)
            {
                rb.velocity = new Vector2(rb.velocity.x, -speedLimit);
            }
        }

    }

    private void OnDrawGizmos()
    {
        if(groundCheck != null)
            Gizmos.DrawCube(groundCheck.position, groundCheckSize);
    }
}
