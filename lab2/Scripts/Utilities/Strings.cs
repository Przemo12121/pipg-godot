namespace Lab2.Scripts.Utilities;

public class Strings
{
    public static class Actions
    {
        public static readonly string MoveUp = "MoveUp";
        public static readonly string MoveDown = "MoveDown";
        public static readonly string MoveLeft = "MoveLeft";
        public static readonly string MoveRight = "MoveRight";
    }

    public static class Nodes
    {
        public static readonly string AnimationSprite = "AnimatedSprite2D";
        public static readonly string CollisionShape = "CollisionShape2D";
        public static readonly string Player = "Player";
        public static readonly string StartPosition = "StartPosition";
        public static readonly string StartTimer = "StartTimer";
        public static readonly string MobTimer = "MobTimer";
        public static readonly string ScoreTimer = "ScoreTimer";
        public static readonly string Message = "Message";
        public static readonly string MessageTimer = "MessageTimer";
        public static readonly string StartButton = "StartButton";
        public static readonly string ScoreLabel = "ScoreLabel";
        public static readonly string HUD = "HUD";
        public static readonly string DeathSound = "DeathSound";
        public static readonly string Music = "Music";
        public static readonly string MobsPath = "MobPath/MobSpawnLocation";
    }

    public static class Animations
    {
        public static readonly string VerticalMovement = "up";
        public static readonly string HorizontalMovement = "walk";
    }

    public static class GodotProperties
    {
        public static readonly string Disabled = "disabled";
    }

    public static class Signals
    {
        public static readonly string Timeout = "timeout";
    }

    public static class Messages
    {
        public static readonly string GameOver = "Game Over";
        public static readonly string Title = "Dodge the\nCreeps!";
        public static readonly string Start = "Get Ready!";
    }

    public static class Groups
    {
        public static readonly string Mobs = "Mobs";
    }
}
