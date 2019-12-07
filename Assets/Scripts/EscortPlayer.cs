using UnityEngine;

public class EscortPlayer : MonoBehaviour
{
    [SerializeField] private GameObject head;
    //should make it faster to find the player instead of checking by tag
    [SerializeField] private GameObject player;
    [SerializeField] private float maxDistanceToTarget = 1.0f;
    private Vector3 lastPosition;
    private Vector3 currentTarget;
    private bool _isEscorting = false;
    [SerializeField] private GameObject[] targets;
    private int _targetIndex;
    private void Awake()
    {
        lastPosition = this.transform.position;
        if (player is null)
        {
            GameObject[] temp;
            temp = GameObject.FindGameObjectsWithTag("Player");
            if (temp is null)
            {
                Debug.LogErrorFormat("There is no game object with the \"Player\" Tag, EscortPlayer script will not work!");
                return;
            }

            if (temp.Length <= 2)
            {
                //We are always using the first player tagged object we find, even if more are present.
                //The VR rig has 2 and the FPSController has 1. Luckily the FindGameObjectsWithTag only searches active objects.
                player = temp[0];
            }
            else
            {
                Debug.LogWarningFormat($"Found {temp.Length} player tagged objects, expected 1 or 2. Attempting to use first object found");
            }
        }
    }
    
    
    private bool FindPlayer()
    {
        //Should we do this correctly and do raycasting from Head object to see if we can see the player.
        //Or simply use the player as long as we haven't finished escorting the player to the correct room?
        return false;
    }
    
    //This needs to be refactored! But KISS for prototyping..
    private void Update()
    {
        float dist;
        //if not escorting player, then find it
        if (!_isEscorting)
        {
            if (!FindPlayer())
            {
                //player not found, continuing to target.
                //This is probably an edge case not needed for our initial solution,
                //since we want the avatar to escort the player to the final destination.
                //but leaving it in for now.
                dist = Vector3.Distance(this.transform.position, currentTarget);
                if (dist < maxDistanceToTarget)
                {
                    _targetIndex = _targetIndex + 1 % targets.Length;
                    currentTarget = targets[_targetIndex].transform.position;
                }
            }
            else
            {
                //player found, lets set last position to player and escort to the target meeting room.
                _isEscorting = true;
                lastPosition = player.transform.position;
            }
        }
        
        //Need to ensure we are moving towards player, should the distance be too large.
        //if escorting, but player is too far away, move towards last known position.
        dist = Vector3.Distance(this.transform.position, lastPosition);
        if (dist > maxDistanceToTarget)
        {
            //move towards last known position;
            currentTarget = lastPosition;
        }
        else
        {
            //if escorting, and player is close enough, continue towards target
            currentTarget = targets[_targetIndex].transform.position;
        }
        
        
        
        
    }

    
}