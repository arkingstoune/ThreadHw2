using System.Threading;
using System.Timers;
//Timer timer = new(ForTimer,null, 5000,1000);// Timer first one
System.Timers.Timer timer1 = new(2000); // second timer has been created and it set up for every 2 second
timer1.Elapsed += ForTimer1;// this method begins the run time
timer1.Enabled = true;// we have enabled our timer
timer1.Start();// timer has been started
CancellationTokenSource cts = new();// Cancellationtokensource is the class we use in case of if we want 
// to trak something communicate with this by that i mean end the procces 
Thread thread1 = new(() => SomeFunction(cts.Token))// we have created thread
{
    Priority = ThreadPriority.Highest// set priority
};
Thread thread2 = new(() => SomeFunction(cts.Token));
Thread thread3 = new(() => SomeFunction(cts.Token));
Thread thread4 = new(() => SomeFunction(cts.Token));

thread1.Start();//started thread 
thread2.Start();
//thread3.Abort();
while(true)
{
    if(Console.ReadKey().Key == ConsoleKey.B) // we read key from keyboard in this case it is b
        cts.Cancel();// change status to canceled one it mean we can cancel the procces
}
void SomeFunction(CancellationToken token)
{
    try// working with execption
    {  
        Thread.Sleep(2000);// wait 2 seconds
        token.ThrowIfCancellationRequested();// if we have cancel request we will throw exception
        Console.WriteLine($"thread key is {Thread.CurrentThread.ManagedThreadId} staus of thread: ---{Thread.CurrentThread.ThreadState}");
    }
    catch(ThreadAbortException)// work with abort "end threat"
    {
        Console.WriteLine("_________");
    }
    catch(System.Exception except)
    {
        Console.WriteLine(except);// throw exception
    }
}
void ForTimer(object? obj)// object varriable is requred
{
    Console.WriteLine("timer started....");
}
void ForTimer1(object? obj, ElapsedEventArgs arg)//in this case to
{
    Console.WriteLine("timer1 started...");
}