namespace List
{
    internal class Program
    {
        static void Main(string[] args)
        {           
            MyList<int> list = new MyList<int>() { };

            var task = Task.Run(() =>
            {
                for (int i = 0; i < 10000000; i++)
                {
                    list.Add(i);
                }
            });


            for (int i = 0; i < 10000000; i++)
            {
                list.Add(i);
            }
       
            task.Wait();

            Console.WriteLine(list.Count);          
        }
    }
}
