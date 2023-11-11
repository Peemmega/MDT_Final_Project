public class Gamemaster {
    Print print = new Print();
    Data appData = new Data();

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
    public bool YesNo(){
        Print("Confirm ?: [Y/N] ");
        bool value;
        switch (Console.ReadLine()){
            case "Y" :
                value = true;
                break;
            case "N" :
                value = false;
                break;
            default: 
                PrintNL("Error please try again");
                value = YesNo();
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

    public void CreateUserData(User user){
        appData.AddUserData(user.GetUserName(),user);
    }

    //--------------------------------------------------------------------
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

    //--------------------------------------------------------------------


    public bool CheckPassWord(string user_name, string Password){
        if (appData.GetUserFormUserName(user_name).GetPassWord() == Password){
            return true;
        } else {
            return false;
        }
    }

    public User Login(Gamemaster master){
        static User Get_User(Gamemaster master){
            master.PrintNL("[ Login ]");

            master.Print("Account: ");
            string user_name = master.GetString(1,20);
            master.Print("Password: ");
            string user_password = master.GetString(8,24);
            User your_Account = new User(user_name,user_password);

            bool confirm = master.YesNo();

            switch (confirm){
                case false :
                    your_Account = Get_User(master);
                    break; 
            }

            return your_Account;
        }

       
        User your_account = Get_User(master);
        if (CheckPassWord(your_account.GetUserName(),your_account.GetPassWord())){
            Console.Clear();
            master.CreateLine();
            master.PrintNL("Login success.");
        } else {
            master.PrintNL("Incorrect Password");
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
            bool confirm = master.YesNo();
            switch (confirm){
                case false :
                    your_Account = Get_User(master);
                    break; 
            }

            return your_Account;
        }
        User your_account = Get_User(master);
        Console.Clear();
        master.CreateLine();
        master.PrintNL("Register success.");
        master.CreateUserData(your_account);
        return your_account;
    }

    public void ShowAllUser(){
        appData.ShowList();
    }
}