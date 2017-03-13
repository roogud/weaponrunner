using UnityEngine;

//a destructible is something that can take damage and be destroyed.
//we can subclass this to different sorts of behaviors when we are
//destroyed or take damage, like play particles or animations.
//destructibles can also be healed, up to their maximum hit points.
public class Destructible : MonoBehaviour
{
    public float maximumHitPoints = 100.0f;
    public float invincibilityTime = 0.5f;

    protected float lastTimeHurt;

	public int fireballPoweredAmmoRemaining;
	public int fireballMaxAmmo = 3;
	public float[] fireballParticleLifetimeValues;


	public bool isFireballPoweredActive {
		get; protected set;
    }

	[SerializeField]
	protected ParticleSystem fireballPoweredParticles;

	[SerializeField]
	protected ParticleSystem deathParticles;


	public bool isAlive {
		get {
			return hitPoints >0;
		}
	}
    
    public virtual float hitPoints
    {
        get;
        protected set;
    }
    
    public virtual bool isDying
    {
        get;
        protected set;
    }
    
    public virtual void Start()
    {
        hitPoints = maximumHitPoints;

        //we're setting the last time hurt in the past so that we can be hurt immediately
        lastTimeHurt = Time.time - invincibilityTime;

		fireballPoweredAmmoRemaining = 0;
		if (fireballPoweredParticles != null) {
			fireballPoweredParticles.gameObject.SetActive (false);
		}
    }
    
    public virtual void Update() {
           
    }
    public virtual void TakeDamage( float amount )
    {
        ModifyHitPoints( -amount );
    }

    public virtual void RecoverHitPoints( float amount )
    {
        ModifyHitPoints( amount );
    }
    
    public virtual void ModifyHitPoints( float amount )
    {
        //if we were recently hurt, we can't be hurt again, so do nothing
        if ( Time.time - lastTimeHurt < invincibilityTime && amount < 0.0f )
        {
            return;
        }

        hitPoints += amount;
        hitPoints = Mathf.Min( hitPoints, maximumHitPoints );
        
        if ( hitPoints <= 0.0f )
        {
            Die();
        }
    }

    public virtual void Die()
    {
		if (isDying)
		{
			return;
		}
		if (deathParticles != null) {
			ParticleSystem deathParticlesInstance = Instantiate(deathParticles);
			deathParticlesInstance.transform.position = transform.position;
		}
        
        isDying = true;
        Object.Destroy( gameObject );
    }
	public virtual void ActivateFireballPowered (int p_count){
		fireballPoweredAmmoRemaining += p_count;
		if (fireballPoweredAmmoRemaining > fireballMaxAmmo) {
			fireballPoweredAmmoRemaining = fireballMaxAmmo;
			UpdateFireballParticleLifetime ();
		}
		isFireballPoweredActive = true;
		FireballPoweredParticlesActive (true);
	}
	public virtual void ShootFireball(){
		fireballPoweredAmmoRemaining--;
		if (fireballPoweredAmmoRemaining < 0) {
			fireballPoweredAmmoRemaining = 0;
		}
		if (fireballPoweredAmmoRemaining == 0) {
			isFireballPoweredActive = false;
			FireballPoweredParticlesActive (false);
		}
		UpdateFireballParticleLifetime ();
	}

	public virtual void FireballPoweredParticlesActive(bool enabled) {
		if (fireballPoweredParticles != null) {
			fireballPoweredParticles.gameObject.SetActive (enabled);
		}
	}

	public virtual void UpdateFireballParticleLifetime() {
		
		int lifetimeIndex = fireballPoweredAmmoRemaining - 1;
		lifetimeIndex = Mathf.Clamp (lifetimeIndex, 0, fireballParticleLifetimeValues.Length - 1);
		float lifetimeValue = fireballParticleLifetimeValues [lifetimeIndex];
			if(fireballPoweredParticles != null) {
				ParticleSystem.MainModule main = fireballPoweredParticles.main;
				main.startLifetime = lifetimeValue;
			}
	}
}