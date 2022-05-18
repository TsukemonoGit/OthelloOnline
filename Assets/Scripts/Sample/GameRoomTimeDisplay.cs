using Photon.Pun;
using TMPro;
using UnityEngine;

public class GameRoomTimeDisplay : MonoBehaviour
{
    private TextMeshProUGUI timeLabel;
    float elapsedTime;
    private void Start()
    {
        timeLabel = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        //まだルームに参加していない場合は更新しない
        if (!PhotonNetwork.InRoom) { return; }
        //まだゲームの開始時刻が設定されていない場合は更新しない
        if(!PhotonNetwork.CurrentRoom.TryGetStartTime(out int timestamp)) { return; }

        //ゲームの経過時間を求めて、少数第一位まで表示する
        elapsedTime = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - timestamp) / 1000f);

        ViewTime(elapsedTime);
      }
    string preTime;
    void ViewTime(float elapsedTime)
    {
        
        string timeString = (Mathf.Floor(elapsedTime / 60).ToString("F0") + " : " + (elapsedTime % 60).ToString("00"));
        if (timeString != preTime)
        {
            timeLabel.text = timeString;
        }
        preTime = timeString;
    }
    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}