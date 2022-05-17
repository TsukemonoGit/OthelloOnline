using Photon.Pun;
using UnityEngine;


public class UpdateUserName : MonoBehaviourPunCallbacks
{
    private PlayerUI[] playerUIs=new PlayerUI[2];
    // Start is called before the first frame update
    void Start()
    {

        //２人目以降が入ったらそれまでに入ってた人のとこのユーザー名も更新
        // RpcSetPlayerName();
        //  if (PhotonNetwork.PlayerList.Length > 1)
        {
            photonView.RPC(nameof(RpcSetPlayerName), RpcTarget.All);
        }
    }

    //誰かが参加するたびにUI更新したい
    [PunRPC]
    private void RpcSetPlayerName()
    {
        playerUIs[0] = GameObject.FindGameObjectWithTag("UserNameBLACK").GetComponent<PlayerUI>();
        playerUIs[1] = GameObject.FindGameObjectWithTag("UserNameWHITE").GetComponent<PlayerUI>();

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            playerUIs[i].SetUserName(PhotonNetwork.PlayerList[i].NickName);
        }
    }
}
