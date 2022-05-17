using Photon.Pun;
using UnityEngine;

public class AvatarFireBullet : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Bullet bulletPrefab = default;

    private int nextBulletId = 0;

    private void Update()
    {
        //じぶんのときだけ
        if (photonView.IsMine)
        {
            //左クリックでカーソルの方向にたまを発射する
            if (Input.GetMouseButtonDown(0))
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var direction = mousePosition - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x);
//どうき
//弾を発射するたびに弾のIDを１ずつ増やしていく
                photonView.RPC(nameof(FireBullet),RpcTarget.All,nextBulletId++ , angle);
            }
        }
    }

    // 弾を発射するメソッドを動悸させる
    [PunRPC]
    private void FireBullet(int id , float angle , PhotonMessageInfo info)
    {
        var bullet = Instantiate(bulletPrefab);
        // 弾を発射した時刻に50msのディレイをかける//通信にかかる遅延を考慮
        //PhotonMessageInfoから。RPCを送信した時刻を取得する

        int timeStamp = unchecked(info.SentServerTimestamp + 50);
        
        bullet.Init(id , photonView.OwnerActorNr, transform.position, angle, timeStamp);
    }
}