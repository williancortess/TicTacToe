using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo
{
    class Minimax
    {

        public void calc_minimax(Table tb)
        {
            int bestX = int.MinValue;
            int bestY = int.MinValue;
            int score = int.MinValue;

            Random r = new Random();
            int rand = r.Next(0, 2);



            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (tb.Get(x, y) == ' ')
                    {
                        char[,] gAux = tb.copyMatriz();
                        gAux[x, y] = 'O';
                        int aux = minmax(gAux, 'X');

                        if (aux > score)
                        {
                            bestX = x;
                            bestY = y;
                            score = aux;
                        }
                        else if (aux == score && rand == 1)
                        {
                            bestX = x;
                            bestY = y;                            
                        }
                    }
                }

            }
            tb.Set(bestX,bestY,'O');            
        }

        public int minmax(char[,] grid, char aux)
        {
            int score = 0;
            //tb.setTable(grid);

            if(GetState(grid) != State.Playing)
            {
                //CHAMA O GET STATE AQUI DA CLASS!!!!!!!!!!!
                return GetComputerScore(grid);
            }
            if(aux == 'X')
            {
                score = int.MaxValue;
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (grid[x, y] == ' ')
                        {
                            char[,] gAux = copyMatriz(grid);
                            gAux[x, y] = aux;
                            score = Math.Min(score,minmax(gAux, 'O'));
                        }
                    }
                }
            }
            if(aux == 'O')
            {
                score = int.MinValue;
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (grid[x, y] == ' ')
                        {
                            char[,] gAux = copyMatriz(grid);
                            gAux[x, y] = aux;
                            score = Math.Max(score, minmax(gAux, 'X'));
                        }
                    }
                }
            }
            return score;
        }


        public enum State { PlayerXWins, PlayerOWins, Draw, Playing };


           public int GetComputerScore(char[,] m_grid)
        {
            State curState = GetState(m_grid);

            //computer wins? return 1
            if (curState == State.PlayerXWins)
                return -1;
            //  if ((computerPlayer == 'O') && curState == State.PlayerOWins)
            //      return 1;

            //opponent wins? return -1
            //  if ((computerPlayer == 'X') && curState == State.PlayerOWins)
            //      return -1;
            if (curState == State.PlayerOWins)
                return 1;



            return 0;
        }


        public State GetState(char[,] m_grid)
        {
            //State.PlayerXWins
            for (int x = 0; x < 3; x++)
                if ((m_grid[x, 0] == 'X') &&
                    (m_grid[x, 1] == 'X') &&
                    (m_grid[x, 2] == 'X'))
                    return State.PlayerXWins;
            for (int y = 0; y < 3; y++)
                if ((m_grid[0, y] == 'X') &&
                    (m_grid[1, y] == 'X') &&
                    (m_grid[2, y] == 'X'))
                    return State.PlayerXWins;

            if ((m_grid[0, 0] == 'X') && (m_grid[1, 1] == 'X') && (m_grid[2, 2] == 'X'))
                return State.PlayerXWins;

            if ((m_grid[0, 2] == 'X') && (m_grid[1, 1] == 'X') && (m_grid[2, 0] == 'X'))
                return State.PlayerXWins;

            //State.PlayerOWins
            for (int x = 0; x < 3; x++)
                if ((m_grid[x, 0] == 'O') &&
                    (m_grid[x, 1] == 'O') &&
                    (m_grid[x, 2] == 'O'))
                    return State.PlayerOWins;
            for (int y = 0; y < 3; y++)
                if ((m_grid[0, y] == 'O') &&
                    (m_grid[1, y] == 'O') &&
                    (m_grid[2, y] == 'O'))
                    return State.PlayerOWins;

            if ((m_grid[0, 0] == 'O') && (m_grid[1, 1] == 'O') && (m_grid[2, 2] == 'O'))
                return State.PlayerOWins;

            if ((m_grid[0, 2] == 'O') && (m_grid[1, 1] == 'O') && (m_grid[2, 0] == 'O'))
                return State.PlayerOWins;

            //State.Draw
            if ((m_grid[0, 0] != ' ') && (m_grid[1, 0] != ' ') && (m_grid[2, 0] != ' ') &&
                (m_grid[0, 1] != ' ') && (m_grid[1, 1] != ' ') && (m_grid[2, 1] != ' ') &&
                (m_grid[0, 2] != ' ') && (m_grid[1, 2] != ' ') && (m_grid[2, 2] != ' '))
                return State.Draw;

            return State.Playing;
        }
        public char[,] copyMatriz(char[,] matriz)
        {
            char[,] aux = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    aux[i, j] = matriz[i, j];
                }
            }
            return aux;
        }
    }
}
