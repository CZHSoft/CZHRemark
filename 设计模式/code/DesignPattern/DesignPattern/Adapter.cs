using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public interface IMic
    {
        void PlayMic();
    }

    public abstract class BasePhone
    {
        public void PlayBluetooth()
        {
            Console.WriteLine("蓝牙播放");
        }
    }

    public class XRAdapter : BasePhone, IMic
    {
        public void PlayMic()
        {
            Console.WriteLine("Mic播放");
        }
    }

    public class Adapter : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始适配器模式...");

            XRAdapter xr = new XRAdapter();

            IMic imic = xr;
            imic.PlayMic();
            xr.PlayBluetooth();

            Console.ReadLine();
            Console.Clear();
        }
    }
}
