using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0;
    public GameObject impactEffect;

    public float damage = 0;
    public float lifetime = 0;
    public bool incendiary = false;
    public float fireDamage = 0;
    public Turret owner;
    public int pierce = 1;
    private int timesHit = 0;

    Vector3 dir;

    public void Seek(Vector3 direction)
    {
        dir = direction;
        if(incendiary){
            GetComponent<SpriteRenderer>().color  = new Color (255, 255, 255, 255); 
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        lifetime -= Time.deltaTime * 10;
        if(lifetime <= 0){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.tag == "Enemy" && timesHit < pierce){
            timesHit++;
            collider2D.GetComponent<Enemy>().TakeDamage(damage);
            if(incendiary){
                collider2D.GetComponent<Enemy>().Burn(fireDamage, owner);
            }
            if(impactEffect != null){
                GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, Quaternion.LookRotation(dir, Vector3.up));
                effectInstance.gameObject.transform.parent = collider2D.gameObject.transform;
                Destroy(effectInstance, 1);
            }
            
            if(timesHit >= pierce){
                Destroy(gameObject);
            }
        }
    }
}
