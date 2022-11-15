using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class IG1EnemyController : AgentController
{
    float _TimeBeforeNextPatrol;
    Vector3 newDirection;
    bool _IsOnPatrol = false;
    float _DistanceToEndPatrol = 0f;
    int _CountTimeStop = 0;
    int _TimeStopBeforeGo = 3;

    [SerializeField] Animator mouton;
    float _DistanceFromTarget;
    float _Precision;
    Vector3 _LastStimulisPosition;
    Vector3 _LastPosition = Vector3.zero;
    Vector3 _InitialPosition;

    Transform _Player;

    public override void Start()
    {
        base.Start();
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<AISenseSight>().AddSenseHandler(new AISense<SightStimulus>.SenseEventHandler(HandleSight));
        GetComponent<AISenseSight>().AddObjectToTrack(_Player);
        GetComponent<AISenseHearing>().AddSenseHandler(new AISense<HearingStimulus>.SenseEventHandler(HandleHearing));
        GetComponent<AISenseHearing>().AddObjectToTrack(_Player);
        _LastPosition = transform.position;
        _LastStimulisPosition = transform.position;
        newDirection = transform.position;
        _InitialPosition = transform.position;

        _Precision = 1f;
        _HasATarget = false;

        _TimeBeforeNextPatrol = Random.Range(1, 3);
    }

    private void FixedUpdate()
    {
        _Speed = Vector3.Distance(_LastPosition, transform.position) / Time.deltaTime;
        _DistanceFromTarget = Vector3.Distance(_LastStimulisPosition, transform.position);
        _DistanceToEndPatrol = Vector3.Distance(newDirection, transform.position);

        if (_DistanceToEndPatrol <= 1)
        {
            _IsOnPatrol = false;
            _CountTimeStop = 0;
        }
        else if (_Speed == 0 && _IsOnPatrol)
        {
            _CountTimeStop++;
        }


        if (!_IsOnPatrol && !_HasATarget)
        {
            _TimeBeforeNextPatrol -= Time.deltaTime;
        }


        if (_DistanceFromTarget < _Precision)
        {
            _HasATarget = false;
            mouton.SetTrigger("Idle");
            mouton.ResetTrigger("Run");
            mouton.ResetTrigger("Walk");
            _CountTimeStop = 0;
        }
        else if (_Speed == 0 && _HasATarget)
        {
            _CountTimeStop++;
        }


        if (_CountTimeStop >= _TimeStopBeforeGo * 50)
        {
            _HasATarget = false;
            _IsOnPatrol = false;
            _CountTimeStop = 0;
        }


        if (!_HasATarget && _TimeBeforeNextPatrol <= 0)
        {
            Patrol();
        }

        _LastPosition = transform.position;
    }

    void HandleSight(SightStimulus sti, AISense<SightStimulus>.Status evt)
    {
        if (sti.position == Vector3.zero) { return; }

        if (evt == AISense<SightStimulus>.Status.Enter)
        {
            Debug.Log("Objet " + evt + " vue en " + sti.position);
            _IsOnPatrol = false;
            _CountTimeStop = 0;
            _HasATarget = true;
            mouton.SetTrigger("Run");
            mouton.ResetTrigger("Walk");
            mouton.ResetTrigger("Idle");
        }

        if ((sti.position - transform.position).sqrMagnitude < 3 * 3)
        {
            FirstPersonController.lastCheckPointPos = new Vector3(-35, 6, -40);
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        _LastStimulisPosition = sti.position;

        FindPathTo(sti.position);

    }

    void HandleHearing(HearingStimulus sti, AISense<HearingStimulus>.Status evt)
    {
        if (sti.position == Vector3.zero) { return; }

        if (evt == AISense<HearingStimulus>.Status.Enter)
        {
            Debug.Log("Objet " + evt + " ouïe en " + sti.position);
            _IsOnPatrol = false;
            _CountTimeStop = 0;
            _HasATarget = true;
        }

        _LastStimulisPosition = sti.position;

        FindPathTo(sti.position);
    }

    void Patrol()
    {
        _IsOnPatrol = true;
        mouton.SetTrigger("Walk");
        mouton.ResetTrigger("Run");
        mouton.ResetTrigger("Idle");
        _TimeBeforeNextPatrol = Random.Range(3, 8);

        newDirection = new Vector3(Random.Range(-35, 126), transform.position.y, Random.Range(-50, 107));

        _DistanceToEndPatrol = Vector3.Distance(newDirection, transform.position);

        if (_DistanceToEndPatrol < 15 || _DistanceToEndPatrol > 25)
        {
            if (transform.position.x > -35 && transform.position.x < 126 && transform.position.z > -50 && transform.position.z < 107)
            {
                Patrol();
                return;
            }
            newDirection = _InitialPosition;
        }

        FindPathTo(newDirection);
    }
}