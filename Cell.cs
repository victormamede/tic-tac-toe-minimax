namespace TicTacToe;

public partial class Cell : Area2D
{
    public int BoardIndex { get; set; } = 0;

    private CellState _state = CellState.FREE;


    private Node2D _cross;
    private Node2D _circle;
    private Node2D _highlight;
    private Label _score;

    public override void _Ready()
    {
        _cross = GetNode<Node2D>("Cross");
        _circle = GetNode<Node2D>("Circle");
        _highlight = GetNode<Node2D>("Highlight");
        _score = GetNode<Label>("Score");

        Redraw();
    }

    public void ChangeState(CellState newState)
    {
        _state = newState;
        Redraw();
    }

    public void SetHighlight(bool highlighted) => _highlight.Visible = highlighted;

    public void SetScore(float score)
    {
        if (score == 0f)
            _score.Text = "";
        else
            _score.Text = score > 0f ? "x" : "o";
    }

    private void Redraw()
    {
        _circle.Visible = _state == CellState.CIRCLE;
        _cross.Visible = _state == CellState.CROSS;
        _score.Visible = _state == CellState.FREE;
    }
}
