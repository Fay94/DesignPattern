using System;

namespace 状态模式的实现
{
    //环境角色---相当于Context类型
    public sealed class Order
    {
        private State current;

        public Order()
        {
            //工作状态初始化为尚无的工作状态，等待接单中
            current = new WaitForAcceptance();
            IsCancel = false;
        }

        public double Minute { get; set; }
        public bool IsCancel { get; set; }
        public bool TaskFinished { get; set; }

        public void SetState(State s)
        {
            current = s;
        }
        public void Action()
        {
            current.Process(this);
        }
    }

    //抽象状态角色---相当于State类型
    public interface State
    {
        //处理订单
        void Process(Order order);
    }

    //等待受理--相当于具体状态角色
    public sealed class WaitForAcceptance : State
    {
        public void Process(Order order)
        {
            System.Console.WriteLine("我们开始受理，准备备货！");
            if (order.Minute < 30 && order.IsCancel)
            {
                System.Console.WriteLine("接受半个小时之内，可以取消订单！");
                order.SetState(new CancelOrder());
                order.Action();
            }
            order.SetState(new AcceptAndDeliver());
            order.TaskFinished = false;
            order.Action();
        }
    }

    //受理发货---相当于具体状态角色
    public sealed class AcceptAndDeliver : State
    {
        public void Process(Order order)
        {
            System.Console.WriteLine("我们货物已经准备好，可以发货了，不可以撤销订单！");
            if (order.Minute < 30 && order.IsCancel)
            {
                System.Console.WriteLine("接受半个小时之内，可以取消订单！");
                order.SetState(new CancelOrder());
                order.Action();
            }
            if (order.TaskFinished == false)
            {
                order.SetState(new ConfirmationReceipt());
                order.Action();
            }
        }
    }

    //确认收货---相当于具体状态角色
    public sealed class ConfirmationReceipt : State
    {
        public void Process(Order order)
        {
            System.Console.WriteLine("检查货物，没问题可以就可以签收！");
            order.SetState(new Success());
            order.TaskFinished = false;
            order.Action();
        }
    }

    //交易成功---相当于具体状态角色
    public sealed class Success : State
    {
        public void Process(Order order)
        {
            System.Console.WriteLine("订单结算");
            order.TaskFinished = true;
        }
    }

    //取消订单---相当于具体状态角色
    public sealed class CancelOrder : State
    {
        public void Process(Order order)
        {
            System.Console.WriteLine("检查货物，有问题，取消订单！");
            order.TaskFinished = true;
        }
    }


    public class Client
    {
        public static void Main(String[] args)
        {
            //订单
            Order order = new Order();
            order.Minute = 9;
            order.Action();
            //可以取消订单
            order.IsCancel = true;
            order.Minute = 20;
            order.Action();
            order.Minute = 33;
            order.Action();
            order.Minute = 43;
            order.Action();

            Console.Read();
        }
    }
}