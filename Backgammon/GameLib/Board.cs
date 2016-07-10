
using System.Collections.Generic;


public enum CellContent { None, PlayerOne, PlayerTwo };

namespace GameLib
{

    public class Board
    {
        private int rowSize = 12;
        private int numOfRows = 12;
        private int oneSideSize = 6;

        private Dictionary<int, int> numOfStonesInLane; //key= lane number, value= number of stones
        public CellContent[,] BoardMatrix { get; }
        private CellContent[] rowOccupation;
        public List<CellContent> EatenStones{ get; private set;}

        //C'tor
        public Board()
        {
            BoardMatrix = new CellContent[numOfRows, rowSize];
            rowOccupation = new CellContent[numOfRows * 2];
            EatenStones = new List<CellContent>();
            numOfStonesInLane = new Dictionary<int, int>();
            
            for (int i = 1; i < 25; i++)
            {
                numOfStonesInLane[i] = 0;
            }
        } /****************************************/


        //Arrange the position of the end of the game for debugging
        public void InitializeBoardCustom()
        {
            for (int i = 0; i < 5; i++)
            {
                BoardMatrix[i, 9] = CellContent.PlayerOne;
                BoardMatrix[i, 10] = CellContent.PlayerOne;
                BoardMatrix[i, 11] = CellContent.PlayerOne;
            }

            for (int i = 7; i < 12; i++)
            {
                BoardMatrix[i, 9] = CellContent.PlayerTwo;
                BoardMatrix[i, 10] = CellContent.PlayerTwo;
                BoardMatrix[i, 11] = CellContent.PlayerTwo;
            }


            rowOccupation[0] = CellContent.PlayerTwo;
            rowOccupation[1] = CellContent.PlayerTwo;
            rowOccupation[2] = CellContent.PlayerTwo;

            rowOccupation[21] = CellContent.PlayerOne;
            rowOccupation[22] = CellContent.PlayerOne;
            rowOccupation[23] = CellContent.PlayerOne;


            numOfStonesInLane[1] = 5;
            numOfStonesInLane[2] = 5;
            numOfStonesInLane[3] = 5;
            numOfStonesInLane[22] = 5;
            numOfStonesInLane[23] = 5;
            numOfStonesInLane[24] = 5;

        } /****************************************/

        //Arrange the start position
        public void InitializeBoard()
        {
            for (int i = 0; i < 5; i++)
            {
                BoardMatrix[i, 0] = CellContent.PlayerTwo;
                BoardMatrix[i, 7] = CellContent.PlayerOne;
            }

            for (int i = 7; i < rowSize; i++)
            {
                BoardMatrix[i, 0] = CellContent.PlayerOne;
                BoardMatrix[i, 7] = CellContent.PlayerTwo;
            }

            for (int i = 0; i < 2; i++)
            {
                BoardMatrix[i, 4] = CellContent.PlayerOne;
                BoardMatrix[i, 11] = CellContent.PlayerTwo;
            }
            BoardMatrix[2, 4] = CellContent.PlayerOne;
            BoardMatrix[9, 4] = CellContent.PlayerTwo;
            for (int i = 10; i < rowSize; i++)
            {
                BoardMatrix[i, 4] = CellContent.PlayerTwo;
                BoardMatrix[i, 11] = CellContent.PlayerOne;
            }


            rowOccupation[0] = CellContent.PlayerOne;
            rowOccupation[4] = CellContent.PlayerTwo;
            rowOccupation[7] = CellContent.PlayerTwo;
            rowOccupation[11] = CellContent.PlayerOne;
            rowOccupation[12] = CellContent.PlayerTwo;
            rowOccupation[16] = CellContent.PlayerOne;
            rowOccupation[19] = CellContent.PlayerOne;
            rowOccupation[23] = CellContent.PlayerTwo;
          
            numOfStonesInLane[1] = 2;
            numOfStonesInLane[5] = 5;
            numOfStonesInLane[8] = 3;
            numOfStonesInLane[12] = 5;
            numOfStonesInLane[13] = 5;
            numOfStonesInLane[17] = 3;
            numOfStonesInLane[20] = 5;
            numOfStonesInLane[24] = 2;

        }/****************************************/


        //get the indexes of all the lanes this player has stones on
        internal List<int> GetPlayerRowOccupation(Player player)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < rowOccupation.Length; i++)
            {
                if ((int)rowOccupation[i] == player.PlayerNumber)
                {
                    list.Add(i + 1);
                }
            }
            return list;
        } /****************************************/



        public void EatOtherPlayer(Move move)
        {
            RemoveStoneFromLane(move.From);
            EatStone(move.To);
        } /****************************************/

        //invoked when a player try to get back on board
        public void GetBackFromEaten(Player player , int to)
        {
            if ((int)rowOccupation[to] != player.PlayerNumber && rowOccupation[to] != CellContent.None)
            {
                EatStone(to);
            }
            else
            {
                AddStoneInLane((CellContent)player.PlayerNumber, to);
            }
            EatenStones.Remove( (CellContent)player.PlayerNumber);
        } /****************************************/


        //move stone from one lane to another, without eating
        public void MakeRegularMove(Move move)
        {
            AddStoneInLane(rowOccupation[move.From - 1], move.To);
            RemoveStoneFromLane(move.From);
        } /****************************************/

        //invoke when the player has all his stones at home and move them out
        public void MoveStoneOut(int from)
        {
            RemoveStoneFromLane(from);
        } /****************************************/

     
        private void AddStoneInLane(CellContent cellType,int lane)
        {

            int i, j;

            // add stone to the new lane
            if (lane <= numOfRows) //lower board
            {
                i = numOfRows - numOfStonesInLane[lane] - 1;  //skip the occupied cells
                j = rowSize - lane; //lane index
                if (i >= oneSideSize) //board is full (don't show the new stone)
                    BoardMatrix[i, j] = cellType;
            }
            else //upper board
            {
                i = numOfStonesInLane[lane]; //skip the occupied cells
                j = lane - rowSize - 1; //lane index
                if (i < oneSideSize) //board is full (don't show the new stone)
                    BoardMatrix[i, j] = cellType;
            }

            //update the occupation of the new lane to the current player
            rowOccupation[lane- 1] = cellType;
            numOfStonesInLane[lane]++;

        }/****************************************/

        private void RemoveStoneFromLane(int lane)
        {
            int i, j;
            //remove stone from the old lane
            if (lane <= numOfRows) //lower board
            {
                i = numOfRows - numOfStonesInLane[lane]; //find the last occupied cell
                j = rowSize - lane; //lane index
                if (i >= 6) //board is not full
                    BoardMatrix[i, j] = CellContent.None; //remove stone
            }
            else //upper board
            {
                i = numOfStonesInLane[lane] - 1; //find the last occupied cell
                j = lane - rowSize - 1; //lane index
                if (i < 6) //board is not full
                    BoardMatrix[i, j] = CellContent.None;
            }

            --numOfStonesInLane[lane];
            //remove occupation of current player if there is no stones on the old lane
            if (numOfStonesInLane[lane] == 0)
                rowOccupation[lane - 1] = CellContent.None;
        }/****************************************/

        //invoked when a player step on other player lane with 1 stone
        private void EatStone(int lane)
        {
            int index = lane - 1;
            //add the player stone to eaten zone
            EatenStones.Add(rowOccupation[index]);

            //save the old occupation for the exchange
            CellContent occupation = rowOccupation[index];

            //remove stone and reset occupation
            RemoveStoneFromLane(lane);
            
            //change occupation of destination to the eating player
            if (occupation == CellContent.PlayerOne)
            {
                AddStoneInLane(CellContent.PlayerTwo , lane);
            }
            else
            {
                AddStoneInLane(CellContent.PlayerOne, lane);
            }
           
        }/****************************************/


        public int GetNumOfStonesInLane(int lane)
        {
            return numOfStonesInLane[lane];
        }/****************************************/

    }
}
