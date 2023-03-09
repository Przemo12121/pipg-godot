using Godot;
using Lab2.Scripts.Utilities;
using System.Threading.Tasks;

public partial class HUD : CanvasLayer
{
    private Timer MesageTimer { get; set; }
    private Label Message { get; set; }
    private Label ScoreLabel { get; set; }
    private Button StartButton { get; set; }

    [Signal]
    public delegate void StartGameEventHandler();

    public void OnStartButtonPressed()
    {
        StartButton.Hide();
        EmitSignal(SignalName.StartGame);
    }

    public void OnMessageTimerTimeout()
    {
        Message.Hide();
    }

    public void UpdateScore(int score)
    {
        ScoreLabel.Text = score.ToString();
    }

    public void ShowMessage(string text)
    {
        Message.Text = text;
        Message.Show();

        MesageTimer.Start();
    }

    async public void ShowGameOver()
    {
        ShowMessage(Strings.Messages.Title);

        await ToSignal(MesageTimer, Strings.Signals.Timeout);
        await Task.Delay(1000);

        Message.Text = Strings.Messages.GameOver;
        Message.Show();

        await Task.Delay(1000);
        await ToSignal(GetTree().CreateTimer(1), Strings.Signals.Timeout);

        StartButton.Show();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        MesageTimer = GetNode<Timer>(Strings.Nodes.MessageTimer);
        Message = GetNode<Label>(Strings.Nodes.Message);
        StartButton = GetNode<Button>(Strings.Nodes.StartButton);
        ScoreLabel = GetNode<Label>(Strings.Nodes.ScoreLabel);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
