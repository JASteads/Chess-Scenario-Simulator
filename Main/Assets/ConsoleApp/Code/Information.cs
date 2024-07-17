using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Code
{
    public class Information
    {
        private short position = 0;
        private short pieceType = 0;
        private short teamColor = 0;

        public Information()
        {

        }
        public Information(short teamColor, short position)
        {
            this.teamColor = teamColor;
            this.position = position;

        }




        public short getPosition()
        {
            return position;
        }
            
        public void setPosition(short newPosition)
        {
            short mask = 63;
            position = newPosition & mask;
        }

        public short getPieceType()
        {
            return pieceType;
        }

        public void setPieceType(short newPieceType)
        {
            short mask = 511;
            pieceType = newPieceType & mask;
            pieceType = pieceType >> 6;
        }

        public short getTeamColor()
        {
            return teamColor;
        }

        public void setTeamColor(short newColor)
        {
            short mask = 1023;
            teamColor = newColor & mask;
            teamColor = teamColor >> 9;
        }

        public void printInformation()
        {
            ConsoleApp.WriteLine("Position: ")
            ConsoleApp.WriteLine("Piece Type: ")
            ConsoleApp.WriteLine("Team Color: ")
        }
    }
}