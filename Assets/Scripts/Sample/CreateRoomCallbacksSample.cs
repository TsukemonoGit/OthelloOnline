using Photon.Pun;
using UnityEngine;

public class CreateRoomCallbacksSample : MonoBehaviourPunCallbacks
{

    //ルーム作成が成功したときに呼ばれるコールバック
    public override void OnCreatedRoom()
    {
        Debug.Log("ルームの作成に成功しました。");
    }

    //ルームの作成が失敗したときに呼ばれるコールバック
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"ルームの作成に失敗しました: {message}");
    }

}
