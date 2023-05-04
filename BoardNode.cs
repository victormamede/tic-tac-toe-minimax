namespace TicTacToe;

using System.Collections.Generic;

public struct BoardNode
{
    public BoardState BoardState;
    public List<BoardNode> Children;
    public int move;
    public int depth;

    public float GetScore() => BoardState.GetWinner() switch
    {
        BoardState.Player.CROSS => 1f,
        BoardState.Player.CIRCLE => -1f,
        _ => 0f,
    };
}