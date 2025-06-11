using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AttackPatternManager : MonoBehaviour
{
    [SerializeField] private AttackPatternData[] _attackPatternData;
    [SerializeField] private Concrete_ANIM[] _concreteAnims;
    [SerializeField] private UnityEvent _OnFightStart;
    [SerializeField] private float startDelay = 0f;
    public void StartFight()
    {
        ProcessCoroutine();
    }

    private Coroutine _currentCoroutine;
    private void ProcessCoroutine()
    {
        _currentCoroutine = StartCoroutine(ProcessPatterns());
    }

    public void Break()
    {
        if(_currentCoroutine != null)
        StopCoroutine(_currentCoroutine);
    }

    private IEnumerator ProcessPatterns()
    {
        _OnFightStart.Invoke();
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            if (_attackPatternData.Length == 0) break;
            foreach (var patternData in _attackPatternData)
            {
                for (int i = 0; i < patternData.amount; i++)
                {
                    var AttackInterface = patternData._attack;

                    bool eventInvoked = false;
                    UnityAction onAttackEndCallback = () => eventInvoked = true;
                    AttackInterface.OnAttackEnd.AddListener(onAttackEndCallback);
                    AttackInterface.Attack(patternData._speed, patternData._cooldownPerAttack, patternData._position);
                    yield return new WaitUntil(() => eventInvoked);
                    AttackInterface.OnAttackEnd.RemoveListener(onAttackEndCallback);
                }
                patternData.OnEnd.Invoke();
                yield return new WaitForSeconds(patternData._cooldownToNextAttack);
            }
        }
    }
}