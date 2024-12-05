using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour{
    public float lifeTime;
    public float speed;
    public bool isEnemyBullet = false;
    private Vector2 lastPos;
    private Vector2 currPos;
    private Vector2 playerPos;


    void Start(){
        StartCoroutine(DeathDelay());
        if (!isEnemyBullet)
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
    }

    void Update(){
        if (isEnemyBullet){
            currPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 10f * Time.deltaTime);
            if (currPos == lastPos)
                Destroy(gameObject);
            lastPos = currPos;
        }
    } 

    public void GetPlayer(Transform player){
        playerPos = player.position;
    }

    IEnumerator DeathDelay(){
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "Enemy" && !isEnemyBullet){
            collider.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }else if (collider.tag == "Player" && isEnemyBullet){
            GameController.instance.DamagePlayer(1);
            Destroy(gameObject);
        }else if (collider.tag == "Wall" || collider.tag == "Door" || collider.tag == "DoorBoxCollider"){
            Destroy(gameObject);
        }else if (collider.tag == "Boss" && !isEnemyBullet){
            collider.gameObject.GetComponent<EnemyController>().health -= 5;
            Destroy(gameObject);
        }
    }
}
