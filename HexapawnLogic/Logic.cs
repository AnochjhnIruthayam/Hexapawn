using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexapawnLogic
{
    public class Logic
    {
        public enum Piece { BLANK, PLAYER1, PLAYER2 }
        public Piece CurrentPLAYER = Piece.PLAYER1;
        public Piece NextPLAYER;
        private Piece[,] GameBoard;
        public int determineRow { get; set; }
        public int determineCol { get; set; }


        public Logic() { }

        public Logic(int _determineCol, int _determineRow)
        {
            determineRow = _determineRow;
            determineCol = _determineCol;
        }


        //**************General Board settings********************//
        public void GameBoardGenerate(int x, int y)
        {
            determineRow = x;
            determineCol = y;
            GameBoard = new Piece[x, y];
        }

        public int getRow()
        {
            return determineRow;
        }
        public int getCol()
        {
            return determineCol;
        }
        public void GameBoardSet(int x, int y, Piece PLAYER)
        {
            GameBoard[x, y] = PLAYER;
        }

        public Piece GameBoardGet(int x, int y)
        {
            return GameBoard[x, y];
        }

        public Piece[,] GameBoardGet()
        {
            return GameBoard;
        }


        //************Piece movement settings*******************//

        public Piece moveStraight(Piece PLAYER, int x, int y)
        {
            switch (PLAYER)
            {
                case Piece.PLAYER1:
                    {
                        //rangecheck
                        if (y >= 0 && y <= determineCol - 2)
                        {
                            //Check if the player 1 piece is selected
                            if (GameBoard[x, y] == Piece.PLAYER1)
                            {
                                //check if the move is possible
                                if (GameBoard[x, y + 1] == Piece.BLANK)
                                {
                                    GameBoard[x, y] = Piece.BLANK;
                                    GameBoard[x, y + 1] = Piece.PLAYER1;
                                    //Succus, now player 2's turn
                                    return Piece.PLAYER2;
                                }
                                //if not possible, return player 1 agian!
                                else if (GameBoard[x, y + 1] != Piece.BLANK)
                                {
                                    return Piece.PLAYER1;
                                }
                            }
                            // player 1 is not selected! return player1
                            return Piece.PLAYER1;
                        }
                        else
                        {
                            return Piece.PLAYER1;
                        }
                    }
                case Piece.PLAYER2:
                    {
                        if (y >= 1 && y <= determineCol - 1)
                        {
                            //Check if the player 2 piece is selected
                            if (GameBoard[x, y] == Piece.PLAYER2)
                            {
                                //check if the move is possible
                                if (GameBoard[x, y - 1] == Piece.BLANK)
                                {
                                    GameBoard[x, y] = Piece.BLANK;
                                    GameBoard[x, y - 1] = Piece.PLAYER2;
                                    //Succus, now player 1's turn
                                    return Piece.PLAYER1;
                                }
                                //if not possible, reuturn player 2!
                                else if (GameBoard[x, y - 1] != Piece.BLANK)
                                {
                                    //ILLEGAL MOVE! Try again
                                    return Piece.PLAYER2;
                                }
                            }
                            //player 2 is not selected, try agin
                            return Piece.PLAYER2;
                        }
                        else
                        {
                            //out of reach, try again
                            return Piece.PLAYER2;
                        }
                    }
                default:
                    {
                        //if somethings wrong, return own player to try again
                        return CurrentPLAYER;
                    }
            }
        }

        public Piece attackLeft(Piece PLAYER, int x, int y)
        {
            switch (PLAYER)
            {
                case Piece.PLAYER1:
                    {
                        //Check if the attack is in range
                        if ((x >= 1 && x <= determineRow - 1) && (y >= 0 && y <= determineCol - 2))
                        {
                            //check if the field is attackable
                            if (GameBoard[x - 1, y + 1] == Piece.PLAYER2)
                            {
                                GameBoard[x, y] = Piece.BLANK;
                                GameBoard[x - 1, y + 1] = Piece.PLAYER1;
                            }
                            else
                            {
                                //Illegal move, attacking own brick/BLANK
                                return Piece.PLAYER1;
                            }
                        }
                        else
                        {
                            //illegal move
                            return Piece.PLAYER1;
                        }
                        return Piece.PLAYER2;
                    }
                case Piece.PLAYER2:
                    {
                        //Check if the attack is NOT in range
                        if ((x >= 1 && x <= determineRow - 1) && (y >= 1 && y <= determineCol - 1))
                        {
                            //check if field is attackable
                            if (GameBoard[x - 1, y - 1] == Piece.PLAYER1)
                            {
                                GameBoard[x, y] = Piece.BLANK;
                                GameBoard[x - 1, y - 1] = Piece.PLAYER2;
                            }
                            else
                            {
                                //Illegal move, attacking own brick/BLANK
                                return Piece.PLAYER2;
                            }

                        }
                        else
                        {
                            return Piece.PLAYER2;
                        }
                        return Piece.PLAYER1;
                    }
                default:
                    {
                        //if somethings wrong, return own player to try again
                        return CurrentPLAYER;
                    }
            }
        }

        public Piece attackRight(Piece PLAYER, int x, int y)
        {
            switch (PLAYER)
            {
                case Piece.PLAYER1:
                    {
                        //Check if the attack is in range
                        if ((x >= 0 && x <= determineRow - 2) && (y >= 0 && y <= determineCol - 2))
                        {
                            //check if the field is attackable
                            if (GameBoard[x + 1, y + 1] == Piece.PLAYER2)
                            {
                                GameBoard[x, y] = Piece.BLANK;
                                GameBoard[x + 1, y + 1] = Piece.PLAYER1;
                            }
                            else
                            {
                                //Illegal move, attacking own brick/BLANK
                                return Piece.PLAYER1;
                            }
                        }
                        else
                        {
                            //illegal move
                            return Piece.PLAYER1;
                        }
                        //If all okay, return next player
                        return Piece.PLAYER2;
                    }
                case Piece.PLAYER2:
                    {
                        //Check if the attack is in range
                        if ((x >= 0 && x <= determineRow - 2) && (y >= 1 && y <= determineCol - 1))
                        {
                            //check if field is attackable
                            if (GameBoard[x + 1, y - 1] == Piece.PLAYER1)
                            {
                                GameBoard[x, y] = Piece.BLANK;
                                GameBoard[x + 1, y - 1] = Piece.PLAYER2;
                            }
                            else
                            {
                                //Illegal move, attacking own brick/BLANK
                                return Piece.PLAYER2;
                            }

                        }
                        else
                        {
                            return Piece.PLAYER2;
                        }
                        return Piece.PLAYER1;
                    }
                default:
                    {
                        //if somethings wrong, return own player to try again
                        return CurrentPLAYER;
                    }

            }
        }



        //**********Determine winner**************************//

        public Piece checkfirstWin()
        {
            for (int i = 0; i < determineRow; i++)
            {
                if (GameBoard[i, determineCol - 1] == Piece.PLAYER1)
                {
                    return Piece.PLAYER1;
                }
                if ((GameBoard[i, 0] == Piece.PLAYER2))
                {
                    return Piece.PLAYER2;
                }
            }
            return Piece.BLANK;
        }

        public Piece checkNonMoveWin()
        {
            int unmoveblePieceCount = 0; // count number of unmoveble pieces.
            int totalPieceCountPl1 = 0;

            //*****************************************CHECK FOR PLAYER 1**********************************///
            for (int i = 0; i < determineRow; i++)
            {
                for (int j = 0; j < determineCol; j++)
                {
                   
                    if (GameBoard[i, j] == Piece.PLAYER1)
                    {
                        totalPieceCountPl1++;
                        //Check left side of the board
                        if ((i == 0) && ((0 <= j) && (j <= determineCol - 2))) // between 0 and the max y size. ensure outofbound array error by offsetting -2
                        {

                            if (((GameBoard[i + 1, j + 1] == Piece.BLANK) || (GameBoard[i + 1, j + 1] == Piece.PLAYER1)) && (GameBoard[i, j + 1] != Piece.BLANK))
                            {
                                unmoveblePieceCount++;
                            }
                        }
                        //check right side
                        if ((i == determineRow - 1) && ((0 <= j) && (j <= determineCol - 2)))
                        {
                            if (((GameBoard[i - 1, j + 1] == Piece.BLANK) || (GameBoard[i - 1, j + 1] == Piece.PLAYER1)) && (GameBoard[i, j + 1] != Piece.BLANK))
                            {
                                unmoveblePieceCount++;
                            }
                        }
                        // Check the rest
                        if (((1 <= i) && (i <= determineRow - 2)) && ((0 <= j) && (j <= determineCol - 2)))
                        {
                            //if((GameBoard[i-1,j+1] != Piece.PLAYER2)||(GameBoard[i+1,j+1] != Piece.PLAYER2)||(GameBoard[i-1,j+1] != Piece.BLANK))
                            if (((GameBoard[i - 1, j + 1] == Piece.BLANK) || (GameBoard[i - 1, j + 1] == Piece.PLAYER1)) & ((GameBoard[i + 1, j + 1] == Piece.BLANK) | (GameBoard[i + 1, j + 1] == Piece.PLAYER1)) & (GameBoard[i, j + 1] != Piece.BLANK))
                            {
                                unmoveblePieceCount++;
                            }
                        }


                    }

                }
            }
            //debug info
            //Console.WriteLine("Player 1\nunmoveblePieceCount: " + unmoveblePieceCount);
            //Console.WriteLine("totalpiece: " + totalPieceCountPl1);
       
                if (unmoveblePieceCount == totalPieceCountPl1)
                {
                    //Return the winner
                    return Piece.PLAYER2;
                }
            
            //******************************CHECK FOR PLAYER 1 ENDED***********************************//

            //******************************CHECK FOR PLAYER 2 ****************************************//
            unmoveblePieceCount = 0; // count number of unmoveble pieces.
            int totalPieceCountpl2 = 0;
            for (int i = 0; i < determineRow; i++)
            {
                for (int j = 0; j < determineCol; j++)
                {
                    if (GameBoard[i, j] == Piece.PLAYER2)
                    {
                        totalPieceCountpl2++;

                        //Check left side of the board
                        if ((i == 0) && ((1 <= j) && (j <= determineCol - 1))) // between 0 and the max y size.
                        {

                            if (((GameBoard[i + 1, j - 1] == Piece.BLANK) || (GameBoard[i + 1, j - 1] == Piece.PLAYER2)) && (GameBoard[i, j - 1] != Piece.BLANK))
                            {
                                unmoveblePieceCount++;
                            }
                        }
                        //check right side
                        if ((i == determineRow - 1) && ((1 <= j) & (j <= determineCol - 1)))
                        {
                            if (((GameBoard[i, j - 1] != Piece.BLANK) || (GameBoard[i - 1, j - 1] == Piece.BLANK)) && (GameBoard[i - 1, j - 1] == Piece.PLAYER2))
                            {
                                unmoveblePieceCount++;
                            }
                        }

                        // Check the rest
                        if (((1 <= i) & (i <= determineRow - 2)) & ((1 <= j) & (j <= determineCol - 2)))
                        {
                            if (((GameBoard[i - 1, j - 1] == Piece.BLANK) || (GameBoard[i - 1, j - 1] == Piece.PLAYER2)) && ((GameBoard[i + 1, j - 1] == Piece.BLANK) || (GameBoard[i + 1, j - 1] == Piece.PLAYER2)) && (GameBoard[i, j - 1] != Piece.BLANK))
                            {
                                unmoveblePieceCount++;
                            }
                        }


                    }

                }
            }
            //debug info
            //Console.WriteLine("Player 2\nunmoveblePieceCount: " + unmoveblePieceCount);
            //Console.WriteLine("totalpiece: " + totalPieceCountpl2);

           
                if (unmoveblePieceCount == totalPieceCountpl2)
                {
                    //Return the winner
                    return Piece.PLAYER1;
                }
            
            //******************************CHECK FOR PLAYER 2 ENDED***********************************//
            return Piece.BLANK;
        }

        //****************CopyBoard*************************//
        public struct CopyBoard
        {
            public Logic.Piece[,] FakeBoard;



            public CopyBoard(int row, int col, Logic logic)
            {
                FakeBoard = new Logic.Piece[row, col];

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        FakeBoard[i, j] = logic.GameBoardGet(i, j);
                    }
                }
            }

            public CopyBoard(int row, int col, CopyBoard B)
            {
                FakeBoard = new Logic.Piece[row, col];

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        FakeBoard[i, j] = B.FakeBoard[i, j];
                    }
                }
            }

            public UInt32 HashCode(int row, int col)
            {
                UInt32 resultHash = 0;
                int n = 0;
                UInt32 pieceValue = 0;
                for (int j = 0; j < row; j++)
                {
                    for (int i = 0; i < col; i++)
                    {
                        if (FakeBoard[i, j] == Logic.Piece.BLANK)
                        {
                            pieceValue = 0;
                        }
                        if (FakeBoard[i, j] == Logic.Piece.PLAYER1)
                        {
                            pieceValue = 1;
                        }
                        if (FakeBoard[i, j] == Logic.Piece.PLAYER2)
                        {
                            pieceValue = 2;
                        }
                        resultHash += (UInt32)Math.Pow(3, n) * pieceValue;
                        n++;
                    }
                }
                return resultHash;
            }
        }

        //************Legal moves***************************//
        public LegalMove[] CheckLegalMoves(Logic.Piece Player, CopyBoard Board)
        {
            List<LegalMove> LegitMovesArray = new List<LegalMove>();
            //Gameboard check
            for (int i = 0; i < determineRow; i++)
            {
                for (int j = 0; j < determineCol; j++)
                {
                    if (Board.FakeBoard[i, j] == Player)
                    {
                        if (Player == Logic.Piece.PLAYER1 && j != determineCol - 1)
                        {
                            //Check if pawns can move forward.(player1)
                            if (Board.FakeBoard[i, j + 1] == Logic.Piece.BLANK)
                            {
                                LegitMovesArray.Add(new LegalMove(i, j, i, j + 1, false));
                            }

                            //Check if pawns can attack. Make sure no checks outside of board.(Player1)
                            if (i != 0 && i != determineRow - 1)
                            {
                                if (Board.FakeBoard[i - 1, j + 1] == Logic.Piece.PLAYER2)
                                {
                                    LegitMovesArray.Add(new LegalMove(i, j, i - 1, j + 1, true));
                                }

                                if (Board.FakeBoard[i + 1, j + 1] == Logic.Piece.PLAYER2)
                                {
                                    LegitMovesArray.Add(new LegalMove(i, j, i + 1, j + 1, true));
                                }
                            }
                            if (i == 0)
                            {
                                if (Board.FakeBoard[i + 1, j + 1] == Logic.Piece.PLAYER2)
                                {
                                    LegitMovesArray.Add(new LegalMove(i, j, i + 1, j + 1, true));
                                }
                            }
                            if (i == determineRow - 1)
                            {
                                if (Board.FakeBoard[i - 1, j + 1] == Logic.Piece.PLAYER2)
                                {
                                    LegitMovesArray.Add(new LegalMove(i, j, i - 1, j + 1, true));
                                }
                            }

                        }


                        if (Player == Logic.Piece.PLAYER2 && j != 0)
                        {
                            //Check if pawns can move forward.(player1)
                            if (Board.FakeBoard[i, j - 1] == Logic.Piece.BLANK)
                            {
                                LegitMovesArray.Add(new LegalMove(i, j, i, j - 1, false));
                            }

                            //Check if pawns can attack. Make sure no checks outside of board.(Player1)
                            if (i != 0 && i != determineRow - 1)
                            {
                                if (Board.FakeBoard[i - 1, j - 1] == Logic.Piece.PLAYER1)//atk left
                                {
                                    LegitMovesArray.Add(new LegalMove(i, j, i - 1, j - 1, true));
                                }

                                if (Board.FakeBoard[i + 1, j - 1] == Logic.Piece.PLAYER1)//atk right
                                {
                                    LegitMovesArray.Add(new LegalMove(i, j, i + 1, j - 1, true));
                                }
                            }
                            if (i == 0)
                            {
                                if (Board.FakeBoard[i + 1, j - 1] == Logic.Piece.PLAYER1)//atk right
                                {
                                    LegitMovesArray.Add(new LegalMove(i, j, i + 1, j - 1, true));
                                }
                            }
                            if (i == determineRow - 1)
                            {
                                if (Board.FakeBoard[i - 1, j - 1] == Logic.Piece.PLAYER1)//atk left
                                {
                                    LegitMovesArray.Add(new LegalMove(i, j, i - 1, j - 1, true));
                                }
                            }
                        }


                    }
                }
            }
            return LegitMovesArray.ToArray();
        }

    }
 

    
}
