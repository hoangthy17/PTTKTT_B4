using System;
using System.Collections.Generic;
using System.Linq;

public class Item
{
    public int Weight { get; set; }
    public int Value { get; set; }
    public double Ratio => (double)Value / Weight; 
}

public class Program
{
    public static void Main()
    {
        // Tạo danh sách các đồ vật
        List<Item> items = new List<Item>
        {
            new Item { Weight = 10, Value = 60 },
            new Item { Weight = 20, Value = 100 },
            new Item { Weight = 30, Value = 120 }
        };

        // Sắp xếp các đồ vật theo tỷ lệ giá trị/trọng lượng giảm dần
        items.Sort((a, b) => b.Ratio.CompareTo(a.Ratio));

        // In ra bảng các đồ vật
        PrintItemTable(items);

        // Ví dụ về việc tính giá trị tối đa có thể đạt được
        int capacity = 50;
        double maxValue = KnapsackGreedy(items, capacity);
        Console.WriteLine($"Gia tri toi da co the dat duoc la: {maxValue}");
    }

    static void PrintItemTable(List<Item> items)
    {
        Console.WriteLine("Danh sach do vat (săp xep theo don gia giam ):");
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("|   Trong luong   |   Gia tri   |   Don gia   |");
        Console.WriteLine("-------------------------------------------------");

        foreach (var item in items)
        {
            Console.WriteLine($"|       {item.Weight,-14} |      {item.Value,-8} |    {item.Ratio,0:F2}     |");
        }

        Console.WriteLine("-------------------------------------------------");
    }

    public static double KnapsackGreedy(List<Item> items, int capacity)
    {
        double totalValue = 0;
        int currentWeight = 0;

        foreach (var item in items)
        {
            if (currentWeight + item.Weight <= capacity)
            {
                // Nếu đồ vật có thể hoàn toàn được thêm vào ba lô
                totalValue += item.Value;
                currentWeight += item.Weight;
            }
            else
            {
                // Nếu không thể thêm toàn bộ đồ vật, chỉ thêm phần còn lại
                int remainingWeight = capacity - currentWeight;
                double fraction = (double)remainingWeight / item.Weight;
                totalValue += item.Value * fraction;
                break; // Không cần thêm đồ vật nữa
            }
        }

        return totalValue;
    }
}

