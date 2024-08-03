using System;
using System.Collections.Generic;

public class Item
{
    public int Weight { get; set; }
    public int Value { get; set; }
    public double Ratio => (double)Value / Weight; // Tính tỷ lệ giá trị/trọng lượng
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

        // Ví dụ về việc tính giá trị tối đa có thể đạt được
        int capacity = 50;
        (int maxValue, List<Item> selectedItems) = Knapsack01(items, capacity);

        Console.WriteLine($"Gia tri toi da co the dat duoc la: {maxValue}");
        Console.WriteLine("Cac do vat da duoc chon:");
        foreach (var item in selectedItems)
        {
            Console.WriteLine($"- Trong luong: {item.Weight}, Gia tri: {item.Value}, Don gia: {item.Ratio:F2}");
        }
    }

    public static (int, List<Item>) Knapsack01(List<Item> items, int capacity)
    {
        int n = items.Count;
        int[,] dp = new int[n + 1, capacity + 1];
        bool[,] keep = new bool[n + 1, capacity + 1]; // Bảng theo dõi đồ vật đã chọn

        // Tạo bảng DP
        for (int i = 1; i <= n; i++)
        {
            for (int w = 0; w <= capacity; w++)
            {
                if (items[i - 1].Weight > w)
                {
                    dp[i, w] = dp[i - 1, w];
                }
                else
                {
                    int valueIfTaken = dp[i - 1, w - items[i - 1].Weight] + items[i - 1].Value;
                    if (valueIfTaken > dp[i - 1, w])
                    {
                        dp[i, w] = valueIfTaken;
                        keep[i, w] = true;
                    }
                    else
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                }
            }
        }

        // Lấy danh sách các đồ vật đã được chọn
        List<Item> selectedItems = new List<Item>();
        int remainingCapacity = capacity;
        for (int i = n; i > 0; i--)
        {
            if (keep[i, remainingCapacity])
            {
                selectedItems.Add(items[i - 1]);
                remainingCapacity -= items[i - 1].Weight;
            }
        }

        // Giá trị tối đa
        return (dp[n, capacity], selectedItems);
    }
}

