using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectMask2D))]
[RequireComponent(typeof(Image))]//set transparency to 0, it's used to make the scoll work on gaps
public class ScrollHandlerComponent : MonoBehaviour, IScrollHandler
{
    public float ElementHeight;

    float scroll = 0f;
    float maxScroll = 0f;
    const float minScroll = 0f;

    public void OnScroll(PointerEventData eventData)
    {
        CalculateScrollLimits();

        float thisScroll = -eventData.scrollDelta.y * 25;

        if(scroll + thisScroll > maxScroll)
        {
            thisScroll = maxScroll - scroll;
            scroll = maxScroll;
        } 
        else if (scroll + thisScroll < minScroll)
        {
            thisScroll = minScroll - scroll;
            scroll = minScroll;
        }
        else
        {
            scroll += thisScroll;
        }


        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).Translate(new Vector3(0, thisScroll, 0));
    }

    private void CalculateScrollLimits()
    {
        if(transform.childCount >= 1)
        maxScroll = (transform.childCount - 1) * ElementHeight;
    }
}
