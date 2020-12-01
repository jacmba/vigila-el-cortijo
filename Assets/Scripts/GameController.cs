using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;

    public GameObject car;

    public CameraController camera;

    public float MaxTimer = 3f;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        EventManager.carEnter += OnCarEnter;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    public void DoAction(string action)
    {
        if (timer > 0)
        {
            return;
        }
        timer = MaxTimer;
        switch (action)
        {
            case "ENTER_CAR":
                player.SetActive(false);
                car.GetComponent<CarController>().enabled = true;
                camera.target = car;
                break;
            case "EXIT_CAR":
                Transform carEntry = car.transform.Find("Entry");
                player.SetActive(true);
                player.transform.position = carEntry.position;
                car.GetComponent<CarController>().enabled = false;
                camera.target = player;
                break;
            default:
                break;
        }
    }

    void OnCarEnter()
    {
        car.GetComponent<CarController>().enabled = true;
        camera.target = car;
    }
}
