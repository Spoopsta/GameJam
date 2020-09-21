using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine.SceneManagement;
using TMPro;
using Random = UnityEngine.Random;


namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    [Serializable]
    public class FirstPersonController : MonoBehaviour
    {
        
        [SerializeField] private bool m_IsWalking;
        [SerializeField] public float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.
        [SerializeField] private AudioClip m_ItemGet;

        
        public GameObject player;

        public GameObject projectileSet1, projectileSet2, projectileSet3, projectileSet4, projectileSet5;
       
        //public GameObject levelManager;
        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        public Vector3 m_MoveDir = Vector3.zero;
        public CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        //  private bool m_Dash;
        //  private bool bAirDashed;
        //  private int iDashedCount;
        // private int iDashCount;
        public float dashSpeed;
        public bool bAirJump;
        public bool playCutscene;
        private bool bCompleteLevel;
        private int endCounter;
        private bool end;
        private bool level2;
        private KeyCode DashKey = KeyCode.LeftShift;
        private AudioSource m_AudioSource;
        public int jumpTextCounter;
        public int dashTextCounter;
        public float decelerationRatePerFrame;
        public float platformAcceleration;
        public float maximumAcceleration;
        public float wallrunAcceleration;
        public float dashCooldown;
        public float dashFrames;
        private bool bSheepText;
        private bool gamePaused;
        // public float jumpPadBoost;

        public TextMeshProUGUI dashText, jumpText, sheepText, keycardsText, instructionsText;

        private RaycastHit rHitL;
        private RaycastHit rHitR;
        private bool bIsWallR;
        private bool bIsWallL;
        private float m_GravityMultiplierOG;

        private Rigidbody rb;

        public Animator anim;

        public GameObject pauseMenu, instructionsPanel;


        public int punchCards;
        public int sheepCollected;

        // Use this for initialization
        private void Start()
        {
            dashCooldown = 0;
            dashFrames = 0;
            GetComponentInChildren<ParticleSystem>().Stop();
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_Jumping = false;
            // iDashCount = 0;
            //  iDashedCount = 0;
            //  bAirDashed = false;
            bAirJump = false;
            bCompleteLevel = false;
            m_AudioSource = GetComponent<AudioSource>();
            m_MouseLook.Init(transform, m_Camera.transform);
            endCounter = 0;
            playCutscene = false;
            end = false;
            anim = GetComponent<Animator>();
            pauseMenu.gameObject.SetActive(false);
            //  textFade = GetComponent<Animator>();
            // textFade.SetBool("FadeIN", false);
            projectileSet1.gameObject.SetActive(false);
            projectileSet2.gameObject.SetActive(false);
            projectileSet3.gameObject.SetActive(false);
            projectileSet4.gameObject.SetActive(false);
            projectileSet5.gameObject.SetActive(false);

            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                level2 = true;
            }
            else {
                level2 = false;
            }
        }

        private void OnTriggerEnter(Collider collision)
        {


            if (collision.gameObject.tag.Equals("Pickup")) {
                collision.gameObject.GetComponent<BoxCollider>().enabled = false;
                collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
                collision.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
                //GetComponentInChildren<ParticleSystem>().Stop();
                dashText.text = "1";
                jumpText.text = "1";
                dashCooldown = 0f;
                bAirJump = true;

                PlayItemGet(collision.gameObject);
            }

         


            //when hitting special pickups it adds to the punch cards int.
            //FirstPersonController is called in GameManager where if punchCards == 1 Wall 1 is set to false.
            if (collision.gameObject.tag.Equals("SpecialPickup")) {
                Debug.Log("punch ard obtained");
                collision.gameObject.SetActive(false);
                punchCards++;

                keycardsText.text = "Keycards Collected: " + punchCards;
                Debug.Log(punchCards);




                /* bCompleteLevel = true;
                 collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
                 collision.gameObject.GetComponent<MeshCollider>().enabled = false;
                 collision.GetComponent<AudioSource>().Play();
                 */
            }

            if (collision.gameObject.tag.Equals("WinWall"))
            {
                SceneManager.LoadScene(sceneBuildIndex: 2);
            }

            if (collision.gameObject.tag.Equals("MiddleWall"))
            {
                m_MoveDir.y = 10.0f;
            }

            /*if (collision.gameObject.tag.Equals("aaa")) {
                 if (bCompleteLevel)
                 {
                    collision.gameObject.GetComponent<MeshCollider>().enabled = false;
                    if (!level2)
                    {
                        end = true;
                    }
                 }
                 else {
                     collision.GetComponent<AudioSource>().Play();
                 }
            }
            */

            if (collision.gameObject.tag.Equals("done")) {
                if (bCompleteLevel) {
                    collision.GetComponent<AudioSource>().Play();
                    collision.gameObject.GetComponent<MeshCollider>().enabled = false;
                    end = true;
                }
            }
            //collision of red circle on basic platforms increases movement speed by platform acceleration to a cap of maximum acceleration
            if (collision.gameObject.tag.Equals("SpeedBoost"))
            {
                if (m_WalkSpeed <= maximumAcceleration)
                {
                    Debug.Log("Movement speed increased");
                    // Debug.Log(m_WalkSpeed);
                    m_WalkSpeed = m_WalkSpeed + platformAcceleration;
                }
            }
            //if colliding with a sheep. sheep int increases byt +1 , sheep text changes to reflect this
            if (collision.gameObject.tag.Equals("Sheep"))
            {
                sheepCollected++;
                sheepText.text = "Sheep Collected: " + sheepCollected;
                // bSheepText = true;
                collision.gameObject.SetActive(false);
            }

            if (collision.gameObject.tag.Equals("Teleporter"))
            {
                dashText.text = "1";
                jumpText.text = "1";
                dashCooldown = 0f;
                bAirJump = true;
            }


            if (collision.gameObject.tag.Equals("Door1"))
            {
                instructionsPanel.gameObject.SetActive(true);
                instructionsText.text = "I need to find the Yellow Key Card to open this door.";
            }

            if (collision.gameObject.tag.Equals("Door2"))
            {
                instructionsPanel.gameObject.SetActive(true);
                instructionsText.text = "I need to find the Blue Key Card to open this door.";
            }

            if (collision.gameObject.tag.Equals("Door3"))
            {
                instructionsPanel.gameObject.SetActive(true);
                instructionsText.text = "I need to find the Red Key Card to open this door.";
            }

            if (collision.gameObject.tag.Equals("JumpInstructions"))
            {
                instructionsPanel.gameObject.SetActive(true);
                instructionsText.text = "Press 'Space' to Jump. Landing in the red circle increases speed";
            }

            if (collision.gameObject.tag.Equals("DashInstructions"))
            {
                instructionsPanel.gameObject.SetActive(true);
                instructionsText.text = "Press 'LShift' to Dash";
            }

            if (collision.gameObject.tag.Equals("WallrunInstructions"))
            {
                instructionsPanel.gameObject.SetActive(true);
                instructionsText.text = "These walls allow you to run along them. As well as giving infinite jumps.";
            }

            //projectiles

            if (collision.gameObject.tag.Equals("ProjectileSet1"))
            {
                projectileSet1.gameObject.SetActive(true);
            }

            if (collision.gameObject.tag.Equals("ProjectileSet2"))
            {
                projectileSet2.gameObject.SetActive(true);
            }

            if (collision.gameObject.tag.Equals("ProjectileSet3"))
            {
                projectileSet3.gameObject.SetActive(true);
            }

            if (collision.gameObject.tag.Equals("ProjectileSet4"))
            {
                projectileSet4.gameObject.SetActive(true);
            }

            if (collision.gameObject.tag.Equals("ProjectileSet5"))
            {
                projectileSet5.gameObject.SetActive(true);
            }



        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag.Equals("ProjectileSet1"))
            {
                projectileSet1.gameObject.SetActive(true);
            }  
            
            if (other.gameObject.tag.Equals("ProjectileSet2"))
            {
                projectileSet2.gameObject.SetActive(true);
            }

            if (other.gameObject.tag.Equals("ProjectileSet3"))
            {
                projectileSet3.gameObject.SetActive(true);
            }

            if (other.gameObject.tag.Equals("ProjectileSet4"))
            {
                projectileSet4.gameObject.SetActive(true);
            }

            if (other.gameObject.tag.Equals("ProjectileSet5"))
            {
                projectileSet5.gameObject.SetActive(true);
            }

        }

        private void OnTriggerExit(Collider other)
         {
            if (other.gameObject.tag.Equals("Door1") || other.gameObject.tag.Equals("Door2") || other.gameObject.tag.Equals("Door3") || other.gameObject.tag.Equals("JumpInstructions")|| 
                other.gameObject.tag.Equals("DashInstructions")||other.gameObject.tag.Equals("WallrunInstructions"))
            {
                instructionsPanel.gameObject.SetActive(false);
            }

            if (other.gameObject.tag.Equals("ProjectileSet1"))
            {
                projectileSet1.gameObject.SetActive(false);
            }

            if (other.gameObject.tag.Equals("ProjectileSet2"))
            {
                projectileSet2.gameObject.SetActive(false);
            }

            if (other.gameObject.tag.Equals("ProjectileSet3"))
            {
                projectileSet3.gameObject.SetActive(false);
            }

            if (other.gameObject.tag.Equals("ProjectileSet4"))
            {
                projectileSet4.gameObject.SetActive(false);
            }

            if (other.gameObject.tag.Equals("ProjectileSet5"))
            {
                projectileSet5.gameObject.SetActive(false);
            }

        }
         

        // Update is called once per frame
        private void Update()
        {
            if (m_WalkSpeed >= 8.0 && !CheckWallTouch())
            {
                m_WalkSpeed = m_WalkSpeed - decelerationRatePerFrame;
            }


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseMenu.gameObject.activeInHierarchy == false)
                {
                    pauseMenu.gameObject.SetActive(true);
                    Time.timeScale = 0f;
                }

               /* if (pauseMenu.gameObject.activeInHierarchy == true)
                {
                    pauseMenu.gameObject.SetActive(false);
                    Time.timeScale = 1f;
                }
               */
            }


            /*
            if (bSheepText == true)
            {
                bSheepText = false;
                this.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Animator>().SetBool("Fade IN", true);
            }

            if (bSheepText == false)
            {
                this.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Animator>().SetBool("Fade IN", false);
            }
            */
            


            if (end) {
                endCounter++;
                if (level2 && endCounter > 120 || !level2 && endCounter > 50)
                {
                    if (!level2)
                    {
                        SceneManager.LoadScene(sceneBuildIndex: 3);
                    }
                    else {
                        SceneManager.LoadScene(sceneBuildIndex: 4);
                    }
                }
            }
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump && (m_CharacterController.isGrounded || bAirJump))
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");

            }

            //WALLRUNNING
            CheckWallTouch();




            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;

                //when hit ground text go 1
                dashText.text = "1";
                jumpText.text = "1";
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;


            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;

            //temporary code to restart level for testing purposes
            ResetLevel();

            //new place for dashing?
            PlayerDashing();

        }



        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {

            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Break();
            }

            //Debug.Log(m_CharacterController.isGrounded);
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            //attempt at adjusting velocity within the character controller.
            Vector3 verticalVelocity = m_CharacterController.velocity;
            verticalVelocity = new Vector3(m_CharacterController.velocity.x, m_CharacterController.velocity.y + 0.5f, m_CharacterController.velocity.z);

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height / 1.5f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;



            //check to make sure speed is increasing on walls
            //Debug.Log(m_WalkSpeed);




            //calling for the jumping script.
            PlayerJumping();



        }

        /// <summary>
        /// This is the code that handles the players jumping.
        /// </summary>
        private void PlayerJumping()
        {
            float speed;
            GetInput(out speed);
            //JUMPING
            if (m_CharacterController.isGrounded)
            {
                bAirJump = false;

                m_MoveDir.y = m_JumpSpeed;
                m_MoveDir.y = -m_StickToGroundForce;



                if (m_Jump)
                {

                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                    jumpText.text = "0";
                    jumpTextCounter++;
                    dashFrames = 0;
                    dashCooldown = 0;

                }

            }
            else
            {
                if (m_Jump)
                {
                    //og
                    m_MoveDir.y = m_JumpSpeed;

                    PlayJumpSound();
                    m_Jump = false;
                    if (bAirJump)
                    {
                        bAirJump = false;
                        jumpText.text = "0";
                    }
                }

                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }

        /// <summary>
        /// This is the code that handles the player dashing 
        /// </summary>
        private void PlayerDashing()
        {
            //perform the dash - if the button is pushed & we aren't in dashCooldown & we're in the air
            if (Input.GetKeyDown(DashKey) && dashCooldown == 0 && !m_CharacterController.isGrounded)
            {
                GetComponentInChildren<ParticleSystem>().Play();
                //when dash text go 0
                dashText.text = "0";
                //No. of frames to dashCooldown:
                dashCooldown = 75f;
                //No. of frames to apply dash over:
                dashFrames = 20f;
            }
            //every frame, reduce dashCooldown frames by one.
            if (dashCooldown > 0)
            {
                dashCooldown--;

            }

            //Every frame, if remaining dash frames > 0, continue dashing and reduce dash frames 
            if (dashFrames > 0)
            {
                transform.position = transform.position + Camera.main.transform.forward * dashSpeed * Time.deltaTime;
                dashFrames--;
            }


            //Not sure what this bit does??!? Left it in. Enlighten me.
            if (dashFrames == 0)
            {
                GetComponentInChildren<ParticleSystem>().Stop();
            }

            if (dashCooldown == 0)
            {
                dashText.text = "1";
            }
            //Every frame - check we're grounded? If so, allow dashing immediately.
            if (m_CharacterController.isGrounded)
            {
                dashCooldown = 0;
                dashFrames = 0;
            }

        }


        //WALL RUNNING
        private bool CheckWallTouch()
        {
            //checking for wall touch on the right of player
            if (Physics.Raycast(transform.position, transform.right, out rHitR, 2.25f))
            {
                if (rHitR.transform.tag == "Wall")
                {
                    bIsWallR = true;
                    bIsWallL = false;
                    bAirJump = true;
                    //anim.SetBool("RWallRun", true);

                    jumpText.text = "1";



                    if (m_WalkSpeed <= 15)
                    {
                        m_WalkSpeed = m_WalkSpeed + wallrunAcceleration;
                    }

                    return true;
                }

            }

            //checking for wall touch on the left of the player
            if (Physics.Raycast(transform.position, -transform.right, out rHitL, 2.25f))
            {
                if (rHitL.transform.tag == "Wall")
                {
                    bIsWallL = true;
                    bIsWallR = false;
                    bAirJump = true;

                    //m_JumpSpeed = 6.0f;
                    jumpText.text = "1";


                    if (m_WalkSpeed <= 15)
                    {
                        m_WalkSpeed = m_WalkSpeed + wallrunAcceleration;
                    }
                    //anim.SetTrigger("LeftWallHit");
                    // anim.SetBool("leftWallHit", true);
                    return true;

                }
            }

            return false;
        }

        //AUDIO
        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }

        private void PlayItemGet(GameObject gObject)
        {
            gObject.GetComponent<AudioSource>().Play();
        }

        //FUCKIN SOMETHING
        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                             Time.fixedDeltaTime;
            }


            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        public void ResumeGame()
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        //MORE AUDIO
        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }

        //RESET LEVEL
        private void ResetLevel()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        //PLAYER INPUT
        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal") * Time.deltaTime;
            float vertical = CrossPlatformInputManager.GetAxisRaw("Vertical") * Time.deltaTime;

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
           m_MouseLook.LookRotation(transform, m_Camera.transform);
        }



        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
