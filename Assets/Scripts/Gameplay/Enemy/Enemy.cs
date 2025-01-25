using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
	[SerializeField, Min(1)] protected int initialHealth = 1;
	[SerializeField, Min(0f)] protected float initialMovementSpeed = 1f;
	[SerializeField, Min(0f)] private float damage = 10f;

	protected Rigidbody2D rb2D;
	protected int currentHealth;
	protected Transform target;

	// TODO:
	// Dashing enemy: while dashing, "remember" attack point, so it will not chase the player while dashing
	// Dashing enemy: while dashing, when it will go to attack point, stop for a while and then go back to the previous position (remembered before the attack point)

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		if(currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}

	protected abstract void FixedUpdate();

	private void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
		currentHealth = initialHealth;

		FindTarget();
	}

	private void FindTarget()
	{
		var player = FindAnyObjectByType<Player>();

		if(player != null)
		{
			target = player.gameObject.transform;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.TryGetComponent(out PlayerStats playerStats))
		{
			playerStats.LoseOxygen(damage);
		}
	}
}