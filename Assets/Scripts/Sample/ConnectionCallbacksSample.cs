using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ConnectionCallbacksSample : MonoBehaviourPunCallbacks
{
    //マスターサーバーへの接続が成功したときに呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーに接続しました");
    }

    //Photonのサーバーから切断されたときに呼ばれるコールバック
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"サーバーとの接続が切断されました: {cause.ToString()}");
    }

}
