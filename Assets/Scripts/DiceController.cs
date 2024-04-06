using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField] Sprite[] diceSides;
    private SpriteRenderer rend;
    private int whosTurn = 0;
    private bool coroutineAllowed = true;
    [SerializeField] GameController game;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = diceSides[5];
    }

    private void OnMouseDown()
    {
        if(!game.gameOver && coroutineAllowed)
            StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
    {
        coroutineAllowed = false;
        int randomDiceSide = 0;
        for(int i = 0; i <= 20 ;i++)
        {
            randomDiceSide = Random.Range(0,6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        game.Play(randomDiceSide + 1);
    }

    public void EnableDice()
    {
        coroutineAllowed = true;
        rend.enabled = true;
    }

    public void DisableDice()
    {
        rend.enabled = false;
    }
}
