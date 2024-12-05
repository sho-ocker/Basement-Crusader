using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    Wander,
    Follow,
    Die,
    Attack,
    Idle
}

public enum EnemyType{
    Melee,
    Ranged,
    Boss
}

public class EnemyController : MonoBehaviour{
    GameObject player;
    
    public EnemyState currentState = EnemyState.Idle;
    public EnemyType enemyType;

    public float range;
    public float speed;
    public float attackRange;
  //  public float bulletSpeed;
    public float cooldown;
    public float health = 100;

    private bool attackCooldown = false;
    private bool chooseDir = false;
    private bool dead = false;
    public bool notInRoom = false;

    private Vector3 randomDir;
    public GameObject bulletPrefab; 

    private Vector3 _dir;

    public NextLevelMenu nextLevelMenu;


    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");            
    }

    void Update(){
        if (health <= 0){
            nextLevelMenu.NextLevel();  
        }

        switch(currentState){
           // case(EnemyState.Idle):
                //Idle();
            //    break;
            case(EnemyState.Wander):
                Wander();
                break;
            case(EnemyState.Follow):
                Follow();
                break;
          //  case(EnemyState.Die):
               // Death();
          //      break;
            case(EnemyState.Attack):
                Attack();
                break;
        }
        
        if (!notInRoom){
            if (IsPlayerInRange(range) && currentState != EnemyState.Die && health > 70){
                currentState = EnemyState.Follow;
            }else if (!IsPlayerInRange(range) && currentState != EnemyState.Die){
                currentState = EnemyState.Wander;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange || health < 70){
                currentState = EnemyState.Attack;
            }
        }else{
            currentState = EnemyState.Idle;
        }
    }

    private bool IsPlayerInRange(float range){
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection(){
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    void Wander(){
        if (!chooseDir){
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;
        if (IsPlayerInRange(range)){
            currentState = EnemyState.Follow;
        }
    }

    void Follow(){
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack(){
        if (!attackCooldown){
            switch (enemyType){
                case (EnemyType.Melee):
                    GameController.instance.DamagePlayer(1);
                    StartCoroutine(Cooldown());
                    break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    StartCoroutine(Cooldown());
                    break;
                case (EnemyType.Boss):
                    if (health > 70){
                        attackRange = 3;
                        GameController.instance.DamagePlayer(1);
                    } else if (health > 30){
                        attackRange = 75;
                        GameObject bulletBoss = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                        bulletBoss.GetComponent<BulletController>().isEnemyBullet = true;
                        bulletBoss.GetComponent<BulletController>().GetPlayer(player.transform);
                    } else if (health > 0){
                        speed = 5;
                        attackRange = 1000;
                        cooldown = 0.25f;
                        GameObject bulletBosss = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                        bulletBosss.GetComponent<BulletController>().isEnemyBullet = true;
                        bulletBosss.GetComponent<BulletController>().GetPlayer(player.transform);
                    }
                    StartCoroutine(Cooldown());
                    break;
            }
        }
    }

    private IEnumerator Cooldown(){
        attackCooldown = true;
        yield return new WaitForSeconds(cooldown);
        attackCooldown = false;
    }

    public void Death(){
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(gameObject);
    }
}
