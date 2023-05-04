namespace TicTacToe;

using System.Collections.Generic;

public struct BoardNode
{
    public BoardState BoardState { get; set; }
    public List<BoardNode> Children { get; set; }
    public int Move { get; set; }
    public int Depth { get; set; }

    public float GetScore() => BoardState.GetWinner() switch
    {
        BoardState.Player.CROSS => 1f,
        BoardState.Player.CIRCLE => -1f,
        _ => 0f,
    };
}