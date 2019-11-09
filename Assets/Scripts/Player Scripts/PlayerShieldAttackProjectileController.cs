using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldAttackProjectileController : MonoBehaviour
{
    public float powerMultiplier = 1f;

    float growTick = 15f;

    float growMaxAmount;

    float attackLasts = 0.5f;

    float axisScale;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "PlayerShieldAttack";
        Destroy(gameObject, attackLasts);
    }

    void FixedUpdate()
    {
        axisScale = growTick * Time.deltaTime*powerMultiplier;
        transform.localScale += new Vector3(axisScale, axisScale, 0);
    }
}
