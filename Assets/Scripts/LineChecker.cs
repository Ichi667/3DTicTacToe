namespace TicTacToe3D
{
    /// <summary>Перевірка всіх 49 виграшних ліній на кубі 3х3х3.</summary>
    public static class LineChecker
    {
        /// <remarks>
        /// Lines[l, k, 0] = x координата k-ї клітинки l-ї лінії  
        /// Lines[l, k, 1] = y  
        /// Lines[l, k, 2] = z
        /// </remarks>
        private static readonly int[,,] Lines = InitLines();

        private static int[,,] InitLines()
        {
            var lines = new int[49, 3, 3];
            int idx = 0;

            // 27 осьових ліній (уздовж X, Y, Z)
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // X-напрям
                    for (int k = 0; k < 3; k++) { lines[idx, k, 0] = k; lines[idx, k, 1] = i; lines[idx, k, 2] = j; }
                    idx++;
                    // Y-напрям
                    for (int k = 0; k < 3; k++) { lines[idx, k, 0] = i; lines[idx, k, 1] = k; lines[idx, k, 2] = j; }
                    idx++;
                    // Z-напрям
                    for (int k = 0; k < 3; k++) { lines[idx, k, 0] = i; lines[idx, k, 1] = j; lines[idx, k, 2] = k; }
                    idx++;
                }
            }

            // 18 діагоналей площин XY
            for (int z = 0; z < 3; z++)
            {
                for (int k = 0; k < 3; k++) { lines[idx, k, 0] = k; lines[idx, k, 1] = k; lines[idx, k, 2] = z; }
                idx++;
                for (int k = 0; k < 3; k++) { lines[idx, k, 0] = k; lines[idx, k, 1] = 2 - k; lines[idx, k, 2] = z; }
                idx++;
            }

            // 18 діагоналей площин XZ
            for (int y = 0; y < 3; y++)
            {
                for (int k = 0; k < 3; k++) { lines[idx, k, 0] = k; lines[idx, k, 1] = y; lines[idx, k, 2] = k; }
                idx++;
                for (int k = 0; k < 3; k++) { lines[idx, k, 0] = k; lines[idx, k, 1] = y; lines[idx, k, 2] = 2 - k; }
                idx++;
            }

            // 18 діагоналей площин YZ
            for (int x = 0; x < 3; x++)
            {
                for (int k = 0; k < 3; k++) { lines[idx, k, 0] = x; lines[idx, k, 1] = k; lines[idx, k, 2] = k; }
                idx++;
                for (int k = 0; k < 3; k++) { lines[idx, k, 0] = x; lines[idx, k, 1] = k; lines[idx, k, 2] = 2 - k; }
                idx++;
            }

            // 4 просторові діагоналі
            for (int k = 0; k < 3; k++) { lines[idx, k, 0] = k; lines[idx, k, 1] = k; lines[idx, k, 2] = k; }
            idx++;
            for (int k = 0; k < 3; k++) { lines[idx, k, 0] = k; lines[idx, k, 1] = k; lines[idx, k, 2] = 2 - k; }
            idx++;
            for (int k = 0; k < 3; k++) { lines[idx, k, 0] = k; lines[idx, k, 1] = 2 - k; lines[idx, k, 2] = k; }
            idx++;
            for (int k = 0; k < 3; k++) { lines[idx, k, 0] = k; lines[idx, k, 1] = 2 - k; lines[idx, k, 2] = 2 - k; }
            // idx == 49

            return lines;
        }

        /// <summary>Повертає 1, якщо виграв X; -1, якщо виграла O; 0, якщо переможця немає.</summary>
        public static int CheckWinner(Board3D board)
        {
            for (int l = 0; l < 49; l++)
            {
                int sum = 0;
                for (int i = 0; i < 3; i++)
                    sum += board[Lines[l, i, 0], Lines[l, i, 1], Lines[l, i, 2]];

                if (sum == 3) return 1;
                if (sum == -3) return -1;
            }
            return 0;
        }
    }
}
