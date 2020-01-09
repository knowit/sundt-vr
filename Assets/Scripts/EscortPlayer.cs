using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.PlayerLoop;
using Debug = UnityEngine.Debug;

public class EscortPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;
    [SerializeField] private Transform player;
    [SerializeField] private float maxPlayerDistance = 5.0f;
    private int _targetIndex = 0;
    private NavMeshAgent _nma;
    private NavMeshPath _path;
    private float prevDistance = 0;

    private bool goToPlayer = true;
    
    private void Awake()
    {
        _path = new NavMeshPath();
        
        _nma = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }
    
    
    
    private IEnumerator FindNewTarget()
    {
        _nma.isStopped = true;
        _targetIndex = (_targetIndex + 1) % targets.Length;    
        bool possible = _nma.CalculatePath(targets[_targetIndex].transform.position, _path);
   
        if (!possible)
        {
            Debug.LogError($"Impossible to reach index {_targetIndex}, name: {targets[_targetIndex].name}");
        }
        yield return new WaitForSeconds(2);
        
        _nma.SetPath(_path);
        _nma.isStopped = false;
    }


    private void Update()
    {
        //note, does not handle player moving towards target room faster than agent if exceeding speedup ratio
        if (!goToPlayer)
        {
            //possible states
            var targetDistance = Vector3.Distance(transform.position, targets[_targetIndex].transform.position);
            if (Math.Abs(targetDistance) < 0.5f)
            {
                Debug.LogError(
                    $"We have arrived at our destination, {targets[_targetIndex].name}, distance {targetDistance} ");
                //we have arrived
                //arrived at target room, should we disable this script? Will need to ensure we aren't blocking the entry to the room.
                _nma.isStopped = true;
                return;
            }

            var currDistance = Vector3.Distance(transform.position, player.transform.position);
            Debug.Log($"{currDistance} from player tagged object");
            if (currDistance <= maxPlayerDistance)
            {
                //ensure that we are moving, since player is within max distance
                _nma.isStopped = false;

                //moving towards room, player following
                var diff = prevDistance - currDistance;
                //change speed to match player
                _nma.speed += diff * 0.1f;

            }
            else
            {
                //player outside range, check if visible
                RaycastHit hit = new RaycastHit();
                var avatarPosition = gameObject.transform.position + Vector3.up;
                var canSeePlayer = Physics.Raycast(avatarPosition,
                    (player.transform.position + Vector3.up) - avatarPosition, out hit);
                if (canSeePlayer)
                {
                    //will wait a bit to see if player follows
                    _nma.isStopped = true;
                    //can play optional sound bite here
                }
                else
                {
                    Debug.LogWarning($"Can't see the player, setting it as new target");
                    //player has moved away, lets set the player as new target
                    _nma.SetDestination(player.transform.position);
                }

            }

            prevDistance = currDistance;
            //moving towards room, player stopped follow
            //player stopped following
            //player is visible, if within certain range, wait. play sound. 
            //player is visible, outside given range, move towards player
            //player is not visible, set current player position as new destination, move there as long as player is not moving more than x from that position
            //player is not visible, but moves outside certain range from current target, set current destination as new target
            //

            if (!_nma.hasPath && !_nma.isStopped)
            {
                //StartCoroutine(FindNewTarget());

                //if we have arrived at actual destination, disable script.
                //else we have arrived at player, set the original destination.
                Debug.LogError("We don't have a a path, but are stopped, disabling script!");
                var tempy = gameObject.GetComponent<EscortPlayer>();
                tempy.enabled = false;
            }

        }
        else
        {
            if (_nma.remainingDistance < 0.01f)
            {
                goToPlayer = false;
                Debug.Log("waiting for new target to take effect");
                StartCoroutine(FindNewTarget());
                
            }
        }
    }

    private void Start()
    {
        //move in the direction of the player, stop n units away from player 
        var position = player.position;
        var direction = Vector3.Normalize(position - transform.position);
        var nearPlayerTarget = position - (3*direction);
        nearPlayerTarget.y = transform.position.y;
        _nma.SetDestination(nearPlayerTarget);
        goToPlayer = true;
    }
}