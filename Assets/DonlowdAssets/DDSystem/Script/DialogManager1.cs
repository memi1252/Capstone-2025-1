using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace Doublsb.Dialog
{
    public class DialogManager1 : MonoBehaviour
    {
        [Header("Game Objects")]
        public GameObject Printer;

        [Header("UI Objects")]
        public TextMeshProUGUI Printer_Text; //대화 출력 TMP

        [Header("Audio Objects")]
        public AudioSource SEAudio;

        [Header("Preference")]
        public float Delay = 0.1f; //글자 딜레이

        [HideInInspector]
        public State state;

        [HideInInspector]
        public string Result;

        private DialogData _current_Data;

        private float _currentDelay;
        private float _lastDelay;
        private Coroutine _textingRoutine;
        private Coroutine _printingRoutine;

        #region Show & Hide
        public void Show(DialogData Data)
        {
            _current_Data = Data;
            _textingRoutine = StartCoroutine(Activate());
        }

        public void Show(List<DialogData> Data)
        {
            StartCoroutine(Activate_List(Data));
        }

        public void Click_Window()
        {
            switch (state)
            {
                case State.Active:
                    StartCoroutine(_skip()); break;

                case State.Wait:
                    if(_current_Data.SelectList.Count <= 0) Hide(); break;
            }
        }

        public void Hide()
        {
            if(_textingRoutine != null)
                StopCoroutine(_textingRoutine);

            if(_printingRoutine != null)
                StopCoroutine(_printingRoutine);

            Printer.SetActive(false);
            state = State.Deactivate;

            if (_current_Data.Callback != null)
            {
                _current_Data.Callback.Invoke();
                _current_Data.Callback = null;
            }
        }
        #endregion

        #region Selector
        public void Select(int index)
        {
            Result = _current_Data.SelectList.GetByIndex(index).Key;
            Hide();
        }
        #endregion

        #region Speed
        public void Set_Speed(string speed)
        {
            switch (speed)
            {
                case "up":
                    _currentDelay -= 0.25f;
                    if (_currentDelay <= 0) _currentDelay = 0.001f;
                    break;

                case "down":
                    _currentDelay += 0.25f;
                    break;

                case "init":
                    _currentDelay = Delay;
                    break;

                default:
                    _currentDelay = float.Parse(speed);
                    break;
            }

            _lastDelay = _currentDelay;
        }
        #endregion

        private void _initialize()
        {
            _currentDelay = Delay;
            _lastDelay = 0.1f;
            Printer_Text.text = string.Empty;

            Printer.SetActive(true);
        }

        #region Show Text
        private IEnumerator Activate_List(List<DialogData> DataList)
        {
            state = State.Active;

            foreach (var Data in DataList)
            {
                Show(Data);
                while (state != State.Deactivate) { yield return null; }
            }
        }

        private IEnumerator Activate()
        {
            _initialize();
            state = State.Active;

            foreach (var item in _current_Data.Commands)
            {
                switch (item.Command)
                {
                    case Command.print:
                        yield return _printingRoutine = StartCoroutine(_print(item.Context));
                        break;

                    case Command.color:
                        _current_Data.Format.Color = item.Context;
                        break;

                    case Command.size:  
                        _current_Data.Format.Resize(item.Context);
                        break;

                    case Command.speed:
                        Set_Speed(item.Context);
                        break;

                    case Command.click:
                        yield return _waitInput();
                        break;

                    case Command.close:
                        Hide();
                        yield break;

                    case Command.wait:
                        yield return new WaitForSeconds(float.Parse(item.Context));
                        break;
                }
            }

            state = State.Wait;
        }

        private IEnumerator _waitInput()
        {
            while (!Input.GetMouseButtonDown(0)) yield return null;
            _currentDelay = _lastDelay;
        }

        private IEnumerator _print(string Text)
        {
            _current_Data.PrintText += _current_Data.Format.OpenTagger;

            for (int i = 0; i < Text.Length; i++)
            {
                _current_Data.PrintText += Text[i];
                Printer_Text.text = _current_Data.PrintText + _current_Data.Format.CloseTagger;

                if (Text[i] != ' ') SEAudio.Play(); // 간단히 SEAudio 재생
                if (_currentDelay != 0) yield return new WaitForSeconds(_currentDelay);
            }

            _current_Data.PrintText += _current_Data.Format.CloseTagger;
        }

        private IEnumerator _skip()
        {
            if (_current_Data.isSkippable)
            {
                _currentDelay = 0;
                while (state != State.Wait) yield return null;
                _currentDelay = Delay;
            }
        }
        #endregion
    }
}
