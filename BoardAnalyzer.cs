namespace TicTacToe;

using System.Collections.Generic;

public partial class BoardAnalyzer : Node
{
    [Signal]
    public delegate void BestMoveCalculatedEventHandler(int bestMove);
    [Signal]
    public delegate void ScoreCalculatedEventHandler(int index, float score);

    [Export]
    public int MaxDepth { get; set; } = 9;

    public void AnalyzeBoardState(BoardState boardState)
    {
        int bestMove = GetBestMove(boardState);
        EmitSignal(SignalName.BestMoveCalculated, bestMove);
    }

    private int GetBestMove(BoardState boardState)
    {
        BoardNode rootNode = GetBoardNode(boardState);
        if (rootNode.Children.Count == 0) return -1;


        int scoreMultiplier = rootNode.BoardState.CurrentPlayer == BoardState.Player.CROSS ? 1 : -1;
        var orderedNodes = rootNode.Children.OrderByDescending(child =>
        {
            float score = GetScore(child);
            EmitSignal(SignalName.ScoreCalculated, child.Move, score);
            return score * scoreMultiplier;
        });

        return orderedNodes.First().Move;
    }

    private float GetScore(BoardNode boardNode)
    {
        if (boardNode.Children.Count == 0) return boardNode.GetScore() / (float)boardNode.Depth;

        if (boardNode.BoardState.CurrentPlayer == BoardState.Player.CROSS)
            return boardNode.Children.Max(node => GetScore(node));
        else
            return boardNode.Children.Min(node => GetScore(node));
    }

    private BoardNode GetBoardNode(BoardState boardState, int move = -1, int depth = 0)
    {
        List<BoardNode> children = new();

        if (depth < MaxDepth && !boardState.GameEnded())
        {
            for (int i = 0; i < 9; i++)
            {
                if (boardState[i] != CellState.FREE) continue;

                var newBoardState = boardState.Clone();
                newBoardState.Play(i);

                children.Add(GetBoardNode(newBoardState, i, depth + 1));
            }
        }

        return new BoardNode
        {
            BoardState = boardState,
            Children = children,
            Move = move,
            Depth = depth
        };
    }
}
