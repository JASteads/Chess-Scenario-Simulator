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

        string[] chessPiece = {"Pawn", "Rook", "Knight", "Bishop", "Queen", "King"};
        string[] color = {"White", "Black"};
        string[] chessBoard = {   "A1" , "B1", "C1", "D1", "E1", "F1", "G1", "H1",
                                  "A2" , "B2", "C2", "D2", "E2", "F2", "G2", "H2",
                                  "A3" , "B3", "C3", "D3", "E3", "F3", "G3", "H3",
                                  "A4" , "B4", "C4", "D4", "E4", "F4", "G4", "H4",
                                  "A5" , "B5", "C5", "D5", "E5", "F5", "G5", "H5",
                                  "A6" , "B6", "C6", "D6", "E6", "F6", "G6", "H6",
                                  "A7" , "B7", "C7", "D7", "E7", "F7", "G7", "H7",
                                  "A8" , "B8", "C8", "D8", "E8", "F8", "G8", "H8",
                                 };
        

        public short getPosition()
        {
            return position;
        }
            
        public void setPosition(short newPosition)
        {
            short mask = 63;
            ushort position = (ushort) (newPosition & mask);
        }

        public short getPieceType()
        {
            return pieceType;
        }

        public void setPieceType(short newPieceType)
        {
            short mask = 511;
            ushort pieceType = (ushort) (newPieceType & mask);
            pieceType = (ushort) (pieceType >> 6);
        }

        public short getTeamColor()
        {
            return teamColor;
        }

        public void setTeamColor(short newColor)
        {
            short mask = 1023;
            ushort teamColor = (ushort) (newColor & mask);
            teamColor = (ushort) (teamColor >> 9);
        }

        public void printInformation()
        {
            Console.WriteLine("Position: " + chessBoard[position]);
            Console.WriteLine("Piece Type: " + chessPiece[pieceType]);
            Console.WriteLine("Team Color: " + color[teamColor]);
        }
        
    }
}