
using System.Collections.Generic;


public enum CellContent { None, PlayerOne, PlayerTwo };

namespace GameLib
{

    public class Board
    {
        private int rowSize = 12;
        private int numOfRows = 12;
        private int oneSideSize = 6;

        private Dictionary<int, int> numOfCheckersInLane; //key= lane number, value= number of stones
        private CellContent[] RowOccupation { get; }
        public CellContent[,] BoardMatrix { get; }
        public List<CellContent> EatenCheckers{ get; private set;}
        /****************************************/

        public Board()
        {
            BoardMatrix = new CellContent[numOfRows, rowSize];
            RowOccupation = new CellContent[numOfRows * 2];
            EatenCheckers = new List<CellContent>();
            numOfCheckersInLane = new Dictionary<int, int>();
            
            for (int i = 1; i < 25; i++)
            {
                numOfCheckersInLane[i] = 0;
            }
        }
        /****************************************/

        //Arrange the position of the end of the game for debugging        
        /*
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


            RowOccupation[0] = CellContent.PlayerTwo;
            RowOccupation[1] = CellContent.PlayerTwo;
            RowOccupation[2] = CellContent.PlayerTwo;

            RowOccupation[21] = CellContent.PlayerOne;
            RowOccupation[22] = CellContent.PlayerOne;
            RowOccupation[23] = CellContent.PlayerOne;


            numOfCheckersInLane[1] = 5;
            numOfCheckersInLane[2] = 5;
            numOfCheckersInLane[3] = 5;
            numOfCheckersInLane[22] = 5;
            numOfCheckersInLane[23] = 5;
            numOfCheckersInLane[24] = 5;

        } 
        /****************************************/

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


            RowOccupation[0] = CellContent.PlayerOne;
            RowOccupation[4] = CellContent.PlayerTwo;
            RowOccupation[7] = CellContent.PlayerTwo;
            RowOccupation[11] = CellContent.PlayerOne;
            RowOccupation[12] = CellContent.PlayerTwo;
            RowOccupation[16] = CellContent.PlayerOne;
            RowOccupation[19] = CellContent.PlayerOne;
            RowOccupation[23] = CellContent.PlayerTwo;
          
            numOfCheckersInLane[1] = 2;
            numOfCheckersInLane[5] = 5;
            numOfCheckersInLane[8] = 3;
            numOfCheckersInLane[12] = 5;
            numOfCheckersInLane[13] = 5;
            numOfCheckersInLane[17] = 3;
            numOfCheckersInLane[20] = 5;
            numOfCheckersInLane[24] = 2;

        }
        /****************************************/
     
        internal List<int> GetPlayerRowOccupation(Player player)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < RowOccupation.Length; i++)
            {
                if ((int)RowOccupation[i] == player.PlayerNumber)
                {
                    list.Add(i + 1);
                }
            }
            return list;
        } 
        /****************************************/

        public void EatOtherPlayer(Move move)
        {
            RemoveCheckerFromLane(move.From);
            EatChecker(move.To);
        } 
        /****************************************/
      
        public void GetBackFromEaten(Player player , int to)
        {
            //eat other player
            if ((int)RowOccupation[to-1] != player.PlayerNumber && RowOccupation[to-1] != CellContent.None)
            {
                EatChecker(to);
            }
            else 
            {
                AddCheckerInLane((CellContent)player.PlayerNumber, to);
            }
         
            EatenCheckers.Remove( (CellContent)player.PlayerNumber);
        }
        /****************************************/
        
        public void MakeRegularMove(Move move)
        {
            AddCheckerInLane(RowOccupation[move.From - 1], move.To);
            RemoveCheckerFromLane(move.From);
        }
        /****************************************/
      
        public void MoveStoneOut(int from)
        {
            RemoveCheckerFromLane(from);
        }
        /****************************************/
   
        private void AddCheckerInLane(CellContent cellType,int lane)
        {
            int i, j;
          
            if (lane <= numOfRows) //lower board
            {
                i = numOfRows - numOfCheckersInLane[lane] - 1;  //skip the occupied cells
                j = rowSize - lane; 
                if (i >= oneSideSize) //board is full (don't show the new stone)
                    BoardMatrix[i, j] = cellType;
            }
            else //upper board
            {
                i = numOfCheckersInLane[lane]; 
                j = lane - rowSize - 1; 
                if (i < oneSideSize) 
                    BoardMatrix[i, j] = cellType;
            }

            RowOccupation[lane- 1] = cellType;
            numOfCheckersInLane[lane]++;

        }
        /****************************************/

        private void RemoveCheckerFromLane(int lane)
        {
            int i, j;
            //remove stone from the old lane
            if (lane <= numOfRows) //lower board
            {
                i = numOfRows - numOfCheckersInLane[lane]; //find the last occupied cell
                if (i > 11) i = 11;
                j = rowSize - lane; 
                if (i >= 6) //board is not full
                    BoardMatrix[i, j] = CellContent.None;
            }
            else //upper board
            {
                i = numOfCheckersInLane[lane] - 1;
                if (i < 0) i = 0;
                j = lane - rowSize - 1; 
                if (i < 6) 
                    BoardMatrix[i, j] = CellContent.None;
            }

            --numOfCheckersInLane[lane];
           
            if (numOfCheckersInLane[lane] == 0)
                RowOccupation[lane - 1] = CellContent.None;
        }
        /****************************************/
     
        private void EatChecker(int lane)
        {
            int index = lane - 1;
          
            EatenCheckers.Add(RowOccupation[index]);

            //save the old occupation for the exchange
            CellContent occupation = RowOccupation[index];
           
            RemoveCheckerFromLane(lane);
            
            //change occupation of destination to the eating player
            if (occupation == CellContent.PlayerOne)
            {
                AddCheckerInLane(CellContent.PlayerTwo , lane);
            }
            else
            {
                AddCheckerInLane(CellContent.PlayerOne, lane);
            }
           
        }
        /****************************************/

        public int GetNumOfCheckersInLane(int lane)
        {
            return numOfCheckersInLane[lane];
        }
        /****************************************/

    }
}
