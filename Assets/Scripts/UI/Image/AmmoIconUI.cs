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

		//Vector3 previousPos = transform.position;
		//transform.SetParent(GameObject.Find("Canvas").transform);
		//transform.position = previousPos;

		triggered = true;
	}

	public void SetGrayedOutState()
	{
        animator.SetTrigger("SetGrayedOutState");
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