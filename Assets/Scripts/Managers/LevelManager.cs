using UnityEngine;

public static class LevelManager
{
    private static int _SelectedLevel = 1;
    private static int _maxLevelReached = 1;

    public static int MaxLevelAvailable = 3;
    public static int SelectedLevel
    {
        get
        {
            return SelectedLevel;
        }
        set
        {
            SelectedLevel = Mathf.Clamp(value, 1, MaxLevelReached);
        }
    }

    public static int MaxLevelReached
    {
        get
        {
            return MaxLevelReached;
        }
        set
        {
            MaxLevelReached = Mathf.Max(MaxLevelReached, value, MaxLevelAvailable);
        }
    }
}
