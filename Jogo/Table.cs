using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo
{
    class Table
    {
        public enum State { PlayerXWins, PlayerOWins, Draw, Playing };

        char[,] m_grid = new char[3, 3];



        public void setTable(char[,] m_aux)
        {
            m_grid = m_aux;
        }

        public char[,] m_grid_atual() { return m_grid; }


     



        public int GetComputerScore(char computerPlayer)
        {
            State curState = GetState();

            //computer wins? return 1
            if ((computerPlayer == 'X') && curState == State.PlayerXWins)
                return 1;
          //  if ((computerPlayer == 'O') && curState == State.PlayerOWins)
          //      return 1;

            //opponent wins? return -1
          //  if ((computerPlayer == 'X') && curState == State.PlayerOWins)
          //      return -1;
            if ((computerPlayer == 'O') && curState == State.PlayerOWins)
                return -1;

            if (curState == State.Draw)
                return 2;

            return 0;
        }

        public List<Table> GetPossibilities(char player)
        {
            List<Table> result = new List<Table>();

            //@REVIEW@ populate result!

            return result;
        }

        public State GetState()
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

            if((m_grid[0, 0] == 'O') && (m_grid[1, 1] == 'O') && (m_grid[2, 2] == 'O'))
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

        public char Get(int x, int y) { return m_grid[x, y]; }

        public void Set(int x, int y, char val) { m_grid[x, y] = val; }

        public void Reset()
        {
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; x++)
                    m_grid[x, y] = ' ';
        }

        public char[,] copyMatriz() {
            char[,] aux = new char [3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    aux[i, j] = m_grid[i, j];
                }
            }
            return aux;
        }



        public int GetTarja()
        {
            

          if((m_grid[0, 0] == 'X') && (m_grid[1, 0] == 'X') && (m_grid[2, 0] == 'X') ||
                (m_grid[0, 0] == 'O') && (m_grid[1, 0] == 'O') && (m_grid[2, 0] == 'O'))
            {
                return 0;
            } else if((m_grid[0, 1] == 'X') && (m_grid[1, 1] == 'X') && (m_grid[2, 1] == 'X') ||
                (m_grid[0, 1] == 'O') && (m_grid[1, 1] == 'O') && (m_grid[2, 1] == 'O'))
            {
                return 1;
            } else if((m_grid[0, 2] == 'X') && (m_grid[1, 2] == 'X') && (m_grid[2, 2] == 'X') ||
                (m_grid[0, 2] == 'O') && (m_grid[1, 2] == 'O') && (m_grid[2, 2] == 'O'))
            {
                return 2;
            } else if ((m_grid[0, 0] == 'X') && (m_grid[0, 1] == 'X') && (m_grid[0, 2] == 'X') ||
              (m_grid[0, 0] == 'O') && (m_grid[0, 1] == 'O') && (m_grid[0, 2] == 'O'))
            {
                return 3;
            } else if ((m_grid[1, 0] == 'X') && (m_grid[1, 1] == 'X') && (m_grid[1, 2] == 'X') ||
            (m_grid[1, 0] == 'O') && (m_grid[1, 1] == 'O') && (m_grid[1, 2] == 'O'))
            {
                return 4;
            } else if ((m_grid[2, 0] == 'X') && (m_grid[2, 1] == 'X') && (m_grid[2, 2] == 'X') ||
            (m_grid[2, 0] == 'O') && (m_grid[2, 1] == 'O') && (m_grid[2, 2] == 'O'))
            {
                return 5;
            } else if((m_grid[0, 0] == 'X') && (m_grid[1, 1] == 'X') && (m_grid[2, 2] == 'X') ||
                (m_grid[0, 0] == 'O') && (m_grid[1, 1] == 'O') && (m_grid[2, 2] == 'O'))
            {
                return 6;
            } else if((m_grid[0, 2] == 'X') && (m_grid[1, 1] == 'X') && (m_grid[2, 0] == 'X') ||
                (m_grid[0, 2] == 'O') && (m_grid[1, 1] == 'O') && (m_grid[2, 0] == 'O'))
            {
                return 7;
            }


            return 8;
            
        }

    }
}
