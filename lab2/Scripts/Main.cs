using Godot;
using Lab2.Scripts.Utilities;

public partial class Main : Node
{
    [Export]
	public PackedScene MobScene { get; set; }
    public int Score { get; set; }

    public void OnScoreTimerTimeout()
    {
        Score++;
    }

    public void OnMobTimerTimeout()
    {
        // Note: Normally it is best to use explicit types rather than the `var`
        // keyword. However, var is acceptable to use here because the types are
        // obviously Mob and PathFollow2D, since they appear later on the line.

        // Create a new instance of the Mob scene.
        var mob = MobScene.Instantiate<Mob>();

        // Choose a random location on Path2D.
        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();

        // Set the mob's direction perpendicular to the path direction.
        float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

        // Set the mob's position to a random location.
        mob.Position = mobSpawnLocation.Position;

        // Add some randomness to the direction.
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        // Choose the velocity.
        var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
        mob.LinearVelocity = velocity.Rotated(direction);

        // Spawn the mob by adding it to the Main scene.
        AddChild(mob);
    }

    public void OnStartTimerTimeout()
    {
        GetNode<Timer>(Strings.Nodes.MobTimer).Start();
        GetNode<Timer>(Strings.Nodes.ScoreTimer).Start();
    }

    public void GameOver()
    {
        GetNode<Timer>(Strings.Nodes.MobTimer).Stop();
        GetNode<Timer>(Strings.Nodes.ScoreTimer).Stop();
    }

    public void NewGame()
    {
        Score = 0;

        var player = GetNode<Player>(Strings.Nodes.Player);
        var startPosition = GetNode<Marker2D>(Strings.Nodes.StartPosition);
        player.Start(startPosition.Position);

        GetNode<Timer>(Strings.Nodes.StartTimer).Start();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        NewGame();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
