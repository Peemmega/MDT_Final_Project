using System;

public class Sceen{
    public void Lobby(User user, Gamemaster master){
        Console.Clear();
        Console.WriteLine($"[ Welcome to Media Adventure {user.GetUserName()} ]");
        Console.WriteLine($"[User] {user.GetUserName()} Lv:{user.GetStats().GetLevel()} exp: {user.GetStats().GetEXP()}/{user.GetStats().GetLevel() * 250}");
        user.GetCurrency().Print_Currency();
        Console.WriteLine("{profile} {quest} {dungeon} {shop} {community} {chat} {logout} {exit}");
        master.MenuSelectionEvent(user,master);
    }

    public void Profile(User user, Gamemaster master){
        Console.Clear();
        Console.WriteLine("[ Profile ]");
        Console.WriteLine($"[User] {user.GetUserName()} Lv:{user.GetStats().GetLevel()}");
        user.GetCurrency().Print_Currency();

        user.GetStats().ShowStats();
        master.GetDataService().ShowPost(user);

        Console.WriteLine("{post} {like} {back}");
        bool Select = false;
        while (!Select){
            switch (Console.ReadLine()){
                case "post":
                    Select = true;
                    master.CreatePost(user,master);
                    break;
                case "like":
                    Select = true;
                    Console.Write("Put postID: ");
                    string postID = Console.ReadLine();
                    master.LikePost(user,master,postID);
                    break;
                case "back":
                    Select = true;
                    Lobby(user,master);
                    break;
            }
        }
    }

    public void Community(User user, Gamemaster master){
        Console.Clear();
        Console.WriteLine("[ Community ]");
        master.GetDataService().ShowPost();
        Console.WriteLine("{post} {like} {back}");
        bool Select = false;
        while (!Select){
            switch (Console.ReadLine()){
                case "post":
                    Select = true;
                    master.CreatePost(user,master);
                    break;
                case "like":
                    Select = true;
                    Console.Write("Put postID: ");
                    string postID = Console.ReadLine();
                    master.LikePost(user,master,postID);
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

    public void InGame(User user, Gamemaster master,Stage stage){
        bool ForceEnd = false;
        

        while (!ForceEnd){
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"[ {stage.GetName()} ]");
            Console.Write("Enemy list : ");
            stage.ShowEnemyList();
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"[{user.GetUserName()}] HP: {user.GetHP()}");
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
                    Reset(ref user, ref stage);
                    Dungeon(user,master);
                    break;
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

                        if (action > 40) { // Attack 60%
                            Console.WriteLine($"[{monster.GetName()} Action] - Attack to {user.GetUserName()}");
                            if (user.GetBlockValue() > 0){ // On Block
                                int dealDMG = Math.Clamp(monster.GetStats().GetATK() - user.GetStats().GetDEF(),1,999);
                                Console.WriteLine($"[{user.GetUserName()}] blocked reduce dmg from {monster.GetStats().GetATK()} to {dealDMG}");
                                user.TakeDamage(dealDMG);
                            } else { // Without Block
                                user.TakeDamage(monster.GetStats().GetATK());
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
                SelectEndGameOption(user,master,stage,"ลองเล่นด่านนี้สิ!");
            } else if (user.GetHP() <= 0) { // You Lose
                ForceEnd = true;
                Console.WriteLine("[You lose]");
                SelectEndGameOption(user,master,stage,"ด่านนี้ยากมาก! ลองมาเล่นดูเร็ว");
            }

            static void SelectEndGameOption(User user,Gamemaster master,Stage stage, string text){
                Console.WriteLine("{replay} {share} {back}");
                bool selected = false;
                bool shared = false;
                while (!selected){
                    switch (Console.ReadLine()){
                        case "replay":
                            selected = true;
                            Console.Clear();
                            Reset(ref user, ref stage);
                            master.GetSceenService().InGame(user,master,stage);
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
                            Reset(ref user, ref stage);
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
        static void Reset(ref User user,ref Stage stage){
            foreach (Monster monster in stage.GetMonster()){
                monster.RecoveryToMaxHP();
                user.RecoveryToMaxHP();
            }

        }
    }

}