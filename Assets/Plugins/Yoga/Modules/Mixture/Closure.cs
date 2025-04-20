using System;

namespace YOGA.Modules.Mixture
{
    /// <summary>
    /// 闭包，用于存储方法的结构体
    /// </summary>
    public struct Closure<TContext>
    {
        Delegate _del;
        TContext _context;

        public Closure(Delegate del, TContext context = default)
        {
            _del = del;
            _context = context;
        }

        public void Reset()
        {
            _del = null;
            _context = default;
        }

        public void Invoke()
        {
            if (_del is Action action)
            {
                action();
            }
            else if (_del is Action<TContext> actionWithContext)
            {
                actionWithContext(_context);
            }
            else
            {
                throw new InvalidOperationException("Unsupported delegate type for Invoke.");
            }
        }

        public TResult Invoke<TResult>()
        {
            if (_del is Func<TResult> func)
            {
                return func();
            }

            if (_del is Func<TContext, TResult> funcWithContext)
            {
                return funcWithContext(_context);
            }

            throw new InvalidOperationException("Unsupported delegate type for Invoke<TResult>.");
        }

        public static Closure<TContext> Create(Action action)
        {
            return new Closure<TContext>(action);
        }

        public static Closure<TContext> Create(Action<TContext> action, TContext context)
        {
            return new Closure<TContext>(action, context);
        }

        public static Closure<TContext> Create<TResult>(Func<TResult> func)
        {
            return new Closure<TContext>(func);
        }

        public static Closure<TContext> Create<TResult>(Func<TContext, TResult> func, TContext context)
        {
            return new Closure<TContext>(func, context);
        }
    }
}
