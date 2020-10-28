using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorCycle : MonoBehaviour
{
    float time = 0f;
    Image background;
    float red = 0f;
    bool redUp = true;
    float green = 0.75f;
    bool greenUp = true;
    float blue = 0.25f;
    bool blueUp = false;

    private void Start()
    {
        background = GameObject.Find("Background").GetComponent<Image>();
    }

    private void Update()
    {
        UpdateColor();
    }

    void UpdateColor()
    {
        time = Time.deltaTime * 0.2f;

        if (red > 1)
            redUp = false;
        if(red < 0)
            redUp = true;

        if (green > 1)
            greenUp = false;
        if(green < 0)
            greenUp = true;

        if (blue > 1)
            blueUp = false;
        if(blue < 0)
            blueUp = true;

        if (redUp)
            red += time;
        else
            red -= time;

        if (blueUp)
            blue += time;
        else
            blue -= time;

        if (greenUp)
            green += time;
        else
            green -= time;

        background.color = new Color(red, green, blue);
    }
}
