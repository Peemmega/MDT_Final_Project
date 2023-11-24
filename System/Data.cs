public class Data{
    private Dictionary<string,User> _users_Data = new Dictionary<string,User>();
    private Dictionary<string,Post> _post_Data = new Dictionary<string,Post>();
    private Dictionary<string,Stage> _stage_Data = new Dictionary<string,Stage>();
    // private Dictionary<string,Monster> _monster_Data = new Dictionary<string,Monster>();
    
    public int GetUserCount(){
       return _users_Data.Count;
    }
    public int GetPostCount(){
       return _post_Data.Count;
    }

    public Post GetPostData(string ID){
       if (_post_Data.ContainsKey(ID)){
            return _post_Data[ID] ;
       } else {
            return null;
       }
    }
    public Stage GetStageData(string ID){
       if (_stage_Data.ContainsKey(ID)){
            return _stage_Data[ID] ;
       } else {
            return null;
       }
    }
    public Data(){
        // Admin Account for test
        AddUserData("Peemmega",new User("Peemmega","12345678"));
        AddUserData("Shoko",new User("Shoko","12345678"));
        // Create Monster Data
        CreateStageData();
    }

    public Monster CreateMonsterData(string name){
        if (name == "Slime"){
            return new Monster("Slime", new Stats(10,2,1,1));
        } else if (name == "King Slime") {
            return new Monster("King Slime", new Stats(100,5,3,1));
        } else {
            return null;
        }
    }


    public void CreateStageData(){
        List<Monster> enemylist = new List<Monster>();
//-------------------------------------------------------------
        enemylist.Add(CreateMonsterData("Slime"));
        AddStageData(new Stage("Forest I",enemylist, new Reward(100,50),"Lv 1+"));
//-------------------------------------------------------------
        enemylist = new List<Monster>();
        enemylist.Add(CreateMonsterData("Slime"));
        enemylist.Add(CreateMonsterData("Slime"));
        enemylist.Add(CreateMonsterData("Slime"));
        AddStageData(new Stage("Forest II",enemylist, new Reward(300,100),"Lv 3+"));
//-------------------------------------------------------------
        enemylist = new List<Monster>();
        enemylist.Add(CreateMonsterData("Slime"));
        enemylist.Add(CreateMonsterData("Slime"));
        enemylist.Add(CreateMonsterData("King Slime"));
        AddStageData(new Stage("Forest III",enemylist, new Reward(500,150),"Lv 5+"));
    }

    // Add Data
    public void AddUserData(string username,User data){
        _users_Data.Add(username,data);
    }
    public void AddPostData(string postID,Post data){
        _post_Data.Add(postID,data);
    }
    // public void AddMonsterData(Monster data){
    //     _monster_Data.Add(data.GetName(),data);
    // }
    public void AddStageData(Stage data){
        _stage_Data.Add(data.GetName(),data);
    }

    //------------------------------------------------------
    public void ShowPost(){
        Console.WriteLine("--------------------- [ Post ] -----------------------");
        int showPostLimit = 6;

        foreach (KeyValuePair<string,Post> post in _post_Data){
            
            if (showPostLimit == 0){ break; }
            --showPostLimit;

            Console.WriteLine($"[Post ID:{post.Key}]");
            Console.WriteLine($"[Username] {post.Value.GetUser().GetUserNameSkin()}");
            Console.WriteLine($"- {post.Value.GetDescription()}");

            if (post.Value.GetFile() != "-"){
                Console.WriteLine($"[File] {post.Value.GetFile()}");
            }
            Console.WriteLine($"[Like : {post.Value.GetLikeCount()}]");
            Console.WriteLine("-----------------------------------------------------------");
        }
    }
    public void ShowPost(User user){
        Console.WriteLine("--------------------- [ Post ] -----------------------");
        int showPostLimit = 6;

        foreach (KeyValuePair<string,Post> post in _post_Data){
            if (post.Value.GetUser().GetUserName() != user.GetUserName()){continue;}
            if (showPostLimit == 0){ break; }
            --showPostLimit;

            Console.WriteLine($"[Post ID:{post.Key}]");
            Console.WriteLine($"[Username] {post.Value.GetUser().GetUserNameSkin()}");
            Console.WriteLine($"- {post.Value.GetDescription()}");

            if (post.Value.GetFile() != "-"){
                Console.WriteLine($"[File] {post.Value.GetFile()}");
            }
            Console.WriteLine($"[Like : {post.Value.GetLikeCount()}]");
            Console.WriteLine("-----------------------------------------------------------");
        }
    }
    public void ShowDungeonStage(){
        Console.WriteLine("-----------------------------------------------------------");
        foreach (KeyValuePair<string,Stage> stage in _stage_Data){
            PrintStageInfomation(stage.Value);
        }
    }

    public void PrintStageInfomation(Stage stage){
        Console.WriteLine($"[Stage ID : {stage.GetName()}] [{stage.GetRecomment()}]");
        Console.Write($"[Enemy list] : ");
        stage.ShowEnemyList();
        Console.WriteLine();
        Console.WriteLine($"[Gold] : {stage.GetReward().GetGold()} [EXP] : {stage.GetReward().GetEXP()}");
        Console.WriteLine("-----------------------------------------------------------");
    }
    public void ShowList(){
        Console.WriteLine("-----------------------------------------------------------");
        foreach (KeyValuePair<string,User> user in _users_Data){
            Console.WriteLine($"[ID:{user.Key}] {user.Value.GetUserName()}");
        }
    }

    public bool IsUsernameInData(string username){
        return  _users_Data.ContainsKey(username);
    }

    public User GetUserFormUserName(string username){
        User val = _users_Data[username];
        return val;
    }

}