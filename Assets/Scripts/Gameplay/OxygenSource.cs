using System;
using UnityEngine;

public class OxygenSource : MonoBehaviour, IAttachable
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform ropeStart;
    [SerializeField] private Transform ropeEnd;

    [SerializeField] float radius = 5;

    GameObject player;

	public bool PlayerIsAttached() => player != null;

    private void Start()
    {
        lineRenderer.SetPosition(0, ropeStart.position);

        InputEvents.InteractAction += Interact;
    }

    private void OnDisable()
    {
        InputEvents.InteractAction -= Interact;
    }

    private void Interact(object sender, EventArgs e)
    {
        Detach();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

            player = collision.gameObject;
            Attach();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return; 
    }

    private void FixedUpdate()
    {
        if(ropeEnd != null)
        {
            lineRenderer.SetPosition(1, ropeEnd.position);
        }
        else
        {
            lineRenderer.SetPosition(1, ropeStart.position);
        }
    }

    public void Detach()
    {
        if (player == null)
            return;
        ropeEnd = null;
        player.GetComponent<Movement>().LimitMovement(false, transform.position);
        player = null;
        PlayerStats.AttachedStatus.Invoke(this, false);
    }

    public void Attach()
    {
        ropeEnd = player.GetComponent<Transform>();
        player.GetComponent<Movement>().LimitMovement(true, transform.position, radius);
        PlayerStats.AttachedStatus.Invoke(this, true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
