using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum ActivationType
{
    Collision = 0,
    Key = 1,
    Start = 2,
    CollisionAndKey = 3
}

public enum ValidationEnum {/* Score,*/ /*SceneName,*/ PlayerPrefs,/* Level, Question*/ }
public enum NumericValidationEnum { Equal, Greater, Less, LessEqual, GreaterEqual }
public class HandleObjectsWithAction : MonoBehaviour
{
    [SerializeField] ActivationType activationType;

    [SerializeField] UnityEvent eventosIniciales;
    [SerializeField] UnityEvent eventosFinales;
    [SerializeField] UnityEvent eventosNoCumplidasValidaciones;

    [SerializeField] KeyCode key;

    [SerializeField] List<string> tags = new List<string>();

    [SerializeField] float delay;

    [SerializeField] bool trigger = false;
    bool activated = false;
    bool targetInside = false;

    [SerializeField] List<Validation> validations = new List<Validation>();

    public List<Validation> Validations { get => validations; set => validations = value; }

    // Start is called before the first frame update
    void Start()
    {
        eventosIniciales?.Invoke();

        if (activationType == ActivationType.Start)
            ActivateDelay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (activationType == ActivationType.Key)
                ActivateDelay();

            else if (activationType == ActivationType.CollisionAndKey)
                if (targetInside)
                    ActivateDelay();
        }


        if (activated)
        {
            delay -= Time.deltaTime;

            if (delay <= 0)
                ExecuteEvents();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (trigger)
        {
            if (activationType == ActivationType.Collision)
            {
                if (tags.Contains(collision.gameObject.tag))
                    ActivateDelay();
            }
            else if (activationType == ActivationType.CollisionAndKey)
                if (tags.Contains(collision.gameObject.tag))
                    targetInside = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (trigger)
        {
            if (activationType == ActivationType.Collision)
            {
                if (tags.Contains(collision.gameObject.tag))
                    ActivateDelay();
            }
            else if (activationType == ActivationType.CollisionAndKey)
                if (tags.Contains(collision.gameObject.tag))
                    targetInside = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!trigger)
        {
            if (activationType == ActivationType.Collision)
            {
                if (tags.Contains(collision.gameObject.tag))
                    ActivateDelay();
            }
            else if (activationType == ActivationType.CollisionAndKey)
                if (tags.Contains(collision.gameObject.tag))
                    targetInside = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!trigger)
        {
            if (activationType == ActivationType.Collision)
            {
                if (tags.Contains(collision.gameObject.tag))
                    ActivateDelay();
            }
            else if (activationType == ActivationType.CollisionAndKey)
                if (tags.Contains(collision.gameObject.tag))
                    targetInside = false;
        }
    }

    public void ActivateDelay()
    {
        if (!activated && VerifyValidations())
            activated = true;
    }

    public void ExecuteEvents()
    {
        eventosFinales?.Invoke();
        activated = false;
        gameObject.SetActive(false);
    }

    public bool VerifyValidations()
    {
        bool complete = true;

        foreach (var item in validations)
        {
            switch (item.ValidationType)
            {
                //case ValidationEnum.Score:

                //    //if (item.NumericValidationType == NumericValidationEnum.Equal)
                //    //    if (DataManager.Instance.GetScore(item.ScoreType) != item.IntValue)
                //    //        complete = false;

                //    //if (item.NumericValidationType == NumericValidationEnum.Greater)
                //    //    if (DataManager.Instance.GetScore(item.ScoreType) <= item.IntValue)
                //    //        complete = false;

                //    //if (item.NumericValidationType == NumericValidationEnum.GreaterEqual)
                //    //    if (DataManager.Instance.GetScore(item.ScoreType) < item.IntValue)
                //    //        complete = false;

                //    //if (item.NumericValidationType == NumericValidationEnum.Less)
                //    //    if (DataManager.Instance.GetScore(item.ScoreType) >= item.IntValue)
                //    //        complete = false;

                //    //if (item.NumericValidationType == NumericValidationEnum.LessEqual)
                //    //    if (DataManager.Instance.GetScore(item.ScoreType) > item.IntValue)
                //    //        complete = false;
                //    break;

                //case ValidationEnum.SceneName:

                //    break;

                //case ValidationEnum.Level:
                //    if (!DataManager.Instance._levels.LevelsData.ValidateLevelBySceneName(item.StringValue))
                //        complete = false;
                //    break;

                //case ValidationEnum.Question:
                //    if (!DataManager.Instance._levels.LevelsData.ValidateQuestionBySceneName(item.StringValue))
                //        complete = false;
                //    break;

                case ValidationEnum.PlayerPrefs:
                    if (!item.BoolValue)
                    {
                        if (!PlayerPrefs.HasKey(item.StringValue))
                            complete = false;
                    }
                    else
                     if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
                        complete = false;
                    break;
            }
        }

        if (!complete)
            eventosNoCumplidasValidaciones?.Invoke();

        return complete;
    }
}

[Serializable]
public class Validation
{
    [SerializeField] ValidationEnum validationType;
    [SerializeField] bool boolValue;
    [SerializeField] string stringValue;
    [SerializeField] int intValue;
    [SerializeField] NumericValidationEnum numericValidationType;

    public ValidationEnum ValidationType { get => validationType; set => validationType = value; }
    public bool BoolValue { get => boolValue; set => boolValue = value; }
    public string StringValue { get => stringValue; set => stringValue = value; }
    public int IntValue { get => intValue; set => intValue = value; }
    public NumericValidationEnum NumericValidationType { get => numericValidationType; set => numericValidationType = value; }
}