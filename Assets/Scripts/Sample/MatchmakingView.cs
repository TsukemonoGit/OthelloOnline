using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchmakingView : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject MotiTimeView;
    [SerializeField]
    private GameObject WaitView;

    [SerializeField]
    private TMP_InputField usernameInputField = default;    //add
    
    [SerializeField]
    private TMP_InputField passwordInputField = default;
    [SerializeField]
    private Button joinRoomButton = default;

    private CanvasGroup canvasGroup;

    private bool isPassword=false;//add
    private bool isUserName=false;
        
        private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        //マスターサーバーに接続するまでは、入力できないようにする。
        canvasGroup.interactable = false;

        //パスワードを入力する前は、ルーム参加ボタンを押せないようにする。
        joinRoomButton.interactable = false;

        passwordInputField.onValueChanged.AddListener(OnPasswordInputFieldValueChanged);
        usernameInputField.onValueChanged.AddListener(OnUserNameInputFieldValueChanged);
        joinRoomButton.onClick.AddListener(OnJoinRoomButtonClick);
    }

    public override void OnConnectedToMaster()
    {
        //マスターサーバーに接続したら、入力できるようにする
        canvasGroup.interactable = true;
    }

    private void OnPasswordInputFieldValueChanged(string value)
    {
        //パスワードを4桁入力したときのみ、ルーム参加ボタンを押せるようにする
isPassword=     (value.Length == 4);

        joinRoomButton.interactable = isPassword && isUserName;
    }
    private void OnUserNameInputFieldValueChanged(string value)
    {
        //userName1文字以上入力したときのみ参加ボタンを押せるようにする
        isUserName = value.Length > 0;
        joinRoomButton.interactable = isPassword && isUserName;
    
    }

    private void OnJoinRoomButtonClick()
    {
        //ルーム参加処理中は、入力できないようにする
        canvasGroup.interactable = false;

        //ルームを非公開に設定する（新規でルームを作成する場合）
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = false;
        PhotonNetwork.NickName = usernameInputField.text;
      //パスワードと同じ名前のルームに参加する（ルームが存在しなければ作成してから参加する）
      PhotonNetwork.JoinOrCreateRoom(passwordInputField.text, roomOptions, TypedLobby.Default);

    }

    public override void OnJoinedRoom()
    {
        //マスタークライアントは、対戦相手待ちと、持ち時間設定を行う。
        if (PhotonNetwork.IsMasterClient)
        {
            WaitView.SetActive(true);
            MotiTimeView.SetActive(true);
        }
        //else
        //{
        //    WaitView.SetActive(false);
        //    //対戦相手が入ってきたら、WaitViewを消したい

        //}
        //ルームへの参加が成功したら、UIを非表示にする。
        gameObject.SetActive(false);

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //ルームへの参加が失敗したら、パスワードを再び入力できるようにする。
        passwordInputField.text = string.Empty;
        canvasGroup.interactable = true;
    }
}