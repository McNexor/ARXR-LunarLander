using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LevelEasy()
    {
        Parameters.Gravity = 9.81f;
        Parameters.Thrust = 25f;
        Parameters.Fuel = 200f;
        Parameters.FuelCost = 10f;
        Parameters.Mass = 10f;
        Parameters.MaxLandingAngle = 15f;
        Parameters.MaxLandingSpeed = 5f;
        PlayGame();
    }

    public void LevelMedium()
    {
        Parameters.Gravity = 9.81f;
        Parameters.Thrust = 25f;
        Parameters.Fuel = 120f;
        Parameters.FuelCost = 15f;
        Parameters.Mass = 10f;
        Parameters.MaxLandingAngle = 10f;
        Parameters.MaxLandingSpeed = 2f;
        PlayGame();
    }

    public void LevelHard()
    {
        Parameters.Gravity = 9.81f;
        Parameters.Thrust = 25f;
        Parameters.Fuel = 100f;
        Parameters.FuelCost = 25f;
        Parameters.Mass = 10f;
        Parameters.MaxLandingAngle = 5f;
        Parameters.MaxLandingSpeed = 1f;
        PlayGame();
    }

    public void LevelExtreme()
    {
        Parameters.Gravity = 50f;
        Parameters.Thrust = 125f;
        Parameters.Fuel = 100f;
        Parameters.FuelCost = 15f;
        Parameters.Mass = 10f;
        Parameters.MaxLandingAngle = 5f;
        Parameters.MaxLandingSpeed = 1f;
        PlayGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
