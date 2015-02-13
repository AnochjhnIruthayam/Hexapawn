using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HexapawnLogic;

namespace HexapawnConsole
{
    public class HexaConsole : Logic
    {
        Logic gameLogicConsole = new Logic();
        private bool gameStatus = true;
        public HexaConsole() { }
        public HexaConsole(int x, int y)
        {
            determineRow = x;
            determineCol = y;
            InitializePieces();
        }
        private void InitializePieces()
        {
            gameLogicConsole.GameBoardGenerate(determineRow, determineCol);
            //Player pieces
            for (int i = 0; i < determineRow; i++)
            {
                gameLogicConsole.GameBoardSet(i, 0,Logic.Piece.PLAYER1); //player 1.
                gameLogicConsole.GameBoardSet(i, determineCol - 1,Logic.Piece.PLAYER2); //player 2.

            }

            //Blanks
            for (int i = 0; i < determineRow; i++)
            {
                for (int j = 1; j < determineCol - 1; j++)
                {
                    gameLogicConsole.GameBoardSet(i, j, Logic.Piece.BLANK);
                }
            }
        }

        public void PrintBoard()
        {
            for (int i = 0; i < determineCol; i++)
            {
                Console.WriteLine("\n");

                for (int j = 0; j < determineRow; j++)
                {

                    if (gameLogicConsole.GameBoardGet(j, i) == Logic.Piece.PLAYER1)
                    {
                        Console.Write("[1] ");
                    }
                    if (gameLogicConsole.GameBoardGet(j, i) == Logic.Piece.PLAYER2)
                    {
                        Console.Write("[2] ");
                    }
                    if (gameLogicConsole.GameBoardGet(j, i) == Logic.Piece.BLANK)
                    {
                        Console.Write("[ ] ");
                    }
                }
            }

            Console.WriteLine("\n");
            Console.WriteLine("\n");
        }
     
        public void testRunQ(bool printBoard, bool printWinList)
        {
            CopyBoard currentState = new CopyBoard(determineRow, determineCol, gameLogicConsole);
            Bruteforce brute= new Bruteforce();
            MiniMax minimax = new MiniMax(determineRow, determineCol);
            QLearning q = new QLearning(determineRow, determineCol);
            LegalMove Qmove = default(LegalMove);
            LegalMove MMmove = default(LegalMove);
            // Compose a string that consists of three lines.
            //string lines = "First line.\r\nSecond line.\r\nThird line.";

            // Write the string to a file.
            int filecount = 0;
            int count = 0;
            int QwinCount = 0;
            //int player1 = 0, player2 = 0;

            //Console.Write("Select Player 1\n 1. Human, 2. Bruteforce, 3. MiniMax, 4. Q-Learning\nPlayer 1: ");
            //player1 = int.Parse(Console.ReadLine());
            //Console.Write("\nSelect Player 2\n 1. Human, 2. Bruteforce, 3. MiniMax, 4. Q-Learning\nPlayer 2: ");
            //player2 = int.Parse(Console.ReadLine());



            //try
            //{
            //    q.load("test");
            //}
            //catch (Exception)
            //{
            //    Console.Write("Loading failed! Creating new files\nPress any key to continue\n");
            //    Console.Read();
            //}
            

            //assign player
            Logic.Piece P1 = Logic.Piece.PLAYER1;
            Logic.Piece P2 = Logic.Piece.PLAYER2;

            gameStatus = true;

            while (true)
            {

                while (gameStatus)
                {
                    //*********************************MINIMAX******************************************//
                    currentState = new CopyBoard(determineRow, determineCol, gameLogicConsole);
                    MMmove = new LegalMove(1, 1, 1, 1, false);
                    MMmove = minimax.MovePieceChosen(30, P1, P2, gameLogicConsole);
                    gameLogicConsole.GameBoardSet(MMmove.FromX, MMmove.FromY, Logic.Piece.BLANK);
                    gameLogicConsole.GameBoardSet(MMmove.ToX, MMmove.ToY, P1);


                    if (gameLogicConsole.checkfirstWin() == P1 || gameLogicConsole.checkNonMoveWin() == P1)
                    {
                        q.QLearningRun(currentState, P2, P1, false);
                        // q.save("test");
                        //reset board
                        InitializePieces();
                        gameStatus = false;
                        break;
                    }
                    //**********************************************************************************//

                    //*************************This is Q************************************************//
                    currentState = new CopyBoard(determineRow, determineCol, gameLogicConsole);
                    Qmove = q.QLearningRun(currentState, P2, P1, false);
                    gameLogicConsole.GameBoardSet(Qmove.FromX, Qmove.FromY, Logic.Piece.BLANK);
                    gameLogicConsole.GameBoardSet(Qmove.ToX, Qmove.ToY, P2);

                    if (gameLogicConsole.checkfirstWin() == P2 || gameLogicConsole.checkNonMoveWin() == P2)
                    {
                        QwinCount++;
                        //call Q to reward the current state
                        q.QLearningRun(currentState, P2, P1, true);
                        //q.save("test");
                        //reset board
                        InitializePieces();
                        gameStatus = false;

                        break;

                    }
                    //**********************************************************************************//
                    PrintBoard();
                    Console.Read();

                }
                //q.save("test");
                count++;
                //Console.Read();
                gameStatus = true;
                if (count == 100)
                {
                    //q.save("4x4");
                    string DataPath = "C:\\Users\\Anoch\\Dropbox\\SDU\\6 Semester\\Bachelor\\Online Dropbox Project\\data\\";
                    string FileName = "QP2vsMiniMaxP1Board5x5Dis01Lear1divnExpl1divnForsight30";
                    string FullPath = DataPath + FileName + ".txt";
                    //System.IO.StreamWriter file = new System.IO.StreamWriter(FullPath, true);

                    Console.Write("Q Win count: " + QwinCount + " out of " + count + "\n");
                    //file.WriteLine(QwinCount);
                    //file.Close();
                    if (filecount++ == 500)
                    {
                        Console.WriteLine("RUNTIME 1000");
                        Console.Read();
                    }
                    PrintBoard();
                    Console.Read();
                   // PrintBoard();
                    QwinCount = 0;
                    count = 0;
                }

            }

            

        }

     /*   public void PlayVsMinimax(Logic.Piece AI, 
        {

             if (gameLogicConsole.CurrentPLAYER == AI)
                {
                    LegalMove move = new LegalMove(1, 1, 1, 1, false);
                    move = minimax.MovePieceChosen(foresight, AI, Simple);
                    gameLogicConsole.GameBoardSet(move.FromX, move.FromY, Logic.Piece.BLANK);
                    gameLogicConsole.GameBoardSet(move.ToX, move.ToY, AI);
                    gameLogicConsole.CurrentPLAYER = Simple;

                    PrintBoard(); 
                    if (gameStatus == false)
                    {
                        break;
                    }
                    //PrintBoard();
                }
                if (gameLogicConsole.CurrentPLAYER == Simple)
                {
                    gameLogicConsole.SimpleBrute(Simple, AI);
                    gameLogicConsole.CurrentPLAYER = AI;
                    PrintBoard(); 
                    if (gameStatus == false)
                    {
                        break;
                    }
                    //PrintBoard();
                } if (gameLogicConsole.CurrentPLAYER == AI)
                {
                    LegalMove move = new LegalMove(1, 1, 1, 1, false);
                    move = MovePieceChosen(foresight, AI, Simple);
                    gameLogicConsole.GameBoardSet(move.FromX, move.FromY, Logic.Piece.BLANK);
                    gameLogicConsole.GameBoardSet(move.ToX, move.ToY, AI);
                    gameLogicConsole.CurrentPLAYER = Simple;

                    PrintBoard(); 
                    if (gameStatus == false)
                    {
                        break;
                    }
                    //PrintBoard();
                }
                if (gameLogicConsole.CurrentPLAYER == Simple)
                {
                    gameLogicConsole.SimpleBrute(Simple, AI);
                    gameLogicConsole.CurrentPLAYER = AI;
                    PrintBoard(); 
                    if (gameStatus == false)
                    {
                        break;
                    }
                    //PrintBoard();
                }

        }



        */





    }
}
