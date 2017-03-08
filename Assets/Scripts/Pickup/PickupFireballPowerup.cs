using UnityEngine;

//this is a Pickup that rewards health when picked up.
public class PickupFireballPowerup : Pickup
{
    public float powerupTime = 1.0f;

    public override void PickUp( PickupGetter getter )
    {
        //first, give health back, if applicable
        Destructible destructible = getter.GetComponent<Destructible>();
        if ( destructible != null )
        {
            destructible.ActivateFireballPowered(powerupTime);
        }

        //then, do our default behavior
        base.PickUp( getter );
    }
}
