using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    OthelloManager manager;
    [SerializeField]
    GameRoomTimeDisplay gameRoomTimeDisplay;

    bool isCountDown;
    private int motiTime;
    private int preTime;
    private int nowCount;
    [SerializeField]
    State myColor = State.BLACK;
    [SerializeField]
    TextMeshProUGUI timeLimitText;

    // Start is called before the first frame update
    public void SetMotiTime(int motiTime)
    {
        this.motiTime = motiTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCountDown){ return; }
       int elapsedTime =  Mathf.FloorToInt(gameRoomTimeDisplay.GetElapsedTime());
        if (elapsedTime - preTime >= 1)
        {
            nowCount--;
            timeLimitText.text = nowCount.ToString();
            preTime++;
        }

    }

    [ContextMenu("textStart")]
   public void StartTimer()
    {
        nowCount = motiTime;
        isCountDown = true;
        preTime = Mathf.FloorToInt(gameRoomTimeDisplay.GetElapsedTime());
    }
    [ContextMenu("textStop")]
    public  void StopTimer()
    {
        isCountDown = false;
        nowCount = motiTime;
        timeLimitText.text = nowCount.ToString();
    }
}
