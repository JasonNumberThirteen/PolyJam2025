using UnityEngine;
using System.Collections;
using System;
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
	[SerializeField, Min(1)] protected int initialHealth = 1;
	[SerializeField, Min(0f)] protected float initialMovementSpeed = 1f;
	[SerializeField, Min(0f)] protected float damage = 10f;
    [SerializeField] private GameObject deathParticle;

    protected Rigidbody2D rb2D;
	protected int currentHealth;
	protected Transform target;


    private SpriteRenderer[] renderers;
    private Material[] materials;
    [SerializeField] private float flashTime = 0.2f;
    public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		if(currentHealth <= 0)
		{
			Instantiate(deathParticle,transform.position, Quaternion.identity);
			EnemySpawnManager.EnemyDied.Invoke(this,EventArgs.Empty);
			Destroy(gameObject);
		}
		else
		{
            StartCoroutine(DamageFlasher());
        }
	}

    private IEnumerator DamageFlasher()
    {
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, 0f, elapsedTime / flashTime);

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetFloat("_FlashAmount", currentFlashAmount);
            }

            yield return null;
        }


        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat("_FlashAmount", 0);
        }

    }

    protected abstract void FixedUpdate();

	protected bool IsCloseToPosition(Vector2 position, float distance) => Vector2.Distance(transform.position, position) <= distance;

	protected virtual void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
		currentHealth = initialHealth;

        renderers = GetComponentsInChildren<SpriteRenderer>();
        materials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
        }


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
			playerStats.GetDamaged(damage, transform.position);
		}
	}
}