using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private Rigidbody playerRb;
    private Animator playerAnim;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround &&!gameOver){
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround =false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound,1.0f);
        }
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
            dirtParticle.Play();
        }
        if(collision.gameObject.CompareTag("Obstacle")){
            Debug.Log("GAME OVER");
            gameOver = true;
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound,1.0f);
            explosionParticle.Play();
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int",1);
        }
        
    }
}
