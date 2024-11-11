using System.Collections.Generic;
using UnityEngine;

public class MapG : MonoBehaviour
{
    enum RoomType { Monster, Boss, Treasure, Event, Player }
    [SerializeField] int[] rooms;
    [SerializeField] int[] treasureRooms;
    [SerializeField] int[] eventRooms;
    [SerializeField] int[] playerRooms;

    [SerializeField] EnemyAI[] enemyTypes;
    [SerializeField] GameObject[] monsterRoom;//怪物房间
    [SerializeField] GameObject[] enemys;

    [SerializeField] GameObject[] bossTypes;
    [SerializeField] GameObject[] bossRoom;//Boss房间

    [SerializeField] GameObject[] loots;
    [SerializeField] GameObject[] treasureRoom;//宝藏房
    [SerializeField] GameObject[] eventRoom;//事件房
    [SerializeField] GameObject[] playerRoom;//玩家房


    private void Awake()
    {

    }
    public void init()
    {
        //  Debug.Log(GameManager.Instance.player.transform.position);
        //新建房间容器
        rooms = new int[25];
        treasureRooms = new int[] { 6, 7, 8, 11, 12, 13, 16, 17, 18 };
        eventRooms = new int[] { 0, 1, 2, 3, 4, 5, 9, 10, 14, 15, 19, 20, 21, 22, 23, 24 };
        playerRooms = new int[] { 0, 4, 2, 24 };

        ChooseRoom(rooms, RoomType.Boss);
        ChooseRoom(rooms, RoomType.Treasure);
        ChooseRoom(rooms, RoomType.Event);
        ChooseRoom(rooms, RoomType.Player);

        GenerateRoom(rooms);
    }
    private void GenerateRoom(int[] rooms)
    {

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //  TODO: enum RoomType{Monster 0,Boss 1,Treasure 2,Event 3,Player 4}
                switch (rooms[i * 5 + j])
                {
                    case 0:
                        var newmonsterRoom = Instantiate(monsterRoom[Random.Range(0, monsterRoom.Length)], new Vector2(i * 20, j * 15), Quaternion.identity, transform);

                        var spawnPoints = new List<Transform>(newmonsterRoom.transform.GetChild(0).GetComponentsInChildren<Transform>());
                        spawnPoints.RemoveAt(0);  // The parent is the first element

                        foreach (var point in spawnPoints)
                        {

                            GameObject newEnemy = Instantiate(enemys[Random.Range(0, enemyTypes.Length)], point.position, Quaternion.identity, newmonsterRoom.transform);

                        }
                        break;
                    case 1:
                        var newBoosRoom = Instantiate(bossRoom[UnityEngine.Random.Range(0, bossRoom.Length)], new Vector2(i * 20, j * 15), Quaternion.identity, transform);
                        Instantiate(bossTypes[Random.Range(0, bossTypes.Length)], newBoosRoom.transform.GetChild(0).position, Quaternion.identity, newBoosRoom.transform);
                        break;
                    case 2:
                        var newTreasureRoom = Instantiate(treasureRoom[UnityEngine.Random.Range(0, treasureRoom.Length)], new Vector2(i * 20, j * 15), Quaternion.identity, transform);
                        Instantiate(loots[Random.Range(0, loots.Length)], newTreasureRoom.transform.GetChild(0).position, Quaternion.identity, newTreasureRoom.transform);
                        break;
                    case 3:
                        Instantiate(eventRoom[UnityEngine.Random.Range(0, eventRoom.Length)], new Vector2(i * 20, j * 15), Quaternion.identity, transform);
                        break;
                    case 4:
                        var roomPos = new Vector2(i * 20, j * 15);
                        Instantiate(playerRoom[UnityEngine.Random.Range(0, playerRoom.Length)], roomPos, Quaternion.identity, transform);
                        GameManager.Instance.player.transform.position = roomPos;

                        break;
                }
            }

        }
    }

    private void ChooseRoom(int[] rooms, RoomType roomType)
    {
        bool notPlaced = true;

        // Helper function to place a room
        void PlaceRoom(int[] roomArray)
        {
            int attempts = 0; // To avoid infinite loops
            const int maxAttempts = 100; // Maximum attempts to find an empty room

            while (notPlaced && attempts < maxAttempts)
            {
                var index = roomArray[UnityEngine.Random.Range(0, roomArray.Length)];
                if (rooms[index] == 0)
                {
                    rooms[index] = (int)roomType;
                    notPlaced = false;
                }
                attempts++;
            }

            if (attempts >= maxAttempts)
            {
                Debug.LogWarning($"Failed to place {roomType} room after {maxAttempts} attempts.");
            }
        }

        switch (roomType)
        {
            case RoomType.Boss:
                rooms[8] = (int)RoomType.Boss;
                rooms[16] = (int)RoomType.Boss;
                break;
            case RoomType.Treasure:
                PlaceRoom(treasureRooms);
                break;
            case RoomType.Event:
                PlaceRoom(eventRooms);
                break;
            case RoomType.Player:
                PlaceRoom(playerRooms);
                break;
        }
    }
}