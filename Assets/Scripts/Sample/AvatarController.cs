using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

//MonoBehaviourPunCallbacksを継承して、phoneViewプロパティを使えるようにする
//IPunObservable で監視対象（定期的に呼び出される同期対象）にする
public class AvatarController : MonoBehaviourPunCallbacks, IPunObservable
{
    //constにしたらしょきかのとこにこの変数使える
    private const float MaxStamina = 6f;

    [SerializeField]
    private Image staminaBar = default;

    private float currentStamina = MaxStamina;

    private void Update()
    {

        //自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine)
        {
            var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            //  transform.Translate(6f * Time.deltaTime * input.normalized);//(入力による移動同期テスト)
            if (input.sqrMagnitude > 0f)
            {
                //入力があったら、スタミナを減少させる
                currentStamina = Mathf.Clamp( currentStamina - Time.deltaTime, 0f, MaxStamina);    //スタミナの更新
                transform.Translate(6f * Time.deltaTime * input.normalized);    //(入力による移動同期テスト)
            }
            else
            {
                //入力がなかったら、スタミナを回復させる
                currentStamina = Mathf.Clamp(currentStamina + Time.deltaTime  * 2, 0f, MaxStamina);
            }
        }
        //スタミナをゲージに反映させる
        staminaBar.fillAmount = currentStamina / MaxStamina;
    }

    //↓これが、定期的に呼ばれる
    //IsWritingが、自分のデータを他に送信する処理、elseが他プレイヤーのデータを読み込んで動悸する処理
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //自分のアバターのスタミナをほかプレイヤー？に送信
            stream.SendNext(currentStamina);
        }
        else
        {
            //ほかプレイヤーのアバターのスタミナを受信する。
            currentStamina = (float)stream.ReceiveNext();
        }
        
    }
}