using UnityEngine;
using UnityEngine.UI;

public class ImageBox : MonoBehaviour
{
    private Image image;
    public Image Image => image;

    private void Start()
    {
        image = transform.Find("Image").GetComponent<Image>();
    }
}
