using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        DontDestroyOnLoad(this);

    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(isAlive)
            UpdateMotor(new Vector3(x, y, 0));
    }

    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoint)
            return;

        hitpoint += healingAmount;

        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;

            GameManager.Instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);

        GameManager.Instance.OnHitpointChange(hitpoint, maxHitpoint);

    }

    public void Respawn()
    {
        isAlive = true;
        Heal(maxHitpoint);
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.Instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (isAlive)
        {
            base.ReceiveDamage(dmg);
            GameManager.Instance.OnHitpointChange(hitpoint, maxHitpoint);
        }
    }

    protected override void Death()
    {
        isAlive = false;
        GameManager.Instance.deathmenuAnimator.SetTrigger("show");
    }
}
