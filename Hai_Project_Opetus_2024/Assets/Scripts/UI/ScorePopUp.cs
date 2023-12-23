using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScorePopUp : MonoBehaviour
{
    public float lifetime = 2f; // How long the score stays on screen
    public float moveSpeed = 1f; // How fast it moves up

    public TextMeshPro textMesh; // Change to Text if using standard UI Text
    private float timer;
    public void SetScore(int score)
    {
        textMesh.text = score.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        // Move the score display up
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);

        // Fade out the score display over time
        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timer / lifetime);

        Color color = textMesh.color;
        color.a = alpha;
        textMesh.color = color;

        // Destroy the object after its lifetime expires
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
