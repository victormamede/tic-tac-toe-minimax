namespace TicTacToe;

public struct BoardState
{
    public CellState this[int index]
    {
        get => _state[index];
        set
        {
            _state[index] = value;
        }
    }

    private readonly CellState[] _state = {
        CellState.FREE,
        CellState.FREE,
        CellState.FREE,
        CellState.FREE,
        CellState.FREE,
        CellState.FREE,
        CellState.FREE,
        CellState.FREE,
        CellState.FREE,
    };

    public Player CurrentPlayer = Player.CROSS;

    readonly int[] WIN_POSITIONS = {
        0b100_100_100,
        0b010_010_010,
        0b001_001_001,
        0b111_000_000,
        0b000_111_000,
        0b000_000_111,
        0b100_010_001,
        0b001_010_100,
    };

    public BoardState()
    {
    }

    public enum Player
    {
        CROSS,
        CIRCLE
    }

    public Nullable<Player> GetWinner()
    {

        foreach (var winPosition in WIN_POSITIONS)
        {
            if ((winPosition & BitwiseBoardState(CellState.CROSS)) == winPosition)
            {
                return Player.CROSS;
            }
            else if ((winPosition & BitwiseBoardState(CellState.CIRCLE)) == winPosition)
            {
                return Player.CIRCLE;
            }

        }

        return null;
    }

    public bool GameEnded()
    {
        if (GetWinner() != null) return true;

        for (int i = 0; i < 9; i++)
        {
            if (_state[i] == CellState.FREE) return false;
        }

        return true;
    }

    public int BitwiseBoardState(CellState type)
    {
        int board = 0;

        foreach (var state in _state)
        {
            board <<= 1;
            if (type == state)
                board++;
        }

        return board;
    }

    public void Play(int cellIndex)
    {
        switch (CurrentPlayer)
        {
            case Player.CROSS:
                _state[cellIndex] = CellState.CROSS;
                CurrentPlayer = Player.CIRCLE;
                break;
            case Player.CIRCLE:
                _state[cellIndex] = CellState.CIRCLE;
                CurrentPlayer = Player.CROSS;
                break;
        }
    }

    public void PrettyPrint()
    {
        for (int i = 0; i < 3; i++)
        {
            string lineString = "|";
            for (int j = 0; j < 3; j++)
            {
                switch (_state[i + (j * 3)])
                {
                    case CellState.FREE:
                        lineString += " ";
                        break;
                    case CellState.CIRCLE:
                        lineString += "o";
                        break;
                    case CellState.CROSS:
                        lineString += "x";
                        break;
                }
                lineString += "|";
            }
            GD.Print(lineString);
            GD.Print("-------");
        }
    }

    public BoardState Clone()
    {
        BoardState boardState = new();
        _state.CopyTo(boardState._state, 0);
        boardState.CurrentPlayer = CurrentPlayer;

        return boardState;
    }
}
