namespace CancelationTokenCode
{
    public class Program
    {
        static async Task Main()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Task task = Task.Run(() => DoWorkAsync(token), token);

            await Task.Delay(1000);

            cts.Cancel();

            await task;

            Console.WriteLine($"Task Status: {task.Status}");
            cts.Dispose();
        }

        static async Task DoWorkAsync(CancellationToken token)
        {
            try
            {
                for (int i = 1; i < 10; i++)
                {
                    token.ThrowIfCancellationRequested();

                    Console.WriteLine($"Квадрат числа {i} равен {i * i}");

                    await Task.Delay(200);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Операция прервана");
            }
        }
    }
}