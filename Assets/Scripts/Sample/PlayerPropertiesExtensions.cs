using ExitGames.Client.Photon;
using Photon.Realtime;

//プレイヤーのカスタムプロパティ用の拡張メソッド
public static class PlayerPropertiesExtensions
{
    private const string ScoreKey = "Score";
    private const string MessageKey = "Message";


    private static readonly Hashtable propsToSet = new Hashtable();

    //プレイヤーのスコアを取得する
    public static int GetScore(this Player player)
    {
        return ( player.CustomProperties[ScoreKey] is int score) ? score : 0; 
    }

    //プレイヤーのメッセージを取得する
    public static string GetMessage(this Player player)
    {
        return (player.CustomProperties[MessageKey] is string message) ? message : string.Empty;
    }
    
    //プレイヤーのスコアを設定する
    public static void SetScore(this Player player, int score)
    {
        propsToSet[ScoreKey] = score;
   //     player.SetCustomProperties(propsToSet);
   //     propsToSet.Clear();
    }
    
    //プレイヤーのメッセージを設定する
    public static void SetMessage(this Player player, string message)
    {
        propsToSet[MessageKey] = message;
       // player.SetCustomProperties(propsToSet);
    //    propsToSet.Clear();
    }

    //プレイヤーのカスタムプロパティを送信する
    public static void SendPlayerProperties(this Player player)
    {
        if (propsToSet.Count > 0)
        {
            player.SetCustomProperties(propsToSet);
            propsToSet.Clear();
        }
    }

    //プレイヤーのスコアを加算する
    public static void AddScore(this Player player, int value)
    {
        propsToSet[ScoreKey] = player.GetScore() + value;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
}