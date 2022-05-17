using Photon.Pun;
//using Photon.Realtime;
using UnityEngine;


//MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{
    //ルームに参加したときに生成されるAvatarの名前。ネットワークオブジェクトプレハブは、Resourvesフォルダに入れる
    private string playerAvatar_Prefab_name = "Avatar";

    private void Start()
    {
        //プレイヤー自身の名前を"Player"に設定する
       // PhotonNetwork.NickName = "Player";
        //PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    ////マスターサーバーへの接続が成功したときに呼ばれるコールバック
    //public override void OnConnectedToMaster()
    //{
    //    //ランダムなルームに参加する
    //    PhotonNetwork.JoinRandomRoom();
    //    ////"Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
    //    //PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    //}

    ////ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    //public override void OnJoinRandomFailed(short returnCode, string message)
    //{
    //    //ルーム設定
    //    //ルームの参加人数を二人に設定する
    //    var roomOptions = new RoomOptions();
    //    roomOptions.MaxPlayers = 2;

    //    PhotonNetwork.CreateRoom(null, roomOptions);
    //}

    //ゲームサーバーへの接続が成功したときに呼ばれるコールバック
    public override void OnJoinedRoom()
    {

        //ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する。
        var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate(playerAvatar_Prefab_name, position, Quaternion.identity);

        //ルームを作成したプレイヤーは、現在のサーバー時刻をゲームの開始時刻に設定する。
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
        }

        //途中参加不可。ルームが満員になったら、以降そのルームへの参加を不許可にする
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }


}