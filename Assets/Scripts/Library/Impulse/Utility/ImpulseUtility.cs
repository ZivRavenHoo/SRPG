using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Impulse
{
    public class Size
    {
        public int width = 0;
        public int height = 0;

        public Size() { }

        public Size(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public float GetResolution()
        {
            return (float)width / height;
        }

        public bool IsGridPositionInSize(GridPosition position)
        {
            int row = position.Row, column = position.Column;
            if (row < 0 || row >= height || column < 0 || column >= width)
                return false;
            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}_{1}", width, height);
        }

        public GridPosition GetRandomPosition()
        {
            GridPosition position = new GridPosition();
            position.Row = Random.Range(0, height - 1);
            position.Column = Random.Range(0, width - 1);
            return position;
        }
    }
    public class GridPosition
    {
        public Vector2Int position = new Vector2Int();
        public int Row
        {
            get => position.x;
            set => position.x = value;
        }
        public int Column
        {
            get => position.y;
            set => position.y = value;
        }

        public GridPosition() { }

        public GridPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public GridPosition(Vector2Int position)
        {
            this.position = position;
        }

        public GridPosition(GridPosition grid)
        {
            this.position = grid.position;
        }

        public override string ToString()
        {
            return string.Format("{0}_{1}", Row, Column);
        }

        public int Distance(GridPosition target)
        {
            return Mathf.Abs(target.Row - Row) + Mathf.Abs(target.Column - Column);
        }
    }

    public static class RendererUtility
    {
        public static float GetAdaptScreenNeedScale(Rect screen,Size map)
        {
            float width = screen.width / map.width;
            float height = screen.height / map.height;
            return Mathf.Min(width, height);
        }

        public static Vector3 LocalPositionRangeGrid(Camera camera,Transform root,float side)
        {
            GridPosition grid = GetMouseGridPosition(camera, root, side);
            return GridPositionToLocalPosition(grid, side);
        }

        public static GridPosition GetMouseGridPosition(Camera camera, Transform root, float side)
        {
            Vector3 localPosition = GetMouseDownLocalPosition(camera, root);
            return LocalPositionToGridPosition(localPosition, side);
        }

        public static Vector2 GetMouseDownLocalPosition(Camera camera, Transform root)
        {
            Vector3 worldPosition = GetMouseDownWorldPosition(camera);
            return root.InverseTransformPoint(worldPosition);
        }

        public static Vector2 GetMouseDownWorldPosition(Camera camera)
        {
            return camera.ScreenToWorldPoint(Input.mousePosition);
        }

        public static GridPosition LocalPositionToGridPosition(Vector3 localPosition, float side)
        {
            GridPosition position = new GridPosition
            {
                Column = (int)(localPosition.x / side),
                Row = (int)(localPosition.y / side)
            };
            return position;
        }

        public static Vector2 GridPositionToLocalPosition(GridPosition position, float side)
        {
            float x = (position.Column + 0.5f) * side;
            float y = (position.Row + 0.5f) * side;
            return new Vector2(x, y);
        }
    }
}
