global using Godot;
global using System;
global using System.Linq;

public partial class Game : Node2D
{
    public override void _Ready()
    {
        Board board = GetNode<Board>("Board");
        BoardAnalyzer boardAnalyzer = GetNode<BoardAnalyzer>("BoardAnalyzer");
        Button restartButton = GetNode<Button>("%Restart");

        board.BoardStateChanged += () => boardAnalyzer.AnalyzeBoardState(board.BoardState);
        boardAnalyzer.BestMoveCalculated += board.OnBestMove;
        boardAnalyzer.ScoreCalculated += board.OnScoreCalculated;
        restartButton.Pressed += board.ResetBoard;
    }
}
