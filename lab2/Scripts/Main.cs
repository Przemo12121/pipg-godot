using Godot;
using Lab2.Scripts.Utilities;
using System;

public partial class Main : Node
{
    [Export]
	public PackedScene MobScene { get; set; }
    
    private HUD HUD { get; set; }
    public int Score { get; set; }
    private PathFollow2D MobsPath { get; set; }
    private Timer MobTimer { get; set; }
    private Timer ScoreTimer { get; set; }
    private Timer StartTimer { get; set; }
    private Marker2D StartPosition { get; set; }
    private Player Player { get; set; }
    private AudioStreamPlayer2D Music { get; set; }
    private AudioStreamPlayer2D DeathSound { get; set; }

    public void OnScoreTimerTimeout()
    {
        Score++;
        HUD.UpdateScore(Score);
    }

    public void OnMobTimerTimeout()
    {
        // Create a new instance of the Mob scene.
        var mob = MobScene.Instantiate<Mob>();

        // Choose a random location on Path2D.
        MobsPath.ProgressRatio = GD.Randf();

        // Set the mob's direction perpendicular to the path direction.
        float direction = MobsPath.Rotation + Mathf.Pi / 2;

        // Set the mob's position to a random location.
        mob.Position = MobsPath.Position;

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
        MobTimer.Start();
        ScoreTimer.Start();
    }

    public void GameOver()
    {
        Music.Stop();
        DeathSound.Play();

        MobTimer.Stop();
        ScoreTimer.Stop();
        HUD.ShowGameOver();
    }

    public void NewGame()
    {
        Music.Play();
        GetTree().CallGroup(Strings.Groups.Mobs, "queue_free");

        Score = 0;
        HUD.UpdateScore(Score);
        HUD.ShowMessage(Strings.Messages.Start);
        
        Player.Start(StartPosition.Position);
        StartTimer.Start();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        Music = GetNode<AudioStreamPlayer2D>(Strings.Nodes.Music);
        DeathSound = GetNode<AudioStreamPlayer2D>(Strings.Nodes.DeathSound);
        Player = GetNode<Player>(Strings.Nodes.Player);
        StartPosition = GetNode<Marker2D>(Strings.Nodes.StartPosition);
        MobTimer = GetNode<Timer>(Strings.Nodes.MobTimer);
        StartTimer = GetNode<Timer>(Strings.Nodes.StartTimer);
        ScoreTimer = GetNode<Timer>(Strings.Nodes.ScoreTimer);
        MobsPath = GetNode<PathFollow2D>(Strings.Nodes.MobsPath);
        HUD = GetNode<HUD>(Strings.Nodes.HUD);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
