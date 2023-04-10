using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Board : Node2D
{
    [Signal]
    public delegate void BoardStateChangedEventHandler();
    public BoardState BoardState = new();
    private List<Cell> _cells = new();

    public override void _Ready()
    {
        int id = 0;
        foreach (var cell in GetNode("Areas").GetChildren().Cast<Cell>())
        {
            cell.BoardIndex = id;
            cell.InputEvent += (Node viewport, InputEvent @event, long shapeId) =>
            {
                if (@event.IsActionReleased("click"))
                    OnCellClick(cell);
            };
            _cells.Add(cell);
            id++;
        }
    }

    public void ResetBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            BoardState[i] = CellState.FREE;
            _cells[i].ChangeState(CellState.FREE);
            _cells[i].SetScore(0);
            _cells[i].SetHighlight(false);
        }
        BoardState.CurrentPlayer = BoardState.Player.CROSS;
    }

    private void OnCellClick(Cell cell)
    {
        if (BoardState.GameEnded()) return;

        Play(cell.BoardIndex);
    }

    private void Play(int index)
    {
        if (BoardState[index] != CellState.FREE) return;

        BoardState.Play(index);
        _cells[index].ChangeState(BoardState[index]);

        EmitSignal(SignalName.BoardStateChanged);
    }

    public void OnBestMove(int bestMove)
    {
        if (bestMove < 0) return;

        _cells.ForEach(cell => cell.SetHighlight(cell.BoardIndex == bestMove));
    }

    internal void OnScoreCalculated(int index, float score)
    {
        _cells[index].SetScore(score);
    }
}
