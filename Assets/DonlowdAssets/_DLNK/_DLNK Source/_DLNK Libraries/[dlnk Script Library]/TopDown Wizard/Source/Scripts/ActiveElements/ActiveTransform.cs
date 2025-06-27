using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTransform : MonoBehaviour
{
    [System.Serializable]
    public class Target
    {
        public GameObject TargetGO; // 대상 게임 오브젝트
        public Vector3 TranslateGO = new Vector3(0,0,0); // 이동할 위치
        public Quaternion RotateGO = new Quaternion(0, 0, 0, 100f); // 회전 값
        public bool Scalable = false; // 크기 조정 가능 여부
        public Vector3 ScaleGO = new Vector3(1, 1, 1); // 크기 값
        [HideInInspector]
        public Vector3 originalTf; // 원래 위치
        [HideInInspector]
        public Vector3 differenceSc; // 크기 차이
    }
    public List<Target> Targets; // 대상 목록
    public float Duration; // 동작 지속 시간
    public bool done; // 동작 완료 여부
    public bool _localAuto = false; // 로컬 자동 모드 여부
    private TDActiveElement activeParent; // 부모 활성 요소
    private bool _ismoving = false; // 이동 중 여부
    private float _timer = 0f; // 타이머
    private float _percentage = 0; // 진행률
    private bool _waitforit = false; // 대기 상태 여부
    private TDScene tdscene; // TDScene 참조
    private bool _isIn; // 안에 있는지 여부
    private bool _isOut; // 밖에 있는지 여부

    void Start()
    {
        // 변수에 대한 참조 찾기
        tdscene = GameObject.FindWithTag("TdLevelManager").GetComponent<TDScene>();
        activeParent = this.GetComponent<TDActiveElement>();

        // 시작 시 이동이 완료되었는지 확인
        foreach (Target tar in Targets)
        {
            // 원래 위치 저장
            tar.originalTf = tar.TargetGO.transform.localPosition;
            // 시작 시 스케일 저장
            if (tar.Scalable)
            {
                tar.differenceSc = (tar.ScaleGO - tar.TargetGO.transform.localScale);
                tar.ScaleGO = tar.TargetGO.transform.localScale;
            }
            if (done)
            {
                // 위치 이동
                tar.TargetGO.transform.localPosition = (tar.originalTf + tar.TranslateGO);
                // 회전
                tar.TargetGO.transform.localRotation = new Quaternion(tar.RotateGO.x, tar.RotateGO.y, tar.RotateGO.z, tar.RotateGO.w);
                // 크기 조정
                if (tar.Scalable)
                    tar.TargetGO.transform.localScale = tar.ScaleGO + tar.differenceSc;
            }
        }

        // 전역 자동 모드가 활성화되었는지 확인
        if (activeParent.Automatic)
            _localAuto = true;
    }
    void Update()
    {
        // 동작 준비가 되었는지 확인
        if (activeParent.ActiveCollider.actived)
        {
            // 자동 모드일 때 동작 수행
            if (_localAuto)
            {
                // 방금 진입했는지 확인
                if (!_isIn)
                {
                    _waitforit = false;
                    _ismoving = true;
                    // 자동 종료 모드가 활성화된 경우
                }
                _isIn = true;
            }
            // 키 입력이 있을 때 동작 수행
            else if (Input.GetKeyDown(tdscene.ActiveKey))
            {
                _ismoving = true;
                _waitforit = false;
                if (activeParent.AutoOnExit && !done)
                    _isIn = true;
            }
        }
        // 자동 종료 모드가 활성화된 경우 종료 동작 수행
        else if (activeParent.ActiveCollider.hasexit == true)
        {
            if (activeParent.AutoOnExit && _isIn)
            {
                _isOut = true;
            }
            _isIn = false;
        }
        // 애니메이션이 끝나기 전에 콜라이더를 나가면 닫기 동작 활성화
        if (activeParent.AutoOnExit && _isOut && !_ismoving)
        {
            _waitforit = false;
            _ismoving = true;
            _isOut = false;
        }

        // 동작 수행
        if (_ismoving & !_waitforit)
        {
            // 시간을 업데이트하고 고정
            _timer = _timer + Time.deltaTime;
            // 이동 완료 비율 설정
            if (!done)
                _percentage = (_timer / Duration);
            else
                _percentage = (1 - (_timer / Duration));

            // 시간이 초과되면 이동 중지
            if (_percentage > 1f)
            {
                _ismoving = false;
                _percentage = 1;
                _timer = 0f;
                done = true;
                _waitforit = true;
                Debug.Log("Door Opened");
            }
            else
                if (_percentage < 0f)
            {
                _ismoving = false;
                _percentage = 0;
                _timer = 0f;
                done = false;
                _waitforit = true;
                Debug.Log("Door Closed");
            }
            foreach (Target tar in Targets)
            {
                // 위치 이동
                tar.TargetGO.transform.localPosition = tar.originalTf + (_percentage * tar.TranslateGO);
                // 회전
                tar.TargetGO.transform.localRotation = new Quaternion(tar.RotateGO.x * _percentage, tar.RotateGO.y * _percentage, tar.RotateGO.z * _percentage, tar.RotateGO.w);
                // 크기 조정
                if (tar.Scalable)
                tar.TargetGO.transform.localScale = tar.ScaleGO + (_percentage * tar.differenceSc);
            }
        }
    }
}