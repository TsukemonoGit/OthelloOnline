using Photon.Pun;
using UnityEngine;

//RPCで実行したいメソッドを持つスクリプトはPhotonViewコンポーネントと同じゲームオブジェクトにアタッチする。
public class RpcSample : MonoBehaviourPunCallbacks
{
    private void Update()
    {
        //マウスクリックごとに、ルーム内のプレイヤー全員にメッセージを送信する。
        if (Input.GetMouseButtonDown(0))
        {
           // RpcSendMessage("こんにちは");//自プレイヤーの操作
            photonView.RPC(nameof(RpcSendMessage), RpcTarget.AllViaServer, "こんにちは");//ほかプレイヤーのメソッドを実行
        }
    }
        //ほかプレイヤーから実行したいめそっどには[PunRPC]をつける
        [PunRPC]
        private void RpcSendMessage(string message, PhotonMessageInfo info)
        {
        Debug.Log($"{info.Sender.NickName}: {message}");
    }
    
}