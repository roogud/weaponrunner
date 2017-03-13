using UnityEngine;

//this is a Pickup that rewards health when picked up.
public class PickupFireballPowerup : Pickup
{
    public int ammoAmount = 1;

    public override void PickUp( PickupGetter getter )
    {
        Destructible destructible = getter.GetComponent<Destructible>();
        if ( destructible != null )
        {
			destructible.ActivateFireballPowered(ammoAmount);
        }

        //then, do our default behavior
        base.PickUp( getter );
    }
}
