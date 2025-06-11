using UnityEngine;
using UnityEngine.Events;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private AttackPatternManager[] _attackPatternManagers;
    [SerializeField] private UnityEvent[] _onConcretePhaseEnd;
    [SerializeField] private UnityEvent[] _onConcretePhaseStart;
    [SerializeField] private StatsData _bossData;
    private float _phasesHpCounter;
    private float _maxHp;
    private int _currentPhase = 0;

    private void Awake()
    {
        _phasesHpCounter = _bossData.MaxHp / _attackPatternManagers.Length;
        _maxHp = _bossData.MaxHp;
    }

    public void StartFirstPhase()
    {
        _onConcretePhaseStart[_currentPhase].Invoke();
        _currentPhase++;
    }
    public void PhaseChecker(int currentHp, int maxHp)
    {
        if(currentHp <= 0) return;
        if (currentHp <= _maxHp - _phasesHpCounter * _currentPhase)
        {
            if (_currentPhase > 0)
            {
                _attackPatternManagers[_currentPhase].Break();
                _onConcretePhaseEnd[_currentPhase].Invoke();
            }
            _onConcretePhaseStart[_currentPhase].Invoke();
            _currentPhase++;
        }
    }
}
