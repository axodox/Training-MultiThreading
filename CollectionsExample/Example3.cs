using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CollectionsExample
{
  /// <summary>
  /// Using ThreadLocal to pass arguments without affecting interfaces
  /// </summary>
  static class Example3
  {
    public static void Run()
    {
      for (int i = 0; i < 2; i++)
      {
        new Thread(Worker).Start(i);
      }
      Console.ReadLine();
    }

    private static void Worker(object o)
    {
      int id = (int)o;
      ThreadState.SetState(id, DoSomething);
    }

    private static void DoSomething()
    {
      PrintState();
      ThreadState.SetState(-1, DoSomethingElse);
      PrintState();
    }

    private static void DoSomethingElse()
    {
      PrintState();
    }

    private static void PrintState()
    {
      Console.WriteLine($"On thread {Thread.CurrentThread.ManagedThreadId} the state is {ThreadState.GetState<int>()}");
    }

    private static class ThreadState
    {
      private static readonly ThreadLocal<Dictionary<Type, Stack<object>>> _states =
        new ThreadLocal<Dictionary<Type, Stack<object>>>(() => new Dictionary<Type, Stack<object>>());

      public static void SetState<T>(T state, Action action)
      {
        var type = typeof(T);
        if (!_states.Value.TryGetValue(type, out var stack))
        {
          stack = _states.Value[type] = new Stack<object>();
        }
        stack.Push(state);

        try
        {
          action();
        }
        finally
        {
          stack.Pop();
          if (stack.Count == 0)
          {
            _states.Value.Remove(type);
          }
        }
      }

      public static T GetState<T>()
      {
        var type = typeof(T);
        if (_states.Value.TryGetValue(type, out var stack))
        {
          return (T)stack.Peek();
        }
        else
        {
          return default(T);
        }
      }
    }
  }
}
