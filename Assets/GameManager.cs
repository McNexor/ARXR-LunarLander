using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private PhysicsSystem _physicsSystem;
    private TMP_Text _fuelText;
    private TMP_Text _speedText;
    private TMP_Text _velocityText;
    private TMP_Text _maxSpeedText;
    private TMP_Text _angleText;
    private TMP_Text _maxAngleText;
    private GameObject _endText;
    private GameObject lander;
    public Sprite _landerCrash;

    // Start is called before the first frame update
    void Start()
    {
        lander = GameObject.Find("Lander");
        GameObject platform = GameObject.Find("Platform");

        _physicsSystem = lander.GetComponent<PhysicsSystem>();
        _fuelText = GameObject.Find("Fuel").GetComponent<TMP_Text>();
        _speedText = GameObject.Find("Speed").GetComponent<TMP_Text>();
        _velocityText = GameObject.Find("Velocity").GetComponent<TMP_Text>();
        _maxSpeedText = GameObject.Find("MaxSpeed").GetComponent<TMP_Text>();
        _angleText = GameObject.Find("Angle").GetComponent<TMP_Text>();
        _maxAngleText = GameObject.Find("MaxAngle").GetComponent<TMP_Text>();

        lander.transform.position = new(Random.Range(-7.5f, 7.5f), 4f);
        platform.transform.position = new(Random.Range(-7f, 7f), -4f);

        _endText = GameObject.Find("End");
        _endText.SetActive(false);
    }

    void Update()
    {
        _fuelText.text = "Fuel = " + _physicsSystem._fuel;
        Vector2 velocity = _physicsSystem._velocity;
        _velocityText.text = "Velocity = " + velocity;
        _speedText.text = "Speed = " + velocity.magnitude;
        _maxSpeedText.text = "MaxSpeed = " + Parameters.MaxLandingSpeed;
        float angle = lander.transform.localEulerAngles.z;
        if (angle < 0.01f) angle = 0f;
        if (angle > 180f) angle = Mathf.Abs(angle - 360f);
        _angleText.text = "Angle = " + angle;
        _maxAngleText.text = "MaxAngle = " + Parameters.MaxLandingAngle;
    }

    public void GameOver()
    {
        _physicsSystem.StopSimulation();
        _endText.GetComponent<TMP_Text>().text = "You Lost!";
        _endText.SetActive(true);
        lander.GetComponent<SpriteRenderer>().sprite = _landerCrash;
        Invoke(nameof(LoadMenu), 3f);
    }
    public void GameWon()
    {
        _physicsSystem.StopSimulation();
        _endText.GetComponent<TMP_Text>().text = "You Won!";
        _endText.SetActive(true);
        Invoke(nameof(LoadMenu), 3f);
    }

    public void HitPlatform()
    {
        float angle = lander.transform.localEulerAngles.z;
        if (angle > 180f) angle = Mathf.Abs(angle - 360f);
        if (_physicsSystem._velocity.magnitude > Parameters.MaxLandingSpeed || angle > Parameters.MaxLandingAngle) GameOver();
        else GameWon();
    }

    void LoadMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
