using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexapawnLogic
{
    public class MiniMax : Logic
    {
        public MiniMax(int x, int y)
        {
            determineRow = x;
            determineCol = y;
        }
        private int foresight = 0;

        public LegalMove MovePieceChosen(int foresight, Logic.Piece AIPlayer, Logic.Piece Enemy, Logic game) //keeps tab on the best move currently.
        {
            CopyBoard TheBoard = new CopyBoard(determineRow, determineCol, game);
            LegalMove[] AIMoves = CheckLegalMoves(AIPlayer, TheBoard);

            LegalMove Chosen = new LegalMove(2, 2, 2, 2, false);

            double MinimaxRating = 0;
            double Estimation = double.NegativeInfinity;

            if (AIMoves.Length == 0)
            {
                // Console.Write("Enemy of AI wins"); 
            }

            foreach (LegalMove move in AIMoves)
            {
                if (move.ToY == 0 || move.ToY == determineCol - 1)
                {
                    Chosen = move;
                    //Console.Write("Winning move from/to: "+"("+Chosen.FromX+","+Chosen.FromY+")("+Chosen.ToX+","+Chosen.ToY+")");
                    return Chosen;
                }

                this.foresight = 0;
                MinimaxRating = 0;
                //Estimation = double.NegativeInfinity;
                CopyBoard FutureBoard = new CopyBoard(determineRow, determineCol, game);

                //FutureBoard = TheBoard;
                FutureBoard.FakeBoard[move.FromX, move.FromY] = Logic.Piece.BLANK;
                FutureBoard.FakeBoard[move.ToX, move.ToY] = AIPlayer;
                LegalMove[] EnemyMoves = CheckLegalMoves(Enemy, FutureBoard);

                if (EnemyMoves.Length == 0)
                {
                    Chosen = move;
                    //Console.Write("AI(Minimax) wins"); 
                    return Chosen;
                }


                MinimaxRating += NumericalValueUtillity(TheBoard, FutureBoard, move.casualty, AIPlayer);
                MinimaxRating -= calcMinValue(foresight, AIPlayer, Enemy, FutureBoard);

                if (MinimaxRating >= Estimation)
                {
                    Estimation = MinimaxRating;
                    Chosen = move;
                }

            }
            // Console.Write("Minimax return value: " + MinimaxRating+"\n");
            // Console.Write("Chosen move: "+ Chosen.FromX+Chosen.FromY+Chosen.ToX+Chosen.ToY + "\n");
            return Chosen;
        }
        //Calc Max Value for Minimax
        private double calcMaxValue(int foresight, Logic.Piece AIPlayer, Logic.Piece Enemy, CopyBoard CurrentGame)
        {
            if (this.foresight < foresight) { this.foresight++; } //amount of turns the AI can look into the future.

            double MaxValue = Double.NegativeInfinity;
            LegalMove[] Legit = CheckLegalMoves(AIPlayer, CurrentGame);

            //A loss is negative in infinity, while a win is positive infinity value. (Terminal test)

            //No possible moves.
            if (Legit.Length == 0)
            {
                MaxValue = double.NegativeInfinity;
                return MaxValue;
            }

            //One of "my" pieces got to the lastspace (in a move) 
            foreach (LegalMove move in Legit)
            {
                if (move.ToY == determineCol - 1)//|| move.ToY == 0)
                {
                    MaxValue = double.PositiveInfinity;
                    return MaxValue;
                }
            }

            if (this.foresight < foresight)
            {
                //double HigherValue=0; //Checks for higher numerical values.
                double ValueOfMove = 0; //Numerical Temp


                foreach (LegalMove move in Legit)
                {
                    //set up the new future board.
                    CopyBoard FutureGameBoard = new CopyBoard(determineRow, determineCol, CurrentGame);
                    FutureGameBoard.FakeBoard[move.FromX, move.FromY] = Logic.Piece.BLANK;
                    FutureGameBoard.FakeBoard[move.ToX, move.ToY] = AIPlayer;

                    //Call value for one move.
                    //ValueOfMove = NumericalValueUtillity(CurrentGame, FutureGameBoard, move.casualty, AIPlayer);

                    //ValueOfMove += calcMinValue(foresight, AIPlayer, Enemy, FutureGameBoard);
                    double ValueofMax = NumericalValueUtillity(CurrentGame, FutureGameBoard, move.casualty, AIPlayer);
                    double ValueofMin = calcMaxValue(foresight, AIPlayer, Enemy, FutureGameBoard);
                    ValueOfMove = ValueofMax - ValueofMin;
                    if (MaxValue < ValueOfMove)
                    {

                        MaxValue = ValueOfMove;
                        //Chosen = move;
                    }

                }
            }
            else //Iterations run out. Return nothing.
            {
                return 0;
            }

            //Console.Write("MaxValue: "); Console.Write(MaxValue);
            return MaxValue;
        }
        //Calc Minimum Value for Minimax
        private double calcMinValue(int foresight, Logic.Piece AIEnemy, Logic.Piece NonAIPlayer, CopyBoard CurrentGame)
        {
            if (this.foresight < foresight) { this.foresight++; } //amount of turns the AI can look into the future.

            double MinValue = Double.NegativeInfinity;
            LegalMove[] Legit = CheckLegalMoves(NonAIPlayer, CurrentGame);

            //A loss is negative in infinity, while a win is positive infinity value. (Terminal test)

            if (Legit.Length == 0)
            {
                MinValue = Double.NegativeInfinity;
                return MinValue;
            }

            //One of "my" pieces got to the lastspace (in a move) 
            foreach (LegalMove move in Legit)
            {
                if (/*move.ToY == determineCol - 1 ||*/ move.ToY == 0)
                {
                    MinValue = Double.PositiveInfinity;
                    return MinValue;
                }
            }

            if (this.foresight < foresight)
            {
                double ValueOfMove = 0; //Numerical Temp
                foreach (LegalMove move in Legit)
                {
                    //set up the new future board.
                    CopyBoard FutureGameBoard = new CopyBoard(determineRow, determineCol, CurrentGame);
                    FutureGameBoard.FakeBoard[move.FromX, move.FromY] = Logic.Piece.BLANK;
                    FutureGameBoard.FakeBoard[move.ToX, move.ToY] = NonAIPlayer;

                    //Call value for one move.
                    double ValueofMin = NumericalValueUtillity(CurrentGame, FutureGameBoard, move.casualty, NonAIPlayer);
                    double ValueofMax = calcMaxValue(foresight, AIEnemy, NonAIPlayer, FutureGameBoard);
                    ValueOfMove = ValueofMin - ValueofMax;
                    if (ValueOfMove > MinValue)
                    {
                        MinValue = ValueOfMove;
                        //Chosen = move;
                    }
                }
            }
            else //Iterations run out. Return nothing.
            {
                return 0;
            }
            //Console.Write("MinValue: "); Console.Write(MinValue);
            //MinValue = MinValue * -1;
            return MinValue;
        }

        private double NumericalValueUtillity(CopyBoard present, CopyBoard future, bool casualty, Logic.Piece Player) //Present board vs Future board (next move)
        {
            double NumericalPresent = 0;
            double NumericalFuture = 0;
            int MyPieces = 0;
            int EnemyPieces = 0;

            if (casualty == true) { NumericalFuture += 3; }




            //Present
            for (int i = 0; i < determineRow; i++)
            {
                for (int j = 0; j < determineCol; j++)
                {
                    if (present.FakeBoard[i, j] != Logic.Piece.BLANK)
                    {
                        //Isolate AI pieces.
                        if (present.FakeBoard[i, j] == Player)
                        {
                            //Guarded pieces
                            if (Player == Logic.Piece.PLAYER1)
                            {
                                if (i != 0 && j != 0 && i != determineRow - 1 && j != determineCol - 1) //All the board except edges guarded
                                {
                                    if (present.FakeBoard[i + 1, j - 1] == Player)
                                    {
                                        NumericalPresent += 1;
                                    }
                                    if (present.FakeBoard[i - 1, j - 1] == Player)
                                    {
                                        NumericalPresent += 1;
                                    }
                                }

                                if (i == 0 && j != determineCol - 1 && j != 0) //edge i=0 guarded
                                {
                                    if (present.FakeBoard[i + 1, j - 1] == Player)
                                    {
                                        NumericalPresent += 1;
                                    }
                                }
                                if (i == determineRow - 1 && j != determineCol - 1 && j != 0) //edge determine-1 guarded
                                {
                                    if (present.FakeBoard[i - 1, j - 1] == Player)
                                    {
                                        NumericalPresent += 1;
                                    }
                                }
                            }
                            else
                            {
                                if (i != 0 && j != 0 && i != determineRow - 1 && j != determineCol - 1) //All the board except edges guarded
                                {
                                    if (present.FakeBoard[i + 1, j + 1] == Player)
                                    {
                                        NumericalPresent += 1;
                                    }
                                    if (present.FakeBoard[i - 1, j + 1] == Player)
                                    {
                                        NumericalPresent += 1;
                                    }
                                }
                                if (i == 0 && j != determineCol - 1 && j != 0) //edge i=0 guarded
                                {
                                    if (present.FakeBoard[i + 1, j + 1] == Player)
                                    {
                                        NumericalPresent += 1;
                                    }
                                }
                                if (i == determineRow - 1 && j != determineCol - 1 && j != 0) //edge determine-1 guarded
                                {
                                    if (present.FakeBoard[i - 1, j + 1] == Player)
                                    {
                                        NumericalPresent += 1;
                                    }
                                }

                            }

                        }
                    }
                }
            }
            //Future
            for (int i = 0; i < determineRow; i++)
            {
                for (int j = 0; j < determineCol; j++)
                {
                    if (future.FakeBoard[i, j] != Logic.Piece.BLANK)
                    {
                        if (future.FakeBoard[i, j] != Player)
                        {
                            EnemyPieces += 1;
                        }
                        //Isolate AI pieces.
                        if (future.FakeBoard[i, j] == Player)
                        {
                            //Firststrike bonus
                            MyPieces += 1;

                            //Guarded pieces
                            if (Player == Logic.Piece.PLAYER1)
                            {
                                if (i != 0 && j != 0 && i != determineRow - 1 && j != determineCol - 1) //All the board except edges guarded
                                {
                                    if (future.FakeBoard[i + 1, j - 1] == Player)
                                    {
                                        NumericalFuture += 1;
                                    }
                                    if (future.FakeBoard[i - 1, j - 1] == Player)
                                    {
                                        NumericalFuture += 1;
                                    }
                                }

                                if (i == 0 && j != determineCol - 1 && j != 0) //edge i=0 guarded
                                {
                                    if (future.FakeBoard[i + 1, j - 1] == Player)
                                    {
                                        NumericalFuture += 1;
                                    }
                                }
                                if (i == determineRow - 1 && j != determineCol - 1 && j != 0) //edge determine-1 guarded
                                {
                                    if (future.FakeBoard[i - 1, j - 1] == Player)
                                    {
                                        NumericalFuture += 1;
                                    }
                                }
                            }
                            else
                            {
                                if (i != 0 && j != 0 && i != determineRow - 1 && j != determineCol - 1) //All the board except edges guarded
                                {
                                    if (future.FakeBoard[i + 1, j + 1] == Player)
                                    {
                                        NumericalFuture += 1;
                                    }
                                    if (future.FakeBoard[i - 1, j + 1] == Player)
                                    {
                                        NumericalFuture += 1;
                                    }
                                }
                                if (i == 0 && j != determineCol - 1 && j != 0) //edge i=0 guarded
                                {
                                    if (future.FakeBoard[i + 1, j + 1] == Player)
                                    {
                                        NumericalFuture += 1;
                                    }
                                }
                                if (i == determineRow - 1 && j != determineCol - 1 && j != 0) //edge determine-1 guarded
                                {
                                    if (future.FakeBoard[i - 1, j + 1] == Player)
                                    {
                                        NumericalFuture += 1;
                                    }
                                }

                            }

                        }
                    }
                }
            }


            if (MyPieces > EnemyPieces)
            {
                NumericalFuture += 2;
            }


            double NumericalValue = NumericalFuture - NumericalPresent;

            return NumericalValue;
        }
    }
}
