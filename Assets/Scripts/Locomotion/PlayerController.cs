using UnityEngine;
using System.Collections;

//this is a controller that listens to keyboard input to move a Mover around.
public class PlayerController : MonoBehaviour
{
    //this is the Mover that we control
    public Mover controlledMover;
    public AudioSource jumpSound;

    //this is the Jumper we control
    public Jumper controlledJumper;

    protected Destructible destructible;
    protected Blaster blaster;

    public void Awake() {
        destructible = GetComponent<Destructible>();
        blaster = GetComponent<Blaster>();
    }
    
    public void Update()
    {
		if (destructible.isAlive) {
        //we can use the convenience of Unity's input axes to get direction automatically.
        //bonus! this works with WASD and we can hook it up to work with controllers!
        controlledMover.AccelerateInDirection( new Vector3( Input.GetAxis( "Horizontal" ), 0.0f, 0.0f ) );

        if ( Input.GetButtonDown( "Jump" ) )
        {
            controlledJumper.Jump();
            jumpSound.Play();
        }
			if (destructible.isFireballPoweredActive) {
				if (Input.GetButtonDown ("Fire1")) {
					blaster.Shoot ();
					destructible.ShootFireball ();
				}
			}
        }
    }
}