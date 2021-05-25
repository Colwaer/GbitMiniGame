using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUpDownCloud : MonoBehaviour
{
    public float speed = 0.05f;
    Material material;
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    private void Update()
    {
        material.mainTextureOffset = new Vector2(material.mainTextureOffset.x - speed * Time.deltaTime, material.mainTextureOffset.y);
    }
}
