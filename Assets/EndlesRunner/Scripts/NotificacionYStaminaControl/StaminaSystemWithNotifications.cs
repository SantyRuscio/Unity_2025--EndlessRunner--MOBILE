using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StaminaSystemWithNotifications : MonoBehaviour
{
    [SerializeField] int _maxStamina = 3;
    [SerializeField] float _timerToRecharge = 500f;
    int _currentStamina;
    private int _extraStamina = 0;


    DateTime _nextStaminaTime;
    DateTime _lastStaminaTime;

    bool recharging;

    [SerializeField] TextMeshProUGUI _staminaText = null;
    [SerializeField] TextMeshProUGUI _timertext = null;    
    
    //Change
    [SerializeField] string _titleNotif = "Full Stamina";
    [SerializeField] string _textNotif = "Tenes la stamina full, volve a jugar conmigo";
    [SerializeField] IconSelecter _smallIcon = IconSelecter.icon_reminder;
    [SerializeField] IconSelecter _largeIcon = IconSelecter.icon_reminderbig;
    TimeSpan timer;
    int id;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        StartCoroutine(RechargeStamina());

        //Change
        if( _currentStamina < _maxStamina)
        {
            timer = _nextStaminaTime - DateTime.Now;
            DisplayNotif();
        }
    }

    IEnumerator RechargeStamina()
    {
        UpdateTimer();
        UpdateStamina();
        recharging = true;

        while(_currentStamina < _maxStamina)
        {
            //Checkeos de tiempo
            DateTime current = DateTime.Now;
            DateTime nextTime = _nextStaminaTime;

            //bool que checkea si se recargo stamina
            bool addingStamina = false;

            while(current > nextTime)
            {
                //No quiero superar mi stamina maxima
                if (_currentStamina >= _maxStamina) break;

                _currentStamina += 1;
                addingStamina = true;
                UpdateStamina();

                //Predecir la proxima vez que se va a recargar stamina.

                DateTime timeToAdd = nextTime;

                //Checkear si el usuario cerro la app
                if(_lastStaminaTime > nextTime) timeToAdd = _lastStaminaTime;

                nextTime = AddDuration(timeToAdd, _timerToRecharge);
            }

            //Si se recargo stamina...
            if(addingStamina)
            {
                _nextStaminaTime = nextTime;
                _lastStaminaTime = DateTime.Now;
            }

            //Updatear UI y gaurdados
            UpdateTimer();
            UpdateStamina();
            SaveData();

            yield return new WaitForEndOfFrame();
        }

       //Change
       NotificationManager.Instance.CancelNotification(id);
       recharging = false;
    }

    private DateTime AddDuration(DateTime timeToAdd, float timerToRecharge)
    {
        return timeToAdd.AddSeconds(timerToRecharge);
        //return timeToAdd.AddMinutes(timerToRecharge);
        //return timeToAdd.AddHours(timerToRecharge);
        //return timeToAdd.AddDays(timerToRecharge);
        //return timeToAdd.AddMilliseconds(timerToRecharge);
        //return timeToAdd.AddTicks((long)timerToRecharge);
        //return timeToAdd.AddMonths((int)timerToRecharge);
        //return timeToAdd.AddYears((int)timerToRecharge);
    }

    /*public bool HasEnoughStamina (int stamina)
    {
        return _currentStamina - stamina >= 0;
    }*/

    //Goes to
    public bool HasEnoughStamina(int stamina) => _currentStamina - stamina >= 0;

   public void UseStamina(int staminaToUse)
   {
       if(HasEnoughStamina(staminaToUse))
       {
           //Jugar nivel
           _currentStamina -= staminaToUse;
           UpdateStamina();

           //Change
           NotificationManager.Instance.CancelNotification(id);
           DisplayNotif();

           if (!recharging)
           {
               //Setear next stamina time y comenzar recarga
               _nextStaminaTime = AddDuration(DateTime.Now, _timerToRecharge);          
               StartCoroutine(RechargeStamina());
           }
       }
       else
       {
           Debug.Log("Feedback no tenes stamina suficiente");
       }
   }

    void DisplayNotif()
    {
           id = NotificationManager.Instance.DisplayNotification(_titleNotif, _textNotif, _smallIcon, _largeIcon,
               AddDuration(DateTime.Now, ((_maxStamina - (_currentStamina) + 1) * _timerToRecharge) + 1 + (float)timer.TotalSeconds));


        //AddDuration(Tiempo actual, ((stamina maxima - (actual) + 1 por tema de que la stamina ya podria haber comenzado a recargarse) y todo eso multiplicado por tiempo de recarga) + segundos para darme tiempo a cancelar la notificacion + Segundos extras por timer de stamina ya comenzado);
    }

    private void UpdateStamina()
    {
        _staminaText.text = $"{_currentStamina} / {_maxStamina}"; 
        //_staminaText.text = _currentStamina + " / " + _maxStamina; 
    }

    public void ForceRefreshUI()
    {
        int extraStamina = PlayerPrefs.GetInt("ExtraStamina", 0);

        _maxStamina = 3 + extraStamina;
        _currentStamina = PlayerPrefs.GetInt(PlayerPrefsKeys.currentStaminaKey, _currentStamina);

        UpdateStamina();
        UpdateTimer();
    }



    private void UpdateTimer()
    {
        if (_currentStamina >= _maxStamina)
        {
            _timertext.text = "Full stamina!";
            return;
        }

        //Estructura que nos da un intervalo de tiempo
        timer = _nextStaminaTime - DateTime.Now;

        //Formato "00" para representar el horario como si fuera un despertador.
        _timertext.text = timer.Minutes.ToString("00") + ":" + timer.Seconds.ToString("00");
        //_timertext.text = $"{timer.Minutes.ToString("00")} : {timer.Seconds.ToString("00")}";
    }

    void SaveData()
    {
        //Current Stamina
        //Next Stamina Time
        //Last Stamina Time

        PlayerPrefs.SetInt(PlayerPrefsKeys.currentStaminaKey, _currentStamina);
        PlayerPrefs.SetString(PlayerPrefsKeys.nextStaminaTimeKey, _nextStaminaTime.ToString());
        PlayerPrefs.SetString(PlayerPrefsKeys.lastStaminaTimeKey, _lastStaminaTime.ToString());
    }

    void LoadData()
    {
        _currentStamina = PlayerPrefs.GetInt(PlayerPrefsKeys.currentStaminaKey, _maxStamina);

        _extraStamina = PlayerPrefs.GetInt("ExtraStamina", 0);
        _maxStamina = 3 + _extraStamina;

        _nextStaminaTime = StringToDateTime(PlayerPrefs.GetString(PlayerPrefsKeys.nextStaminaTimeKey));
        _lastStaminaTime = StringToDateTime(PlayerPrefs.GetString(PlayerPrefsKeys.lastStaminaTimeKey));

        UpdateStamina();
    }


    DateTime StringToDateTime(string date)
    {
        if (string.IsNullOrEmpty(date))
            return DateTime.Now; //Devuelve horario actual argentino, UtcNow devuelve horario universal (Argentina Utc-3)
        else
            return DateTime.Parse(date);
    }
    public void ResetStaminaSystem()
    {
        NotificationManager.Instance.CancelNotification(id);
 
        PlayerPrefs.DeleteKey(PlayerPrefsKeys.currentStaminaKey);
        PlayerPrefs.DeleteKey(PlayerPrefsKeys.nextStaminaTimeKey);
        PlayerPrefs.DeleteKey(PlayerPrefsKeys.lastStaminaTimeKey);
        PlayerPrefs.DeleteKey("ExtraStamina");
 
        _currentStamina = 3;
        _maxStamina = 3;
 
        _nextStaminaTime = DateTime.Now;
        _lastStaminaTime = DateTime.Now;
 
        UpdateStamina();
        _timertext.text = "Full stamina!";
 
        Debug.Log("Stamina reseteada completamente.");
    }



    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnDisable()
    {
        SaveData();
    }
}
