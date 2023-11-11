public class Currency{
    private int _Gold = 0;
    private int _Gem = 0;

    public void Add(string currency,int value){
        if (currency == "Gold"){
            _Gold += value;
        } else if (currency == "Gem"){
            _Gem += value;
        }
    }

    public void Remove(string currency,int value){
        if (currency == "Gold"){
            _Gold -= value;
        } else if (currency == "Gem"){
            _Gem -= value;
        }
    }

    public void PrintCurrency(){
        Console.WriteLine($"--------------------");
        Console.WriteLine($"[Gold] : {_Gold}");
        Console.WriteLine($"[Gem] : {_Gem}");
    }
}