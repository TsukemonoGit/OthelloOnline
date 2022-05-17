using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
[SerializeField]
    private bool canClick = true;
    [SerializeField]
    private Vector2 clickedPosition;

    public void CanClick(bool canClick)
    {
        this.canClick = canClick;
    }
    public void ClickedPosition(Vector2 clickedPisition)
    {
        if (canClick)
        {
            this.clickedPosition = clickedPisition;
        }
    }
}
