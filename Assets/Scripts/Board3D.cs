using System;

namespace TicTacToe3D
{
    public class Board3D
    {
        private readonly int[,,] _cells = new int[3,3,3];

        public int this[int x,int y,int z]
        {
            get => _cells[x,y,z];
            set => _cells[x,y,z] = value;
        }

        public void Clear() => Array.Clear(_cells,0,_cells.Length);

        public bool IsFull()
        {
            foreach (var c in _cells) if (c==0) return false;
            return true;
        }
    }
}