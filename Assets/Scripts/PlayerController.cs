using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0;
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private bool gameOver;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        gameOver = false;
    }

    private void FixedUpdate()
    {
        if (!gameOver)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            rb.AddForce(movement * speed);
        }
    }

    private void Update()
    {
        if (gameOver)
        {
            if (count < 8)
            {
                loseTextObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("space");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
            gameOver = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

        switch (other.gameObject.tag)
        {
            case "NPortal":
                this.transform.position = new Vector3(0f, 0.5f, -9.15f);
                break;
            case "SPortal":
                this.transform.position = new Vector3(0f, 0.5f, 9.15f);
                break;
            case "EPortal":
                this.transform.position = new Vector3(-9.15f, 0.5f, 0f);
                break;
            case "WPortal":
                this.transform.position = new Vector3(9.15f, 0.5f, 0f);
                break;
            case "Enemy":
                gameOver = true;
                break;
        }
    }

}
