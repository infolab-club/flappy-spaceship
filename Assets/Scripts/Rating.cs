using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rating : MonoBehaviour
{
    [SerializeField]
    private Text textListPlayers;
    private readonly string DATABASE_URL = "https://itmo-test.firebaseio.com/";
    private DatabaseReference reference;
    private List<User> users = new List<User>();

    void Start()
    {
        InitializeFirebase(DATABASE_URL);
        /*
        for (int i = 7; i < 100; i++)
        {
            User newUser = new User(i, "Имя", "Фамилия", "", "", 10, 0, 0);
            SaveDataToFirebase(newUser);
        }
        */
        LoadDataFromFirebase();
    }

    private void Update()
    {
        textListPlayers.text = "";
        textListPlayers.rectTransform.sizeDelta = new Vector2(textListPlayers.rectTransform.rect.width, 0);
        for (int i = 0; i < users.Count; i++)
        {
            textListPlayers.rectTransform.sizeDelta = new Vector2(textListPlayers.rectTransform.rect.width, i * 70);
            textListPlayers.text += (i + 1) + ". " + users[i].GetName() + " " + users[i].GetSurname() + " ( " + users[i].GetGameScore() + " )" + "\n";
        }
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
                SortPlayers();
            }
        });
    }

    private void SortPlayers()
    {
        users.Sort(delegate (User x, User y)
        {
            return -x.GetGameScore().CompareTo(y.GetGameScore());
        });
    }
}
