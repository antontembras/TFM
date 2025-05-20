using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public GameStatus gamesStatus = null;
    public Animator m_Animator;
    public Transform weaponSlot;
    public GameObject weapon1, weapon2, weapon3;


    public AudioClip m_fireSound, m_hurtSound, m_deathSound;
    private AudioSource audioSource;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode changeWeapon = KeyCode.Q;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [HideInInspector] public float actualTimeBetweenAttacks = 0;
    public float timeBetweenAttacks;


    [HideInInspector] float timeStartHurt = 0;

    [HideInInspector] private bool isDying = false;
    [HideInInspector] float timeStartDying = 0;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        audioSource = GetComponent<AudioSource>();
        readyToJump = true;
    }

    private void Update()
    {

        if (timeStartHurt > 0)
        {
            timeStartHurt -= Time.deltaTime;

            if (timeStartHurt <= 1.0f)
            {
                m_Animator.SetBool("isHurt", false);
            }
        }


        if (isDying)
        {
            timeStartDying -= Time.deltaTime;

            if (timeStartDying <= 0)
            {

                SceneManager.LoadScene("GameOver");
            }
        }
        else {

            // ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            if (actualTimeBetweenAttacks <= timeBetweenAttacks)
            {
                actualTimeBetweenAttacks += Time.deltaTime;
                if (actualTimeBetweenAttacks > timeBetweenAttacks)
                {
                    if (gamesStatus.weaponEquipped == 1)
                    {
                        m_Animator.SetBool("isSwordAttack", false);
                    }
                    else if (gamesStatus.weaponEquipped == 2)
                    {
                        m_Animator.SetBool("isShooting", false);
                    }
                }
            }


            MyInput();
            SpeedControl();

            // handle drag
            if (grounded)
            {
                rb.drag = groundDrag;
            }
            else
            {
                rb.drag = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void SetWeapon()
    {
        if (gamesStatus.weaponEquipped == 2)
            {
                if (gamesStatus.hasBullets && gamesStatus.hasRevolver)
                {
                    GameObject newWeapon = (GameObject)Instantiate(weapon2);
                    newWeapon.transform.parent = weaponSlot;
                    newWeapon.transform.localPosition = new Vector3(-0.024f, -0.048f, 0.005f);
                    newWeapon.transform.localRotation = Quaternion.Euler(-71.107f, 156.841f, -65.671f);
                }
            }
            else
            {
                if (gamesStatus.hasMoonSword)
                {
                    GameObject newWeapon = (GameObject)Instantiate(weapon3);
                    newWeapon.transform.parent = weaponSlot;
                    newWeapon.transform.localPosition = new Vector3(0.056f, -0.054f, -0.002f);
                    newWeapon.transform.localRotation = Quaternion.Euler(-83.33f, 113.223f, -19.509f);
                }
                else
                {
                    GameObject newWeapon = (GameObject)Instantiate(weapon1);
                    newWeapon.transform.parent = weaponSlot;
                    newWeapon.transform.localPosition = new Vector3(-0.139f, -0.048f, 0.019f);
                    newWeapon.transform.localRotation = Quaternion.Euler(-2.834f, 91.083f, 72.126f);
                }
            }
        m_Animator.SetFloat("weaponEquipped", gamesStatus.weaponEquipped);
    }


    private void MyInput()
    {

        if (Time.timeScale == 0)
        {
            return;
        }
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKeyDown(changeWeapon))
        {
            if (gamesStatus.weaponEquipped == 1)
            {
                if (gamesStatus.hasBullets && gamesStatus.hasRevolver)
                {
                    gamesStatus.weaponEquipped = 2;
                }
            }
            else
            {
                gamesStatus.weaponEquipped = 1;
            }
            Destroy(weaponSlot.GetChild(0).gameObject);
            SetWeapon();
        }
        if (actualTimeBetweenAttacks > timeBetweenAttacks)
        {
            if (Input.GetMouseButtonDown(0))
            {
                actualTimeBetweenAttacks = 0;
                if (gamesStatus.weaponEquipped == 1)
                {
                    m_Animator.SetBool("isSwordAttack", true);
                }
                else if (gamesStatus.weaponEquipped == 2)
                {
                    m_Animator.SetBool("isShooting", true);

                    audioSource.clip = m_fireSound;
                    audioSource.Play();
                    actualTimeBetweenAttacks = 0;

                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
                    {
                        if (hit.collider.tag == "Enemy")
                        {
                            enemyAI eai = hit.collider.gameObject.GetComponent<enemyAI>();
                            eai.HitGun();
                            eai.currentState.Impact();
                        }
                    }
                }
            }
        }
    }

    public void NewWeapon()
    {
        Destroy(weaponSlot.GetChild(0).gameObject);
        SetWeapon();
    }
    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


        if ((verticalInput != 0 || horizontalInput != 0) && grounded)
        {
            m_Animator.SetFloat("Forward", moveSpeed);
        }
        else
        {
            m_Animator.SetFloat("Forward", 0);
        }


        m_Animator.SetBool("OnGround", grounded);
        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (!gamesStatus.hasJumpBoots)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(transform.up * jumpForce * 1.5f, ForceMode.Impulse);
        }

    }
    private void ResetJump()
    {
        readyToJump = true;
    }


    public void Hit(float damage)
    {
        if (timeStartHurt <= 0 && !isDying)
        {
            m_Animator.SetBool("isHurt", true);
            timeStartHurt = 1.5f;
            gamesStatus.playerLife -= damage;

            audioSource.clip = m_hurtSound;
            audioSource.Play();

            if (gamesStatus.playerLife <= 0)
            {
                gamesStatus.playerLife = 0;
                Die();
            }
            else
            {
                //Instantiate(bloodPrefab, transform.position + new Vector3(0, 2.5f, 0), transform.rotation);
            }
        }
    }

    public void Die()
    {
        isDying = true;
        timeStartDying = 2.0f;
        m_Animator.SetBool("isDead", true);        
        audioSource.clip = m_deathSound;
        audioSource.Play();
        Destroy(gameObject.GetComponent<BoxCollider>());
       //Destroy(gameObject.GetComponent<ThirdPersonUserControl>());
       //Destroy(gameObject.GetComponent<ThirdPersonCharacter>());
        Destroy(gameObject.GetComponent<Rigidbody>());
    }

}