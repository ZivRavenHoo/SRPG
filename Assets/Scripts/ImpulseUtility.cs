using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ImpulseUtility
{
    public struct Size
    {
        public int width;
        public int height;

        public Size(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
    public struct GridPosition
    {
        public int row;
        public int column;

        public GridPosition(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public override string ToString()
        {
            return string.Format("{0}_{1}", row, column);
        }
    }

    public static class RendererUtility
    {
        public static Vector3 GridPositionToLocalPositionl(GridPosition position)
        {
            int row = position.row;
            int column = position.column;
            return new Vector3(column, row, 0);
        }

        public static GridPosition GetMouseGridPosition(Camera camera, Transform root)
        {
            Vector3 worldPosition = GetMouseDownWorldPosition(camera);
            Vector3 localPosition = root.InverseTransformPoint(worldPosition);
            return LocalPositionToGridPosition(localPosition);
        }

        private static Vector3 GetMouseDownWorldPosition(Camera camera)
        {
            return camera.ScreenToWorldPoint(Input.mousePosition);
        }

        private static GridPosition LocalPositionToGridPosition(Vector3 localPosition)
        {
            GridPosition position;
            position.column = (int)localPosition.x;
            position.row = (int)localPosition.y;
            return position;
        }
    }
}
