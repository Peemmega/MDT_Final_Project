

class Program {
    static void Main(string[] args){
        Gamemaster gamemaster = new Gamemaster();
        User user = GetUser(gamemaster);
        gamemaster.LoadLobby(user,gamemaster);
    }

    public static User GetUser(Gamemaster gamemaster){
        Console.Clear();
        gamemaster.PrintNL(" Welcome to Media Adventure ");
        gamemaster.PrintNL(" Create Work by Nhakluamak Group ");
        gamemaster.PrintNL(" Choice: [login] [register] ");
        User returnVal;
        
        switch (Console.ReadLine()){
            case "login":
                returnVal = gamemaster.Login(gamemaster);
                break;
            case "register":
                returnVal = gamemaster.Register(gamemaster);
                break;   
            default:
                Console.Clear();
                gamemaster.Print("Error pls try again");
                returnVal = GetUser(gamemaster);
                break;     
        }
        return returnVal;
    }
}