using UnityEngine;
using UnityEngine.Animations;

public class LookConstraint : MonoBehaviour
{
    [SerializeField] private LookAtConstraint _lookAtConstraint;
    private string _playerTag = "Player";
    private void Awake()
    {
        ConstraintSource constraintSource = new ConstraintSource();
        var player = GameObject.FindGameObjectWithTag(_playerTag).transform;
        if (player != null)
        {
            constraintSource.sourceTransform = player;
            _lookAtConstraint.AddSource(constraintSource);
        }
    }
}
