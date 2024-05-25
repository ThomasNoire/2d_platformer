using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public GameObject Win;
	public GameObject GameOver;
	public GameObject m_playerHitPrefab;
	public AudioClip m_jumpClip;
	public AudioClip m_hitClip;
	public bool IsSkipJumpSe;
	public bool isGameWon = false;

	public void Dead()
	{
		gameObject.SetActive( false );
		var cameraShake = FindObjectOfType<CameraShaker>();
		cameraShake.Shake();
		Instantiate( m_playerHitPrefab, transform.position, Quaternion.identity );
		var audioSource = FindObjectOfType<AudioSource>();
		audioSource.PlayOneShot( m_hitClip );
		GameOver.gameObject.SetActive( true );
		audioSource.Stop();
	}

	public void OnRetry()
	{
		SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
	}

	private void Awake()
	{
		var motor = GetComponent<PlatformerMotor2D>();
		motor.onJump += OnJump;
	}

	private void OnJump()
	{
		if ( IsSkipJumpSe )
		{
			IsSkipJumpSe = false;
		}
		else
		{
			var audioSource = FindObjectOfType<AudioSource>();
			audioSource.PlayOneShot( m_jumpClip );
		}
	}

	private void OnTriggerEnter2D( Collider2D other )
    {
        if ( other.name.Contains( "Goal" ) )
        {
            Win.gameObject.SetActive( true );
            isGameWon = true; 
        }
    }

	private void Update()
	{
		if (isGameWon)
		{
			var playerController = GetComponent<PlayerController2D>();
			if (playerController != null)
			{
				playerController.enabled = false;
			}
		}
	}
}