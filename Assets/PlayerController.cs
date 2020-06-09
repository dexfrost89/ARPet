using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private const float FoodWasteSpeed = 1f;
    private const float WaterWasteSpeed = 1f;
    private const float SleepWasteSpeed = 1f;
    private const float SleepGainSpeed = 10f;
    private const float JoyWasteSpeed = 1f;
    private const float JoyGainSpeed = 10f;
    private const float DefaultFoodAmount = 100f;
    private const float DefaultWaterAmount = 100f;
    private const float DefaultSleepAmount = 100f;
    private const float DefaultJoyAmount = 100f;

    private float _foodAmount = DefaultFoodAmount;
    private float _waterAmount = DefaultWaterAmount;
    private float _sleepAmount = DefaultSleepAmount;
    private float _joyAmount = DefaultJoyAmount;

    public float FoodAmount => _foodAmount;
    public float WaterAmount => _waterAmount;
    public float SleepAmount => _sleepAmount;
    public float JoyAmount => _joyAmount;

    private bool sleeping = false;
    private bool playing = false;

    [SerializeField] private UnityEngine.UI.Image foodScale, waterScale, sleepScale, joyScale;
    [SerializeField] private UnityEngine.UI.Text foodText, waterText, sleepText, joyText;

    [SerializeField] private Animator gameCharacter;

    private bool dead = false;

    [SerializeField] private AudioSource snoor_sound, play_sound, eat_sound, drink_sound, death_sound;


    private void Update()
    {
        _foodAmount -= FoodWasteSpeed * Time.deltaTime;
        _foodAmount = Mathf.Max(0, _foodAmount);

        _waterAmount -= WaterWasteSpeed * Time.deltaTime;
        _waterAmount = Mathf.Max(0, _waterAmount);

        if (!sleeping)
        {
            _sleepAmount -= SleepWasteSpeed * Time.deltaTime;
            _sleepAmount = Mathf.Max(0, _sleepAmount);
        }
        else
        {
            _sleepAmount += SleepGainSpeed * Time.deltaTime;
            _sleepAmount = Mathf.Min(DefaultSleepAmount, _sleepAmount);
        }

        if (!playing)
        {
            _joyAmount -= JoyWasteSpeed * Time.deltaTime;
            _joyAmount = Mathf.Max(0, _joyAmount);
        }
        else
        {

            _joyAmount += JoyGainSpeed * Time.deltaTime;
            _joyAmount = Mathf.Min(DefaultJoyAmount, _joyAmount);
        }


        /*if (_foodAmount < DefaultFoodAmount * 0.7f)
        {
            Debug.Log("Need more food");
        }*/

        if (_foodAmount <= 0)
        {
            if(!dead)
            {
                Dead();
            }
            dead = true;
        }
        if (_sleepAmount <= 0)
        {
            if (!dead)
            {
                Dead();
            }
            dead = true;
        }
        if (_joyAmount <= 0)
        {
            if (!dead)
            {
                Dead();
            }
            dead = true;
        }
        if (_waterAmount <= 0)
        {
            if (!dead)
            {
                Dead();
            }
            dead = true;
        }


        if (!dead)
        {
            foodScale.fillAmount = 1f - (float)((int)(10 - (_foodAmount - 1) / 10f)) / 10f;
            foodText.text = ((int)(_foodAmount)).ToString() + "/" + ((int)(DefaultFoodAmount)).ToString();

            waterScale.fillAmount = 1f - (float)((int)(10 - (_waterAmount - 1) / 10f)) / 10f;
            waterText.text = ((int)(_waterAmount)).ToString() + "/" + ((int)(DefaultWaterAmount)).ToString();

            sleepScale.fillAmount = 1f - (float)((int)(10 - (_sleepAmount - 1) / 10f)) / 10f;
            sleepText.text = ((int)(_sleepAmount)).ToString() + "/" + ((int)(DefaultSleepAmount)).ToString();

            joyScale.fillAmount = 1f - (float)((int)(10 - (_joyAmount - 1) / 10f)) / 10f;
            joyText.text = ((int)(_joyAmount)).ToString() + "/" + ((int)(DefaultFoodAmount)).ToString();
        } else
        {
        }
    }

    [SerializeField] private UnityEngine.UI.Button restart_button;

    public void Restart()
    {
        SceneManager.LoadScene("ImageTracking_Targets");
    }

    public void Dead()
    {

        snoor_sound.Stop();
        play_sound.Stop();
        eat_sound.Stop();
        drink_sound.Stop();
        gameCharacter.SetBool("Dead", true);
        restart_button.gameObject.SetActive(true);
        death_sound.Play();
    }

    
    public void OnEnable()
    {
        if (!dead)
        {
            if(sleeping)
            {
                snoor_sound.Play();
                gameCharacter.SetBool("Sleep", true);
            } else if(playing)
            {

                play_sound.Play();
                gameCharacter.SetBool("Play", true);
            }
        } else
        {
            gameCharacter.SetBool("Dead", true);
        }
    }
    public void feed(int amount)
    {
        if (!dead)
        {
            sleeping = false;
            playing = false;
            _foodAmount += amount;
            _foodAmount = Mathf.Min(DefaultFoodAmount, _foodAmount);
            gameCharacter.SetBool("Sleep", false);
            gameCharacter.SetBool("Play", false);
            gameCharacter.SetTrigger("Food");
            eat_sound.Play();
            snoor_sound.Stop();
            play_sound.Stop();
        }
    }

    public void drink(int amount)
    {
        if (!dead)
        {
            sleeping = false;
            playing = false;
            _waterAmount += amount;
            _waterAmount = Mathf.Min(DefaultWaterAmount, _waterAmount);

            gameCharacter.SetBool("Sleep", false);
            gameCharacter.SetBool("Play", false);
            gameCharacter.SetTrigger("Drink");
            drink_sound.Play();
            snoor_sound.Stop();
            play_sound.Stop();
        }
    }

    public void sleep()
    {
        if (!dead)
        {
            sleeping = true;
            playing = false;

            gameCharacter.SetBool("Sleep", true);
            gameCharacter.SetBool("Play", false);
            snoor_sound.Play();
            play_sound.Stop();
        }
    }

    public void play(int amount)
    {
        if (!dead)
        {
            sleeping = false;
            playing = true;
            gameCharacter.SetBool("Sleep", false);
            gameCharacter.SetBool("Play", true);
            play_sound.Play();
            snoor_sound.Stop();
        }

    }
}
