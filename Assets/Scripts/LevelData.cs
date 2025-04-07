using System;

[Serializable]
public class Level // Ensure this remains a data structure
{
    public int level_id;
    public bool playable;
    public int stars;
    public int replaytimes;
}

[Serializable]
public class Player
{
    public string gender;
}

[Serializable]
public class LevelAccounts
{
    public Level[] levels;
    public Player[] player; // ✅ Ensuring structure remains the same
}
