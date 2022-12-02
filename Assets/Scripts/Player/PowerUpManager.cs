using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    public static PowerUpManager instance;

    [SerializeField] private bool _dagger, _puloDuplo, _dash;

    public bool daggerBool
    {
        get
        {
            return _dagger;
        }

        set
        {
            _dagger = value;
        }
    }

    public bool puloDuploBool
    {
        get
        {
            return _puloDuplo;
        }

        set
        {
            _puloDuplo = value;
        }
    }

    public bool dashBool
    {
        get
        {
            return _dash;
        }

        set
        {
            _dash = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start ()
    {

	}
	

	void Update ()
    {
		
	}
}
