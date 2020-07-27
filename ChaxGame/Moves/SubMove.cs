using System;
namespace ChaxGame.Moves
{
    public struct SubMove
    {
        public int idBefore, idAfter, idOpp1, idOpp2, nbKills;

        public void DoStep(Cube cube, Content player)
        {
            cube.SetCell(idBefore, Content.Empty);
            cube.SetCell(idAfter, player);
            if (idOpp1 != -1) cube.SetCell(idOpp1, Content.Empty);
            if (idOpp2 != -1) cube.SetCell(idOpp2, Content.Empty);
        }

        public void UndoStep(Cube cube, Content player, Content opponent)
        {
            if (idOpp1 != -1) cube.SetCell(idOpp1, opponent);
            if (idOpp2 != -1) cube.SetCell(idOpp2, opponent);

            cube.SetCell(idAfter, Content.Empty);
            cube.SetCell(idBefore, player);
        }


        public override string ToString()
        {
            return $"idBefore:{idBefore}; idAfter:{idAfter}; idOpp1:{idOpp1}; idOpp2:{idOpp2}; nbKills:{nbKills}";
        }
    }
}
