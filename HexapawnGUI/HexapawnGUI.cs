using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HexapawnLogic;

namespace HexapawnGUI
{
    public partial class HexapawnGUI : Form
    {
        Logic game = new Logic();
        LegalMove MMmove = default(LegalMove);
        LegalMove Qmove = default(LegalMove);
        //Bruteforce AI = new Bruteforce();
        private int determineRow;
        private int determineCol;
        MiniMax minimax;
        Bruteforce brute;
        QLearning q;
        Logic.CopyBoard currentState;
        Logic.Piece AIPLayer = Logic.Piece.BLANK;
        bool SelectedMinimax = false, SelectedBrute = true, SelectedQ = false;

        private int chessField = 50;
        private int chessPieceSize = 30;
        int PickX, PickY, NewX, NewY;
        bool ActicateAI = true;

        public HexapawnGUI()
        {
            //InitializeComponent();
            //this.MouseClick += MouseClickHandler1;
        }
        public HexapawnGUI(int x, int y)
        {
            InitializeComponent();
            //boxCol.Hide();
            //boxRow.Hide();
            //btnGenerate.Hide();
            this.MouseClick += MouseClickHandler1;

            determineRow = x;
            determineCol = y;
            minimax = new MiniMax(x, y);
            brute = new Bruteforce();
            q = new QLearning(x, y);
            q.load("5x5");
            //btnQTrain.Hide();
            resetBoard();


            //Upon activiation Brute is selected:
            SelectedMinimax = false;
            SelectedQ = false;
            SelectedBrute = true;

            resetBoard();
            btnQTrain.Hide();
            game.CurrentPLAYER = Logic.Piece.PLAYER1;
            AIPLayer = Logic.Piece.PLAYER1;
            PlayGame(AIPLayer);

            this.Invalidate();
        }

        private void resetBoard()
        {
            game.GameBoardGenerate(determineRow, determineCol);
            //Player pieces
            for (int i = 0; i < determineRow; i++)
            {
                game.GameBoardSet(i, 0, Logic.Piece.PLAYER1); //player 1.
                game.GameBoardSet(i, determineCol - 1, Logic.Piece.PLAYER2); //player 2.

            }

            //BLANKs
            for (int i = 0; i < determineRow; i++)
            {
                for (int j = 1; j < determineCol - 1; j++)
                {
                    game.GameBoardSet(i, j, Logic.Piece.BLANK);
                }
            }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen black = new Pen(Color.Black, 1);
            Pen white = new Pen(Color.WhiteSmoke, 1);
            Pen green = new Pen(Color.ForestGreen, 1);
            Pen blue = new Pen(Color.DeepSkyBlue, 1);
            Pen pick = new Pen(Color.DarkOrange, 3);

            int xStart = e.ClipRectangle.Left / chessField;
            int yStart = e.ClipRectangle.Top / chessField;
            int xEnd = e.ClipRectangle.Right / chessField + 1;
            int yEnd = e.ClipRectangle.Bottom / chessField + 1;
            xStart = xStart < 0 ? 0 : xStart;
            yStart = yStart < 0 ? 0 : yStart;

            //Create Board.
            for (int i = xStart; i < game.GameBoardGet().GetLength(0) && i < xEnd; i++)
            {
                for (int j = yStart; j < game.GameBoardGet().GetLength(1) && j < yEnd; j++)
                {

                    g.FillRectangle(black.Brush, new Rectangle(i * chessField, j * chessField, chessField, chessField));
                    g.FillRectangle(white.Brush, new Rectangle(i * chessField + 1, j * chessField + 1, chessField - 2, chessField - 2));


                    if (game.GameBoardGet(i, j) == Logic.Piece.PLAYER1)
                    {
                        g.FillRectangle(green.Brush, new Rectangle(i * chessField + (chessField / 5), j * chessField + (chessField / 5), chessPieceSize, chessPieceSize));
                    }

                    if (game.GameBoardGet(i, j) == Logic.Piece.PLAYER2)
                    {
                        g.FillRectangle(blue.Brush, new Rectangle(i * chessField + (chessField / 5), j * chessField + (chessField / 5), chessPieceSize, chessPieceSize));
                    }



                    if ((PickX != NewX) || (PickY != NewY))
                    {
                        NewX = PickX;
                        NewY = PickY;

                        if (game.GameBoardGet(PickX, PickY) != Logic.Piece.BLANK)
                        {
                            g.DrawRectangle(pick, new Rectangle(PickX * chessField + (chessField / 5) - 1, PickY * chessField + (chessField / 5) - 1, chessPieceSize + 2, chessPieceSize + 2));
                        }
                    }
                }
            }

        }

        private void MouseClickHandler1(Object sender, MouseEventArgs args)
        {

            if (determineRow * chessField > args.X && determineCol * chessField > args.Y)
            {
                Invalidate(new Rectangle(PickX * chessField, PickY * chessField, chessField, chessField));
                Update();


                PickX = args.X / chessField;
                PickY = args.Y / chessField;
            
                Invalidate(new Rectangle(PickX * chessField, PickY * chessField, chessField, chessField));
                Update();

            }
        }

        private void btnMoveStraight_Click(object sender, EventArgs e)
        {
            if (AIPLayer == Logic.Piece.PLAYER1)
            {
                if (game.GameBoardGet(PickX, PickY - 1) == Logic.Piece.BLANK)
                {
                    game.NextPLAYER = game.moveStraight(game.CurrentPLAYER, PickX, PickY);
                    actionAfter();
                    game.CurrentPLAYER = AIPLayer;
                    PlayGame(AIPLayer);
                }
                else
                {
                    MessageBox.Show("Illegal Move. Try again");
                }
            }


            if (AIPLayer == Logic.Piece.PLAYER2)
            {
                if (game.GameBoardGet(PickX, PickY + 1) == Logic.Piece.BLANK)
                {
                    game.NextPLAYER = game.moveStraight(game.CurrentPLAYER, PickX, PickY);
                    actionAfter();
                    game.CurrentPLAYER = AIPLayer;
                    PlayGame(AIPLayer);
                }
                else
                {
                    MessageBox.Show("Illegal Move. Try again");
                }

            }



        }

        private void btnAttackLeft_Click(object sender, EventArgs e)
        {
            if (AIPLayer == Logic.Piece.PLAYER1&&(PickX!=0))
            {
                if (game.GameBoardGet(PickX-1, PickY-1) == Logic.Piece.PLAYER1)
                {
                    game.NextPLAYER = game.attackLeft(game.CurrentPLAYER, PickX, PickY);
                    actionAfter();
                    game.CurrentPLAYER = AIPLayer;
                    PlayGame(AIPLayer);
                }
                else
                {
                    MessageBox.Show("Illegal Move. Try again");
                }

            }
            if (AIPLayer == Logic.Piece.PLAYER2 && (PickX != 0))
            {
                if (game.GameBoardGet(PickX-1, PickY + 1) == Logic.Piece.PLAYER2)
                {
                    game.NextPLAYER = game.attackLeft(game.CurrentPLAYER, PickX, PickY);
                    actionAfter();
                    game.CurrentPLAYER = AIPLayer;
                    PlayGame(AIPLayer);
                }
                else
                {
                    MessageBox.Show("Illegal Move. Try again");
                }

            }

        }

        private void btnAttackRight_Click(object sender, EventArgs e)
        {
            if (AIPLayer == Logic.Piece.PLAYER1 && (PickX != determineRow-1))
            {
                if (game.GameBoardGet(PickX + 1, PickY - 1) == Logic.Piece.PLAYER1)
                {
                    game.NextPLAYER = game.attackRight(game.CurrentPLAYER, PickX, PickY);
                    actionAfter();
                    game.CurrentPLAYER = AIPLayer;
                    PlayGame(AIPLayer);
                }
                else
                {
                    MessageBox.Show("Illegal Move. Try again");
                }

            }

            if (AIPLayer == Logic.Piece.PLAYER2 && (PickX != determineRow - 1))
            {
                if (game.GameBoardGet(PickX + 1, PickY + 1) == Logic.Piece.PLAYER2)
                {
                    game.NextPLAYER = game.attackRight(game.CurrentPLAYER, PickX, PickY);
                    actionAfter();
                    game.CurrentPLAYER = AIPLayer;
                    PlayGame(AIPLayer);
                }
                else
                {
                    MessageBox.Show("Illegal Move. Try again");
                }

            }


        }

        private void afterBtnActionClick()
        {
            //returned own player, aka. illegal move

            if (ActicateAI == false)
            {
                if (game.NextPLAYER == game.CurrentPLAYER)
                {
                    labalStatus.Text = "Illegal move, try again";
                }
                //if returned next player, succes
                if (game.NextPLAYER != game.CurrentPLAYER)
                {
                    game.CurrentPLAYER = game.NextPLAYER;
                    labalStatus.Text = "All OK";
                }

                if (game.NextPLAYER == Logic.Piece.PLAYER1)
                {
                    labelPlayer.ForeColor = Color.ForestGreen;
                    labelPlayer.Text = "Player 1";
                }
                else
                {
                    labelPlayer.ForeColor = Color.DeepSkyBlue;
                    labelPlayer.Text = "Player 2";

                }
            }


            //if (ActicateAI == true)
            //{
            //    if (game.NextPLAYER == game.CurrentPLAYER)
            //    {
            //        labalStatus.Text = "Illegal move, try again";
            //    }
            //    if (game.NextPLAYER != game.CurrentPLAYER)
            //    {
            //        game.CurrentPLAYER = game.NextPLAYER;
            //        if (game.CurrentPLAYER == Logic.Piece.PLAYER1)
            //        {
            //            if (SelectedMinimax)
            //            {
            //                MMmove = new LegalMove(1, 1, 1, 1, false);
            //                MMmove = minimax.MovePieceChosen(30, Logic.Piece.PLAYER1, Logic.Piece.PLAYER2, game);
            //                game.GameBoardSet(MMmove.FromX, MMmove.FromY, Logic.Piece.BLANK);
            //                game.GameBoardSet(MMmove.ToX, MMmove.ToY, Logic.Piece.PLAYER1);
            //                game.CurrentPLAYER = Logic.Piece.PLAYER2;
            //                Refresh();
            //            }
            //            if (SelectedBrute)
            //            {
            //                brute.AIBruteforce(Logic.Piece.PLAYER1, Logic.Piece.PLAYER2, game);
            //                game.CurrentPLAYER = Logic.Piece.PLAYER2;
            //                labelPlayer.ForeColor = Color.DeepSkyBlue;
            //                labelPlayer.Text = "Player 2";
            //                Refresh();
            //            }

            //            if (SelectedQ)
            //            {
            //                currentState = new Logic.CopyBoard(determineRow, determineCol, game);
            //                Qmove = q.QLearningRun(currentState, Logic.Piece.PLAYER1, Logic.Piece.PLAYER2, false);
            //                game.GameBoardSet(Qmove.FromX, Qmove.FromY, Logic.Piece.BLANK);
            //                game.GameBoardSet(Qmove.ToX, Qmove.ToY, Logic.Piece.PLAYER1);
            //                game.CurrentPLAYER = Logic.Piece.PLAYER2;
            //                labelPlayer.ForeColor = Color.DeepSkyBlue;
            //                labelPlayer.Text = "Player 2";
            //                Refresh();

            //            }
            //        }
            //        labalStatus.Text = "All OK";
            //    }
                
            //}
            labelPlayer.ForeColor = Color.DeepSkyBlue;
            labelPlayer.Text = "Player 2";




            if (game.checkNonMoveWin() == Logic.Piece.PLAYER1 && game.checkfirstWin() == Logic.Piece.PLAYER1)
            {
                currentState = new Logic.CopyBoard(determineRow, determineCol, game);
                labelPlayer.ForeColor = Color.ForestGreen;
                labelPlayer.Text = "Player 1 Win";
                Refresh();
                if (SelectedQ)
                    q.QLearningRun(currentState, Logic.Piece.PLAYER1, Logic.Piece.PLAYER2, true);
                MessageBox.Show("PLAYER 1 WIN");
                Close();
            }

            if (game.checkNonMoveWin() == Logic.Piece.PLAYER2 && game.checkfirstWin() == Logic.Piece.PLAYER2)
            {
                labelPlayer.ForeColor = Color.DeepSkyBlue;
                labelPlayer.Text = "Player 2 Win";
                Refresh();
                MessageBox.Show("PLAYER 2 WIN");
                Close();
            }
            Refresh();
        }

        public void actionAfter()
        {

            this.Invalidate();
            if (game.checkNonMoveWin() == Logic.Piece.PLAYER1)
            {
                MessageBox.Show("PLAYER 1 WIN");
            }
            if (game.checkNonMoveWin() == Logic.Piece.PLAYER2)
            {
                MessageBox.Show("PLAYER 2 WIN");
            }
            if (game.checkfirstWin() == Logic.Piece.PLAYER1)
            {
                MessageBox.Show("PLAYER 1 WIN");
            }
            if (game.checkfirstWin() == Logic.Piece.PLAYER2)
            {
                MessageBox.Show("PLAYER 2 WIN");
            }
        }


        private void rbBruteforce_CheckedChanged(object sender, EventArgs e)
        {
            SelectedMinimax = false;
            SelectedQ = false;
            SelectedBrute = true;

            resetBoard();
            btnQTrain.Hide();
            game.CurrentPLAYER = Logic.Piece.PLAYER1;
            AIPLayer = Logic.Piece.PLAYER2;
            PlayGame(AIPLayer);
        }

        private void rbMiniMax_CheckedChanged(object sender, EventArgs e)
        {
            SelectedMinimax = true;
            SelectedQ = false;
            SelectedBrute = false;

            resetBoard();
            btnQTrain.Hide();
            game.CurrentPLAYER = Logic.Piece.PLAYER1;
            AIPLayer = Logic.Piece.PLAYER2;
            PlayGame(AIPLayer);         
        }

        private void rbQLearning_CheckedChanged(object sender, EventArgs e)
        {
            SelectedQ = true;
            SelectedBrute = false;
            SelectedMinimax = false;
            btnQTrain.Visible = true;

            resetBoard();
            
            game.CurrentPLAYER = Logic.Piece.PLAYER1;
            AIPLayer = Logic.Piece.PLAYER2;
            //Qtraning(AIPLayer);
            PlayGame(AIPLayer);    
        }

        private void PlayGame(Logic.Piece AI)
        {

                if (AI == Logic.Piece.PLAYER1 && game.CurrentPLAYER == Logic.Piece.PLAYER1)
                {
                    if (SelectedBrute == true)
                    {
                        PlayVsBrute(Logic.Piece.PLAYER1, Logic.Piece.PLAYER2);
                        this.Invalidate();
                        actionAfter();
                    }
                    if (SelectedMinimax == true)
                    {
                        PlayVsMinimax(Logic.Piece.PLAYER1, Logic.Piece.PLAYER2,30);
                        this.Invalidate();
                        actionAfter();                      
                    }
                    if (SelectedQ == true)
                    {
                        PlayVsQ(currentState, Logic.Piece.PLAYER1, Logic.Piece.PLAYER2, false);
                        actionAfter();
                        this.Invalidate();
                    }
                   
                }

                if (AI == Logic.Piece.PLAYER2 && game.CurrentPLAYER == Logic.Piece.PLAYER2)
                {
                    if (SelectedBrute == true)
                    {
                        
                        PlayVsBrute(Logic.Piece.PLAYER2, Logic.Piece.PLAYER1);
                        this.Invalidate();
                        actionAfter();

                    }
                    if (SelectedMinimax == true)
                    {
                        
                        PlayVsMinimax(Logic.Piece.PLAYER2, Logic.Piece.PLAYER1, 30);
                        actionAfter();
                        this.Invalidate();
                    }
                    if (SelectedQ == true)
                    {
                        PlayVsQ(currentState, Logic.Piece.PLAYER2, Logic.Piece.PLAYER1, false);
                        actionAfter();
                        this.Invalidate();

                    }
                }
        }

        private void btnQTrain_Click(object sender, EventArgs e)
        {
            Qtraning(AIPLayer);
        }

        private void PlayVsBrute(Logic.Piece AI, Logic.Piece Opponent)
        {
            if (game.CurrentPLAYER == AI)
            {
                brute.AIBruteforce(AI, Opponent, game);
                game.CurrentPLAYER = Opponent;
            }
        }

        private void PlayVsMinimax(Logic.Piece AI, Logic.Piece Opponent, int foresight)
        {
            if (game.CurrentPLAYER == AI)
            {
                LegalMove move = new LegalMove(1, 1, 1, 1, false);
                move = minimax.MovePieceChosen(foresight, AI, Opponent, game);
                game.GameBoardSet(move.FromX, move.FromY, Logic.Piece.BLANK);
                game.GameBoardSet(move.ToX, move.ToY, AI);
                game.CurrentPLAYER = Opponent;
            }
        }

        private void PlayVsQ(Logic.CopyBoard currentState, Logic.Piece AI, Logic.Piece Opponent, bool win)
        {
            if (game.CurrentPLAYER == AI)
            {
                currentState = new Logic.CopyBoard(determineRow, determineCol, game);
                Qmove = q.QLearningRun(currentState, AI, Opponent, false);
                game.GameBoardSet(Qmove.FromX, Qmove.FromY, Logic.Piece.BLANK);
                game.GameBoardSet(Qmove.ToX, Qmove.ToY, AI);
                game.CurrentPLAYER = Opponent;
            }
        }

        private void radioButton3B_CheckedChanged(object sender, EventArgs e)
        {
            SelectedMinimax = false;
            SelectedQ = false;
            SelectedBrute = true;

            resetBoard();
            btnQTrain.Hide();
            game.CurrentPLAYER = Logic.Piece.PLAYER1;
            AIPLayer = Logic.Piece.PLAYER1;
            PlayGame(AIPLayer);
        }

        private void radioButton2M_CheckedChanged(object sender, EventArgs e)
        {
            SelectedMinimax = true;
            SelectedQ = false;
            SelectedBrute = false;

            resetBoard();
            btnQTrain.Hide();
            game.CurrentPLAYER = Logic.Piece.PLAYER1;
            AIPLayer = Logic.Piece.PLAYER1;
            PlayGame(AIPLayer);
        }

        private void radioButton1Q_CheckedChanged(object sender, EventArgs e)
        {
            SelectedMinimax = false;
            SelectedQ = true;
            SelectedBrute = false;

            resetBoard();
            btnQTrain.Visible = true;
            game.CurrentPLAYER = Logic.Piece.PLAYER1;
            AIPLayer = Logic.Piece.PLAYER1;
            //Qtraning(AIPLayer);
            PlayGame(AIPLayer);
        }

        private void Qtraning(Logic.Piece AI)
        {
            Logic.Piece Opponent = Logic.Piece.BLANK;

            if(AI == Logic.Piece.PLAYER1)
            {
                Opponent = Logic.Piece.PLAYER2;
            }
            if (AI == Logic.Piece.PLAYER2)
            {
                Opponent = Logic.Piece.PLAYER1;
            }

            labelTrainQ.Text = "Training in process!";
            currentState = new Logic.CopyBoard(determineRow, determineCol, game);
            Qmove = default(LegalMove);
            MMmove = default(LegalMove);

            int QwinCount = 0;
            int count = 0;
            currentState = new Logic.CopyBoard(determineRow, determineCol, game);
            bool gameStatus = true, outer = true;

            while (outer)
            {

                while (gameStatus)
                {

                    //****************************************This is Q********************************************//
                    currentState = new Logic.CopyBoard(determineRow, determineCol, game);
                    Qmove = q.QLearningRun(currentState, AI, Opponent, false);
                    game.GameBoardSet(Qmove.FromX, Qmove.FromY, Logic.Piece.BLANK);
                    game.GameBoardSet(Qmove.ToX, Qmove.ToY, AI);

                    if (game.checkfirstWin() == AI || game.checkNonMoveWin() == AI)
                    {
                        QwinCount++;
                        //call Q to reward the current state
                        q.QLearningRun(currentState, AI, Opponent, true);
                        //reset board
                        resetBoard();
                        gameStatus = false;
                        break;

                    }
                    //*********************************************************************************************//

                    //****************************************Q Opponent*******************************************//
                    //*********************************MINIMAX******************************************//
                    currentState = new Logic.CopyBoard(determineRow, determineCol, game);
                    MMmove = new LegalMove(1, 1, 1, 1, false);
                    MMmove = minimax.MovePieceChosen(30, Opponent, AI, game);
                    game.GameBoardSet(MMmove.FromX, MMmove.FromY, Logic.Piece.BLANK);
                    game.GameBoardSet(MMmove.ToX, MMmove.ToY, Opponent);


                    if (game.checkfirstWin() == Opponent || game.checkNonMoveWin() == Opponent)
                    {

                        q.QLearningRun(currentState, AI, Opponent, false);

                        //reset board
                        resetBoard();
                        gameStatus = false;
                        break;
                    }
                    //**********************************************************************************//



                }

                Qprogress.Value = count++;

                gameStatus = true;
                if (count == 10000)
                {
                    labelTrainQ.Text = "Q Training done";
                    resetBoard();
                    this.Invalidate();
                    QwinCount = 0;
                    count = 0;
                    outer = false;
                    break;

                }
            }
            game.CurrentPLAYER = AI;
            PlayGame(AI);
        }


    }
}
