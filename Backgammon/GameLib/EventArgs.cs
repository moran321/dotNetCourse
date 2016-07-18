﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//this code contains all the classes of the events args

namespace GameLib
{
    /****************************************/
    public class DisplayInstuctionsEventArgs : EventArgs
    {
       
    }
    /****************************************/
    
    /****************************************/
    public class DisplayMessageEventArgs : EventArgs
    {
        public string Message { get; }

        public DisplayMessageEventArgs(string message)
        {
            Message = message;
        }
    }
    /****************************************/

    /****************************************/
    public class TurnEventArgs : EventArgs
    {
        public Move[] Moves { get; }
        public int[] Dice { get; }

        public TurnEventArgs(Move[] moves, int[] dice)
        {
            Moves = moves;
            Dice = dice;

        }

    }
    /****************************************/



    /****************************************/
    public class RollDiceEventArgs : EventArgs
    {

        public RollDiceEventArgs()
        {

        }

    }
    /****************************************/


    /****************************************/
    public class GameOverEventArgs : EventArgs
    {
       // public enum Victory {None, Regular, Mars, TurkishMars }
        public GameManager.Victory VictoryType { get; }
        public Player Winner { get; }
        public GameOverEventArgs(GameManager.Victory victory, Player winner)
        {
            VictoryType = victory;
            Winner = winner;
        }
    }
    /****************************************/
}