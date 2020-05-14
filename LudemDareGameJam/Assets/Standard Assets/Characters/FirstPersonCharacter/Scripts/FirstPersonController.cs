using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;




namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
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
        //public GameObject levelManager;
        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private bool m_Dash;
        private bool bAirDashed;
        private int iDashedCount;
        private int iDashCount;
        private bool bAirJump;
        public bool playCutscene;
        private bool bCompleteLevel;
        private int endCounter;
        private bool end;
        private bool level2;
        private KeyCode DashKey = KeyCode.LeftShift;
        private AudioSource m_AudioSource;

        private RaycastHit rHitL;
        private RaycastHit rHitR;
        private bool bIsWallR;
        private bool bIsWallL;
        private float m_GravityMultiplierOG;


       [SerializeField] public int punchCards;

        // Use this for initialization
        private void Start()
        {
            //levelManager = FindObjectOfType<LevelManager>();
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            iDashCount = 0;
            iDashedCount = 0;
            bAirDashed = false;
            bAirJump = false;
            bCompleteLevel = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);
            endCounter = 0;
            playCutscene = false;
            end = false;
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
            //if (collision.gameObject.tag.Equals("Void") || collision.gameObject.tag.Equals("Projectile")) {
              //  Application.LoadLevel(Application.loadedLevel);

                //levelManager.GetComponent<GameManager>().RespawnPlayer();
            //}

            if (collision.gameObject.tag.Equals("Pickup")) {
                collision.gameObject.GetComponent<BoxCollider>().enabled = false;
                collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
                collision.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
                bAirJump = true;
                if (bAirDashed) {
                    bAirDashed = false;
                    iDashedCount = 0;
                }
                PlayItemGet(collision.gameObject);
            }


            //when hitting special pickups it adds to the punch cards int.
            //FirstPersonController is called in GameManager where if punchCards == 1 Wall 1 is set to false.
            if (collision.gameObject.tag.Equals("SpecialPickup")) {
                Debug.Log("punch ard obtained");
                collision.gameObject.SetActive(false);
                punchCards++;
                Debug.Log(punchCards);

                if (collision.gameObject.tag.Equals("WinWall"))
                {
                    SceneManager.LoadScene(sceneBuildIndex: 2);
                }


               /* bCompleteLevel = true;
                collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
                collision.gameObject.GetComponent<MeshCollider>().enabled = false;
                collision.GetComponent<AudioSource>().Play();
                */
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

            //if (collision.gameObject.tag.Equals("Checkpoints"))
           // {
               // levelManager.GetComponent<LevelManager>().currentCheckpoint = collision.gameObject;
           // }
        }

        // Update is called once per frame
        private void Update()
        {
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

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            if (Input.GetKey(DashKey) && !bAirDashed) {
                m_Dash = true;
            }

           /* if (Input.GetKey(KeyCode.Escape)) {
                SceneManager.LoadScene(sceneBuildIndex: 0);
            }
            */

            m_PreviouslyGrounded = m_CharacterController.isGrounded;

            //temporary code to restart level for testing purposes
            ResetLevel();
        }


        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;

            if (CheckWallTouch()){
                m_GravityMultiplier = 1.0f;
                m_WalkSpeed = 20.0f;
               // bAirDashed = false;
                
            }
            else {
                m_GravityMultiplier = 2;
                m_WalkSpeed = 8.0f;
            }

            //check to make sure speed is increasing on walls
            //Debug.Log(m_WalkSpeed);

            if (m_Dash && iDashedCount == 0) {
                if (!m_CharacterController.isGrounded) {
                    bAirDashed = true;
                }
                m_MoveDir.x = desiredMove.x * 20;
                m_MoveDir.z = desiredMove.z * 20;
                iDashCount += 1;
            }

            if (m_Dash && iDashCount > 20) {
                m_Dash = false;
                iDashCount = 0;
                iDashedCount = 1;
            }

            if (iDashedCount > 0) {
                if (iDashedCount > 15 && !m_Jumping) {
                    iDashedCount = -1;
                }
                iDashedCount += 1;
            }

            if (m_CharacterController.isGrounded)
            {
                bAirJump = false;
                bAirDashed = false;
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {

                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }

            }
            else
            {
                if (m_Jump)
                {                        
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    if (bAirJump)
                    {
                       bAirJump = false;
                    }
                }
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }

        private bool CheckWallTouch()
        {
            if (Physics.Raycast(transform.position, transform.right, out rHitR, 1))
            {
                if (rHitR.transform.tag == "Wall")
                {
                    bIsWallR = true;
                    bIsWallL = false;
                    bAirJump = true;
                    //bAirDashed = false;
                    return true;
                }
            }

            if (Physics.Raycast(transform.position, -transform.right, out rHitL, 1))
            {
                if (rHitL.transform.tag == "Wall")
                {
                    bIsWallL = true;
                    bIsWallR = false;
                    bAirJump = true;
                    //bAirDashed = false;
                    return true;
                }
            }

            return false;
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }

        private void PlayItemGet(GameObject gObject)
        {
            gObject.GetComponent<AudioSource>().Play();
        }

   
        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }
            

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }
        
        


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded || m_Dash)
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
            if (!m_UseHeadBob || m_Dash)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
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


        private void ResetLevel()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

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
            m_MouseLook.LookRotation (transform, m_Camera.transform);
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
