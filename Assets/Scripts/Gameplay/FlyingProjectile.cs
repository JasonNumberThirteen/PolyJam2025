using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyingProjectile : MonoBehaviour
{
	private float damage;
	private float movementSpeed;
	private Vector2 direction;
	private Rigidbody2D rb2D;
	
	public void Setup(float damage, float movementSpeed, Vector2 direction)
	{
		this.damage = damage;
		this.movementSpeed = movementSpeed;
		this.direction = direction;
	}

	private void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		var speed = movementSpeed*Time.fixedDeltaTime;

		rb2D.MovePosition(rb2D.position + speed*direction);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.TryGetComponent(out PlayerStats playerStats))
		{
			playerStats.LoseOxygen(damage);
		}

		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}
}