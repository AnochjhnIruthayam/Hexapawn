using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HexapawnLogic
{
    class GuardMove
    {
        public int FromX, FromY;


        public GuardMove(int FromX, int FromY)
        {
            this.FromX = FromX;
            this.FromY = FromY;
        }
    }

    public class Bruteforce : Logic
    {
        public Bruteforce() { }


        ////********************Bruteforce*********************//

        public Piece AIBruteforce(Piece AIPlayer, Piece Opponent, Logic logic)
        {
            List<GuardMove> GuardDoubleList = new List<GuardMove>(); //Keeps tab of double guarded pawns
            List<GuardMove> GuardSingleList = new List<GuardMove>(); //Same but just for one pawn guard
            List<GuardMove> NoGuardJustMove = new List<GuardMove>();


            for (int i = 0; i < logic.determineRow; i++)
            {
                for (int j = 0; j < logic.determineCol; j++)
                {
                    //player 1 is AI
                    if (logic.GameBoardGet(i, j) == AIPlayer && AIPlayer == Piece.PLAYER1)
                    {
                        if (i > 0 && i < logic.determineRow - 1 && j != logic.determineCol - 1)
                        {
                            //logic.attackLeft
                            if (logic.GameBoardGet(i - 1, j + 1) == Opponent)
                            {
                                return logic.attackLeft(AIPlayer, i, j);
                            }
                            //logic.attackRight
                            if (logic.GameBoardGet(i + 1, j + 1) == Opponent)
                            {
                                return logic.attackRight(AIPlayer, i, j);

                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                if (j != logic.determineCol - 1)
                                    //logic.attackRight;
                                    if (logic.GameBoardGet(i + 1, j + 1) == Opponent)
                                    {
                                        return logic.attackRight(AIPlayer, i, j);
                                    }

                            }
                            else
                            {
                                if (j != logic.determineCol - 1)
                                    //logic.attackLeft
                                    if (logic.GameBoardGet(i - 1, j + 1) == Opponent)
                                    {
                                        return logic.attackLeft(AIPlayer, i, j);

                                    }
                            }
                        }

                        if (i != 0 && i != logic.determineRow - 1 && j != logic.determineCol - 1 && logic.GameBoardGet(i, j + 1) == Piece.BLANK)
                        {
                            if (logic.GameBoardGet(i - 1, j) == Piece.PLAYER1 && logic.GameBoardGet(i + 1, j) == Piece.PLAYER1)
                            {
                                //double guard
                                GuardMove Doubled = new GuardMove(i, j);
                                GuardDoubleList.Add(Doubled);
                            }
                            if (logic.GameBoardGet(i - 1, j) == Piece.PLAYER1 || logic.GameBoardGet(i + 1, j) == Piece.PLAYER1)
                            {
                                //double guard
                                GuardMove Singled = new GuardMove(i, j);
                                GuardSingleList.Add(Singled);
                            }
                        }

                        if (i == 0 && j != logic.determineCol - 1 && logic.GameBoardGet(i, j + 1) == Piece.BLANK)
                        {
                            if (logic.GameBoardGet(i + 1, j) == Piece.PLAYER1)
                            {
                                GuardMove Singled = new GuardMove(i, j);
                                GuardSingleList.Add(Singled);
                            }
                        }

                        if (i == logic.determineRow - 1 && j != logic.determineCol - 1 && logic.GameBoardGet(i, j + 1) == Piece.BLANK)
                        {
                            if (logic.GameBoardGet(i - 1, j) == Piece.PLAYER1)
                            {
                                GuardMove Singled = new GuardMove(i, j);
                                GuardSingleList.Add(Singled);
                            }
                        }

                        //MoveStuff
                        if (j != logic.determineCol - 1)
                            if (logic.GameBoardGet(i, j + 1) == Piece.BLANK)
                            {
                                GuardMove Moving = new GuardMove(i, j);
                                NoGuardJustMove.Add(Moving);

                                //return logic.moveStraight(AIPlayer, i, j);

                            }
                    }

                    //Player 2 is AI
                    if (logic.GameBoardGet(i, j) == AIPlayer && AIPlayer == Piece.PLAYER2)
                    {
                        if (i > 0 && i < logic.determineRow - 1 && j != 0)
                        {
                            //logic.attackLeft
                            if (logic.GameBoardGet(i - 1, j - 1) == Opponent)
                            {
                                return logic.attackLeft(AIPlayer, i, j);
                            }
                            //logic.attackRight
                            if (logic.GameBoardGet(i + 1, j - 1) == Opponent)
                            {
                                return logic.attackRight(AIPlayer, i, j);

                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                if (j != 0)
                                    //logic.attackRight;
                                    if (logic.GameBoardGet(i + 1, j - 1) == Opponent)
                                    {
                                        return logic.attackRight(AIPlayer, i, j);
                                    }

                            }
                            else
                            {
                                if (j != 0)
                                    //logic.attackLeft
                                    if (logic.GameBoardGet(i - 1, j - 1) == Opponent)
                                    {
                                        return logic.attackLeft(AIPlayer, i, j);

                                    }
                            }
                        }


                        if (i != 0 && i != logic.determineRow - 1 && j != 0 && logic.GameBoardGet(i, j - 1) == Piece.BLANK)
                        {
                            if (logic.GameBoardGet(i - 1, j) == Piece.PLAYER2 && logic.GameBoardGet(i + 1, j) == Piece.PLAYER2)
                            {
                                //double guard
                                GuardMove Doubled = new GuardMove(i, j);
                                GuardDoubleList.Add(Doubled);
                            }
                            if (logic.GameBoardGet(i - 1, j) == Piece.PLAYER2 || logic.GameBoardGet(i + 1, j) == Piece.PLAYER2)
                            {
                                //double guard
                                GuardMove Singled = new GuardMove(i, j);
                                GuardSingleList.Add(Singled);
                            }
                        }

                        if (i == 0 && j != 0 && logic.GameBoardGet(i, j - 1) == Piece.BLANK)
                        {
                            if (logic.GameBoardGet(i + 1, j) == Piece.PLAYER2)
                            {
                                GuardMove Singled = new GuardMove(i, j);
                                GuardSingleList.Add(Singled);
                            }
                        }

                        if (i == logic.determineRow - 1 && j != 0 && logic.GameBoardGet(i, j - 1) == Piece.BLANK)
                        {
                            if (logic.GameBoardGet(i - 1, j) == Piece.PLAYER2)
                            {
                                GuardMove Singled = new GuardMove(i, j);
                                GuardSingleList.Add(Singled);
                            }
                        }

                        //MoveStuff
                        if (j != 0)
                            if (logic.GameBoardGet(i, j - 1) == Piece.BLANK)
                            {
                                GuardMove Moving = new GuardMove(i, j);
                                NoGuardJustMove.Add(Moving);

                                //return logic.moveStraight(AIPlayer, i, j);

                            }
                    }

                }
            }

            int RandomArrayPosition;
            Random Random = new Random();
            int MoveX = -1;
            int MoveY = -1;

            //Double guard;
            if (GuardDoubleList.Count != 0)
            {
                RandomArrayPosition = Random.Next(0, GuardDoubleList.Count);
                //Console.Write(RandomArrayPosition+" Double");

                MoveX = GuardDoubleList[RandomArrayPosition].FromX;
                MoveY = GuardDoubleList[RandomArrayPosition].FromY;

                return logic.moveStraight(AIPlayer, MoveX, MoveY);
            }
            //Single guard
            if (GuardSingleList.Count != 0)
            {
                RandomArrayPosition = Random.Next(0, GuardSingleList.Count);
                // Console.Write(RandomArrayPosition+" Single");

                MoveX = GuardSingleList[RandomArrayPosition].FromX;
                MoveY = GuardSingleList[RandomArrayPosition].FromY;

                return logic.moveStraight(AIPlayer, MoveX, MoveY);
            }
            //Merely moving
            if (NoGuardJustMove.Count != 0)
            {
                RandomArrayPosition = Random.Next(0, NoGuardJustMove.Count);
                // Console.Write(RandomArrayPosition + " Moving");

                MoveX = NoGuardJustMove[RandomArrayPosition].FromX;
                MoveY = NoGuardJustMove[RandomArrayPosition].FromY;

                return logic.moveStraight(AIPlayer, MoveX, MoveY);
            }




            return Piece.BLANK;
        }



    }


}
