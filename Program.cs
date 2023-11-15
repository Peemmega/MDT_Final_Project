

class Program {
    static void Main(string[] args){
        Gamemaster gamemaster = new Gamemaster();
        User user = GetUser(gamemaster);
        gamemaster.LoadLobby(user,gamemaster);
    }

    public static User GetUser(Gamemaster gamemaster){
        gamemaster.PrintNL(" Welcome to Media Adventure ");
        gamemaster.PrintNL(" Create Work by Nhakluamak Group ");
        gamemaster.PrintNL(" Choice: [1 Login] [2 Register] ");
        User returnVal;
        
        switch (Console.ReadLine()){
            case "1":
                returnVal = gamemaster.Login(gamemaster);
                break;
            case "2":
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