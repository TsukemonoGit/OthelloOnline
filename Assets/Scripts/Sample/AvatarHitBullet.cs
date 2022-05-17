using Photon.Pun;
using UnityEngine;

public class AvatarHitBullet : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            //あたったのが弾
            if(other.TryGetComponent<Bullet>(out var bullet))
            {
                //ほかプレイヤーの弾が当たった
                if (bullet.OwnerId != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    //あたった弾を消す
                    photonView.RPC(nameof(HitBullet), RpcTarget.All, bullet.Id, bullet.OwnerId);
                }
            }
        }
    }

    //存在する弾から当てた弾と同じID、OWNERIDの弾を探して、ですとろい
    [PunRPC]
    private void HitBullet(int id, int ownerId)
    {
        //FindObjectsOfType()はかなり処理が重い関数です。ここではサンプルコードを簡潔にするために使用していますが、実践では弾をリストで管理するクラスなどを作成して、処理を高速化する方が良いでしょう。
        var bullets = FindObjectsOfType<Bullet>();
       foreach(var bullet in bullets)
        {
            if (bullet.Equals(id, ownerId))
            {
                //自身が発射した玉が当たった場合には、自身のスコアを増やす
                if (ownerId == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    PhotonNetwork.LocalPlayer.AddScore(10);             //拡張メソッド　PlayerPropertiesExtensions　から
                }
                Destroy(bullet.gameObject);
                break;
            }
        }
    }
}