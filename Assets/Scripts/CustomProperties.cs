using ExitGames.Client.Photon;

using UnityEngine;
using TMPro;
//持ち時間カウントダウンのクラス

public  class CustomProperties:MonoBehaviour
    {
    [SerializeField]
    private TextMeshProUGUI BLACK;
   
    [SerializeField]
    private TextMeshProUGUI WHITE;



    private int MotiTime;
    public void SetMotiTime( int motiTime)
    {
        MotiTime = motiTime;
      
        BLACK.text = motiTime.ToString();
        WHITE.text = motiTime.ToString();
    }
    public int GetMotiTime()
    {
        return MotiTime;
    }
 
}

