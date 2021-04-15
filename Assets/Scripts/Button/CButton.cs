using UnityEngine;
using Public;

public class CButton : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Respond();
    }
    protected virtual void Respond() { }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
