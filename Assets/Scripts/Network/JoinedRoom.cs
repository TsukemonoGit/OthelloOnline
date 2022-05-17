using Photon.Pun;
using UnityEngine;
//部屋に参加成功したときにおこなわれること

public class JoinedRoom : MonoBehaviourPunCallbacks
{

    //ルームに参加したときに生成されるオブジェクトの名前。ネットワークオブジェクトプレハブは、Resourvesフォルダに入れる
    private string playerAvatar_Prefab_name = "Player";

    private void Start()
    {
        
        //PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedRoom()
    {
        //ルームに参加したプレイヤーのアバターを生成
        PhotonNetwork.Instantiate(playerAvatar_Prefab_name, transform.position, Quaternion.identity);

        //ルームを作成したプレイヤーは、現在のサーバー時刻をゲームの開始時刻に設定する。
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
        }


        //途中参加不可。ルームが満員になったら、以降そのルームへの参加を不許可にする。
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }

     
      
    }
   


}