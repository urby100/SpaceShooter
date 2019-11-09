using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage3Controller : MonoBehaviour
{
    public GameObject LaserEnemy;
    public GameObject LaserEnemyPath;
    bool shootLaserOnce = false;
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

    public float addMovementSpeedToBoss = 1.5f;
    public List<float> laserAngleList;
    int laserAngleListIterator = 0;
    float laserAngleGoal;
    float laserRotationSpeed = 10f;
    float laserCurrentAngle;


    float attackSpeed = 0.1f;
    float nextAttackTime;

    public float shootCenterProjectileMaxAngle;


    // Start is called before the first frame update
    void Start()
    {
        leftBossLaser.transform.localEulerAngles = new Vector3(
               leftBossLaser.transform.localEulerAngles.x,
               leftBossLaser.transform.localEulerAngles.y,
                laserAngleList[laserAngleListIterator]);
        rightBossLaser.transform.localEulerAngles = new Vector3(
               leftBossLaser.transform.localEulerAngles.x,
               leftBossLaser.transform.localEulerAngles.y,
                -laserAngleList[laserAngleListIterator]);
        laserAngleListIterator++;

        bossScript = transform.parent.GetComponent<BossController>();
        bossScript.movementSpeedMultiplier = addMovementSpeedToBoss;
    }

    private void FixedUpdate()
    {
        
        laserCurrentAngle = leftBossLaser.transform.localEulerAngles.z;
        if (laserCurrentAngle > 300)
        {
            laserCurrentAngle = (laserCurrentAngle - 360);
        }
        if (laserCurrentAngle < shootCenterProjectileMaxAngle && !shootLaserOnce)
        {
            GameObject laserProjectile = Instantiate(LaserEnemy,
                bossProjectileSpawnPoint2.transform.position, new Quaternion());
            laserProjectile.GetComponent<BossLaserProjectileController>().path = LaserEnemyPath;
            laserProjectile.GetComponent<BossLaserProjectileController>().movementSpeed = 10;
            laserProjectile.GetComponent<BossLaserProjectileController>().boss = transform.parent.gameObject;
            laserProjectile.GetComponent<BossLaserProjectileController>().laserOffNodeNum = 4;
            laserProjectile.GetComponent<BossLaserProjectileController>().laserOnNodeNum = 5;
            laserProjectile.transform.parent = enemyProjectiles.transform;
            shootLaserOnce = true;
        }
        if (Time.time > nextAttackTime)
        {
            if (laserCurrentAngle > shootCenterProjectileMaxAngle)
            {
                GameObject projectileCenter = Instantiate(bossProjectilePrefab,
                    bossProjectileSpawnPoint2.transform.position, new Quaternion());
                projectileCenter.GetComponent<BossProjectileController>().path = projectilePath2;
                projectileCenter.GetComponent<BossProjectileController>().movementSpeed = 15;
                projectileCenter.transform.parent = enemyProjectiles.transform;
                shootLaserOnce = false;

            }

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


            nextAttackTime = Time.time + attackSpeed;

        }
        if (Mathf.Approximately(laserCurrentAngle, laserAngleList[laserAngleListIterator]))
        {
            laserAngleListIterator++;
            if (laserAngleListIterator >= laserAngleList.Count)
            {
                laserAngleListIterator = 0;
            }
        }


        leftBossLaser.transform.localRotation =
            Quaternion.RotateTowards(leftBossLaser.transform.localRotation,
            Quaternion.Euler(0, 0, laserAngleList[laserAngleListIterator]),
            laserRotationSpeed * Time.deltaTime);
        rightBossLaser.transform.localRotation =
            Quaternion.RotateTowards(rightBossLaser.transform.localRotation,
            Quaternion.Euler(0, 0, -laserAngleList[laserAngleListIterator]),
            laserRotationSpeed * Time.deltaTime);

        projectilePath1.transform.localRotation = leftBossLaser.transform.localRotation;
        projectilePath3.transform.localRotation = rightBossLaser.transform.localRotation;






    }
}
