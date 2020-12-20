using Aura2API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LightFlash : MonoBehaviour
{
    public Vector2 flashTime;
    public Vector2Int flashCount;
    public Vector2 flashDelayTime;

    public Light light;
    public AuraLight aura;
    public Material mat;

    public Vector4 eColor;
    private void Start()
    {
        light = gameObject.transform.GetChild(2).gameObject.GetComponent<Light>();
        aura = gameObject.transform.GetChild(2).gameObject.GetComponent<AuraLight>();
        mat = gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material;
        eColor = mat.GetColor("_EmissionColor");
        StartCoroutine("Flash");
    }
    void OnLight()
    {
        light.enabled = true;
       // aura.enabled = true;
        mat.SetColor("_EmissionColor", eColor);
    }

    void OffLight()
    {
        light.enabled = false;
       // aura.enabled = false;
        mat.SetColor("_EmissionColor", new Vector4(0, 0, 0, 0));
    }

    IEnumerator Flash()
    {
        while (true)
        {
            for (int i = 0; i < Random.Range(flashCount.x, flashCount.y); i++)
            {

                yield return new WaitForSeconds(Random.Range(flashTime.x, flashTime.y));
                OffLight();
                yield return new WaitForSeconds(Random.Range(flashTime.x, flashTime.y));
                OnLight();
            }
            yield return new WaitForSeconds(Random.Range(flashDelayTime.x, flashDelayTime.y));
        }
    }
}