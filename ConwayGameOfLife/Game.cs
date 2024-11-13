using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayGameOfLife;

public class Game
{
    int GameSize = 100;
    bool[] PreviousGen = [];
    bool[] NextGen = [];

    public Game()
    {
        GameInitialization();
        // Print(PreviousGen);

        GameLoop();

    }

    void GameInitialization()
    {
        CreatingGeneration();
        SpawningCellsInPreviousGen();
    }
    void CreatingGeneration()
    {
        PreviousGen = new bool[GameSize];
        NextGen = new bool[GameSize];
    }

    void SpawningCellsInPreviousGen()
    {
        // TODO: randomizing spawn
        Random rnd = new Random();

        for (int i = 0; i < PreviousGen.Length; i++)
        {
            int cellLuck = rnd.Next(100);

            if (cellLuck >= 30)
            {
                PreviousGen[i] = true;
            }
            else
            {
                PreviousGen[i] = false;

            }
        }
    }
    void Print(bool[] generation)
    {
        foreach (var cell in generation)
        {
            if (cell == true)
            {
                Console.Write('#');

            }
            else
            {
                Console.Write(' ');

            }
        }
        Console.WriteLine();
    }

    void GameLoop()
    {

        int roundCounter = 0;
        while (CheckIfAnyCellsAreAlive() && roundCounter < 100)
        {
            Console.WriteLine($"Current Generation: {roundCounter}");
            ApplyRules();
            ForwardGeneration();
            Print(PreviousGen);
            roundCounter++;
            Thread.Sleep(1000);
        }
    }

    void ApplyRules()
    {
        for (int i = 0; i < PreviousGen.Length; i++)
        {
            // for every cell:
            int aliveNeighbours = CountAliveNeighbours(i);
            if (aliveNeighbours == 3 || (aliveNeighbours == 2 && PreviousGen[i] == true))
            {
                NextGen[i] = true;
            }
        }

    }

    int CountAliveNeighbours(int cell)
    {
        int aliveNeighbours = 0;
        for (int i = cell - 2; i < cell + 3; i++)
        {
            // making the game field toroidal
            if (i < 0)
            {
                if (PreviousGen[PreviousGen.Length - (-1 * i)] == true)
                {
                    aliveNeighbours++;
                }
            }
            else if (i >= PreviousGen.Length)
            {
                if (PreviousGen[i - PreviousGen.Length] == true)
                {
                    aliveNeighbours++;
                }
            }
            else if (i == cell)
            {
                continue;
            }
            else
            {
                if (PreviousGen[i] == true)
                {
                    aliveNeighbours++;
                }
            }

        }
        return aliveNeighbours;
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
