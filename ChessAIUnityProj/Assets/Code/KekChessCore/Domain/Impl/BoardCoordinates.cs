using System;
using System.Text;

namespace ChessAI.Domain.Impl
{
    public readonly struct BoardCoordinates
    {

        public static BoardCoordinates FromString(string boardCoordinate)
        {
            if (boardCoordinate.Length != 2)
                throw new ArgumentException("Invalid board coordinates format. Length should be equals 2");
            if (boardCoordinate[0] < 'a' || boardCoordinate[0] > 'h')
                throw new ArgumentException("Invalid board coordinates format. First character should be in range \'a\'..\'h\'");
            if (boardCoordinate[1] < '1' || boardCoordinate[1] > '8')
                throw new ArgumentException("Invalid board coordinates format. First character should be in range \'1\'..\'8\'");
            return new BoardCoordinates((boardCoordinate[0] - 'a', boardCoordinate[1] - '1'));
        }
        
        public static BoardCoordinates FromNumeric((int, int) boardCoordinate)
        {
            return new BoardCoordinates(boardCoordinate);
        }
        
        public static BoardCoordinates FromNumeric(int coord1, int coord2)
        {
            return FromNumeric((coord1, coord2));
        }

        private readonly (int, int) _numericRepresentation;

        private BoardCoordinates((int, int) numericRepresentation)
        {
            _numericRepresentation = numericRepresentation;
        }
        
        public (int, int) ToNumeric()
        {
            return _numericRepresentation;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append((char)(_numericRepresentation.Item1 + 'a'));
            sb.Append((char)(_numericRepresentation.Item2 + '1'));
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is BoardCoordinates coord && coord._numericRepresentation == _numericRepresentation;
        }

        public bool Equals(BoardCoordinates other)
        {
            return _numericRepresentation.Equals(other._numericRepresentation);
        }

        public override int GetHashCode()
        {
            return _numericRepresentation.GetHashCode();
        }

        public static bool operator ==(BoardCoordinates boardCoordinates1, BoardCoordinates boardCoordinates2)
        {
            return boardCoordinates1._numericRepresentation == boardCoordinates2._numericRepresentation;
        }

        public static bool operator !=(BoardCoordinates boardCoordinates1, BoardCoordinates boardCoordinates2)
        {
            return !(boardCoordinates1 == boardCoordinates2);
        }

        public static implicit operator BoardCoordinates(string s) => FromString(s);
        
        public static implicit operator BoardCoordinates((int, int) numeric) => FromNumeric(numeric);
    }
}