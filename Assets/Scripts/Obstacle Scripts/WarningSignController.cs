using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSignController : MonoBehaviour
{
    SpriteRenderer warningSignSR;
    public float destroyDelay = 1.5f;

    float minOpacityValue = 0.6f;
    float opacityChangeAmount = 0.05f;
    float changeOpacityDir = -1;
    Color currentColor;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "WarningSign";
        warningSignSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Destroy(gameObject, destroyDelay);
    }

    // Update is called once per frame
    void Update()
    {
        currentColor = warningSignSR.color;
        currentColor.a = currentColor.a + (opacityChangeAmount * changeOpacityDir);
        if (currentColor.a <= minOpacityValue) {
            changeOpacityDir = 1;
        }
        if (currentColor.a >= 1)
        {
            changeOpacityDir = -1;
        }
        warningSignSR.color = currentColor;
    }

}
