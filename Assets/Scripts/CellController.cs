using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CellController : MonoBehaviour
{
   public  ClickManager clickManager;
    public Vector2Int myPosition;

    public void Init(Vector2Int position)
    {
        clickManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ClickManager>();
        myPosition = position;
    }

    //public void ClickCell()
    //{
    //    clickManager.ClickedPosition ( myPosition);
    //    Debug.Log("Click");
        
            
    //}
}
