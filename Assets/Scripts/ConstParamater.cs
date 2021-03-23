//�萔�܂Ƃ߃X�N���v�g

/// <summary>
/// �^�[��
/// </summary>
public static class Turn
{
    /// <summary>
    /// �v���C���[�̃^�[��
    /// </summary>
    public const int PLAYERTURN = 1;
    /// <summary>
    /// �G�̃^�[��
    /// </summary>
    public const int ENEMYTURN = -1;
}


/// <summary>
/// ������Enum
/// 1=��=Up
/// 2=�E=Right
/// 3=��=Down
/// 4=��=Left
/// </summary>
public enum DirectionEnum
{
    Up = 1,
    Right = 2,
    Down = 3,
    Left = 4
}

/// <summary>
/// �Ֆʔz��̐��l
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