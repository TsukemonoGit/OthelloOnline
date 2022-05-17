using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 origin; //弾を発射した時刻の座標
    private Vector3 velocity;
    private int timeStamp;  //弾を発射した時刻
    

    //----弾に弾のIDと弾を発射したプレイヤーのIDを持たせる----
    //弾のIDを返すプロパティ
    public int Id { get; private set; }
    //弾を発射したプレイヤーのIDを返すプロパティ
    public int OwnerId { get; private set; }    

    //  同じ弾かどうかをIDで判定するメソッド
    public bool Equals(int id , int ownerId)
    {
        return (id == Id && ownerId == OwnerId); 
    }

    //初期化
    public void Init(int id, int ownerId, Vector3 origin, float angle , int timeStamp)
    {
        Id = id;
        OwnerId = ownerId;
        this.origin = origin;
        velocity = 9f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
        this.timeStamp = timeStamp;

        //一度だけ直接Update()を呼んで、transform.positionの初期値を決める
        Update();
    }


    private void Update()
    {
        //弾を発射した時刻から現在時刻までの経過時間を求める
        float elapsedTime = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - timeStamp)/1000f);
        //弾を発射した時点での座標・速度・経過時間から現在の座標を求める
        transform.position = origin + velocity * elapsedTime;

    }

    //画面外に移動したら削除する
    //（Unityのエディター上ではシーンビューの画面も影響するので注意
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}