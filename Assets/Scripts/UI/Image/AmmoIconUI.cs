using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AmmoIconUI : MonoBehaviour
{
	private Animator animator;

	private bool triggered;

	public void SetReductionState()
	{
		if(triggered)
		{
			return;
		}

		animator.SetTrigger("SetReductionState");

		triggered = true;
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
}