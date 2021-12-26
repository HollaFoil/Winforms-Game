using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaidimas
{
    struct SolutionClick
    {
        public int x;
        public int y;
        public SolutionClick(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class GameSolver
    {
        private bool possible;
        private int timelimit;
        private List<List<int>> grid = new List<List<int>>();
        private List<SolutionClick> solution = new List<SolutionClick>();
        Stopwatch sw = new Stopwatch();
        public GameSolver(int[,] weights, int langeliuKiekis, int timelimit)
        {
            CreateGrid(weights, langeliuKiekis);
            OutputGrid(langeliuKiekis);
            sw.Start();
            possible = Solve(grid);
            OutputGrid(langeliuKiekis);
            OutputSolution();
            sw.Stop();
        }
        public bool IsPossible()
        {
            return possible;
        }
        public List<SolutionClick> GetSolution()
        {
            return solution;
        }
        private bool Solve(List<List<int>> g)
        {
            if (sw.ElapsedMilliseconds / 1000 > timelimit) return false;
            for (int i = 0; i < g.Count; i++)
            {
                for (int j = 0; j < g[i].Count; j++)
                {
                    if (!IsInBounds(i, j, g)) continue;
                    GetDeepCopy(g, out List<List<int>> copy);
                    int valid = CrawlAdjacentsFromPoint(i, j, copy[i][j], copy);
                    if (valid == 1) continue;

                    solution.Add(new SolutionClick(i+1, j+1));
                    ClearDeletedPoints(copy);
                    if (copy.Count == 0) return true;
                    bool isSolution = Solve(copy);
                    if (!isSolution) solution.RemoveAt(solution.Count - 1);
                    else return true;
                }
            }
            return false;
        }
        private int CrawlAdjacentsFromPoint(int x, int y, int weight, List<List<int>> g)
        {
            if (g[x][y] != weight) return 0;
            g[x][y] = -1;
            int sum = 1;
            if (IsInBounds(x + 1, y, g)) sum += CrawlAdjacentsFromPoint(x + 1, y, weight, g);
            if (IsInBounds(x - 1, y, g)) sum += CrawlAdjacentsFromPoint(x - 1, y, weight, g);
            if (IsInBounds(x, y + 1, g)) sum += CrawlAdjacentsFromPoint(x, y + 1, weight, g);
            if (IsInBounds(x, y - 1, g)) sum += CrawlAdjacentsFromPoint(x, y - 1, weight, g);
            return sum;
        }
        private void ClearDeletedPoints(List<List<int>> g)
        {
            for (int i = 0; i < g.Count; i++)
            {
                for (int j = 0; j < g[i].Count; j++)
                {
                    if (g[i][j] == -1) g[i].RemoveAt(j--);
                   
                }
                if (g[i].Count == 0) g.RemoveAt(i--);
            }
        }
        private bool IsInBounds(int x, int y, List<List<int>> grid)
        {
            return ((grid.Count > x && x >= 0) && (grid[x].Count > y && y >= 0));
        }
        private List<List<int>> GetDeepCopy (List<List<int>> original, out List<List<int>> copy)
        {
            copy = new List<List<int>>();
            foreach (List<int> list in original)
            {
                copy.Add(new List<int>());
                foreach (int i in list)
                {
                    copy[copy.Count - 1].Add(i);
                }
            }
            return copy;
        }
        private void CreateGrid(int[,] weights, int langeliuKiekis) 
        {
            for (int i = 0; i < langeliuKiekis; i++)
            {
                grid.Add(new List<int>(langeliuKiekis));
                for (int j = 0; j < langeliuKiekis; j++)
                {
                    grid[i].Add(weights[i,j]);
                }
            }
        }
        private void OutputGrid(int langeliuKiekis)
        {
            string lines = "";
            for (int i = 0; i < langeliuKiekis; i++)
            {
                for (int j = 0; j < langeliuKiekis; j++)
                {
                    lines += grid[i][j].ToString() + " "; 
                }
                lines += "\n";
            }
            File.WriteAllText("outputGrid.txt", lines);
        }
        private void OutputSolution()
        {
            string lines = "";
            foreach (SolutionClick move in solution) lines += move.x.ToString() + ", " + move.y.ToString() + "\n";
            File.WriteAllText("outputSolution.txt", lines);
        }
    }
}
