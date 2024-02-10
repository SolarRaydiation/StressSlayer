using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    ClockManager cm;

    [Header("Sprite Renderers")]
    public SpriteRenderer citySR;
    public SpriteRenderer gradientSR;
    public SpriteRenderer sunMoonSR;

    [Header("Morning Sprites")]
    public Sprite cityMorning;
    public Sprite gradientMorning;
    public Sprite sunMorning;

    [Header("Afternoon Sprites")]
    public Sprite cityAfternoon;
    public Sprite gradientAfternoon;
    public Sprite sunAfternoon;

    [Header("Evening Sprites")]
    public Sprite cityEvening;
    public Sprite gradientEvening;
    public Sprite moon;

    void Start()
    {
        cm = ClockManager.GetInstance();
        UpdateSprites(cm.currentDaySection);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprites(cm.currentDaySection);
    }

    private void UpdateSprites(ClockManager.DaySection ds)
    {
        switch(ds)
        {
            case ClockManager.DaySection.Morning:
                citySR.sprite = cityMorning;
                gradientSR.sprite = gradientMorning;
                sunMoonSR.sprite = sunMorning;
                break;
            case ClockManager.DaySection.Afternoon:
                citySR.sprite = cityAfternoon;
                gradientSR.sprite = gradientAfternoon;
                sunMoonSR.sprite = sunAfternoon;
                break;
            case ClockManager.DaySection.Evening:
                citySR.sprite = cityEvening;
                gradientSR.sprite = gradientEvening;
                sunMoonSR.sprite = moon;
                break;
            default:
                Debug.LogError($"Error changing parallax. Recieved unknown {ds} instead!");
                break;
        }
    }
}