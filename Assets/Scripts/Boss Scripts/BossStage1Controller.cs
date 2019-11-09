using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage1Controller : MonoBehaviour
{
    BossController bossScript;
    public GameObject leftBossLaser;
    public GameObject rightBossLaser;
    public GameObject frontBossLaser;
    public GameObject bossProjectileSpawnPoint;
    public GameObject projectilesPath;
    public GameObject enemyProjectiles;
    public GameObject bossProjectilePrefab;

    public float laserAngleStart = 30f;
    public float laserAngleEnd = -30;
    float laserAngleGoal;
    float laserRotationSpeed = 10f;
    float laserCurrentAngle;

    float leftLaserLocalStartX;
    float rightLaserLocalStartX;

    float frontLaserScaler = 15;
    float frontLaserLocalStartScaleY;

    float attackSpeed = 0.1f;
    float nextAttackTime;

    float shootProjectilesWhileLaserAngleMin = 10f;
    float bossMaxMovementSpeedMultiplier = 3f;
    float bossMovementSpeedMultiplierTick = 0.8f;

    float laserDanceAttackLasts = 5f;
    float laserDanceAttackTime;
    bool laserDanceAttackTimeSet = false;

    // Start is called before the first frame update
    void Start()
    {
        leftBossLaser.transform.localEulerAngles = new Vector3(
               leftBossLaser.transform.localEulerAngles.x,
               leftBossLaser.transform.localEulerAngles.y,
                laserAngleStart);
        rightBossLaser.transform.localEulerAngles = new Vector3(
               leftBossLaser.transform.localEulerAngles.x,
               leftBossLaser.transform.localEulerAngles.y,
                -laserAngleStart);
        laserAngleGoal = laserAngleEnd;

        bossScript = transform.parent.GetComponent<BossController>();
        leftLaserLocalStartX = leftBossLaser.transform.localPosition.x;
        rightLaserLocalStartX = rightBossLaser.transform.localPosition.x;
        frontLaserLocalStartScaleY = frontBossLaser.transform.localScale.y;
    }

    private void FixedUpdate()
    {




        laserCurrentAngle = leftBossLaser.transform.localEulerAngles.z;
        if (laserCurrentAngle > 300)
        {
            laserCurrentAngle = (laserCurrentAngle - 360);
        }
        if (Time.time > nextAttackTime && laserCurrentAngle > shootProjectilesWhileLaserAngleMin)
        {
            GameObject projectile = Instantiate(bossProjectilePrefab,
                bossProjectileSpawnPoint.transform.position, new Quaternion());
            projectile.GetComponent<BossProjectileController>().path = projectilesPath;
            projectile.GetComponent<BossProjectileController>().movementSpeed = 15;
            projectile.transform.parent = enemyProjectiles.transform;
            nextAttackTime = Time.time + attackSpeed;
        }
        if ((laserCurrentAngle < shootProjectilesWhileLaserAngleMin && laserAngleGoal< laserCurrentAngle))
        {
            bossScript.movementSpeedMultiplier = Mathf.Clamp(
                bossScript.movementSpeedMultiplier + (bossMovementSpeedMultiplierTick * Time.deltaTime),
                1, bossMaxMovementSpeedMultiplier);
            if (!laserDanceAttackTimeSet) {
                laserDanceAttackTime = Time.time + laserDanceAttackLasts;
                laserDanceAttackTimeSet = true;
            }
        }
        if (Mathf.Approximately(laserCurrentAngle,laserAngleGoal))
        {
            
            if (laserAngleStart== laserAngleGoal)
            {
                laserAngleGoal = laserAngleEnd;
            }
            if (laserAngleEnd==laserAngleGoal && laserDanceAttackTimeSet && Time.time > laserDanceAttackTime)
            {
                laserAngleGoal = laserAngleStart;
                laserDanceAttackTimeSet = false;
                bossScript.movementSpeedMultiplier = 1;
            }
        }


        leftBossLaser.transform.localRotation =
            Quaternion.RotateTowards(leftBossLaser.transform.localRotation,
            Quaternion.Euler(0, 0, laserAngleGoal),
            laserRotationSpeed * Time.deltaTime);
        rightBossLaser.transform.localRotation =
            Quaternion.RotateTowards(rightBossLaser.transform.localRotation,
            Quaternion.Euler(0, 0, -laserAngleGoal),
            laserRotationSpeed * Time.deltaTime);

        //scale front laser
        float distance = Mathf.Abs(rightBossLaser.transform.localPosition.x - leftBossLaser.transform.localPosition.x);
        frontBossLaser.transform.localScale =
            new Vector3(frontBossLaser.transform.localScale.x,
            distance,
            frontBossLaser.transform.localScale.z);

        //nastavimo oddaljenost laserjev
        if (laserCurrentAngle <= 0)
        {
            leftBossLaser.transform.localPosition =
                new Vector3(leftLaserLocalStartX + laserCurrentAngle / frontLaserScaler,
                leftBossLaser.transform.localPosition.y,
                leftBossLaser.transform.localPosition.z);
            rightBossLaser.transform.localPosition =
                new Vector3(rightLaserLocalStartX + -laserCurrentAngle / frontLaserScaler,
                rightBossLaser.transform.localPosition.y,
                rightBossLaser.transform.localPosition.z);


        }



    }
}
