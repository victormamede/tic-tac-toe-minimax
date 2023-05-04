using System.Collections.Generic;

public struct BoardNode
{
    public BoardState BoardState;
    public List<BoardNode> Children;
    public int move;
    public int depth;

    public float GetScore()
    {
        switch (BoardState.GetWinner())
        {
            case BoardState.Player.CROSS:
                return 1f;
            case BoardState.Player.CIRCLE:
                return -1f;
        }
        return 0f;
    }
}