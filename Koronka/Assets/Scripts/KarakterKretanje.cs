using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

/* 
 * Skripta koja kontroliše kretanje karaktera i interakciju sa elementima. 
 * Povezuje se za instancu igrača. 
 */
public class KarakterKretanje : MonoBehaviour
{
	/*
	 * Javna polja koja je moguće menjati za svakog karaktera.
	 * Definišu dinamiku kretanja, brzinu kretanja, skoka i broj skokova.
	 * Takođe se čuva objekat koji definiše tačku ponovnog pojavljivanja nakon
	 * gubitka života.
	 */
	public float movementSpeed = 2f;
	public float jumpSpeed = 5f;
	public int maximumJumps = 2;
	public Transform spawnPoint;

	private float inputHorizontal;
	private int jumpsCount;
	Animator anim;
	bool rigthDirection = true;

	// Inicijalizacija.
	void Start()
	{
		anim = GetComponent<Animator>();
		jumpsCount = maximumJumps;
	}

	/* 
	 * Ukoliko je pritisnut taster Space i moguće je skočiti,
	 * pomeriti karaktera i umanjiti broj skokova.
	 * Kontrola ostatka kretanja i podešavanje parametara animacije.
	 */
	void FixedUpdate()
	{
		inputHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");

		anim.SetFloat("brzina", Mathf.Abs(inputHorizontal));
		anim.SetBool("skok", CrossPlatformInputManager.GetButtonDown("Jump") && jumpsCount != 0);

		if (CrossPlatformInputManager.GetButtonDown("Jump") && jumpsCount != 0)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
			jumpsCount--;

		}

		GetComponent<Rigidbody2D>().velocity = new Vector2(inputHorizontal * movementSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (inputHorizontal > 0 && !rigthDirection)
		{
			PromeniPravac();
		}
		else if (inputHorizontal < 0 && rigthDirection)
		{
			PromeniPravac();
		}
	}

	// Kontrola promene pravca.
	void PromeniPravac()
	{
		rigthDirection = !rigthDirection;
		Vector3 karakterScale = transform.localScale;
		karakterScale.x *= -1;
		transform.localScale = karakterScale;
	}

	/* 
	 * Detekcija da li je karakter u koliziji sa platformom.
	 * Ako jeste, osvežiti broj dozvoljenih skokova.
	 */
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "platform")
		{
			jumpsCount = maximumJumps;
		}
	}

	/*
	 * Detekcija da li je karakter u dodiru sa elementima
	 * za rezultat, dodavanje života ili sa kućom.
	 */
	private void OnTriggerEnter2D(Collider2D other)
    {		
		switch (other.gameObject.tag)
		{
			case "disinfectant":
				ScoreController.instance.ChangeScore("disinfectant");
				Destroy(other.gameObject);
				break;
			case "soap":
				ScoreController.instance.ChangeScore("soap");
				Destroy(other.gameObject);
				break;
			case "mask":
				ScoreController.instance.ChangeScore("mask");
				Destroy(other.gameObject);
				break;
			case "gloves":
				ScoreController.instance.ChangeScore("gloves");
				Destroy(other.gameObject);
				break;
			case "safezone":
				spawnPoint.transform.position = other.gameObject.transform.position;
				break;
			case "extralife":
				ScoreController.instance.IncreaseNumberOfLives();
				Destroy(other.gameObject);
				break;
			default:
				break;
		}
	}
}