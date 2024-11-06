using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum BattleState { START, PLAYER1TURN, PLAYER2TURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public Transform player1BattleStation;
    public Transform player2BattleStation;

    Unit player1Unit;
    Unit player2Unit;

    public Text dialogueText;
    public Text mathProblemText;
    public InputField answerInput;

    public BattleHUD player1HUD;
    public BattleHUD player2HUD;

    public BattleState state;

    private int correctAnswer;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject player1GO = Instantiate(player1Prefab, player1BattleStation);
        player1Unit = player1GO.GetComponent<Unit>();

        GameObject player2GO = Instantiate(player2Prefab, player2BattleStation);
        player2Unit = player2GO.GetComponent<Unit>();

        dialogueText.text = "Player 1 and Player 2 prepare for battle...";

        player1HUD.SetHUD(player1Unit);
        player2HUD.SetHUD(player2Unit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYER1TURN;
        PlayerTurn();
    }

    void GenerateMathProblem()
    {
        int num1 = UnityEngine.Random.Range(1, 10);
        int num2 = UnityEngine.Random.Range(1, 10);
        string[] operators = { "+", "-", "*", "/" };
        string selectedOperator = operators[UnityEngine.Random.Range(0, operators.Length)];

        switch (selectedOperator)
        {
            case "+":
                correctAnswer = num1 + num2;
                break;
            case "-":
                correctAnswer = num1 - num2;
                break;
            case "*":
                correctAnswer = num1 * num2;
                break;
            case "/":
                correctAnswer = num1 / num2;  // Integer division for simplicity
                break;
        }

        mathProblemText.text = $"Solve: {num1} {selectedOperator} {num2}";
        answerInput.text = ""; // Clear previous input
    }

    IEnumerator CheckAnswerAndExecuteAction(Action<bool> action)
    {
        int playerAnswer;
        if (int.TryParse(answerInput.text, out playerAnswer) && playerAnswer == correctAnswer)
        {
            dialogueText.text = "Correct! Double effect!";
            yield return new WaitForSeconds(1f);
            action(true); // Double effect
        }
        else
        {
            dialogueText.text = "Incorrect. Normal effect.";
            yield return new WaitForSeconds(1f);
            action(false); // Normal effect
        }
    }

    IEnumerator Player1Attack(bool doubleEffect)
    {
        int damage = doubleEffect ? player1Unit.damage * 2 : player1Unit.damage;
        bool isDead = player2Unit.TakeDamage(damage);
        player2HUD.SetHP(player2Unit.currentHP);
        dialogueText.text = "Player 1 attacks!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle("Player 1");
        }
        else
        {
            state = BattleState.PLAYER2TURN;
            PlayerTurn();
        }
    }

    IEnumerator Player2Attack(bool doubleEffect)
    {
        int damage = doubleEffect ? player2Unit.damage * 2 : player2Unit.damage;
        bool isDead = player1Unit.TakeDamage(damage);
        player1HUD.SetHP(player1Unit.currentHP);
        dialogueText.text = "Player 2 attacks!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle("Player 2");
        }
        else
        {
            state = BattleState.PLAYER1TURN;
            PlayerTurn();
        }
    }

    void EndBattle(string winningPlayer)
    {
        dialogueText.text = winningPlayer + " won the battle!";
    }

    void PlayerTurn()
    {
        if (state == BattleState.PLAYER1TURN)
        {
            dialogueText.text = "Player 1, solve the math problem:";
            GenerateMathProblem();
        }
        else if (state == BattleState.PLAYER2TURN)
        {
            dialogueText.text = "Player 2, solve the math problem:";
            GenerateMathProblem();
        }
    }

    public void OnAttackButton()
    {
        if (state == BattleState.PLAYER1TURN)
        {
            StartCoroutine(CheckAnswerAndExecuteAction(doubleEffect => StartCoroutine(Player1Attack(doubleEffect))));
        }
        else if (state == BattleState.PLAYER2TURN)
        {
            StartCoroutine(CheckAnswerAndExecuteAction(doubleEffect => StartCoroutine(Player2Attack(doubleEffect))));
        }
    }

    IEnumerator Player1Heal(bool doubleEffect)
    {
        int healAmount = doubleEffect ? 10 : 5;
        player1Unit.Heal(healAmount);
        player1HUD.SetHP(player1Unit.currentHP);
        dialogueText.text = "Player 1 feels renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYER2TURN;
        PlayerTurn();
    }

    IEnumerator Player2Heal(bool doubleEffect)
    {
        int healAmount = doubleEffect ? 10 : 5;
        player2Unit.Heal(healAmount);
        player2HUD.SetHP(player2Unit.currentHP);
        dialogueText.text = "Player 2 feels renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYER1TURN;
        PlayerTurn();
    }

    public void OnHealButton()
    {
        if (state == BattleState.PLAYER1TURN)
        {
            StartCoroutine(CheckAnswerAndExecuteAction(doubleEffect => StartCoroutine(Player1Heal(doubleEffect))));
        }
        else if (state == BattleState.PLAYER2TURN)
        {
            StartCoroutine(CheckAnswerAndExecuteAction(doubleEffect => StartCoroutine(Player2Heal(doubleEffect))));
        }
    }
}
