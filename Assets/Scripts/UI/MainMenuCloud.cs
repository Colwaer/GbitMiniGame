using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCloud : MonoBehaviour
{
    public Transform endDestination;
    public Transform beginDestination;
    public float deep = 1.0f;
    public bool isRandom = true;
    private SpriteRenderer sp;
    
    private void Awake()
    {
        if (isRandom)
        {
            deep = Random.Range(1f, 5f);
            transform.position = new Vector3(transform.position.x, Random.Range(-5f, 5f), 0);
        }
            
        sp = GetComponent<SpriteRenderer>();
        transform.localScale = transform.localScale * (1 - deep / 10);
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1 - deep / 10);
    }
    private void Update()
    {
        if (transform.position.x < endDestination.position.x)
        {
            transform.position += new Vector3(1 / deep * Time.deltaTime / 2, 0, 0);
        }
        else
        {
            transform.position = new Vector3(beginDestination.position.x, transform.position.y, 0);
        }
    }
}
