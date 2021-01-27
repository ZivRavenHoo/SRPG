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

        public float GetResolution()
        {
            return (float)width / height;
        }

        public bool IsGridPositionInSize(GridPosition position)
        {
            int row = position.row, column = position.column;
            if (row < 0 || row > height || column < 0 || column > width)
                return false;
            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}_{1}", width, height);
        }

        public GridPosition GetRangePosition()
        {
            GridPosition position = new GridPosition();
            position.row = Random.Range(0, height - 1);
            position.column = Random.Range(0, width - 1);
            return position;
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
            GridPosition position;
            position.column = (int)(localPosition.x / side);
            position.row = (int)(localPosition.y / side);
            return position;
        }

        public static Vector2 GridPositionToLocalPosition(GridPosition position, float side)
        {
            float x = (position.column + 0.5f) * side;
            float y = (position.row + 0.5f) * side;
            return new Vector2(x, y);
        }
    }
}
