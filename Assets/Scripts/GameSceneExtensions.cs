public static class GameSceneExtensions
{
    public static string GetSceneName(this GameScene scene) =>
        scene switch
        {
            GameScene.Overworld => "Overworld",
            GameScene.Level0 => "OmniScene",
            GameScene.Level1 => "1",
            GameScene.Level2 => "2",
            GameScene.Level3 => "3",
            GameScene.Level4 => "4",
            GameScene.Level5 => "5",
        };
}
