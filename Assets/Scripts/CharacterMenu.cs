using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Textfields
    public Text levelText, hitpointText, goldText, upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            if (currentCharacterSelection == GameManager.Instance.playerSprites.Count)
                currentCharacterSelection = 0;  
        }
        else 
        {
            currentCharacterSelection--;
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.Instance.playerSprites.Count - 1;
        }

        OnSelectionChanged();

    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.Instance.playerSprites[currentCharacterSelection];
        GameManager.Instance.player.SwapSprite(currentCharacterSelection);
    }

    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.Instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
        //
    }

    //Update Character Information
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.Instance.weaponSprites[GameManager.Instance.weapon.weaponLevel];
        if (GameManager.Instance.weapon.weaponLevel == GameManager.Instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else 
            upgradeCostText.text = GameManager.Instance.weaponPrices[GameManager.Instance.weapon.weaponLevel].ToString();
        

        //Meta

        hitpointText.text = GameManager.Instance.player.hitpoint.ToString();
        goldText.text = GameManager.Instance.gold.ToString();
        levelText.text = GameManager.Instance.GetCurrentLevel().ToString();

        //xpbar
        int currLevel = GameManager.Instance.GetCurrentLevel();
        if (currLevel == GameManager.Instance.xpTable.Count)
        {
            xpText.text = GameManager.Instance.experience.ToString() + " total experience points"; //display total xp
            xpBar.localScale = Vector3.one;
        }
        else 
        {
            int currLevelXp = GameManager.Instance.GetXpToLevel(currLevel);
            int prevLevelXp = GameManager.Instance.GetXpToLevel(currLevel -1);


            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.Instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() +" / " + diff;
        }
    }



}
