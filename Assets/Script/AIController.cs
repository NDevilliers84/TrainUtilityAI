using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {
    
    [Header("Parameters")]
    [SerializeField] private List<UtilityAction> _actions;
    [SerializeField] private float _paramSpeed;
    [SerializeField] private float _paramDistance;
    [SerializeField] private bool _overrideContext;
    [SerializeField] private Transform _bedTransform;
    [SerializeField] private Transform _PCTransform;
    [SerializeField] private Transform _fridgeTransform;
    [SerializeField] private Transform _toiletTransform;
    
    [Header("Context Override")]
    [SerializeField, Range(0, 1)] private float _hunger;
    [SerializeField, Range(0, 1)] private float _fatigue;
    [SerializeField, Range(0, 1)] private float _boredom;
    [SerializeField, Range(0, 1)] private float _bladder;
    private NavMeshAgent Yvan;
    
    public Dictionary<string, float> Context = new();
    public Dictionary<string, Transform> Actions = new();
    
    private void Start() {
        Context.Add("Fatigue", 0);
        Context.Add("Hunger", 0);
        Context.Add("Boredom", 0);
        Context.Add("Bladder", 0);
        Context.Add("DistanceToFridge", 0);
        Context.Add("DistanceToToilet", 0);
        Context.Add("DistanceToBed", 0);
        Context.Add("DistanceToPC", 0);
        Actions.Add("GoSleep", _bedTransform);
        Actions.Add("GoEat", _fridgeTransform);
        Actions.Add("GoGame", _PCTransform);
        Actions.Add("GoWC", _toiletTransform);
    }

    private void Update() {
        if (_overrideContext) {
            Context["Fatigue"] = _fatigue;
            Context["Hunger"] = _hunger;
            Context["Boredom"] = _boredom;
            Context["Bladder"] = _bladder;
        }
        else {
            Context["Fatigue"] = Mathf.Clamp01(Context["Fatigue"] + Time.deltaTime * _paramSpeed);
            Context["Hunger"] = Mathf.Clamp01(Context["Hunger"] + Time.deltaTime * _paramSpeed);
            Context["Boredom"] = Mathf.Clamp01(Context["Boredom"] + Time.deltaTime * _paramSpeed);
            Context["Bladder"] = Mathf.Clamp01(Context["Bladder"] + Time.deltaTime * _paramSpeed);
        }
        Context["DistanceToFridge"] = Mathf.Clamp01(Vector3.Distance(transform.position, _fridgeTransform.position) / _paramDistance);
        Context["DistanceToToilet"] = Mathf.Clamp01(Vector3.Distance(transform.position, _toiletTransform.position) / _paramDistance);
        Context["DistanceToBed"] = Mathf.Clamp01(Vector3.Distance(transform.position, _bedTransform.position) / _paramDistance);
        Context["DistanceToPC"] = Mathf.Clamp01(Vector3.Distance(transform.position, _PCTransform.position) / _paramDistance);

        var bestAction = ChooseAction();
        Debug.Log(bestAction.Name);
        Move(bestAction);
    }

    private UtilityAction ChooseAction() {
        UtilityAction bestAction = null;
        float bestScore = -1f;
        foreach (UtilityAction utilityAction in _actions) {
            float score = utilityAction.Evaluate(Context);

            if (score > bestScore) {
                bestScore = score;
                bestAction = utilityAction;
            }
        }

        return bestAction;
    }

    private void Move(UtilityAction action)
    {
        if(Actions.TryGetValue(action.Name, out Transform target))
        {
            Yvan.SetDestination(target.position);
        }
    }
}