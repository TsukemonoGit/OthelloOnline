using Photon.Pun;
using UnityEngine;

public class SetMotiTime : MonoBehaviourPunCallbacks
{
   // private int motiTime;
 public void SetTime(int motiTime)
    {

        photonView.RPC(nameof(AllSetTime), RpcTarget.All,motiTime);        
        //this.motiTime = motiTime;
        }
    [PunRPC]
    private void AllSetTime(int motiTime)
    {
        GameObject.FindGameObjectWithTag("SampleScene").GetComponent<CustomProperties>().SetMotiTime(motiTime);

    }
    private void Start()
    {
        //二人目が入ってきたとき、一人目のWaitVIEWけす
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            photonView.RPC(nameof(CloseWaitingView), RpcTarget.MasterClient);            
        }
    }
    [PunRPC]
    private void CloseWaitingView()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("WaitView");
        if (obj != null)
        {
            obj.SetActive(false);
        }
    }
}
