using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int _movesForLevel = 5;
    [SerializeField] TextMeshProUGUI _textMovesLeft;

    public int GetMovesLeft()
    {
        return _movesForLevel;
    }

    private void Start()
    {
        if (_textMovesLeft)
            _textMovesLeft.text = "Moves Left : " + _movesForLevel;
    }

    public void MoveMade(bool win)
    {
        if (_movesForLevel != 0)
            _movesForLevel--;

        if (_textMovesLeft)
            _textMovesLeft.text = "Moves Left : " + _movesForLevel;

        if (win)
        {
            Win();
        }
        else if (_movesForLevel <= 0)
        {
            Loose();
        }
    }

    void Win()
    {
        var manager = FindObjectOfType<PauseMenu>();
        if (manager)
            manager.Win();
    }

    void Loose()
    {
        var manager = FindObjectOfType<PauseMenu>();
        if (manager)
            manager.Loose();
    }
}
