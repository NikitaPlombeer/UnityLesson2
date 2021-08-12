using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float Speed;
    public float RotationSpeed;
    public Text ScoreText;
    
    public float CameraMinAngle;
    public float CameraMaxAngle;
    
    private int _coins;
    private float _cameraXAngle;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        DisplayCoinsResult();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.LeftShift)) v *= 3;
        
        Vector3 direction = new Vector3(h, 0, v);
        transform.Translate((Speed * Time.deltaTime) * direction);
    }

    private void Rotate()
    {
        float yRotation = FixBacklash(Input.GetAxis("Mouse X"), 0.1f);
        float xRotation = FixBacklash(Input.GetAxis("Mouse Y"), 0.1f);

        transform.Rotate(0, yRotation * RotationSpeed * Time.deltaTime, 0);

        _cameraXAngle -= xRotation * RotationSpeed * Time.deltaTime;
        _cameraXAngle = Mathf.Clamp(_cameraXAngle, CameraMinAngle, CameraMaxAngle);
        Camera.main.transform.localRotation = Quaternion.Euler(_cameraXAngle, 0f, 0f);
    }

    private float FixBacklash(float value, float accuracy)
    {
        if (value < accuracy && value > -accuracy) return 0f;
        return value;
    }

    public void CoinCollected()
    {
        _coins++;
        DisplayCoinsResult();
    }

    private void DisplayCoinsResult()
    {
        if (_coins == LabyrinthCreator.TotalCoins)
        {
            ScoreText.text = "The End";
        }
        else
        {
            ScoreText.text = _coins + "/" + LabyrinthCreator.TotalCoins;
        }
    }
}
