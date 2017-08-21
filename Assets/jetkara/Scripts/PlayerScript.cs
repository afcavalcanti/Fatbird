using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Leap;

public class PlayerScript : MonoBehaviour
{
	bool dead;
	public AudioClip[] auClip;
	public GameObject fire;
	Controller LeapController;

	void Start()
	{
		dead = false;
		GetComponent<AudioSource>().clip = auClip[0];
		LeapController = new Controller ();
	}

	void Update()
	{
		if(gameObject.tag == "Player1"){
			Frame frame = LeapController.Frame ();
			if (frame.Hands.Count > 0) {
				List<Hand> hands = frame.Hands;
				Hand firstHand = hands [0];

				if (firstHand.GrabStrength > 0.8f && !dead) {
					RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			
					if (hit.collider == null) {
						Jump ();
					}
				}
			}
			if (Input.GetMouseButton(0) && !dead) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

				if (hit.collider == null) {
					Jump ();
				}
			}
			if (Ardunity.MappingInput._analogValue > 0.3f && !dead){
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

				if (hit.collider == null) {
					Jump ();
				}
			}
		}
		if(gameObject.tag == "Player2"){
			Frame frame = LeapController.Frame ();
			if (frame.Hands.Count > 0) {
				List<Hand> hands = frame.Hands;
				Hand secondHand = hands [0];

				if (secondHand.GrabStrength > 0.8f && !dead) {
					RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

					if (hit.collider == null) {
						Jump ();
					}
				}
			}
			if (Input.GetMouseButton(1) && !dead) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

				if (hit.collider == null) {
					Jump ();
				}
			}
			if (Ardunity.MappingInput._analogValue > 0.3f && !dead){
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

				if (hit.collider == null) {
					Jump ();
				}
			}
		}
	}

	void Jump()
	{
		fire.SetActive (true);
		GetComponent<AudioSource>().Play();
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
	}

	void OnTriggerEnter2D(Collider2D col) 
	{
		if (!dead)
		{
			if (col.tag == "Score")
			{
				GameObject.FindObjectOfType<GameManager>().Score++;
				Destroy(col.gameObject);
			}
			else if (col.tag == "Finish")
			{
				dead = true;
				GetComponent<AudioSource>().clip = auClip[1];
				GetComponent<AudioSource>().Play();
				Invoke("BackToMain", 1.5f);
			}
		}
	}

	void BackToMain()
	{
        SceneManager.LoadScene("MainMenu");
	}
}
