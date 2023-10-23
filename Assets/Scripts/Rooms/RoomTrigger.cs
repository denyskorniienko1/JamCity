using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private GameManagerM01 gameManager;
    public int roomId;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerM01>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.PlayerRoomId = roomId;
            Debug.Log("Set player roomId to " + roomId);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.PlayerRoomId = -1;
            Debug.Log("Set player roomId to " + -1);
        }
    }
}
