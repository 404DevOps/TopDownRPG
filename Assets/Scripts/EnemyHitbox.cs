using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    //Damage
    public int damage = 1;
    public float pushForce = 5;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name == "Player")
        {
            //New DMG
            Damage dmg = new Damage() { damageAmount = damage, pushForce = pushForce, origin = transform.position };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
