using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    /* Индентификатор пользователя. */
    private int Id;
    /* Имя пользователя. */
    private string Name;
    /* Фамилия пользователя. */
    private string Surname;
    /* Отчество пользователя. */
    private string Patronymic;
    /* Email пользователя. */
    private string Email;
    /* Очки пользователя за игру. */
    private int GameScore;
    /* Очки пользователя за тест. */
    private int TestScore;
    /* Время, потраченное пользователем на тест. */
    private int TestTime;

    public User(int Id, string Name, string Surname, string Patronymic,
        string Email, int GameScore, int TestScore, int TestTime)
    {
        this.Id = Id;
        this.Name = Name;
        this.Surname = Surname;
        this.Patronymic = Patronymic;
        this.Email = Email;
        this.GameScore = GameScore;
        this.TestScore = TestScore;
        this.TestTime = TestTime;
    }

    /* ID */
    public int GetId()
    {
        return Id;
    }

    public void SetId(int Id)
    {
        this.Id = Id;
    }

    /* NAME */
    public string GetName()
    {
        return Name;
    }

    public void SetName(string Name)
    {
        this.Name = Name;
    }

    /* SURNAME */
    public string GetSurname()
    {
        return Surname;
    }

    public void SetSurname(string Surname)
    {
        this.Surname = Surname;
    }

    /* PATRONYMIC */
    public string GetPatronymic()
    {
        return Patronymic;
    }

    public void SetPatronymic(string Patronymic)
    {
        this.Patronymic = Patronymic;
    }

    /* EMAIL */
    public string GetEmail()
    {
        return Email;
    }

    public void SetEmail(string Email)
    {
        this.Email = Email;
    }

    /* GAME SCORE */
    public int GetGameScore()
    {
        return GameScore;
    }

    public void SetGameScore(int GameScore)
    {
        this.GameScore = GameScore;
    }

    /* TEST SCORE */
    public int GetTestScore()
    {
        return TestScore;
    }

    public void SetTestScore(int TestScore)
    {
        this.TestScore = TestScore;
    }

    /* TEST TIME */
    public int GetTestTime()
    {
        return TestTime;
    }

    public void SetTestTime(int TestTime)
    {
        this.TestTime = TestTime;
    }
}
