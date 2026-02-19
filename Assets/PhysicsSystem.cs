using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;



public class PhysicsSystem : MonoBehaviour
{
    private bool _simulate = false;

    private float _lastTickDelta = 0.0f;

    public float _fuel = 100f;
    public Vector2 _position;
    public Vector2 _velocity;
    public Vector2 _acceleration;

    public NativeHashMap<uint, Vector2> _forces;

    private bool _thrusting = false;
    const int TILT_LEFT = -1, NO_TILT = 0, TILT_RIGHT = 1;
    private float _tilting = NO_TILT;


    // Start is called before the first frame update
    void Start()
    {

        _position = new();
        _velocity = new();
        _acceleration = new();

        _fuel = Parameters.Fuel;

        _forces = new NativeHashMap<uint, Vector2>(10, Allocator.Persistent);
        _forces.Add(0, new Vector2(0.0f, -Parameters.Gravity));

        _simulate = true;
    }

    // Update is called once per frame
    void Update()
    {
        _lastTickDelta += Time.deltaTime;

        if (_lastTickDelta < 0.033f) return;

        _position = transform.position;

        for (; _lastTickDelta > 0.033f;)
        {

            if (_simulate) Tick();
            _lastTickDelta -= 0.033f;

        }

        ApplyPosition();

        _lastTickDelta = 0.0f;
    }

    void Tick()
    {

        if (_tilting != 0)
        {
            transform.Rotate(new(0.0f, 0.0f, 0.033f * 90f * _tilting));
            if (_thrusting)
            {
                StopThrust();
                Thrust();
            }
        }

        if (_thrusting) _fuel = Mathf.Max(_fuel - Parameters.FuelCost * 0.033f, 0f);

        Vector2 currentForce = new();

        var kvArray = _forces.GetKeyValueArrays(Allocator.Temp);
        for (int i = 0; i < kvArray.Length; i++)
        {
            currentForce += kvArray.Values[i];
        }
        kvArray.Dispose();

        _acceleration = currentForce / Parameters.Mass;
        _velocity += 0.033f * _acceleration;
        _position += _velocity * 0.033f;
    }

    void ApplyPosition()
    {
        transform.position = (Vector3)_position;
    }

    public void AddForce(uint id, Vector2 force)
    {
        _forces.Add(id, force);
    }

    public void RemoveForce(uint id)
    {
        _forces.Remove(id);
    }

    public void Thrust()
    {
        if (!_thrusting && _fuel > 0f) AddForce(1, transform.rotation * new Vector3(0f, Parameters.Thrust, 0f));
        _thrusting = true;
    }

    public void StopThrust()
    {
        if (_thrusting) RemoveForce(1);
        _thrusting = false;
        
    }

    public void Tilt(float tiltDirection)
    {
        _tilting = tiltDirection;
    }

    public void StartSimulation()
    {
        _simulate = true;
    }

    public void StopSimulation()
    {
        _simulate = false;
    }

    void OnDestroy()
    {
        if (_forces.IsCreated)
        {
            _forces.Dispose();
        }
    }

}


