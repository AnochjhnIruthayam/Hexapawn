using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HexapawnLogic
{
    public class QLearning : Logic
    {
        public QLearning(int x, int y)
        {
            determineRow = x;
            determineCol = y;
        }

        //percept
        CopyBoard previousState = default(CopyBoard);
        LegalMove previousAction = default(LegalMove);
        LegalMove optimalMove = default(LegalMove);
        int rewardSignal = 1; // Reward Signal
        // double alpha = 0.7; //Alpha value aka. learning rate
        double lamda = 0.1; // lamda value aka discount factor
        double currentReward = 0;




        //persistent
        //Q-table
        public Dictionary<UInt32, Dictionary<UInt32, double>> Q = new Dictionary<UInt32, Dictionary<UInt32, double>>();

        //N_sa table of freq.  for state-action pairs
        public Dictionary<UInt32, Dictionary<UInt32, int>> N = new Dictionary<UInt32, Dictionary<UInt32, int>>();


        public LegalMove QLearningRun(CopyBoard currentState, Logic.Piece QAgentPlayer, Logic.Piece QEnemy, bool win)
        {


            //check if currentState key is in the table or not
            if (!Q.ContainsKey(currentState.HashCode(determineRow, determineCol)))
            {
                Q.Add(currentState.HashCode(determineRow, determineCol), new Dictionary<UInt32, double>());
            }
            if (!N.ContainsKey(currentState.HashCode(determineRow, determineCol)))
            {
                N.Add(currentState.HashCode(determineRow, determineCol), new Dictionary<UInt32, int>());
            }

            // this will skip the first turn over, since there is no previous state in the beginning (initial state)
            if (previousState.Equals(default(CopyBoard)))
            {
                previousState = currentState;
                previousAction = explorationFunction(currentState, QAgentPlayer);
                currentReward = 0;
                return previousAction;
            }


            //check if previousState key is in the table or not
            if (!Q.ContainsKey(previousState.HashCode(determineRow, determineCol)))
            {
                Q.Add(previousState.HashCode(determineRow, determineCol), new Dictionary<UInt32, double>());
            }
            //check if previousAction key is in the table or not
            if (!Q[previousState.HashCode(determineRow, determineCol)].ContainsKey(previousAction.HashCode(determineRow, determineCol)))
            {
                Q[previousState.HashCode(determineRow, determineCol)].Add(previousAction.HashCode(determineRow, determineCol), 0);
            }



            //we do the same for the N table, check if the state exists in the table, if not then add
            if (!N.ContainsKey(previousState.HashCode(determineRow, determineCol)))
            {
                N.Add(previousState.HashCode(determineRow, determineCol), new Dictionary<UInt32, int>());
            }
            if (!N[previousState.HashCode(determineRow, determineCol)].ContainsKey(previousAction.HashCode(determineRow, determineCol)))
            {
                N[previousState.HashCode(determineRow, determineCol)].Add(previousAction.HashCode(determineRow, determineCol), 0);
            }
            //check for terminal state

            if (win)
            {
                Q[previousState.HashCode(determineRow, determineCol)][previousAction.HashCode(determineRow, determineCol)] = rewardSignal;
                //previousState = default(CopyBoard);
                //previousAction = default(LegalMove);
            }




            //increment the  freq. equation
            N[previousState.HashCode(determineRow, determineCol)][previousAction.HashCode(determineRow, determineCol)]++;

            //update Q table with Q-function
            //Q[previousState.HashCode(determineRow, determineCol)][previousAction.HashCode(determineRow, determineCol)] = Q[previousState.HashCode(determineRow, determineCol)][previousAction.HashCode(determineRow, determineCol)] + (alpha* (N[previousState.HashCode(determineRow, determineCol)][previousAction.HashCode(determineRow, determineCol)])) * (currentReward + lamda * maxQAction(currentState) - Q[previousState.HashCode(determineRow, determineCol)][previousAction.HashCode(determineRow, determineCol)]);
            Q[previousState.HashCode(determineRow, determineCol)][previousAction.HashCode(determineRow, determineCol)] = Qfunction(currentState, QAgentPlayer);
            previousState = currentState;
            previousAction = explorationFunction(currentState, QAgentPlayer);
            currentReward = 0;


            return previousAction;
        }

        private double Qfunction(CopyBoard currentState, Logic.Piece QAgentPlayer)
        {
            return Q[previousState.HashCode(determineRow, determineCol)][previousAction.HashCode(determineRow, determineCol)] + (alphaLearning(N[previousState.HashCode(determineRow, determineCol)][previousAction.HashCode(determineRow, determineCol)])) * (currentReward + lamda * maxQAction(currentState, QAgentPlayer) - Q[previousState.HashCode(determineRow, determineCol)][previousAction.HashCode(determineRow, determineCol)]);
        }

        private double alphaLearning(int frequency)
        {
            return 1 / (((double)frequency));
        }


        private double maxQAction(CopyBoard state, Logic.Piece QAgentPlayer)
        {
            double max = 0;
            double value = 0;
            LegalMove[] selctedAction = CheckLegalMoves(QAgentPlayer, state);


            foreach (LegalMove moveAction in selctedAction)
            {
                value = 0;
                //if there is no action in the specified area, no value
                if (!Q[state.HashCode(determineRow, determineCol)].ContainsKey(moveAction.HashCode(determineRow, determineCol)))
                {
                    value = 0;
                }
                // if there is an action, then set the value to value
                else
                {
                    value = Q[state.HashCode(determineRow, determineCol)][moveAction.HashCode(determineRow, determineCol)];
                }


                //find the maximum value of moves in this state
                if (max <= value)
                    max = value;
            }



            return max;
        }

        private LegalMove explorationFunction(CopyBoard state_u, Logic.Piece QAgentPlayer)
        {
            //initialize
            double max = double.NegativeInfinity;
            double value = 0;
            previousAction = default(LegalMove);
            //load possible moves
            LegalMove[] selctedAction = CheckLegalMoves(QAgentPlayer, state_u);

            //examine each possible move in this stawte
            foreach (LegalMove moveAction in selctedAction)
            {
                value = 0;
                if (Q[state_u.HashCode(determineRow, determineCol)].ContainsKey(moveAction.HashCode(determineRow, determineCol)))
                {
                    //more frequent you make the action, there will be more less probabilty be picked as a potentiel action (1/N[state][action])
                    value = Q[state_u.HashCode(determineRow, determineCol)][moveAction.HashCode(determineRow, determineCol)] + 1 / ((double)N[state_u.HashCode(determineRow, determineCol)][moveAction.HashCode(determineRow, determineCol)]);
                }
                else
                {
                    //if there is nothing, then assign value as zero. it will pick the last possible move
                    value = 1;
                }
                //makes sure we pick the action with the largest value
                if (max < value)
                {

                    max = value;
                    optimalMove = moveAction;
                }
            }
            return optimalMove;
        }

        public void save(string filename)
        {
            BinaryWriter writer = new BinaryWriter(new FileStream(filename + ".datQ", FileMode.Create));
            foreach (KeyValuePair<UInt32, Dictionary<UInt32, double>> item in Q)
            {
                foreach (KeyValuePair<UInt32, double> item2 in item.Value)
	            {
		            writer.Write(item.Key);
                    writer.Write(item2.Key);
                    writer.Write(item2.Value);
	            }
            }
            writer.Close();


            writer = new BinaryWriter(new FileStream(filename + ".datN", FileMode.Create));
            foreach (KeyValuePair<UInt32, Dictionary<UInt32, int>> item in N)
            {
                foreach (KeyValuePair<UInt32, int> item2 in item.Value)
                {
                    writer.Write(item.Key);
                    writer.Write(item2.Key);
                    writer.Write(item2.Value);
                }
            }
            writer.Close();
        }

        public void load(string filename)
        {
            BinaryReader reader = new BinaryReader(new FileStream(filename + ".datQ", FileMode.Open, FileAccess.Read));
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                UInt32 key = reader.ReadUInt32();
                if (!Q.ContainsKey(key))
                {
                    Q.Add(key, new Dictionary<uint, double>());
                }
                UInt32 key2 = reader.ReadUInt32();
                Q[key].Add(key2, reader.ReadDouble());
            }
            reader.Close();

            reader = new BinaryReader(new FileStream(filename + ".datN", FileMode.Open, FileAccess.Read));
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                UInt32 key = reader.ReadUInt32();
                if (!N.ContainsKey(key))
                {
                    N.Add(key, new Dictionary<uint, int>());
                }
                UInt32 key2 = reader.ReadUInt32();
                N[key].Add(key2, reader.ReadInt32());
            }
            reader.Close();
        }


    }
}
