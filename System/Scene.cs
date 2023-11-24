using System;
using System.Security.Cryptography.X509Certificates;

public class Sceen{
    public void Lobby(User user, Gamemaster master){
        Console.Clear();
        Console.WriteLine($"[ Welcome to Media Adventure {user.GetUserName()} ]");
        Console.WriteLine($"[User] {user.GetUserNameSkin()} HP:{user.GetHP()}/{user.GetStats().GetHP()}");
        user.GetStats().ShowStats();
        user.GetCurrency().Print_Currency();
        Console.WriteLine("{profile} {friend} {dungeon} {shop} {community} {inventory} {logout} {exit}");
        master.MenuSelectionEvent(user,master);
    }

    public void Profile(User user, Gamemaster master){
        Console.Clear();
        Console.WriteLine("[ Profile ]");
        Console.WriteLine($"[User] {user.GetUserNameSkin()} Lv:{user.GetStats().GetLevel()}");
        Console.WriteLine($"[Profile] {user.GetProfile().GetImage()} [Banner] {user.GetProfile().GetBanner()}");
        Console.WriteLine($"[Bio] {user.GetProfile().GetBio()}");
        Console.WriteLine($"[Follows {user.GetFollow().Count}] [Followers {user.GetFollower().Count}]");
        master.GetDataService().ShowPost(user);

        Console.WriteLine("{post} {check post} {change name} {set bio} {back}");
        bool Select = false;
        while (!Select){
            switch (Console.ReadLine()){
                case "post":
                    Select = true;
                    master.CreatePost(user,master);
                    Profile(user, master);
                    break;
                 case "check post":  
                    Select = true;
                    Console.Write("Put postID: ");
                    string postID = Console.ReadLine();
                    CheckPost(user,master,postID);
                    Profile(user,master);
                    break;
                case "change name":
                    Select = true;
                    user.ChangeUserName(master);
                    Profile(user,master);
                    break;
                case "set bio":
                    Select = true;
                    user.ChangeBio(master);
                    Profile(user,master);
                    break;  
                case "back":
                    Select = true;
                    Lobby(user,master);
                    break;
            }
        }
    }


    public void Profile(User user, Gamemaster master, User searh_user){
        Console.Clear();
        Console.WriteLine($"[ {searh_user.GetUserName()} Profile ]");
        Console.WriteLine($"[User] {searh_user.GetUserNameSkin()} Lv:{searh_user.GetStats().GetLevel()}");
        Console.WriteLine($"[Profile] {searh_user.GetProfile().GetImage()} [Banner] {searh_user.GetProfile().GetBanner()}");
        Console.WriteLine($"[Bio] {searh_user.GetProfile().GetBio()}");
        Console.WriteLine($"[Follows {searh_user.GetFollow().Count}] [Followers {searh_user.GetFollower().Count}]");
        master.GetDataService().ShowPost(searh_user);

        Console.WriteLine("{follow} {check post} {back}");
        bool Select = false;
        while (!Select){
            switch (Console.ReadLine()){
                case "follow":
                    master.Follow(user,searh_user);
                    break;
                 case "check post":  
                    Select = true;
                    Console.Write("Put postID: ");
                    string postID = Console.ReadLine();
                    CheckPost(user,master,postID);
                    Profile(user, master, searh_user);
                    break;
                case "back":
                    Select = true;
                    Friend(user,master);
                    break;
            }
        }
    }

    public void Friend(User user, Gamemaster master){
        Console.Clear();
        Console.WriteLine($"[ Follows {user.GetFollow().Count}]");
        Console.WriteLine($"[ Followers {user.GetFollower().Count}]");
        Console.WriteLine("{search} {follow} {follower} {back}");
        bool Select = false;
        while (!Select){
            switch (Console.ReadLine()){  
                case "search":
                    Console.Write("Input Name: ");
                    string find_User = Console.ReadLine();
                    if (master.GetDataService().IsUsernameInData(find_User) && find_User != user.GetUserName()){
                        Select = true;
                        User found_user = master.GetDataService().GetUserFormUserName(find_User);
                        Profile(user,master,found_user);
                    }
                    break;    
                case "follow":
                    ShowFollow(user);
                    break;    
                case "follower":
                    ShowFollower(user);
                    break;      
                case "back":
                    Select = true;
                    Lobby(user,master);
                    break;
            }
        }
    }

    public void ShowFollow(User user){
        Console.WriteLine("[Follow]------------------------------------------");
        foreach (User follow in user.GetFollow()){
            Console.WriteLine($"[ID:{follow.GetUserName()}]");
        }        
    }

    public void ShowFollower(User user){
        Console.WriteLine("[Follower]------------------------------------------");
        foreach (User follower in user.GetFollower()){
            Console.WriteLine($"[ID:{follower.GetUserName()}]");
        }        
    }

    public void Inventory(User user, Gamemaster master){
        Console.WriteLine("[ Inventory ]");
        Console.WriteLine("--------------------- [ Items ] -----------------------");
        user.ShowItemList();
        Console.WriteLine("--------------------- [ Cosmatic ] -----------------------");
        user.ShowNameSkinList();
        Console.WriteLine($"[{user.GetUserNameSkin()}] HP: {user.GetHP()}");
        bool Select = false;
        while (!Select){
            Console.WriteLine("{use} {back}");
            switch (Console.ReadLine()){
                case "use":
                    Console.Write("[Use] Input text in side [] to use: ");
                    string findItem = Console.ReadLine();
                    int list = 0;

                    if (int.TryParse(findItem, out list)){
                        Select = true;
                        Console.Clear();
                        master.Use(user,master,list);
                        Inventory(user,master);
                    } else {
                        Select = true;
                        Console.Clear();
                        master.Use(user,master,findItem);
                        Inventory(user,master);
                    }

                    break;
                case "back":
                    Select = true;
                    Lobby(user,master);
                    break;
            }
        }
    }

    public void CheckPost(User user, Gamemaster master, string postID){
        Console.Clear();
        if (master.GetDataService().GetPostData(postID) != null){
            Console.WriteLine("--------------------- [ Post ] -----------------------");
            int showPostLimit = 5;
            Post post = master.GetDataService().GetPostData(postID);            
            
            Console.WriteLine($"[Post ID:{post}]");
            Console.WriteLine($"[Username] {post.GetUser().GetUserNameSkin()}");
            Console.WriteLine($"- {post.GetDescription()}");

            if (post.GetFile() != "-"){
                Console.WriteLine($"[File] {post.GetFile()}");
            }
            Console.WriteLine($"[Like : {post.GetLikeCount()}]");
            Console.WriteLine("------- [ Comment ] -------");
            int max = post.GetComment().Count-1;
            for (int i = max - showPostLimit ; i <= max; i++){
                if (i >= 0 && post.GetComment()[i] != null){
                    Console.WriteLine($"{post.GetComment()[i]}");   
                }   
            }

            Console.WriteLine("{like} {comment} {back}");
            bool Select = false;
            while (!Select){
                switch (Console.ReadLine()){
                    case "comment":  
                        Select = true;
                        Console.Write("[Text your comment]");
                        post.Comment(user,Console.ReadLine());
                        CheckPost(user, master, postID);
                        break;
                    case "like":
                        Select = true;
                        post.LikePost(user);
                        CheckPost(user, master, postID);
                        break;
                    case "back":
                        Select = true;
                        break;
                }
            }
        } else {
            Console.WriteLine("[No data]");
        }
    }

    public void Community(User user, Gamemaster master){
        Console.Clear();
        Console.WriteLine("[ Community ]");
        master.GetDataService().ShowPost();
        Console.WriteLine("{post} {check post} {back}");
        bool Select = false;
        while (!Select){
            switch (Console.ReadLine()){
                case "post":
                    Select = true;
                    master.CreatePost(user,master);
                    Community(user,master);
                    break;
                case "check post":  
                    Select = true;
                    Console.Write("Put postID: ");
                    string postID = Console.ReadLine();
                    CheckPost(user,master,postID);
                    Community(user, master);
                    break;

                case "back":
                    Select = true;
                    Lobby(user,master);
                    break;
            }
        }
    }

    public void Shop(User user, Gamemaster master){
        Console.Clear();
        Console.WriteLine("[ Shop ]");
        master.GetShopService().ShowCosmaticList(user);
        master.GetShopService().ShowItemList();
        user.GetCurrency().Print_Currency();
        bool Select = false;
        while (!Select){
            Console.WriteLine("{buy} {back}");
            switch (Console.ReadLine()){
                case "buy":
                    Console.Write("[What do you want to buy?]: ");
                    master.GetShopService().Buy(user,master,Console.ReadLine());
                    break;
                case "back":
                    Select = true;
                    Lobby(user,master);
                    break;
            }
        }
    }
   

    public void Dungeon(User user, Gamemaster master){
        Console.Clear();
        Console.WriteLine("[ Dungeon ]");
        master.GetDataService().ShowDungeonStage();
        Console.WriteLine("{play} {back}");
        bool Select = false;
        while (!Select){
            switch (Console.ReadLine()){
                case "play":
                    Select = true;
                    Console.Write("Put Stage ID: ");
                    string StageID = Console.ReadLine();
                    master.PlayStage(user,master,StageID);
                    break;
        
                case "back":
                    Select = true;
                    Lobby(user,master);
                    break;
            }
        }
    }

    public void InGame(User user, Gamemaster master,Stage stage,List<User> party){
        bool ForceEnd = false;
        

        while (!ForceEnd){
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"[ {stage.GetName()} ]");
            Console.Write("Enemy list : ");
            stage.ShowEnemyList();
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------");
            
            foreach (User member in party){
                Console.WriteLine($"[{member.GetUserNameSkin()}] HP: {member.GetHP()}");
            }

            Console.WriteLine("{attack} {block} {item} {exit}");

            user.ReduceBlockValue();
            
            bool actioned = false;
            while (!actioned){
                switch (Console.ReadLine()){
                case "attack":
                    actioned = true;
                    Console.Clear();
                    Attack(ref stage, ref user);
                    break;

                case "block":
                    actioned = true;
                    Console.Clear();
                    Block(ref user);
                    break;
                
                case "exit":
                    actioned = true;
                    ForceEnd = true;
                    Reset(ref party, ref stage);
                    Dungeon(user,master);
                    break;
                }
            }
            
            static bool IsStageCleared(Stage stage){
                bool val = true;
                foreach (Monster monster in stage.GetMonster()){
                    if (monster.GetHP() > 0 ){
                        val = false;
                    }
                }
                return val;
            }

            // Party
            bool partyActioned = false;
            if (!ForceEnd){
                foreach (User member in party){
                    if (member != user){
                        if (member.GetHP() > 0){
                            Random random_Action = new Random();
                            int action = random_Action.Next(0,100);
                            member.ReduceBlockValue();
                            
                            
                            static Monster GetTarget(Stage stage){
                                Random random_Action = new Random();
                                Monster Target = stage.GetMonster()[random_Action.Next(0,stage.GetMonster().Count)];
                                if (Target.GetHP() <= 0 && stage.GetMonster().Count != 1 && (!IsStageCleared(stage))){
                                    Target =  GetTarget(stage);
                                } 
                                return Target;
                            }

                            Monster Target = GetTarget(stage);

                            if (action > 40) { // Attack 60%
                                Console.WriteLine($"[{member.GetUserNameSkin()} Action] - Attack to {Target.GetName()}");
                                if (Target.GetBlockValue() > 0){ // On Block
                                    int dealDMG = Math.Clamp(member.GetStats().GetATK() - Target.GetStats().GetDEF(),1,999);
                                    Console.WriteLine($"[{Target.GetName()}] blocked reduce dmg from {member.GetStats().GetATK()} to {dealDMG}");
                                    Target.TakeDamage(dealDMG);
                                } else { // Without Block
                                    Target.TakeDamage(member.GetStats().GetATK());
                                }
                            } else { // Block 40%
                                Console.WriteLine($"[{member.GetUserNameSkin()} Action] - Block [reduce dmg for 2 turn]");
                                member.UseBlock();
                            }

                        }
                        partyActioned = true;
                    }

                }
            }
            

            // Monster Action
            bool monsterActioned = false;
            if (!ForceEnd){
                foreach (Monster monster in stage.GetMonster()){
                    if (monster.GetHP() > 0){
                        Random random_Action = new Random();
                        int action = random_Action.Next(0,100);
                        monster.ReduceBlockValue();

                
                        static User GetTarget(List<User> party,User user){
                            Random random_Action = new Random();
                            User Target = party[random_Action.Next(0,party.Count)];                            
                            if (Target.GetHP() <= 0 && (party.Count != 1)){
                                Target =  user;
                            } 
                            return Target;
                        }

                        User Target = GetTarget(party,user);



                        if (action > 40) { // Attack 60%
                            Console.WriteLine($"[{monster.GetName()} Action] - Attack to {Target.GetUserNameSkin()}");
                            if (Target.GetBlockValue() > 0){ // On Block
                                int dealDMG = Math.Clamp(monster.GetStats().GetATK() - Target.GetStats().GetDEF(),1,999);
                                Console.WriteLine($"[{Target.GetUserNameSkin()}] blocked reduce dmg from {monster.GetStats().GetATK()} to {dealDMG}");
                                Target.TakeDamage(dealDMG);
                            } else { // Without Block
                                Target.TakeDamage(monster.GetStats().GetATK());
                            }
                        } else { // Block 40%
                            Console.WriteLine($"[{monster.GetName()} Action] - Block [reduce dmg for 2 turn]");
                            monster.UseBlock();
                        }
                        
                        monsterActioned = true;
                    }
                }
            }

            if (!monsterActioned && !ForceEnd){ // You Win
                ForceEnd = true;
                Console.WriteLine("[You won]");
                user.GetStats().AddEXP(stage.GetReward().GetEXP());
                user.GetCurrency().Add("Gold",stage.GetReward().GetGold());
                SelectEndGameOption(user,master,stage,party,"ลองเล่นด่านนี้สิ!");
            } else if (user.GetHP() <= 0) { // You Lose
                ForceEnd = true;
                Console.WriteLine("[You lose]");
                SelectEndGameOption(user,master,stage,party,"ด่านนี้ยากมาก! ลองมาเล่นดูเร็ว");
            }

            static void SelectEndGameOption(User user,Gamemaster master,Stage stage, List<User> party, string text){
                Console.WriteLine("{replay} {share} {back}");
                bool selected = false;
                bool shared = false;
                while (!selected){
                    switch (Console.ReadLine()){
                        case "replay":
                            selected = true;
                            Console.Clear();
                            Reset(ref party, ref stage);
                            master.GetSceenService().InGame(user,master,stage,party);
                        break;
                        case "share":
                            if (!shared){
                                shared = true;
                                master.ShareStagePost(user,master,text,stage);
                                Console.WriteLine("[System] - Shared post");
                            }
                        break;
                        case "back":
                            selected = true;
                            Reset(ref party, ref stage);
                            master.GetSceenService().Dungeon(user,master);
                        break;
                    }
                }
            }
        }

        static void Attack(ref Stage stage,ref User user){
            Monster target;
            if (stage.GetMonsterCount() == 1){
                target = stage._monsterOnStage[0];
            } else {
                static Monster getTarget(Stage stage){
                Console.Write("Select monster to attack: [number] ");
                    int num = int.Parse(Console.ReadLine()) - 1;
                    if (stage._monsterOnStage[num] != null){
                        return stage._monsterOnStage[num];
                    } else {
                        Console.WriteLine("[Error pls try again]");
                        Console.WriteLine("");
                        return getTarget(stage);
                    }
                }
                target = getTarget(stage);
            }
           
            Console.WriteLine($"[{user.GetUserName()} Action] - Attack to {target.GetName()}");
            if (target.GetBlockValue() > 0){ // On Block
                int dealDMG = Math.Clamp(user.GetStats().GetATK() - target.GetStats().GetDEF(),1,999);

                Console.WriteLine($"[{target.GetName()}] blocked reduce dmg from {user.GetStats().GetATK()} to {dealDMG}");
                target.TakeDamage(dealDMG);
            } else { // Without Block
                target.TakeDamage(user.GetStats().GetATK());
            }
        }
        
        static void Block(ref User user){
            Console.WriteLine($"[{user.GetUserName()} Action] - Block [reduce dmg for 2 turn]");
            user.UseBlock();
        }
        static void Reset(ref List<User> party,ref Stage stage){
            foreach (Monster monster in stage.GetMonster()){
                monster.RecoveryToMaxHP();
                foreach (User user in party){
                    if (user.GetHP() <= 0){
                        user.RecoveryToMaxHP();
                    }
                }
                
            }

        }
    }

}