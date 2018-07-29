using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 最小二乘法线性回归
{
    class Program
    {
        static void Main(string[] args)
        {
            //设置一个包含5个点的两个数组
            Point[] array = new Point[5];
            array[0] = new Point(10, 100);
            array[1] = new Point(20, 200);
            array[2] = new Point(30, 300);
            array[3] = new Point(40, 400);
            array[4] = new Point(50, 500);
            LinearRegression(array);
            Console.Read();
        }


        /// <summary>
        /// Point结构
        /// 二维笛卡尔坐标系
        /// </summary>
        public struct Point
        {
            public double X;
            public double Y;
            public Point(double x = 0, double y = 0)
            {
                X = x;
                Y = y;
            }
        }

        /// <summary>
        /// 对一组点通过最小二乘法进行线性回归
        /// </summary>
        /// <param name="parray"></param>
        public static void LinearRegression(Point[] parray)
        {
            //点数不能小于2
            if (parray.Length < 2)
            {
                Debug.WriteLine("点的数量小于2，无法进行线性回归");
                Console.WriteLine("点的数量小于2，无法进行线性回归");
                return;
            }
            //求出横纵坐标的平均值
            double averagex = 0, averagey = 0;
            foreach (Point p in parray)
            {
                averagex += p.X;
                averagey += p.Y;
            }
            averagex /= parray.Length;
            averagey /= parray.Length;
            //经验回归系数的分子与分母
            double numerator = 0;
            double denominator = 0;
            foreach (Point p in parray)
            {
                numerator += (p.X - averagex) * (p.Y - averagey);
                denominator += (p.X - averagex) * (p.X - averagex);
            }
            //回归系数b（Regression Coefficient）
            double RCB = numerator / denominator;
            //回归系数a
            double RCA = averagey - RCB * averagex;
            Console.WriteLine("回归系数A： " + RCA.ToString("0.0000"));
            Console.WriteLine("回归系数B： " + RCB.ToString("0.0000"));
            Console.WriteLine(string.Format("方程为： y = {0} + {1} * x",
              RCA.ToString("0.0000"), RCB.ToString("0.0000")));
            //剩余平方和与回归平方和
            double residualSS = 0;  //（Residual Sum of Squares）
            double regressionSS = 0; //（Regression Sum of Squares）
            foreach (Point p in parray)
            {
                residualSS +=
                  (p.Y - RCA - RCB * p.X) *
                  (p.Y - RCA - RCB * p.X);
                regressionSS +=
                  (RCA + RCB * p.X - averagey) *
                  (RCA + RCB * p.X - averagey);
            }
            Console.WriteLine("剩余平方和： " + residualSS.ToString("0.0000"));
            Console.WriteLine("回归平方和： " + regressionSS.ToString("0.0000"));
        }
    }
}
