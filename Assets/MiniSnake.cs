using UnityEngine;
using System.Collections.Generic;
using System.Collections;

enum Direction
{
    Up,
    Right,
    Down,
    Left
}

public class MiniSnake : MonoBehaviour
{
    Direction currentDirection = Direction.Right;
    public GameObject headPrefab;
    public GameObject tailPrefab;
    List<GameObject> tail = new List<GameObject>();
    public float baseSpeed = 5;
    public float speed = 5;
    GameManager gameManager;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameObject head = Instantiate(headPrefab, transform.position, Quaternion.identity);

        tail.Add(head);

        head.transform.parent = transform;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

        HandleRotationInput();
        MoveFoward();
        UpdateTailPositions();

        if (gameManager.gameHasEnded)
        {
            return;
        }

    }

    void RotateCounterClockwise()
    {
        currentDirection = (Direction)(((int)currentDirection + 3) % 4);
    }

    void RotateClockwise()
    {
        currentDirection = (Direction)(((int)currentDirection + 1) % 4);
    }

    void MoveFoward()
    {
        Vector3 moveDirection = Vector3.zero;
        switch (currentDirection)
        {
            case Direction.Up:
                moveDirection = Vector3.up;
                break;
            case Direction.Down:
                moveDirection = Vector3.down;
                break;
            case Direction.Left:
                moveDirection = Vector3.left;
                break;
            case Direction.Right:
                moveDirection = Vector3.right;
                break;
        }
        transform.position += moveDirection * speed * Time.deltaTime;
    }
      
    void HandleRotationInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            RotateCounterClockwise();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            RotateClockwise();
        }
       
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            float newSpeed = baseSpeed * 2;
            speed = newSpeed;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            speed = baseSpeed;
        }
    }

    public void AddTail()
    {
        Vector3 newPosition = Vector3.zero;
        switch (currentDirection)
        {
            case Direction.Up:
                newPosition = tail[tail.Count - 1].transform.position - new Vector3(0, 1, 0);
                break;
            case Direction.Down:
                newPosition = tail[tail.Count - 1].transform.position + new Vector3(0, 1, 0);
                break;
            case Direction.Left:
                newPosition = tail[tail.Count - 1].transform.position + new Vector3(1, 0, 0);
                break;
            case Direction.Right:
                newPosition = tail[tail.Count - 1].transform.position - new Vector3(1, 0, 0);
                break;
        }

        GameObject newTail = Instantiate(tailPrefab, newPosition, Quaternion.identity);
        tail.Add(newTail);
    }


    void UpdateTailPositions()
    {
        float segmentDistance = 1.2f;
        for (int i = 1; i < tail.Count; i++)
        {
            GameObject currentSegment = tail[i];
            GameObject segmentInFront = tail[i - 1];

            Vector3 newPosition = segmentInFront.transform.position -
                (segmentInFront.transform.position - currentSegment.transform.position).normalized *
                segmentDistance;

            currentSegment.transform.position = Vector3.Lerp(currentSegment.transform.position,
                newPosition, Time.deltaTime * 30);
        }
    }

}
