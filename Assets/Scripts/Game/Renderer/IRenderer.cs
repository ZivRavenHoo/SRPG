using UnityEngine;

public interface IRenderer<R,D>
    where R:MonoBehaviour
{
    void Bind();
    void Refresh();
}
