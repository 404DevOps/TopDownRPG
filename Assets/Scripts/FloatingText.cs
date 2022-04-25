using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool isActive;
    public GameObject gameObject;
    public Text text;

    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        isActive = true;
        lastShown = Time.time;
        gameObject.SetActive(true);

    }
    public void Hide()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    public void UpdateFloatingText()
    {
        if (!isActive)
            return;

        if (Time.time - lastShown > duration)
            Hide();

        gameObject.transform.position += motion * Time.deltaTime;
    }
}
