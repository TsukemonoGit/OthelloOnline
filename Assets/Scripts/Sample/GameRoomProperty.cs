using ExitGames.Client.Photon;
using Photon.Realtime;

//ゲームの開始時間を設定取得する拡張メソッド
public static class GameRoomProperty
{
    private const string KeyStartTime = "StartTime";

    private static readonly Hashtable propsToSet = new Hashtable();

    //ゲームの開始時刻が設定されていれば取得する
    public static bool TryGetStartTime(this Room room , out int timestamp)
    {
        if(room.CustomProperties[KeyStartTime] is int value)
        {
            timestamp = value;
            return true;
        }
        else
        {
            timestamp = 0;
            return false;
        }
    }

    //ゲームの開始時刻を設定する
    public static void SetStartTime(this Room room, int timestamp)
    {
        propsToSet[KeyStartTime] = timestamp;
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
}