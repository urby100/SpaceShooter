using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour
{
    public Vector2 offset=new Vector2(-2,-2);

    SpriteRenderer mainSprRnd;
    SpriteRenderer shadowSprRnd;

    Transform mainTransform;
    Transform shadowTransform;

    public Material shadowMaterial;
    public Color shadowColor;

    // Start is called before the first frame update
    void Start()
    {
        mainSprRnd = GetComponent<SpriteRenderer>();
        mainTransform = transform;
        shadowTransform = new GameObject().transform;
        shadowSprRnd = shadowTransform.gameObject.AddComponent<SpriteRenderer>();



        shadowSprRnd.material = shadowMaterial;
        shadowSprRnd.color = shadowColor;

        shadowSprRnd.sortingLayerName = "Shadow";
        shadowSprRnd.sortingOrder = 0;

        shadowTransform.position = mainTransform.position;
        shadowTransform.localScale = mainTransform.lossyScale;
        shadowTransform.rotation = mainTransform.rotation;
        shadowTransform.parent = mainTransform;
        shadowTransform.gameObject.name = "Shadow Sprite";
    }

    private void LateUpdate()
    {
        shadowTransform.position =
            new Vector2(mainTransform.position.x+offset.x, mainTransform.position.y + offset.y );

        shadowSprRnd.sprite = mainSprRnd.sprite;
    }
}
