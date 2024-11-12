using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayGameOfLife;

public class Game
{
    int GameSize = 32;
    bool[] PreviousGen = [];
    bool[] NextGen = [];

    public Game()
    {
        GameInitialization();
        Print(PreviousGen);

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
        for (int i = 0; i < PreviousGen.Length; i++)
        {
            if (i % 2 == 0)
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
            Console.Write($" {cell} ");
        }
    }

    void GameLoop()
    {
        bool atLeastOneCellAlive = true;

        while (atLeastOneCellAlive)
        {

            ApplyRules();
            // Swapping cells from previous to next Gen

            SwapGenNames();
            Print(PreviousGen);
        }
    }


    void ApplyRules()
    {
        for (int i = 0; i < PreviousGen.Length; i++)
        {
            Check
        }

    }
    void SwapGenNames()
    {
    }
}
