using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Cloud_Color : MonoBehaviour
{
    private Tilemap tilemap;
    public WindArea windArea;
    public Color targetColor;
    public float ColorChangeTime = 1.5f;
    private bool isEnabled = true;
    private void Awake() 
    {
        tilemap = GetComponent<Tilemap>();    
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (!isEnabled)
            return;
        if (other.collider.CompareTag("Player"))
        {
            if (other.collider.attachedRigidbody.velocity.magnitude > 13f)
            {
                isEnabled = false;
                windArea.Count++;
                Debug.Log("start change color");
                StartCoroutine(ChangeColor(ColorChangeTime));
            }
        }    
    }
    IEnumerator ChangeColor(float time)
    {
        float timer = 0;
        Color originColor = tilemap.color;
        while (timer <= time)
        {
            timer += Time.deltaTime;
            tilemap.color = Color.Lerp(originColor, targetColor, timer / time);
            yield return null;
        }
        Destroy(this);
    }

}
