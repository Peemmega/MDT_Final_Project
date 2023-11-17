public class Monster {
    private String _name;
    private Stats _stats;
    private int _hp;
    private int _blockValue = 0;

    public Monster(string monsterName,Stats monster_Stats){
        _name = monsterName;
        _stats = monster_Stats;
        _hp = monster_Stats.GetHP();
    }
    public void TakeDamage(int val){
        _hp -= val;
    }
    public void RecoveryToMaxHP(){
        _hp = _stats.GetHP();
        _blockValue = 0;
    }
    public string GetName(){
        return _name;
    }
    public int GetHP(){
        return _hp;
    }
    public int GetBlockValue(){
        return _blockValue;
    }
    public void UseBlock(){
        _blockValue += 3;
    }
    public void ReduceBlockValue(){
        if (_blockValue > 0){
            _blockValue -= 1;
        }   
    }
    public Stats GetStats(){
        return _stats;
    }
}

