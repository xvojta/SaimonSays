using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public AudioClip[] audioClips;

    public GameObject up; //1
    public GameObject down; //2
    public GameObject left; //3
    public GameObject right; //4
    public float step = .5f;
    public Text pointsText;

    bool done = true;
    float timer = 5f;
    int direction = 0;
    int points = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5f)
        {
            if (!done) lose();

            timer = 0f;

            done = false;
            direction = Random.Range(1, 4+1);
            GetComponent<AudioSource>().PlayOneShot(audioClips[direction - 1]);
        }
    }

    private void OnGUI()
    {
        if (!Event.current.isKey || done) return;
        int inversion =  Event.current.type == EventType.KeyDown? 1 : -1;

        var keyDirec = 0;

        switch (Event.current.keyCode)
        {
            case KeyCode.W:
                keyDirec = 1;
                up.transform.position += new Vector3(0, step*inversion, 0);
                break;
            case KeyCode.A:
                keyDirec = 3;
                left.transform.position += new Vector3(-step*inversion, 0, 0);
                break;
            case KeyCode.S:
                keyDirec = 2;
                down.transform.position += new Vector3(0, -step * inversion, 0);
                break;
            case KeyCode.D:
                keyDirec = 4;
                right.transform.position += new Vector3(step * inversion, 0, 0);
                break;
        }
        if(keyDirec == direction)
        {
            done = true;
            points++;
            pointsText.text = points.ToString();
            //win
        }
        else if(keyDirec != 0 && inversion > 0)
        {
            lose();
        }
    }

    void anim(int key)
    {
        Task.Run(async delegate
        {
            switch (key)
            {
                case 1:
                    up.transform.position += new Vector3(0, step, 0);
                    break;
                case 3:
                    left.transform.position += new Vector3(-step, 0, 0);
                    break;
                case 2:
                    down.transform.position += new Vector3(0, -step, 0);
                    break;
                case 4:
                    right.transform.position += new Vector3(step, 0, 0);
                    break;
            }

            await Task.Delay(250);

            switch (key)
            {
                case 1:
                    up.transform.position -= new Vector3(0, step, 0);
                    break;
                case 3:
                    left.transform.position -= new Vector3(-step, 0, 0);
                    break;
                case 2:
                    down.transform.position -= new Vector3(0, -step, 0);
                    break;
                case 4:
                    right.transform.position -= new Vector3(step, 0, 0);
                    break;
            }
        });
    }

    void lose()
    {
        points = 0;
        Debug.Break();
    }
}
