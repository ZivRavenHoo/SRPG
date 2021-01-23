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

        public static GridPosition GetMouseGridPosition(Camera camera, Transform root, float side)
        {
            Vector3 localPosition = GetMouseDownLocalPosition(camera, root);
            return LocalPositionToGridPosition(localPosition, side);
        }

        public static Vector3 GetMouseDownLocalPosition(Camera camera, Transform root)
        {
            Vector3 worldPosition = GetMouseDownWorldPosition(camera);
            return root.InverseTransformPoint(worldPosition);
        }

        public static Vector3 GetMouseDownWorldPosition(Camera camera)
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

        public static Vector3 GridPositionToLocalPosition(GridPosition position, float side)
        {
            float x = (position.column + 0.5f) * side;
            float y = (position.row + 0.5f) * side;
            return new Vector3(x, y, 0);
        }
    }
}
