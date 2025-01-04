using System;

namespace Game.UI.Inspector
{
    [Serializable]
    public class Position
    {
        public int Column;
        public int Row;
        public int Combination;

        public Position(int combination, int column, int row)
        {
            Combination = combination;
            Column = column;
            Row = row;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Position other = (Position)obj;
            
            return Combination == other.Combination && Column == other.Column && Row == other.Row;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Combination.GetHashCode();
                hash = hash * 23 + Column.GetHashCode();
                hash = hash * 23 + Row.GetHashCode();
                return hash;
            }
        }
    }
}