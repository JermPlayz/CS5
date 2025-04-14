using System;
using System.Collections;
using UnityENgine;
using TMPro;

namespace ByteBattle.Typography.Typewriter
{
    [RequireComponent(typeof(TMP_Text))]

    public class TypewriterEffect: MonoBehavior
    {
        private TMP_Text _textBox;

        //Only for prototyping
        [Header("Test String")]

        [SerializeField] private string testText;


        //Basic typewriter functionality

        private int _currentVisibleCharacterIndex;
        private Coroutine _typewriter Coroutine;

        private WaitForSeconds _simpleDelay;
        private WaitForSeconds _interpunctuationDelay;

        [Header("Typewriter Settings")]
        [SerializeField] private float charactersPerSecond = 20; 
       
















        private void Awake()
        {
            _textBox = GetComponent<TMP_Text>();
            _simpleDelay = new WaitForSeconds(1/charactersPerSecond);
            _interpunctuationDelay = new WaitForSeconds(_interpunctuationDelay)

        }








