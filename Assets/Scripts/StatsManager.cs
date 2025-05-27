namespace TicTacToe3D
{
    public class StatsManager
    {
        public int XWins { get; private set; }
        public int OWins { get; private set; }
        public int Draws { get; private set; }

        public void RegisterResult(int winner)
        {
            if (winner == 1) XWins++;
            else if (winner == -1) OWins++;
            else Draws++;
        }
    }
}
