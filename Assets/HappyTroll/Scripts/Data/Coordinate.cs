using UnityEngine;

namespace HappyTroll
{
    public class Coordinate
    {
        public int _col;
        public int _row;

        private static float tileWidth;
        private static float tileHeight;
        private static float tileDepth;

        public Coordinate(int row, int col)
        {
            _col = col;
            _row = row;
        }

        public static void SetTileSizes(float width, float height, float depth = 0)
        {
            tileWidth = width;
            tileHeight = height;
            tileDepth = depth;
        }

        public static Vector3 CoordinateToWorld(Coordinate coordinate)
        {
            return new Vector3(coordinate._col * tileWidth, coordinate._row * tileHeight, tileDepth);
        }

        public static Coordinate WorldToCoordinate(Vector3 worldPoint)
        {
            return new Coordinate(Mathf.CeilToInt(worldPoint.x / tileWidth),
                Mathf.CeilToInt(worldPoint.y / tileHeight));
        }
    }
}
