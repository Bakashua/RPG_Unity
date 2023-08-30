using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    public enum TEXTEFFECT
    { BIGGER, COLOR, FADE, GRADIENT, SMALLER, WOBBLECHARACTER, WOBBLEWORD}
    public TEXTEFFECT Texteffect;

    [Header("Text")]
    public TMP_Text textYouWantToFade;

    [Header("Change Color")]
    public Color32 NewColor;

    [Header("Fade Out Text")]
    public float ShowDuration = 1f;
    public float FadeSpeed = 0.05f;
    public float MoveStep = 0.1f;

    [Header("Text Size")]
    public int sizeChanger;
    
    [Header("Character Wobble")]
    Mesh mesh;
    Vector3[] vertices;
    public float offSet1 = 3.3f;
    public float offSet2 = 2.5f;

    [Header("Word Wobble")]
    public List<int> wordIndexes;
    public List<int> wordLengths;

    [Header("Color Gradient")]
    public Gradient colorGrad;


    public bool showOnEnable;

    public void OnEnable()
    {
        if (showOnEnable) { Show(); }
    }
    private void Start()
    {
        wordIndexes = new List<int> { 0 };
        wordLengths = new List<int>();

        string s = textYouWantToFade.text;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(index + 1);
        }
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);
    }

    private void Update()
    {
        //Show();
    }

    public void Show()
    { PlayEffect();  }

    private void PlayEffect()
    {
        if (textYouWantToFade == null) { textYouWantToFade = gameObject.GetComponent<TMP_Text>(); }

        switch (Texteffect)
        {
            case TEXTEFFECT.BIGGER: OnClickBigger(); break;
            case TEXTEFFECT.COLOR: OnClickColor(); break;
            case TEXTEFFECT.FADE: ShowFadeOutText(); break;
            case TEXTEFFECT.GRADIENT: StartCoroutine(ColorGrandient()); break;
            case TEXTEFFECT.SMALLER: OnClickSmaller(); break;
            case TEXTEFFECT.WOBBLECHARACTER: StartCoroutine(WobbleCharacter()); break;
            case TEXTEFFECT.WOBBLEWORD: StartCoroutine(WobbleWord()); break;
        }
    }

    public void ShowFadeOutText()
    {
        StartCoroutine(FadeOutText());
    }

    IEnumerator FadeOutText()
    {
        yield return new WaitForSeconds(ShowDuration);


        while (textYouWantToFade.color.a >= 0)
        {
            textYouWantToFade.color = new Color(textYouWantToFade.color.r,
                textYouWantToFade.color.g,
                textYouWantToFade.color.b,
                textYouWantToFade.color.a - 0.05f);
            yield return new WaitForSeconds(FadeSpeed);
        }
    }  

    public void OnClickBigger()
    {
        Debug.Log("aaa");
        textYouWantToFade.fontSize = textYouWantToFade.fontSize + sizeChanger;
    }

    public void OnClickSmaller()
    {
        Debug.Log("aaa");
        textYouWantToFade.fontSize = textYouWantToFade.fontSize - sizeChanger;
    }

    public void OnClickColor()
    {
        //textYouWantToFade.color = new Color32((byte)Random.Range(50, 255), (byte)Random.Range(50, 255), (byte)Random.Range(50, 200), (byte)Random.Range(50, 255));
        textYouWantToFade.color = NewColor;
    }

    public IEnumerator WobbleWord()
    {
        textYouWantToFade.ForceMeshUpdate();
        mesh = textYouWantToFade.mesh;
        vertices = mesh.vertices;



        for (int w = 0; w < wordIndexes.Count; w++)
        {
            int wordIndex = wordIndexes[w];
            Vector3 offset = Wobble(Time.time + w);

            Debug.Log(offset);

            for (int i = 0; i < wordLengths[w]; i++)
            {
                TMP_CharacterInfo c = textYouWantToFade.textInfo.characterInfo[wordIndex + i];

                int index = c.vertexIndex; ;

                vertices[index] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;
            }
        }

            //yield return new WaitForSeconds(0);
            yield return null;
        StartCoroutine(WobbleWord());
    }

    public IEnumerator WobbleCharacter()
    {
        textYouWantToFade.ForceMeshUpdate();
        mesh = textYouWantToFade.mesh;
        vertices = mesh.vertices;

            for (int i = 0; i < textYouWantToFade.textInfo.characterCount; i++)
            {
                TMP_CharacterInfo c = textYouWantToFade.textInfo.characterInfo[i];

                int index = c.vertexIndex;

                Vector3 offset2 = Wobble(Time.time + i);
                vertices[index] += offset2;
                vertices[index + 1] += offset2;
                vertices[index + 2] += offset2;
                vertices[index + 3] += offset2;
            }

            mesh.vertices = vertices;
            textYouWantToFade.canvasRenderer.SetMesh(mesh);        

        yield return null;
        StartCoroutine(WobbleCharacter());
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 3.3f), Mathf.Cos(time * 2.5f));
    }

    public IEnumerator ColorGrandient()
    {
        textYouWantToFade.ForceMeshUpdate();
        mesh = textYouWantToFade.mesh;
        vertices = mesh.vertices;

        Color[] colors = mesh.colors;

            for (int w = 0; w < wordIndexes.Count; w++)
            {
                int wordIndex = wordIndexes[w];

                for (int i = 0; i < wordLengths[w]; i++)
                {
                TMP_CharacterInfo c = textYouWantToFade.textInfo.characterInfo[wordIndex + i];

                int index = c.vertexIndex;

                colors[index] = colorGrad.Evaluate(Mathf.Repeat(Time.time + vertices[index].x * 0.001f, 1f));
                colors[index + 1] = colorGrad.Evaluate(Mathf.Repeat(Time.time + vertices[index + 1].x * 0.001f, 1f));
                colors[index + 2] = colorGrad.Evaluate(Mathf.Repeat(Time.time + vertices[index + 2].x * 0.001f, 1f));
                colors[index + 3] = colorGrad.Evaluate(Mathf.Repeat(Time.time + vertices[index + 3].x * 0.001f, 1f));

                }
            }

        yield return null;
        StartCoroutine(ColorGrandient());
    }

}






