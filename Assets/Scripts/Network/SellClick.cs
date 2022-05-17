using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SellClick : MonoBehaviourPunCallbacks
{
    public int id;
 //   public PhotonView photonView;
    private OthelloManager manager;
    State myState;
    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーのID取得
        //photonView = this.GetComponent<PhotonView>();
        id = photonView.OwnerActorNr;
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<OthelloManager>();
        if (id == 1) { myState = State.BLACK; }
        else { myState = State.WHITE; }
    }

    // Update is called once per frame
    void Update()
    {
       
       if (photonView.IsMine)
        {
            //自分のターンじゃなかったら何もしない
            if (myState != manager.GetState()) { return; }

            //マウスクリックした場所からRayを飛ばし、オブジェクトがあればTRUE

            //クリック判定
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Click");
                Ray ray = new Ray();
                RaycastHit hit = new RaycastHit();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //マウスクリックした場所からRayを飛ばす。ヒットしたらTRUE
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Hit");
                      CellController controller =  hit.collider.gameObject.GetComponent<CellController>();
                    if (controller != null){
                        //Debug.Log(controller.myPosition);
                      //  manager.testPosition = controller.myPosition;
                        
                        //クリックした座標がリバース可能化チェック
                        bool canReverse = manager.PositionCanReverse(controller.myPosition);
                        if (canReverse)
                        {
                            photonView.RPC(nameof(Reverse), RpcTarget.All,controller.myPosition.x, controller.myPosition.y);
                        }
                    }
                }

                    

            }
        }
    }
    [PunRPC]
    private void Reverse(int  positionX,int positionY)
    {
        manager.Reverse(myState,new Vector2Int(positionX,positionY));
    }
}
