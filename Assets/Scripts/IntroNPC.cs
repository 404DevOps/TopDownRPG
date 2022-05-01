using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroNPC : Collidable
{
    public string message;

    private float coolDown = 4.0f;
    private float lastShown;

    protected override void Start()
    {
        base.Start();
        lastShown = -coolDown;
    }
    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastShown > coolDown)
        {
            lastShown = Time.time;
            GameManager.Instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0,0.16f,0), Vector3.zero, coolDown);
        }
    }
}
