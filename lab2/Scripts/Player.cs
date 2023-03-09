using Godot;
using Lab2.Scripts.Utilities;

public partial class Player : Area2D
{
	private Vector2 ScreenSize { get; set; }
	private AnimatedSprite2D AnimationSprite { get; set; }

    public void Start(Vector2 position)
    {
        Position = position;

        Show();

        GetNode<CollisionShape2D>(Strings.Nodes.CollisionShape)
            .Disabled = false;
    }

    public void OnBodyEntered(Node2D body)
    {
        Hide(); // Player disappears after being hit.

        EmitSignal(SignalName.Hit);

        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>(Strings.Nodes.CollisionShape)
            .SetDeferred(Strings.GodotProperties.Disabled, true);
    }

    [Export]
	public int Speed { get; set; } = 400;

    [Signal]
    public delegate void HitEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() 
	{
		ScreenSize = GetViewportRect().Size;
		AnimationSprite = GetNode<AnimatedSprite2D>(Strings.Nodes.AnimationSprite);

		Hide();
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) 
	{ 
		var velocity = Vector2.Zero;

		if (Input.IsActionPressed(Strings.Actions.MoveUp))
		{
			velocity.Y -= 1;
		}
        if (Input.IsActionPressed(Strings.Actions.MoveDown))
        {
            velocity.Y += 1;
        }
        if (Input.IsActionPressed(Strings.Actions.MoveRight))
        {
            velocity.X += 1;
        }
        if (Input.IsActionPressed(Strings.Actions.MoveLeft))
        {
            velocity.X -= 1;
        }

		if (velocity.X is not 0) 
		{
			AnimationSprite.Animation = Strings.Animations.HorizontalMovement;
			AnimationSprite.FlipV = false;
			AnimationSprite.FlipH = velocity.X < 0;
		}
		else if (velocity.Y is not 0)
        {
            AnimationSprite.Animation = Strings.Animations.VerticalMovement;
            AnimationSprite.FlipH = false;
            AnimationSprite.FlipV = velocity.Y > 0;
        }

        if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			AnimationSprite.Play();
        }
		else
		{
			AnimationSprite.Stop();
		}

		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );
    }
}