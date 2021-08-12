using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCreator : MonoBehaviour
{

    private static int _totalCoins;
    public static int TotalCoins => _totalCoins;
    
    [SerializeField] private GameObject wallPart;
    [SerializeField] private GameObject coin;
    [SerializeField] private Texture2D labyrinth;
    [SerializeField] private Transform ground;

    private float _wallScale;


    // Start is called before the first frame update
    void Awake()
    {
        _wallScale = wallPart.transform.localScale.x;
        ground.localScale = new Vector3(labyrinth.width * _wallScale * 1.5f, 0.5f, labyrinth.height * _wallScale * 1.5f);

        for (int i = 0; i < labyrinth.width; i++)
        {
            for (int j = 0; j < labyrinth.height; j++)
            {
                Color pixel = labyrinth.GetPixel(i, j);
                if (pixel == Color.black)
                {
                    CreateWallPart(i, j);
                } else if (pixel == Color.red)
                {
                    MovePlayer(i, j);
                } else if (pixel == Color.blue)
                {
                    CreateCoin(i, j);
                }
            }
        }
    }

    private void CreateWallPart(int x, int z)
    {
        Instantiate(wallPart, TexturePositionToWorldSpace(x, z), Quaternion.identity);
    }

    private Vector3 TexturePositionToWorldSpace(int x, int z)
    {
        float startPosX = -(labyrinth.width / 2) * _wallScale;
        float startPosY = -(labyrinth.height / 2) * _wallScale;
        return new Vector3(startPosX + x * _wallScale, 1, startPosY + z * _wallScale);
    }
    
    private void MovePlayer(int x, int z)
    {
        Transform playerTransform = FindObjectOfType<PlayerController>().transform;
        playerTransform.position = TexturePositionToWorldSpace(x, z);
    }

    private void CreateCoin(int x, int z)
    {
        Instantiate(coin, TexturePositionToWorldSpace(x, z), Quaternion.Euler(90, 0, 0));
        _totalCoins++;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
