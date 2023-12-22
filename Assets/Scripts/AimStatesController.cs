using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using Cinemachine;

public class AimStatesController : MonoBehaviour
{
    [SerializeField] Animator anim;

    [SerializeField] LayerMask aimLayers;
    [SerializeField] Transform aimPosition;
    RaycastHit hit;

    float _newRightHandWeight = 0;
    float _newLeftHandWeight = 0;
    [SerializeField] float handsWeightSpeed;

    float _newRigWeight = 0;
    [SerializeField] float _rigWeightSpeed;
    Rig _rigWeightControl;

    [SerializeField] float aimSpeed;
    [SerializeField] Vector3 newAimPosition;

    [SerializeField] Vector3 _idleOffsetPosition, _aimingOffsetPosition, _currentOffsetPosition;
    [SerializeField] float _offsetSpeed;

    IdleState _idle = new IdleState();
    AimState _aim = new AimState();

    AimBaseState currentState;

    [SerializeField] PlayerInput _inputControl;

    [SerializeField] CinemachineVirtualCamera _thirdPersonCamera;
    Cinemachine3rdPersonFollow _thirdPersonSettings;

    [SerializeField] List<Gun> _guns = new List<Gun>();
    int _currentGunIndex = 0;

    bool _aiming;

    Gun _currentGun;

    [SerializeField] GunInventory _inventory;

    public AimState Aim { get => _aim; set => _aim = value; }
    public IdleState Idle { get => _idle; set => _idle = value; }
    public float NewRightHandWeight { get => _newRightHandWeight; set => _newRightHandWeight = value; }
    public PlayerInput InputControl { get => _inputControl; set => _inputControl = value; }
    public float NewLeftHandWeight { get => _newLeftHandWeight; set => _newLeftHandWeight = value; }
    public float NewRigWeight { get => _newRigWeight; set => _newRigWeight = value; }
    public Animator Anim { get => anim; set => anim = value; }
    public Vector3 IdleOffsetPosition { get => _idleOffsetPosition; set => _idleOffsetPosition = value; }
    public Vector3 AimingOffsetPosition { get => _aimingOffsetPosition; set => _aimingOffsetPosition = value; }
    public Vector3 CurrentOffsetPosition { get => _currentOffsetPosition; set => _currentOffsetPosition = value; }
    public Transform AimPosition { get => aimPosition; set => aimPosition = value; }
    public Gun CurrentGun { get => _currentGun; set => _currentGun = value; }
    public bool Aiming { get => _aiming; set => _aiming = value; }
    public GunInventory Inventory { get => _inventory; set => _inventory = value; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        InputControl = GetComponent<PlayerInput>();
        _rigWeightControl = GetComponentInChildren<Rig>();

        _thirdPersonSettings = _thirdPersonCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        ChangeState(_idle);
    }

    // Update is called once per frame
    void Update()
    {
        SmoothHandsWeight();
        SmoothRigWeight();
        MouseScroll();

        if (AimPosition.position != newAimPosition)
            AimPosition.position = Vector3.Lerp(AimPosition.position, newAimPosition, aimSpeed * Time.deltaTime);

        if (_thirdPersonSettings.ShoulderOffset != _currentOffsetPosition)
            _thirdPersonSettings.ShoulderOffset = Vector3.Lerp(_thirdPersonSettings.ShoulderOffset, _currentOffsetPosition, _offsetSpeed * Time.deltaTime);

        currentState.UpdateState(this);

        anim.SetBool("Aim", Aiming);
    }

    private void FixedUpdate()
    {
        CreateAimPoint();

        currentState.FixedState(this);
    }

    public void ChangeState(AimBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public void CreateAimPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, aimLayers))
            newAimPosition = hit.point;
    }

    public void SmoothHandsWeight()
    {
        float currentRightHandWeight = anim.GetLayerWeight(anim.GetLayerIndex("RightHand"));

        if (currentRightHandWeight != NewRightHandWeight)
            anim.SetLayerWeight(anim.GetLayerIndex("RightHand"), Mathf.MoveTowards(currentRightHandWeight, NewRightHandWeight, handsWeightSpeed * Time.deltaTime));

        float currentLeftHandWeight = anim.GetLayerWeight(anim.GetLayerIndex("LeftHand"));

        if (currentLeftHandWeight != NewLeftHandWeight)
            anim.SetLayerWeight(anim.GetLayerIndex("LeftHand"), Mathf.MoveTowards(currentLeftHandWeight, NewRightHandWeight, handsWeightSpeed * Time.deltaTime));
    }

    public void SmoothRigWeight()
    {
        float currentRigWeight = _rigWeightControl.weight;

        if (currentRigWeight != NewRigWeight)
            _rigWeightControl.weight = Mathf.MoveTowards(currentRigWeight, NewRigWeight, _rigWeightSpeed * Time.deltaTime);
    }

    public void ChangeGun()
    {
        for (int i = 0; i < _guns.Count; i++)
        {
            if (i == _currentGunIndex)
            {
                _currentGun = _guns[i];
                _currentGun.UpdatePivotPosition(AimPosition);
                _currentGun.gameObject.SetActive(true);
            }
            else
                _guns[i].gameObject.SetActive(false);
        }
    }

    public void AddGun(string gunName)
    {
        _inventory.AddGun(gunName);

        if (!_currentGun)
        {
            GetGunIndex(true);
            ChangeGun();
        }

    }

    public void MouseScroll()
    {
        if (_inventory.GunNames.Count > 0)
        {
            float scrollValue = _inputControl.actions["ChangeGun"].ReadValue<float>();

            if (scrollValue > 0)
            {
                GetGunIndex(true);
            }

            if (scrollValue < 0)
            {
                GetGunIndex(false);
            }

            ChangeGun();
        }
    }

    public void ClearInventory()
    {
        _inventory.GunNames.Clear();
    }

    public void GetGunIndex(bool up)
    {
        for (int i = 0; i < 1; i++)
        {
            if (up)
            {
                _currentGunIndex++;

                if (_currentGunIndex > _guns.Count - 1)
                {
                    _currentGunIndex = 0;
                }

                if (!_inventory.GunNames.Contains(_guns[_currentGunIndex].GunName))
                {
                    i--;
                }
            }

            else
            {
                _currentGunIndex--;

                if (_currentGunIndex < 0)
                {
                    _currentGunIndex = _guns.Count - 1;
                }

                if (!_inventory.GunNames.Contains(_guns[_currentGunIndex].GunName))
                {
                    i--;
                }
            }
        }
    }
}
