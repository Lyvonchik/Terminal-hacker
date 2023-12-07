using UnityEngine;

public class Hacker : MonoBehaviour
{
    public int level = 0;
    private string[] LevelName = { "Соседский Wi-Fi", "Пентагон", "Друга"};
    public string password;
    private string[] PasswordsForLvl1 = { "Молоко", "Осёл", "Клоун", "Капча", "Чай", "Париж" };
    private string[] PasswordsForLvl2 = { "Размарин", "Кефир", "Чучело", "Шишка", "Сладкий", "Подъезд" };
    private string[] PasswordsForLvl3 = { "Черенок", "Заводской", "Кондиционер", "Армстронг", "Майнкрафт", "Реабилитация" };

    enum Screen { MainMenu, Password, Win }
    Screen CurrentScreen;

    void Start()
    {
        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        CurrentScreen = Screen.MainMenu;
        
        Terminal.ClearScreen();
        Terminal.WriteLine("Приветствую тебя юный хацкер.\nЧто сегодня собираешься взломать?\n\n" +
            "1-Взломать Соседский Wi-Fi\n" +
            "2-Взлом Пентагона\n" +
            "3-Взлом Друга\n\n" +
            "Сделай выбор, нажми на кнопку!");
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            level = 0;
            ShowMainMenu();
        }
    }

    void OnUserInput(string input)
    {
        if (input.ToLower() == "назад" || input.ToLower() == "меню")
        {
            level = 0;
            ShowMainMenu();
        }
        else if (CurrentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (CurrentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }
    
    void RunMainMenu(string input)
    {
        if (input == "1" || input == "2" || input == "3")
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else
        {
            ShowMainMenu();
        }
    }

    void AskForPassword()
    {
        CurrentScreen = Screen.Password;
        Terminal.ClearScreen();
        Terminal.WriteLine("Решил взломать " + LevelName[level-1] + "!?\nВведи \"назад\" или кнопка \"esc\" если передумал");
        SetRandomPassword();
        Terminal.WriteLine("Попытайтесь расшифровать пароль:\n" + password.Anagram());
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = PasswordsForLvl1[Random.Range(0, PasswordsForLvl1.Length)];
                break;
            case 2:
                password = PasswordsForLvl2[Random.Range(0, PasswordsForLvl2.Length)];
                break;
            case 3:
                password = PasswordsForLvl3[Random.Range(0, PasswordsForLvl3.Length)];
                break;
            default:
                Debug.LogError("Уровень не был выбран!");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input.ToLower() == password.ToLower())
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
            Terminal.WriteLine("Пароль не верный");
            Terminal.WriteLine("Попробуй ещё раз:");
        }
    }

    void DisplayWinScreen()
    {
        CurrentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        switch (password.ToLower())
        {
            case "молоко":
                Terminal.WriteLine("А вот и молочко!");
                Terminal.WriteLine(@"
   __
  |==| 
  /  \
 /~~~~\
/      \
|------|
|МОЛОКО|
|------|
|______|");
                break;

            case "чучело":
                Terminal.WriteLine("Страшное!?");
                Terminal.WriteLine(@"
     __
 ___/__\___
   {*__*}
   __||__
  /|    |\
 //|____|\\  
// |____| \\
     ||
     ||");
                break;

            case "майнкрафт":
                Terminal.WriteLine("МОЯ ЖИЗНЬ");
                Terminal.WriteLine(@"
       _________
      /        /|        ___/\___
     /        / |      _/        \_
    /________/  |     /  __/||\__  \
    |        |  |    /__/   ||   \__\
    |        |  /           ||
    |        | /            ||
    |________|/             ||");
                break;

            default:
                Terminal.WriteLine("Поздравляю!\n\"Назад\" или кнопка \"esc\" для выбора уровня.");
                break;
        }
    }
}
