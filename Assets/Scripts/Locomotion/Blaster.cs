using UnityEngine;

[RequireComponent(typeof(GroundDetector))]
public class Blaster : MonoBehaviour
{
    [SerializeField]
    protected GameObject fireballPrefab;

	public Vector2 shootOffset;
    public Vector2 fireballVelocity;

    public void Start(){
    }

    public void Update(){      
    }
    public void Shoot(){
        GameObject fireball = Instantiate (fireballPrefab);
        fireball.GetComponent<Rigidbody2D>().velocity = fireballVelocity;
		fireball.transform.position = (Vector2)transform.position + shootOffset;
    }
}