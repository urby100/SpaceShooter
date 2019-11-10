using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public ParticleSystem effect;
    public GameObject player;
    public List<Sprite> powerUpSprites;

    Rigidbody2D rb;
    float movementSpeed = 3;
    float rotationSpeed = 0.2f;

    int randomSelector;
    string[] powerUpNames = new string[6] { "Invincibility", "HP", "Shield", "ScoreMultiplier","NitroFuel","LaserFuel" };

    public bool invincible = false;
    public float health = 0;
    public float shield = 0;
    public float nitroFuel = 0;
    public float laserFuel = 0;
    public float scoreMultiplier = 0;

    float healthAmount = 20;
    float shieldAmount = 25;
    float nitroFuelAmount = 40;
    float laserFuelAmount = 30;
    float scoreMultiplierAmount = 5;

    float restrictHorizontal = 12f;
    float restrictVertical = 7f;

    // Start is called before the first frame update
    void Start()
    {
        randomSelector = Random.Range(0, powerUpNames.Length);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = powerUpSprites[randomSelector];
        switch (randomSelector)
        {
            case 0:
                invincible = true;
                break;
            case 1:
                health = healthAmount;
                break;
            case 2:
                shield = shieldAmount;
                break;
            case 3:
                scoreMultiplier = scoreMultiplierAmount;
                break;
            case 4:
                nitroFuel = nitroFuelAmount;
                break;
            case 5:
                laserFuel = laserFuelAmount;
                break;
            default:
                break;
        }

        gameObject.name = powerUpNames[randomSelector] + "PowerUp";
        rb = GetComponent<Rigidbody2D>();
        setPath();

         Color MyPixel = powerUpSprites[randomSelector].texture.GetPixel(0, 0);
         var mainPS = effect.main;
         mainPS.startColor = MyPixel;
    }

    void FixedUpdate()
    {
        //rotate towards player slowly
        Vector2 vectorToTarget = player.transform.position - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        rb.velocity = transform.up * movementSpeed;

        //movement restrict
        if (transform.position.x >= restrictHorizontal || transform.position.x <= -restrictHorizontal
            || transform.position.y >= restrictVertical || transform.position.y <= -restrictVertical)
        {
            Destroy(gameObject);
        }
    }

    void setPath()
    {
        rb.velocity = Vector2.zero;
        Vector2 direction = new Vector2(
            player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y);
        transform.up = direction;
    }
}
