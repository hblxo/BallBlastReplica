using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    public bool dividable;
    public int point;
    public float force = 15f;
    private TextMesh _pointText;
    public Sprite[] sprites;
    public Rigidbody2D rb;

    void Start()
    {
        int level = GameManager.Level;

        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);
        point = Random.Range(level + 1, level * 10 + 1);
        Debug.Log(point);
        GameManager.instance.coinsCount++;
        _pointText = GetComponentInChildren<TextMesh>();
        _pointText.text = point.ToString();
    }

    void Awake()
    {
        dividable = true;
    }

    private void InstantiateMiniCoin()
    {
        float deltaX = Random.Range(-0.5f, 0.5f);
        float deltaY = Random.Range(-0.5f, 0.5f);
        Vector3 newPos = new Vector3(transform.position.x + deltaX, transform.position.y + deltaY, transform.position.z);
        Vector3 scale = new Vector3(0.3f, 0.3f, 0.3f);

        newPos.x = newPos.x < -(Screen.width / 2) ? - (Screen.width / 2) + 1 : newPos.x;
        newPos.x = newPos.x > (Screen.width / 2) ? (Screen.width / 2) - 1 : newPos.x;
        newPos.y = newPos.y < -(Screen.height / 2) ? - (Screen.height / 2) + 1 : newPos.y;
        newPos.y = newPos.y > (Screen.height / 2) ? (Screen.height / 2) - 1 : newPos.y;

        var miniCoin = Instantiate(gameObject, newPos, transform.rotation);
        miniCoin.transform.localScale = scale;
        miniCoin.transform.GetComponent<Rigidbody2D>().mass += 0.1f;
        miniCoin.gameObject.GetComponent<Coin>().point = point / 2;
        miniCoin.gameObject.GetComponent<Coin>().dividable = false;
    }
    
    private void DivideCoin()
    {
        if (!dividable)
            return;
        InstantiateMiniCoin();
        InstantiateMiniCoin();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            GameManager.instance.score += point;
            DivideCoin();
            Destroy(gameObject);
            GameManager.instance.coinsCount--;
        }
        else if (other.collider.CompareTag("Player")) 
        {
            GameManager.instance.GameOver();
        }
        else if (other.collider.CompareTag("Box"))
        {
//            Debug.Log("Collision");
        }
    }
    
}