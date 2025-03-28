using UnityEngine;

public static class LevelManager
{
    private static int _selectedLevel = 1;
    private static int _maxLevelReached = 1;

    public static int MaxLevelAvailable = 3;

    public static int SelectedLevel
    {
        get => _selectedLevel;
        set => _selectedLevel = Mathf.Clamp(value, 1, _maxLevelReached);
    }

    public static int MaxLevelReached
    {
        get => _maxLevelReached;
        set => _maxLevelReached = Mathf.Clamp(value, 1, MaxLevelAvailable);
    }
}
