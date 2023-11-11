public class Sceen{

    public void CreateLine(){
        Console.WriteLine("---------------");
    }

    public void Lobby(User user){
        CreateLine();
        Console.WriteLine($"[ Welcome to Media Adventure {user.GetUserName()} ]");
        Console.WriteLine("[1] Yo");
        Console.WriteLine("[2] Yi");
        Console.WriteLine("[3] Ya");
        CreateLine();
    }



    public User Login(){
        static string GetString(int min_length,int max_length){
            string input_value = Console.ReadLine();
            if (input_value.Length >= min_length && input_value.Length <= max_length){
                return input_value; 
            } 
            else {
                Console.WriteLine($"press input between {min_length} - {max_length} ");
                return GetString(min_length,max_length);
            }
        }

        static User Get_User(){
            Console.WriteLine("Login");
            Console.Write("Username: ");
            string user_name = GetString(1,20);
            Console.Write("Password: ");
            string user_password = GetString(8,24);
            User your_Account = new User(user_name,user_password);

            Console.WriteLine("Confirm your account?????: [Y/N]");

            switch (Console.ReadLine()){
                case "Y" :
                    break;
                case "N" :
                    your_Account = Get_User();;
                    break;
                default: 
                    Console.WriteLine("Please try again");
                    your_Account = Get_User();;
                    break;    
            }

            return your_Account;
        }

        
        User your_account = Get_User();
        CreateLine();
        Console.Clear();
        Console.WriteLine("Login success!");
        return your_account;
    }

}