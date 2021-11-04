using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Registration : MonoBehaviour
{
    [SerializeField]
    private Text TextScore;
    [SerializeField]
    private InputField InputName;
    [SerializeField]
    private InputField InputSurname;
    [SerializeField]
    private InputField InputEmail;

    private readonly string DATABASE_URL = "https://itmo-test.firebaseio.com/";
    private DatabaseReference reference;
    private List<User> users = new List<User>();

    void Start()
    {
        InitializeFirebase(DATABASE_URL);
        LoadDataFromFirebase();
        TextScore.text = "" + GameData.MaxScore;
    }

    public void InitializeFirebase(string databaseUrl)
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(databaseUrl);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log("Firebase initialized.");
    }

    public void SaveDataToFirebase(User user)
    {
        string id = user.GetId().ToString();
        string name = user.GetName();
        string surname = user.GetSurname();
        string patronymic = user.GetPatronymic();
        string email = user.GetEmail();
        int gameScore = user.GetGameScore();
        int testScore = user.GetTestScore();
        int testTime = user.GetTestTime();

        reference.Child("users").Child(id).Child("name").SetValueAsync(name);
        reference.Child("users").Child(id).Child("surname").SetValueAsync(surname);
        reference.Child("users").Child(id).Child("patronymic").SetValueAsync(patronymic);
        reference.Child("users").Child(id).Child("email").SetValueAsync(email);
        reference.Child("users").Child(id).Child("game_score").SetValueAsync(gameScore);
        reference.Child("users").Child(id).Child("test_score").SetValueAsync(testScore);
        reference.Child("users").Child(id).Child("test_time").SetValueAsync(testTime);
        // Debug.Log("Firebase save: " + id + " " + gameScore);
    }

    public void LoadDataFromFirebase()
    {
        FirebaseDatabase.DefaultInstance
        .GetReference("users")
        .GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.Log("Firebase error.");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("Firebase load: " + snapshot.GetRawJsonValue());
                foreach (var i in snapshot.Children)
                {
                    int id = System.Convert.ToInt32(i.Key);
                    string name = i.Child("name").Value.ToString();
                    string surname = i.Child("surname").Value.ToString();
                    string patronymic = i.Child("patronymic").Value.ToString();
                    string email = i.Child("email").Value.ToString();
                    int gameScore = System.Convert.ToInt32(i.Child("game_score").Value);
                    int testScore = System.Convert.ToInt32(i.Child("test_score").Value);
                    int testTime = System.Convert.ToInt32(i.Child("test_time").Value);

                    users.Add(new User(id, name, surname, patronymic, email, gameScore, testScore, testTime));
                }
            }
        });
    }

    public void AddUser()
    {
        if (InputName.text != "" && InputSurname.text != "" && InputEmail.text != "")
        {
            bool isExist = false;
            User oldUser = new User(0, "", "", "", "", 0, 0, 0);

            foreach(User user in users)
            {
                if (user.GetName().ToLower().Equals(InputName.text.ToLower()) && user.GetSurname().ToLower().Equals(InputSurname.text.ToLower()))
                {
                    isExist = true;
                    oldUser = user;
                    break;
                }
            }

            if (isExist)
            {
                oldUser.SetGameScore(Math.Max(oldUser.GetGameScore(), GameData.MaxScore));
                SaveDataToFirebase(oldUser);
            }
            else
            {
                User newUser = new User(users.Count, InputName.text, InputSurname.text, "", InputEmail.text, GameData.MaxScore, 0, 0);
                SaveDataToFirebase(newUser);
            }
            SceneManager.LoadScene("Rating");
        }
    }
}
