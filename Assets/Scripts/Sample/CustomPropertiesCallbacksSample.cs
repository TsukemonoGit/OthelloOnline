﻿using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CustomPropertiesCallbacksSample : MonoBehaviourPunCallbacks
{

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        //カスタムプロパティが更新されたプレイヤーのプレイヤー名とIDをコンソールに出力
        Debug.Log($"{targetPlayer.NickName}({targetPlayer.ActorNumber})");


        //更新された　プレイヤー　のカスタムプロパティのペアをコンソールに出力
        foreach(var prop in changedProps)
        {
            Debug.Log($"{prop.Key}: {prop.Value}");
        }

    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        //更新された　ルーム　のカスタムプロパティのペアをコンソールに出力
        foreach(var prop in propertiesThatChanged)
        {
            Debug.Log($"{prop.Key}: {prop.Value}");

        }
    }
}
