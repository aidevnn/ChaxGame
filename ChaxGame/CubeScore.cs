using System;
namespace ChaxGame
{
    public struct CubeScore
    {
        public int NbPlayer, NbOpponent, DomPlayer, DomOpponent;
        public Content Player;

        public CubeScore(Content player, int nbPlayer, int nbOpponent, int domPlayer, int domOpponent)
        {
            Player = player;
            NbPlayer = nbPlayer;
            NbOpponent = nbOpponent;
            DomPlayer = domPlayer;
            DomOpponent = domOpponent;
        }

        public int Nb(Content player) => player == Player ? NbPlayer : NbOpponent;
        public int Dom(Content player) => player == Player ? DomPlayer : DomOpponent;
    }
}
