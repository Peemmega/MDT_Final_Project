
public class Gamemaster {
    private Print print = new Print();
    private Data appData = new Data();
    private Sceen appScreen = new Sceen();
    private Shop _shop_Data = new Shop();

    // Service
    public Data GetDataService(){
        return appData;
    }
    public Sceen GetSceenService(){
        return appScreen;
    }
    public Shop GetShopService(){
        return _shop_Data;
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
        your_account = master.GetDataService().GetUserFormUserName(your_account.GetUserName());
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

    // Follow
    public void Follow(User user, User follow){
        if (user != follow){
            if (user.GetFollow().Contains(follow)){
                Console.WriteLine($"[{user.GetUserName()} unfollowed {follow.GetUserName()}]");
                user.GetFollow().Remove(follow);
                follow.GetFollower().Remove(user);
            } else {
                Console.WriteLine($"[{user.GetUserName()} followed {follow.GetUserName()}]");
                user.GetFollow().Add(follow);
                follow.GetFollower().Add(user);
            }
        }
    }

    // Use Item
    public void Use(User user, Gamemaster master, int rank){
        Item item = user.Item_Inventory()[rank];
        if (item != null){
            item.Use(user);
        }
    }

    public void Use(User user, Gamemaster master, string boughtname){
        if (user.NameSkin_Inventory().ContainsKey(boughtname)) {
            NameSkin skin = user.NameSkin_Inventory()[boughtname];
            if (skin != null){
                skin.Use(user);
            } else {
                Console.WriteLine($"[Fail] Dont have this skin");
            }
        }
    }

    // Create
    public void CreateUserData(User user){
        appData.AddUserData(user.GetUserName(),user);
    }
    public void AddItem(User user, Item item){
        user.Item_Inventory().Add(item);
    }

    public void AddNameSkin(User user, NameSkin skin){
        user.NameSkin_Inventory().Add(skin.GetName(),skin);
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
    }

    public void PlayStage(User user, Gamemaster master, string stageID){
        Console.Clear();
        Stage stageData = appData.GetStageData(stageID);
        List<User> party = new List<User>();
        party.Add(user);

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
                        appScreen.InGame(user,master,stageData,party);
                        break;
                    case "invite":
                        Console.WriteLine("[Follow List]");
                        appScreen.ShowFollow(user);
                        Console.Write("[Input username to invite] ");
                        string invited = Console.ReadLine();
                        if (GetDataService().GetUserFormUserName(invited) != null){
                            bool inParty = party.Contains(GetDataService().GetUserFormUserName(invited));
                            if (user.GetFollow().Contains(GetDataService().GetUserFormUserName(invited))){
                                if (inParty){
                                    Console.WriteLine($"{GetDataService().GetUserFormUserName(invited).GetUserNameSkin()} already join your party!");
                                } else {
                                    party.Add(GetDataService().GetUserFormUserName(invited));
                                    Console.WriteLine($"{GetDataService().GetUserFormUserName(invited).GetUserNameSkin()} join your party!");
                                }
                            }
                        } 
                        
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
                case "friend":
                    EndSelection = true;
                    appScreen.Friend(user,master);
                    break;    
                case "dungeon":
                    EndSelection = true;
                    appScreen.Dungeon(user,master);
                    break;
                case "shop":
                    EndSelection = true;
                    appScreen.Shop(user,master);
                    break;
                case "community":
                    EndSelection = true;
                    appScreen.Community(user,master);
                    break;
                case "inventory":
                    EndSelection = true;
                    Console.Clear();
                    appScreen.Inventory(user,master);
                    break;
                case "chat":
                    PrintNL("Comming soon..");
                    break;
                case "logout":
                    EndSelection = true;
                    User newUser = Program.GetUser(master);
                    LoadLobby(newUser,master);
                    break;
                case "I'm Batman":
                    user.GetStats().AddEXP(50000);
                    user.GetCurrency().Add("Gold",5000);
                break;   
                case "exit":
                    EndSelection = true;
                break;
            }
        }
    }
}