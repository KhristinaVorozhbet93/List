namespace List
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyList<int> list = new MyList<int>() {1,2,3,4,5};

            Console.WriteLine(list[4]);
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            list.Add(6);
            Console.WriteLine(list.Remove(2));
            list.RemoveAt(1);
            Console.WriteLine(list.Contains(7));

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            list.Clear();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
