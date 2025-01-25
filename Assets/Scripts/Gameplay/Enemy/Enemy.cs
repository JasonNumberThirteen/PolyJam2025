using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
	[SerializeField, Min(1)] protected int initialHealth = 1;
	[SerializeField, Min(0f)] protected float initialMovementSpeed = 1f;
	[SerializeField, Min(0f)] protected float damage = 10f;

	protected Rigidbody2D rb2D;
	protected int currentHealth;
	protected Transform target;

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		if(currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}

	protected abstract void FixedUpdate();

	protected bool IsCloseToPosition(Vector2 position, float distance) => Vector2.Distance(transform.position, position) <= distance;

	protected virtual void Awake()
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