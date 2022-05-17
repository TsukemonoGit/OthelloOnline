using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text player1_name;
    [SerializeField]
    private TMPro.TMP_Text player2_name;

    [SerializeField]
    private TMPro.TMP_Text player1_score;
    [SerializeField]
    private TMPro.TMP_Text player2_score;
    [SerializeField]
    private TMPro.TMP_Text result;

    private int scoreBLACK;
    private int scoreWHITE;
    private string syousya;
    public void EndState(Vector2Int score)
    {
        scoreBLACK = score.x;
        scoreWHITE = score.y;
       // GetComponent<OthelloManager>().enabled = false;
        Debug.Log("おわり");
        syousya = scoreBLACK > scoreWHITE ? "黒" : "白";
        ResultView();
       }
    private void ResultView()
    {
        player1_name.text = PhotonNetwork.PlayerList[0].NickName;
        player2_name.text = PhotonNetwork.PlayerList[1].NickName;
        player1_score.text = scoreBLACK.ToString();
        player2_score.text = scoreWHITE.ToString();
        result.text = syousya + "の勝ち";

    }
}
