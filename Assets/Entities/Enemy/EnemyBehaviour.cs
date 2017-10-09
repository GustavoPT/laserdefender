using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject projectile;
	public float projectileSpeed = 10f;
	public float health = 150;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;
	private ScoreKeeper scoreKeeper;
	
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	void Update () {
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability){
			Fire ();
		}
	}
	
	void Fire() {
		Vector3 startPosition = transform.position + new Vector3(0,-1,0);
		GameObject missile = Instantiate(projectile,startPosition,Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector2(0,-projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound,transform.position);
	}
	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log(collider);
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0){
				Die();
			}
		}
	}
	
	void Die(){
//		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
//		man.LoadLevel("Win Screen");
		AudioSource.PlayClipAtPoint(deathSound,transform.position);
		scoreKeeper.Score(scoreValue);
		Destroy(gameObject);
	}
	
}
