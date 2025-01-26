using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static EventHandler<bool> ReloadStatus;
    public static EventHandler<bool> OutOfAmmoStatus;
    public static EventHandler<int> FlipDirection; 
    public static EventHandler FinishedReloadAnim;


    private Movement movement;
    [SerializeField] private float shotgunKnockback = 5f;
    [SerializeField] private float shotReload = 2f;
    [SerializeField] private float shotReloadTimer = 0f;
	[SerializeField, Min(1)] private int damage = 1;
	[SerializeField, Min(0.01f)] private float shotgunRange = 10f;
	[SerializeField] private LayerMask shotgunLayerMask;
	[SerializeField] private AudioSource shotgunShotSoundAudioSource;
	[SerializeField] private AudioSource shotgunReloadSoundAudioSource;
	[SerializeField] private Animator shotgunAnimator;
	[SerializeField] private Animator shotgunReloadingHandAnimator;
    [SerializeField] private Transform visuals;
    [SerializeField] private PlayerRotationAdjuster rotationAdjuster;
    [SerializeField] private Transform reloadHand;
    bool hasShot = false;
    bool outOfAmmo = false;
    bool isReloading = false;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }
    private void Start()
    {
        InputEvents.ShootAction += Shoot;
        OutOfAmmoStatus += OutOfAmmo;
        ReloadStatus += SetReloadStatus;
    }

    private void SetReloadStatus(object sender, bool reloadStatus)
    {
        isReloading = reloadStatus;
    }

    private void OutOfAmmo(object sender, bool e)
    {
        outOfAmmo = e;
    }

    private void OnDisable()
    {
        InputEvents.ShootAction -= Shoot;
        OutOfAmmoStatus -= OutOfAmmo;
    }

    private void Shoot(object sender, EventArgs e)
    {
        if (outOfAmmo)
            return;

        if(!hasShot) 
        {
            var plane = new Plane(Vector3.back, Vector3.zero);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float enter))
            {
                var worldPosition = ray.GetPoint(enter);
                var forceDirection = transform.position - worldPosition;

                movement.SendImpulse(forceDirection.normalized * shotgunKnockback);

                hasShot = true;

				var shotDirection = worldPosition - transform.position;
				var hit2D = Physics2D.Raycast(ray.origin, ray.direction, shotgunRange, shotgunLayerMask);
				
				if(hit2D.collider != null && hit2D.collider.TryGetComponent(out Enemy enemy))
				{
					enemy.TakeDamage(damage);
				}

				if(shotgunShotSoundAudioSource != null)
				{
					shotgunShotSoundAudioSource.Play();
				}

				if(shotgunAnimator != null)
				{
                    //shotgunAnimator.SetBool("Triggered", true);
                    shotgunAnimator.SetTrigger("Triggered");
				}

				if(shotgunReloadingHandAnimator != null)
				{
					shotgunReloadingHandAnimator.SetBool("Triggered", false);
				}

                if (!rotationAdjuster.IsInReloadRange())
                    return;
                ReloadStatus.Invoke(this, true);
            }
        }
    }

    private void Update()
    {
        FlipCheck();

        if (!hasShot)
            return;
        if (outOfAmmo)
            return;
        if (!isReloading)
        {
            if (rotationAdjuster.IsInReloadRange())
            {
                ReloadStatus.Invoke(this, true);
            }
            else
            {
                return;
            }
        }
        



        if (shotReloadTimer < shotReload)
        {
            shotReloadTimer += Time.deltaTime;
        }
        else if(hasShot)
        {
            hasShot = false;
            shotReloadTimer = 0;

			if(shotgunReloadSoundAudioSource != null)
			{
				shotgunReloadSoundAudioSource.Play();
			}

			/*if(shotgunAnimator != null)
			{
				shotgunAnimator.SetBool("Triggered", false);
			}*/

			if(shotgunReloadingHandAnimator != null)
			{
                reloadHand.gameObject.SetActive(false);
				shotgunReloadingHandAnimator.SetBool("Triggered", true);
			}

            ReloadStatus.Invoke(this, false);
        }
    }

    void FlipCheck()
    {
        var plane = new Plane(Vector3.back, Vector3.zero);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float enter))
        {
            var worldPosition = ray.GetPoint(enter);

            if(worldPosition.x < transform.position.x)
            {
                if(visuals.transform.localScale.x  > 0)
                {
                    FlipVisual();
                    FlipDirection.Invoke(this, -1);
                }
            }
            else
            {
                if (visuals.transform.localScale.x < 0)
                {
                    FlipVisual();
                    FlipDirection.Invoke(this, 1);
                }
            }
        }

    }

    void FlipVisual()
    {
        visuals.transform.localScale = new Vector3(-visuals.transform.localScale.x, visuals.transform.localScale.y, visuals.transform.localScale.z);
    }

    
}
