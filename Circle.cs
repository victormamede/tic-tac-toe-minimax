namespace TicTacToe;

[Tool]
public partial class Circle : Node2D
{
    [Export]
    public float Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            QueueRedraw();
        }
    }
    private float _radius = 0;
    [Export]
    public float Width
    {
        get => _width;
        set
        {
            _width = value;
            QueueRedraw();
        }
    }
    private float _width = -1f;

    [Export]
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            QueueRedraw();
        }
    }
    private Color _color = new Color(1f, 1f, 1f);


    public override void _Draw()
    {
        DrawArc(Vector2.Zero, _radius, 0, 2 * Mathf.Pi, 36, _color, _width);
    }
}
