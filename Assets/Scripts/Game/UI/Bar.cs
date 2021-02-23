using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private Transform fill;

    private float percent;
    public float Percent
    {
        get => percent;
        set
        {
            if (value > 1)
                value = 1;
            else if (value < 0)
                value = 0;

            if (value == percent)
                return;
            percent = value;
            Refresh();
        }
    }

    private void Awake()
    {
        Percent = 1f;
    }

    private void Refresh()
    {
        Vector3 scale = fill.localScale;
        scale.x = percent;
        fill.localScale = scale;
    }
}
