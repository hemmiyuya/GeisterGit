//定数まとめスクリプト

/// <summary>
/// ターン
/// </summary>
public static class Turn
{
    /// <summary>
    /// プレイヤーのターン
    /// </summary>
    public const int PLAYERTURN = 1;
    /// <summary>
    /// 敵のターン
    /// </summary>
    public const int ENEMYTURN = -1;
}


/// <summary>
/// 向きのEnum
/// 1=上=Up
/// 2=右=Right
/// 3=下=Down
/// 4=左=Left
/// </summary>
public enum DirectionEnum
{
    Up = 1,
    Right = 2,
    Down = 3,
    Left = 4
}

/// <summary>
/// 盤面配列の数値
/// Null=0
/// PlayerBlue=01,
/// PlayerRed=02,
/// EnemyBlue=11,
/// EnumyRed=12,
/// </summary>
public enum BoardValue
{
    Null=0,
    PlayerBlue=01,
    PlayerRed=02,
    EnemyBlue=11,
    EnemyRed=12,
}