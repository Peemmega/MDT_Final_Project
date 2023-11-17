public class Gamemaster {
    private Print print = new Print();
    private Data appData = new Data();
    private Sceen appScreen = new Sceen();

    // Service
    public Data GetDataService(){
        return appData;
    }
    public Sceen GetSceenService(){
        return appScreen;
    }

    // String thing
    public string GetString(int min_length,int max_length){
        string input_value = Console.ReadLine();
        if (input_value.Length >= min_length && input_value.Length <= max_length){
            return input_value; 
        } 
        else {
            Console.WriteLine($"press input between {min_length} - {max_length} ");
            return GetString(min_length,max_length);
        }
    }
    public bool YesNo(string text){
        Print($"{text} ?: [y/n] ");
        bool value;
        switch (Console.ReadLine()){
            case "y" :
                value = true;
                break;
            case "n" :
                value = false;
                break;
            default: 
                PrintNL("Error please try again");
                value = YesNo(text);
            break;    
            }      
        return value;     
    }

    public void Print(string text){
        print.systemPrint(text);
    }

    public void PrintNL(string text){
        print.systemPrintNewLine(text);
    }

    public void CreateLine(){
        print.CreateLine();
    }

    // Login Register

    static string CheckPassword(Gamemaster master){
        master.Print("Password: ");
        string user_password = master.GetString(8,24);
        master.Print("Confirm Password: ");
        string confirmPassword = master.GetString(8,24);

        if (user_password == confirmPassword){
            return user_password;
        } else {
            master.PrintNL("Error password not macth please try again");
            return CheckPassword(master);
        }        
    }

    public bool CheckPassWord(string user_name, string Password){
        Console.Clear();
       
        if (appData.IsUsernameInData(user_name) && appData.GetUserFormUserName(user_name).GetPassWord() == Password){
            return true;
        } else {
            PrintNL("Username or Password incorrect");
            return false;
        }
    }

    public void LoadLobby(User user,Gamemaster master){
        appScreen.Lobby(user,master);
    }

    public User Login(Gamemaster master){
        static User Get_User(Gamemaster master){
            master.PrintNL("[ Login ]");

            master.Print("Account: ");
            string user_name = master.GetString(1,20);
            master.Print("Password: ");
            string user_password = master.GetString(8,24);
            User your_Account = new User(user_name,user_password);

            bool confirm = master.YesNo("Confirm");

            switch (confirm){
                case false :
                    your_Account = Get_User(master);
                break; 
            }

            return your_Account;
        }

        User your_account = Get_User(master);
        
        if (!CheckPassWord(your_account.GetUserName(),your_account.GetPassWord())){
            bool confirm = master.YesNo("Want to create new account");
            if (confirm) {
                your_account = Register(master);
            } else {
                your_account = Login(master);
            }
        }
        return your_account;
    }

    public User Register(Gamemaster master){
        static User Get_User(Gamemaster master){
            master.PrintNL("[ Register ]");

            master.Print("Account: ");
            string user_name = master.GetString(1,20);
            string user_password = CheckPassword(master);
            User your_Account = new User(user_name,user_password);
            bool confirm = master.YesNo("Confirm");
            switch (confirm){
                case false :
                    your_Account = Get_User(master);
                break; 
            }

            return your_Account;
        }
        User your_account = Get_User(master);
        // Console.Clear();
        // master.CreateLine();
        // master.PrintNL("Register success.");
        if (appData.IsUsernameInData(your_account.GetUserName())){
            PrintNL("This username already use");
            bool confirm = master.YesNo("Want to loggin");
            if (confirm) {
                your_account = Login(master);
            } else {
                your_account = Register(master);
            }
        } else {
            master.CreateUserData(your_account);
        }
        return your_account;
    }

    public void ShowAllUser(){
        appData.ShowList();
    }

    // Create
    public void CreateUserData(User user){
        appData.AddUserData(user.GetUserName(),user);
    }

    public void CreatePost(User user, Gamemaster master){
        Console.Write("Description: ");
        string text = Console.ReadLine();
        switch (text){
            case "Back":
            break;
            default: // Post
                bool sendfile = YesNo("want to add file");
                Post newPost;
                if (sendfile){
                    Console.Write("add file : ");
                    newPost = new Post(user,text,Console.ReadLine());
                } else {
                    newPost = new Post(user,text);
                }

                appData.AddPostData(user.GetUserName() + "_" + appData.GetPostCount() ,newPost);
                break;
        }
        appScreen.Community(user,master);
    }

        public void ShareStagePost(User user, Gamemaster master, string text, Stage stage){
            Post newPost = new Post(user,text,stage.GetName() + ".url");
            appData.AddPostData(user.GetUserName() + "_" + appData.GetPostCount() ,newPost);
        }


    public void LikePost(User user, Gamemaster master, string postID){
        Post post = appData.GetPostData(postID);
        if (post != null) {
            post.LikePost(user);
        } else {
            PrintNL("No post data");
        }
        appScreen.Community(user,master);
    }

    public void PlayStage(User user, Gamemaster master, string stageID){
        Console.Clear();
        Stage stageData = appData.GetStageData(stageID);
        if (stageData != null) {
            appData.PrintStageInfomation(stageData);
            
            Console.WriteLine("{play} {invite} {back}");
            bool Select = false;
            while (!Select){
                switch (Console.ReadLine()){
                    case "play":
                        Select = true;
                        Console.Clear();
                        Console.WriteLine($"[Loding] : {stageData.GetName()}");
                        appScreen.InGame(user,master,stageData);
                        break;
                    case "invite":
                        Console.WriteLine("Coming soon...?");
                        break;
                    case "back":
                        Select = true;
                        appScreen.Dungeon(user,master);
                        break;
                }
            }

        } else {
            appScreen.Dungeon(user,master);
        }   
    }




    // Selection
    public void MenuSelectionEvent(User user,Gamemaster master){
        bool EndSelection = false;
        while (!EndSelection){
            Console.Write("[Select menu]: ");
            switch(Console.ReadLine()){
                case "profile":
                    EndSelection = true;
                    appScreen.Profile(user,master);
                    break;
                case "quest":
                    PrintNL("Comming soon..");
                    break;
                case "dungeon":
                    EndSelection = true;
                    appScreen.Dungeon(user,master);
                    break;
                case "shop":
                    PrintNL("Comming soon..");
                    break;
                case "community":
                    EndSelection = true;
                    appScreen.Community(user,master);
                    break;
                case "chat":
                    PrintNL("Comming soon..");
                    break;
                case "logout":
                    EndSelection = true;
                    User newUser = Program.GetUser(master);
                    LoadLobby(newUser,master);
                    break;
                case "exit":
                    EndSelection = true;
                break;
            }
        }
    }
}