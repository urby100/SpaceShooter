using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage2Controller : MonoBehaviour
{
    BossController bossScript;
    public GameObject leftBossLaser;
    public GameObject rightBossLaser;
    public GameObject frontBossLaser;

    public GameObject bossProjectileSpawnPoint1;
    public GameObject bossProjectileSpawnPoint2;
    public GameObject bossProjectileSpawnPoint3;

    public GameObject projectilePath1;
    public GameObject projectilePath2;
    public GameObject projectilePath3;

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

    float shootProjectilesWhileLaserAngleMin = 8f;
    float shootLeftAndRightProjectilesWhileLaserAngleMin = 15f;
    float bossMaxMovementSpeedMultiplier = 4f;
    float bossMovementSpeedMultiplierTick = 0.8f;

    float laserDanceAttackLasts = 6f;
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

            GameObject projectileCenter = Instantiate(bossProjectilePrefab,
                bossProjectileSpawnPoint2.transform.position, new Quaternion());
            projectileCenter.GetComponent<BossProjectileController>().path = projectilePath2;
            projectileCenter.GetComponent<BossProjectileController>().movementSpeed = 15;
            projectileCenter.transform.parent = enemyProjectiles.transform;

            if (laserCurrentAngle > shootLeftAndRightProjectilesWhileLaserAngleMin)
            {
                GameObject projectileLeft = Instantiate(bossProjectilePrefab,
                    bossProjectileSpawnPoint1.transform.position, new Quaternion());
                projectileLeft.GetComponent<BossProjectileController>().path = projectilePath1;
                projectileLeft.GetComponent<BossProjectileController>().movementSpeed = 15;
                projectileLeft.transform.parent = enemyProjectiles.transform;


                GameObject projectileRight = Instantiate(bossProjectilePrefab,
                    bossProjectileSpawnPoint3.transform.position, new Quaternion());
                projectileRight.GetComponent<BossProjectileController>().path = projectilePath3;
                projectileRight.GetComponent<BossProjectileController>().movementSpeed = 15;
                projectileRight.transform.parent = enemyProjectiles.transform;

            }

            nextAttackTime = Time.time + attackSpeed;

        }
        if ((laserCurrentAngle < shootProjectilesWhileLaserAngleMin && laserAngleGoal < laserCurrentAngle))
        {
            bossScript.movementSpeedMultiplier = Mathf.Clamp(
                bossScript.movementSpeedMultiplier + (bossMovementSpeedMultiplierTick * Time.deltaTime),
                1, bossMaxMovementSpeedMultiplier);
            if (!laserDanceAttackTimeSet)
            {
                laserDanceAttackTime = Time.time + laserDanceAttackLasts;
                laserDanceAttackTimeSet = true;
            }
        }
        if (Mathf.Approximately(laserCurrentAngle, laserAngleGoal))
        {

            if (laserAngleStart == laserAngleGoal)
            {
                laserAngleGoal = laserAngleEnd;
            }
            if (laserAngleEnd == laserAngleGoal && laserDanceAttackTimeSet && Time.time > laserDanceAttackTime)
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
        if (laserCurrentAngle > shootLeftAndRightProjectilesWhileLaserAngleMin)
        {
            projectilePath1.transform.localRotation = leftBossLaser.transform.localRotation;
            projectilePath3.transform.localRotation = rightBossLaser.transform.localRotation;
        }



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
