using Godot;
using Lab2.Scripts.Utilities;

public partial class Mob : RigidBody2D
{
    private AnimatedSprite2D AnimationSprite { get; set; }

    public void OnVisibleOnScreenNotifier2DScreenExited()
    {
        QueueFree();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimationSprite = GetNode<AnimatedSprite2D>(Strings.Nodes.AnimationSprite);
        AnimationSprite.Play();
        string[] mobTypes = AnimationSprite.SpriteFrames.GetAnimationNames();
        AnimationSprite.Animation = mobTypes[GD.Randi() % mobTypes.Length];
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
