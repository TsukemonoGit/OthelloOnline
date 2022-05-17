using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;

public class MatchmakingViewVer2 : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject MotiTimeView;
    [SerializeField]
    private GameObject WaitView;
    [SerializeField]
    public  TMP_InputField usernameInputField = default;    //add

    private RoomList roomList = new RoomList();
    public RoomButton[] roomButtons;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        //いんぷっとふぃーるどSDD
        usernameInputField.onValueChanged.AddListener(OnUserNameInputFieldValueChanged);

        canvasGroup = GetComponent<CanvasGroup>();
        //ロビーに参加するまでは、すべてのルーム参加ボタンを押せないようにする。
        canvasGroup.interactable = false;

        //すべてのルームボタンを初期化する。
        int roomId = 1;
        for(int i = 0; i < roomButtons.Length; i++)
        {
            roomButtons[i].Init(this, roomId++);
        }
        //foreach(Transform child in transform)
        //{
        //    if(child.TryGetComponent<RoomButton>(out var roomButton))
        //    {
        //        roomButton.Init(this, roomId++);
        //        roomButtonList.Add(roomButton);
        //    }
        //}
    }
    private void OnUserNameInputFieldValueChanged(string value)
    {
        //userName1文字以上入力したときのみ参加ボタンを押せるようにする
        canvasGroup.interactable = value.Length > 0;

    }
    public override void OnJoinedLobby()
    {
        //ロビーに参加したら、ルーム参加ボタンを押せるようにする
      //  canvasGroup.interactable = true;
    }
    public override void OnRoomListUpdate(List<RoomInfo> changedRoomList)
    {
        roomList.Update(changedRoomList);

        //すべてのルーム参加ボタンの表示を更新する
        foreach (var roomButton in roomButtons)
        {
            if (roomList.TryGetRoomInfo(roomButton.RoomName, out var roomInfo))
            {
                roomButton.SetPlayerCount(roomInfo.PlayerCount);
            }
            else
            {
                roomButton.SetPlayerCount(0);
            }
        }

    }

    public void OnJoiningRoom()
    {
        //ルーム参加処理中はすべてのルーム参加ボタンを押せないようにする
        canvasGroup.interactable = false;
       
    }

    public override void OnJoinedRoom()
    {
        //マスタークライアントは、対戦相手待ちと、持ち時間設定を行う。
        if (PhotonNetwork.IsMasterClient)
        {
            WaitView.SetActive(true);
            MotiTimeView.SetActive(true);
        }

        //ルームへの参加が成功したら、UIを非表示にする
        gameObject.SetActive(false);

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //ルームへの参加が失敗したら、再びルーム参加ボタンを押せるようにする
        canvasGroup.interactable = true;
    }
}
