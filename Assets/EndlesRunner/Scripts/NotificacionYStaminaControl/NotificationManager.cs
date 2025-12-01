using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;


public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    AndroidNotificationChannel notifChannel;

    private void Awake()
    {
        if(Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        //Opcional a usar
        //AndroidNotificationCenter.CancelAllDisplayedNotifications();
        //AndroidNotificationCenter.CancelAllScheduledNotifications();
        if (PlayerPrefs.HasKey("Display_ComeBack")) CancelNotification(PlayerPrefs.GetInt("Display_ComeBack"));

        notifChannel = new AndroidNotificationChannel()
        {
            Id = "reminder_notif_ch",
            Name = "Reminder Notification",
            Description = "Reminder to login",
            Importance = Importance.High,
            EnableVibration = true,
            VibrationPattern = new long[] { 1000, 400, 1500, 400, 1000}
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);

        AndroidNotificationCenter.CancelAllNotifications();

        PlayerPrefs.SetInt("Display_ComeBack", DisplayNotification("VUELVE!!!!", "Te necesito, necesito que me juegues",
            IconSelecter.icon_reminder, IconSelecter.icon_reminderbig, DateTime.Now.AddSeconds(30)));
    }

    public int DisplayNotification(string title, string text, IconSelecter iconSmall, IconSelecter iconLarge, DateTime fireTime)
    {
        var notification = new AndroidNotification()
        {
            Title = title,
            Text = text,
            SmallIcon = iconSmall.ToString(),
            LargeIcon = iconLarge.ToString(),
            FireTime = fireTime,
            ShowTimestamp = true,
            Color = Color.cyan,
        };

        return AndroidNotificationCenter.SendNotification(notification, notifChannel.Id);
    }

    public void CancelNotification(int id)
    {
        AndroidNotificationCenter.CancelScheduledNotification(id);
    }
}

public enum IconSelecter
{
    icon_reminder,
    icon_reminderbig
}
