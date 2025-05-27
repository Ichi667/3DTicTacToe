using System;

namespace TicTacToe3D
{
    public class AiEngine
    {
        private readonly int depth;
        public AiEngine(int d=2)=>depth=d;

        public (int x,int y,int z) NextMove(Board3D b,int player)
        {
            int best=int.MinValue; (int bx,int by,int bz)=(-1,-1,-1);
            for(int x=0;x<3;x++)
            for(int y=0;y<3;y++)
            for(int z=0;z<3;z++)
            {
                if(b[x,y,z]!=0) continue;
                b[x,y,z]=player;
                int score=-MiniMax(b,depth-1,-player);
                b[x,y,z]=0;
                if(score>best){best=score;(bx,by,bz)=(x,y,z);}
            }
            return(bx,by,bz);
        }

        private int MiniMax(Board3D b,int d,int player)
        {
            int w=LineChecker.CheckWinner(b);
            if(w!=0) return w*player;
            if(d==0||b.IsFull()) return 0;
            int best=int.MinValue;
            for(int x=0;x<3;x++)
            for(int y=0;y<3;y++)
            for(int z=0;z<3;z++)
            {
                if(b[x,y,z]!=0) continue;
                b[x,y,z]=player;
                int s=-MiniMax(b,d-1,-player);
                b[x,y,z]=0;
                best=Math.Max(best,s);
            }
            return best;
        }
    }
}