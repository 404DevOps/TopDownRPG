using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectible
{
    public Sprite emptyChest;
    public int goldAmount = 5;
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.Instance.ShowText($"+ {goldAmount}g",25, Color.yellow, this.transform.position, Vector3.up * 25, 1.5f);
            GameManager.Instance.gold += goldAmount;
        }
    }
}
