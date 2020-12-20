using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public Material _material;
    public Vector2 _texOffset;
    public float speed;
    void Start() {
        _material = gameObject.GetComponent<MeshRenderer>().material;
        _texOffset = _material.GetTextureOffset("_MainTex"); } 
    void Update() {  
        _texOffset.x += Time.deltaTime * speed;
        _material.SetTextureOffset("_MainTex", _texOffset);
        _material.SetTextureOffset("_EmissionMap", _texOffset);
    }
        
}
