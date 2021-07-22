using System;

namespace DesignPattern.创建型
{
    /// <summary>
    /// 汽车抽象类
    /// </summary>
    public abstract class Car
    {
        // 开始行驶
        public abstract void Go();
    }

    /// <summary>
    /// 红旗汽车
    /// </summary>
    public class HongQiCar : Car
    {
        public override void Go()
        {
            Console.WriteLine("红旗汽车开始行驶了！");
        }
    }

    /// <summary>
    /// 奔驰车
    /// </summary>
    public class BenChiCar : Car
    {
        /// <summary>
        /// 重写抽象类中的方法
        /// </summary>
        public override void Go()
        {
            Console.WriteLine("奔驰车开始行驶了！");
        }
    }

    /// <summary>
    /// 抽象工厂类
    /// </summary>
    public abstract class Factory
    {
        public abstract Car CreateCar();
    }

    /// <summary>
    /// 红旗汽车工厂类
    /// </summary>
    public class HongQiCarFactory : Factory
    {
        /// <summary>
        /// 负责生产红旗汽车
        /// </summary>
        /// <returns></returns>
        public override Car CreateCar()
        {
            return new HongQiCar();
        }
    }

    /// <summary>
    /// 奔驰车的工厂类
    /// </summary>
    public class BenChiCarFactory : Factory
    {
        /// <summary>
        /// 负责生产奔驰车
        /// </summary>
        /// <returns></returns>
        public override Car CreateCar()
        {
            return new BenChiCar();
        }
    }

    /// <summary>
    /// 客户端调用
    /// </summary>
    class FactoryClient
    {
        static void Main(string[] args)
        {
            // 如果客户又生产一辆奔驰车
            // 再另外初始化一个奔驰车的工厂
            Factory benChiCarFactory = new BenChiCarFactory();

            // 利用奔驰车的工厂生产奔驰车
            Car benChi = benChiCarFactory.CreateCar();
            benChi.Go();

            Console.Read();
        }
    }
}
