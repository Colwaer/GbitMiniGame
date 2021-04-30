using UnityEngine;

public class CameraArea : MonoBehaviour
{
    private void OnDrawGizmos() 
    {   
        Gizmos.color = Color.cyan;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
