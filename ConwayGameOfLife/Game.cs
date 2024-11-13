namespace ConwayGameOfLife;

public class Game
{
    const int RowLength = 160;
    int GameSize = RowLength * 40;
    int InitialSurvivalPercentage = 11;
    bool[] PreviousGen = [];
    bool[] NextGen = [];

    public Game()
    {
        GameInitialization();
        GameLoop();
    }

    void GameInitialization()
    {
        InitiliazingGeneration();
        SpawningCellsRandomized();
    }
    void InitiliazingGeneration()
    {
        PreviousGen = new bool[GameSize];
        NextGen = new bool[GameSize];
    }

    void SpawningCellsRandomized()
    {
        Random rnd = new Random();

        for (int i = 0; i < PreviousGen.Length; i++)
        {
            int survivorshipFactor = rnd.Next(100);

            PreviousGen[i] = survivorshipFactor <= InitialSurvivalPercentage;

        }
    }
    void PrintGeneration(bool[] generation, int currentGeneration)
    {
        string result = $"Current Generation: {currentGeneration}\n";

        for (int y = 0; y < PreviousGen.Length / RowLength; y++)
        {
            for (int x = 0; x < RowLength; x++)
            {
                if (PreviousGen[CoordinatesToIndex(x, y)] == true)
                {
                    result += "X";

                }
                else
                {
                    result += " ";
                }

            }
            result += "\n";
        }

        Console.Write(result);
    }

    void GameLoop()
    {

        int generationCounter = 0;
        while (CheckIfAnyCellsAreAlive())
        {
            PrintGeneration(PreviousGen, generationCounter);
            StartingEvolution();
            ForwardGeneration();
            Thread.Sleep(100);
            Console.SetCursorPosition(0, 0);
            generationCounter++;
        }
    }

    void StartingEvolution()
    {
        for (int i = 0; i < PreviousGen.Length; i++)
        {
            int aliveNeighbours = CountAliveNeighbours(i);
            if (aliveNeighbours == 3 || (aliveNeighbours == 2 && PreviousGen[i] == true))
            {
                NextGen[i] = true;
            }
        }

    }

    int CountAliveNeighbours(int cellIndex)
    {
        int aliveNeighbours = 0;
        int coordinateX = IndexToCoordinateX(cellIndex);
        int coordinateY = IndexToCoordinateY(cellIndex);
        int amountOfColumns = PreviousGen.Length / RowLength;

        for (int i = coordinateY - 1; i <= coordinateY + 1; i++)
        {
            for (int j = coordinateX - 1; j <= coordinateX + 1; j++)
            {
                int x = j;
                int y = i;

                if (y == coordinateY && x == coordinateX)
                {
                    continue;
                }
                // check if outside of border
                //if (y < 0 || x < 0 || y > amountOfColumns - 1 || x > RowLength - 1)
                //{
                //    continue;
                //}
                // toroidal mode

                if (y < 0)
                {
                    y = amountOfColumns - 1;
                }
                else if (y >= amountOfColumns - 1)
                {
                    y = 0;
                }
                if (x < 0)
                {
                    x = RowLength - 1;
                }
                else if (x >= RowLength - 1)
                {
                    x = 0;
                }


                if (PreviousGen[CoordinatesToIndex(x, y)])
                {
                    aliveNeighbours++;
                }

            }

        }


        return aliveNeighbours;
    }
    int IndexToCoordinateX(int index)
    {
        return index % RowLength;
    }

    int IndexToCoordinateY(int index)
    {
        return index / RowLength;
    }

    int CoordinatesToIndex(int coordinateX, int coordinateY)
    {
        return coordinateY * RowLength + coordinateX;
    }

    void ForwardGeneration()
    {
        for (int i = 0; i < PreviousGen.Length; i++)
        {
            PreviousGen[i] = NextGen[i];
            NextGen[i] = false;
        }

    }
    bool CheckIfAnyCellsAreAlive()
    {
        foreach (var cell in PreviousGen)
        {
            if (cell)
            {
                return true;
            }
        }
        return false;
    }
}
