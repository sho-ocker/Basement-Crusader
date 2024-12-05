using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{
    public float speed;
    Rigidbody2D rigidbody;
    public Animator animator;

    public Text collectedText;
    public static int collectedAmount = 0;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;

    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;

        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float shootH = Input.GetAxis("ShootHorizontal");
        float shootV = Input.GetAxis("ShootVertical");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));


        if ((shootH != 0 || shootV !=0) && Time.time > lastFire + fireDelay){
            Shoot(shootH, shootV);
            lastFire = Time.time;
        }

        rigidbody.velocity = new Vector3(horizontal*speed, vertical*speed, 0);

        if (rigidbody.velocity.x > 0){
            transform.localScale = new Vector3(10, 10, 0);
        }else if (rigidbody.velocity.x < 0){
            transform.localScale = new Vector3(-10, 10, 0);
        }

        collectedText.text = "Items Collected: " + collectedAmount;
    }

    void Shoot(float x, float y){
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        );
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Spike"){
            GameController.instance.DamagePlayer(1);
        }
    }
}
